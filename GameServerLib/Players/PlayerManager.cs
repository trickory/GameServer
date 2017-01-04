using System.Collections.Generic;
using ENet;
using LeagueSandbox.GameServer.Enet;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Packets;

namespace LeagueSandbox.GameServer.Players
{
    public class PlayerManager
    {
        private NetworkIdManager _networkIdManager;

        private List<Pair<uint, ClientInfo>> _players = new List<Pair<uint, ClientInfo>>();
        private int _currentId = 1;
        private Dictionary<Team, uint> _userIdsPerTeam = new Dictionary<Team, uint>
        {
            { Team.Order, 0 },
            { Team.Chaos, 0 }
        };

        public PlayerManager(NetworkIdManager networkIdManager)
        {
            _networkIdManager = networkIdManager;
        }

        private Team GetTeamIdFromConfig(PlayerConfig p)
        {
            if (p.Team.ToLower() == "blue")
            {
                return Team.Order;
            }

            return Team.Chaos;
        }

        public void AddPlayer(KeyValuePair<string, PlayerConfig> p)
        {
            var summonerSkills = new[]
            {
                p.Value.Summoner1,
                p.Value.Summoner2
            };
            var teamId = GetTeamIdFromConfig(p.Value);
            var player = new ClientInfo(
                p.Value.Rank,
                teamId,
                p.Value.Ribbon,
                p.Value.Icon,
                p.Value.Skin,
                p.Value.Name,
                summonerSkills,
                _currentId // same as StartClient.bat
            );
            _currentId++;
            var c = new Champion(p.Value.Champion, (uint)player.UserId, _userIdsPerTeam[teamId]++, p.Value.Runes, player);
            c.SetTeam(teamId);

            var pos = c.GetSpawnPosition();
            c.setPosition(pos.X, pos.Y);
            c.LevelUp();

            player.Champion = c;
            var pair = new Pair<uint, ClientInfo> {Item2 = player};
            _players.Add(pair);
        }

        // GetPlayerFromPeer
        public ClientInfo GetPeerInfo(Peer peer)
        {
            foreach (var player in _players)
            {
                if (player.Item1 == peer.Address.port)
                {
                    return player.Item2;
                }
            }

            return null;
        }

        public ClientInfo GetClientInfoByChampion(Champion champ)
        {
            return GetPlayers().Find(c => c.Item2.Champion == champ).Item2;
        }

        public List<Pair<uint, ClientInfo>> GetPlayers()
        {
            return _players;
        }
    }
}

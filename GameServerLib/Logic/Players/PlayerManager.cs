using ENet;
using LeagueSandbox.GameServer.Core.Logic.PacketHandlers;
using LeagueSandbox.GameServer.Core.Logic.RAF;
using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.Logic.Packets;
using System.Collections.Generic;

namespace LeagueSandbox.GameServer.Logic.Players
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
            var c = new Obj_AI_Hero(p.Value.Champion, (uint)player.UserId, _userIdsPerTeam[teamId]++, p.Value.Runes, player);
            c.Team = teamId;

            var pos = Extensions.GetSpawnPosition(c);
            c.Position = pos;
            //c.LevelUp();

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

        public ClientInfo GetClientInfoByChampion(Obj_AI_Hero champ)
        {
            return GetPlayers().Find(c => c.Item2.Champion == champ).Item2;
        }

        public List<Pair<uint, ClientInfo>> GetPlayers()
        {
            return _players;
        }
    }
}

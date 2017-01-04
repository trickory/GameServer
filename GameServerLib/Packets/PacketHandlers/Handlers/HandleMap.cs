using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleMap : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private PlayerManager _playerManager = Program.ResolveDependency<PlayerManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            // Builds team info e.g. first UserId set on Blue has PlayerId 0
            // increment by 1 for each added player
            var screenInfo = new LoadScreenInfo(_playerManager.GetPlayers());
            var pInfo = _game.PacketHandlerManager.sendPacket(peer, screenInfo, Channel.CHL_LOADING_SCREEN);

            // Distributes each players info by UserId
            var bOk = false;
            foreach (var player in _playerManager.GetPlayers())
            {
                // Giving the UserId in loading screen a name
                var loadName = new LoadScreenPlayerName(player);
                // Giving the UserId in loading screen a champion
                var loadChampion = new LoadScreenPlayerChampion(player);
                var pName = _game.PacketHandlerManager.sendPacket(peer, loadName, Channel.CHL_LOADING_SCREEN);
                var pHero = _game.PacketHandlerManager.sendPacket(peer, loadChampion, Channel.CHL_LOADING_SCREEN);

                bOk = pName && pHero;

                if (!bOk)
                {
                    break;
                }
            }

            return pInfo && bOk;
        }
    }
}

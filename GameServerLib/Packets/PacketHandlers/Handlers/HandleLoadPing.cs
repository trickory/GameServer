using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleLoadPing : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private PlayerManager _playerManager = Program.ResolveDependency<PlayerManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var loadInfo = new PingLoadInfo(data);
            var peerInfo = _playerManager.GetPeerInfo(peer);
            if (peerInfo == null)
            {
                return false;
            }
            var response = new PingLoadInfo(loadInfo, peerInfo.UserId);

            //_logger.LogCoreInfo($"Loaded: {loadInfo.loaded}, ping: {loadInfo.ping}, {loadInfo.f3}");
            return _game.PacketHandlerManager.broadcastPacket(response, Channel.CHL_LOW_PRIORITY, PacketFlags.None);
        }
    }
}

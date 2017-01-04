using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleGameNumber : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private PlayerManager _playerManager = Program.ResolveDependency<PlayerManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var world = new WorldSendGameNumber(1, _playerManager.GetPeerInfo(peer).Name);
            return _game.PacketHandlerManager.sendPacket(peer, world, Channel.CHL_S2C);
        }
    }
}

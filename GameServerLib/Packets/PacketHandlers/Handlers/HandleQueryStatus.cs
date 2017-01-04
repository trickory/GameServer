using ENet;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleQueryStatus : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var response = new QueryStatus();
            return _game.PacketHandlerManager.sendPacket(peer, response, Channel.CHL_S2C);
        }
    }
}

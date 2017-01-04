using ENet;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandlePauseReq : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            _game.Pause();
            return true;
        }
    }
}
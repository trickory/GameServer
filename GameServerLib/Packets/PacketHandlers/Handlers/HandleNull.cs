using ENet;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleNull : IPacketHandler
    {
        public bool HandlePacket(Peer peer, byte[] data)
        {
            return true;
        }
    }
}

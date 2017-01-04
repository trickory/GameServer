using ENet;
using LeagueSandbox.GameServer.Enet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleSurrender : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private PlayerManager _pm = Program.ResolveDependency<PlayerManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var c = _pm.GetPeerInfo(peer).Champion;
            Surrender surrender = new Surrender(c, 0x03, 1, 0, 5, c.Team, 10.0f);
            _game.PacketHandlerManager.broadcastPacketTeam(Team.Order, surrender, Channel.CHL_S2C);
            return true;
        }
    }
}

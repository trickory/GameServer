using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleClick : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private Logger _logger = Program.ResolveDependency<Logger>();
        private PlayerManager _playerManager = Program.ResolveDependency<PlayerManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var click = new Click(data);
            _logger.LogCoreInfo(
                $"Object {_playerManager.GetPeerInfo(peer).Champion.NetId} clicked on {click.targetNetId}"
            );

            return true;
        }
    }
}

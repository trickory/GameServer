using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleAttentionPing : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private PlayerManager _playerManager = Program.ResolveDependency<PlayerManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var ping = new AttentionPing(data);
            var response = new AttentionPingAns(_playerManager.GetPeerInfo(peer), ping);
            return _game.PacketHandlerManager.broadcastPacketTeam(
                _playerManager.GetPeerInfo(peer).Team,
                response,
                Channel.CHL_S2C
            );
        }
    }
    public enum Ping : byte
    {
        Default = 0,
        Danger = 2,
        Missing = 3,
        OnMyWay = 4,
        Assist = 6
    }
}

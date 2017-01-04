using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleCursorPositionOnWorld : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private PlayerManager _playerManager = Program.ResolveDependency<PlayerManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var cursorPosition = new CursorPositionOnWorld(data);
            var response = new DebugMessage($"X: {cursorPosition.X} Y: {cursorPosition.Y}");

            return _game.PacketHandlerManager.broadcastPacket(response, Channel.CHL_S2C);
        }
    }
}

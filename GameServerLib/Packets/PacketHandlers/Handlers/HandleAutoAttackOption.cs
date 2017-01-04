using ENet;
using LeagueSandbox.GameServer.Chatbox;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleAutoAttackOption : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private ChatCommandManager _chatCommandManager = Program.ResolveDependency<ChatCommandManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var autoAttackOption = new AutoAttackOption(data);
            var state = "Deactivated";
            if (autoAttackOption.activated == 1)
            {
                state = "Activated";
            }

            _chatCommandManager.SendDebugMsgFormatted(
                ChatCommandManager.DebugMsgType.Normal,
                "Auto attack: " + state
            );
            return true;
        }
    }
}

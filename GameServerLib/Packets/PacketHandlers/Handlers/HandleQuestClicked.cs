using ENet;
using LeagueSandbox.GameServer.Chatbox;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleQuestClicked : IPacketHandler
    {
        private ChatCommandManager _chatCommandManager = Program.ResolveDependency<ChatCommandManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var questClicked = new QuestClicked(data);

            _chatCommandManager.SendDebugMsgFormatted(
                ChatCommandManager.DebugMsgType.Normal,
                $"Clicked quest with netid: {questClicked.netid}"
            );
            return true;
        }
    }
}

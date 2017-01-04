using ENet;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class JunglespawnCommand : ChatCommand
    {
        public JunglespawnCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var _logger = Program.ResolveDependency<Logger>();
            _logger.LogCoreInfo(".junglespawn command not implemented");
            _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.Info, "Command not implemented");
        }
    }
}

using ENet;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class NewCommand : ChatCommand
    {
        public NewCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.Info, "The new command added by " + ChatCommandManager.CommandStarterCharacter + "help has been executed");
            _owner.RemoveCommand(Command);
        }
    }
}

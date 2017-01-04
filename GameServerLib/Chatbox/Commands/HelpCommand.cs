using ENet;
using LeagueSandbox.GameServer.Core.Logic;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    public class HelpCommand : ChatCommand
    {
        public HelpCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var game = Program.ResolveDependency<Game>();
            if (!game.Config.ChatCheatsEnabled)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.Info, "Chat commands are disabled in this game.");
                return;
            }

            var chatboxManager = Program.ResolveDependency<ChatCommandManager>();

            var commands = "";
            var count = 0;
            foreach (var command in _owner.GetCommandsStrings())
            {
                count++;
                commands += $"<font color =\"#E175FF\"><b>{ChatCommandManager.CommandStarterCharacter}{command}</b><font color =\"#FFB145\">, ";
            }

            _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.Info, "List of available commands: ");
            _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.Info, commands);
            _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.Info, "There are " + count + " commands");

            _owner.AddCommand(new NewCommand("newcommand", "", _owner));
        }
    }
}

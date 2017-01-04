using ENet;
using LeagueSandbox.GameServer.Core.Logic;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class SpawnStateCommand : ChatCommand
    {
        public SpawnStateCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var game = Program.ResolveDependency<Game>();

            var split = arguments.ToLower().Split(' ');

            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
            else if (split[1] == "1")
            {
                game.Map.SetSpawnState(true);
            }
            else if (split[1] == "0")
            {
                game.Map.SetSpawnState(false);
            }
            else
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
        }
    }
}

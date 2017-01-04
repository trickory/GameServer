using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class ModelCommand : ChatCommand
    {
        public ModelCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.Split(' ');
            if (split.Length >= 2)
            {
                playerManager.GetPeerInfo(peer).Champion.Model = split[1];
            }
            else
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
        }
    }
}

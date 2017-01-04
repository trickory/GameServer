using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class SizeCommand : ChatCommand
    {
        public SizeCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');
            float size;
            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
            else if (float.TryParse(split[1], out size))
            {
                playerManager.GetPeerInfo(peer).Champion.GetStats().Size.BaseValue = size;
            }
        }
    }
}

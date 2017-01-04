using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class XpCommand : ChatCommand
    {
        public XpCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');
            float xp;
            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
                return;
            }
            if (float.TryParse(split[1], out xp))
            {
                playerManager.GetPeerInfo(peer).Champion.GetStats().Experience = xp;
            }
        }
    }
}

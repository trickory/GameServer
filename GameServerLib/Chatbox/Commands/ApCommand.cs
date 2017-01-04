using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class ApCommand : ChatCommand
    {
        public ApCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var playerManager = Program.ResolveDependency<PlayerManager>();
            var split = arguments.ToLower().Split(' ');
            float ap;
            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
            else if (float.TryParse(split[1], out ap))
            {
                playerManager.GetPeerInfo(peer).Champion.GetStats().AbilityPower.FlatBonus = ap;
            }
        }
    }
}

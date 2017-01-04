using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class ChangeTeamCommand : ChatCommand
    {
        public ChangeTeamCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');
            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
                return;
            }

            int t;
            if (!int.TryParse(split[1], out t))
            {
                return;
            }

            var team = CustomConvert.ToTeamId(t);
            playerManager.GetPeerInfo(peer).Champion.SetTeam(team);
        }
    }
}

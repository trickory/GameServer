using ENet;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class TpCommand : ChatCommand
    {
        public TpCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var game = Program.ResolveDependency<Game>();
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');
            float x, y;
            if (split.Length < 3)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
                return;
            }
            if (float.TryParse(split[1], out x))
            {
                if (float.TryParse(split[2], out y))
                {
                    game.PacketNotifier.NotifyTeleport(playerManager.GetPeerInfo(peer).Champion, x, y);
                }
            }
        }
    }
}

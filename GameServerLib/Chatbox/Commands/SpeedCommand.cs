using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class SpeedCommand : ChatCommand
    {
        public SpeedCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var _playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');
            float speed;
            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
            if (float.TryParse(split[1], out speed))
            {
                _playerManager.GetPeerInfo(peer).Champion.GetStats().MoveSpeed.FlatBonus = speed;
            }
            else
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.Error, "Incorrect parameter");
            }
        }
    }
}

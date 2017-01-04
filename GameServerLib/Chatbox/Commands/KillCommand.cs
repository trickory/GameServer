using ENet;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class KillCommand : ChatCommand
    {
        public KillCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var _game = Program.ResolveDependency<Game>();
            var _playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');

            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
            else if (split[1] == "minions")
            {
                var objects = _game.Map.GetObjects();
                foreach (var o in objects)
                {
                    (o.Value as Minion)?.die(_playerManager.GetPeerInfo(peer).Champion); // :(
                }
            }
            else
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
        }
    }
}

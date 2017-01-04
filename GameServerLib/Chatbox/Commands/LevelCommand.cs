using ENet;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class LevelCommand : ChatCommand
    {
        public LevelCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var _game = Program.ResolveDependency<Game>();
            var _playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');
            byte lvl;
            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
            else if (byte.TryParse(split[1], out lvl))
            {
                if (lvl < 1 || lvl > 18)
                {
                    return;
                }

                var experienceToLevelUp = _game.Map.ExpToLevelUp[lvl-1];
                _playerManager.GetPeerInfo(peer).Champion.GetStats().Experience = experienceToLevelUp;
            }
        }
    }
}

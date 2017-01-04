using ENet;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class ChCommand : ChatCommand
    {

        public ChCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var game = Program.ResolveDependency<Game>();
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.Split(' ');
            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
                return;
            }
            var currentChampion = playerManager.GetPeerInfo(peer).Champion;
            var c = new Champion(
                split[1],
                (uint)playerManager.GetPeerInfo(peer).UserId,
                0, // Doesnt matter at this point
                currentChampion.RuneList,
                playerManager.GetClientInfoByChampion(currentChampion),
                currentChampion.NetId
            );
            c.setPosition(
                playerManager.GetPeerInfo(peer).Champion.X,
                playerManager.GetPeerInfo(peer).Champion.Y
            );
            c.Model = split[1]; // trigger the "modelUpdate" proc
            c.SetTeam(playerManager.GetPeerInfo(peer).Champion.Team);
            game.Map.RemoveObject(playerManager.GetPeerInfo(peer).Champion);
            game.Map.AddObject(c);
            game.PacketNotifier.NotifyLevelUp(c);
            playerManager.GetPeerInfo(peer).Champion = c;
        }
    }
}

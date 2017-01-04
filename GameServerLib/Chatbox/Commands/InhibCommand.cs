using ENet;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class InhibCommand : ChatCommand
    {
        public InhibCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var game = Program.ResolveDependency<Game>();
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var sender = playerManager.GetPeerInfo(peer);
            var min = new Monster(
                sender.Champion.X,
                sender.Champion.Y,
                sender.Champion.X,
                sender.Champion.Y,
                "Worm",
                "Worm"
                );
            game.Map.AddObject(min);
        }
    }
}

using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class CoordsCommand : ChatCommand
    {
        public CoordsCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var logger = Program.ResolveDependency<Logger>();
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var champion = playerManager.GetPeerInfo(peer).Champion;

            logger.LogCoreInfo($"At {champion.X}; {champion.Y}; {champion.GetZ()}");

            _owner.SendDebugMsgFormatted(
                ChatCommandManager.DebugMsgType.Normal,
                $"At Coords - X: {champion.X} Y: {champion.Y} Z: {champion.GetZ()}"
            );
        }
    }
}

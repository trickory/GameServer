using ENet;
using LeagueSandbox.GameServer.Packets;
using LeagueSandbox.GameServer.Packets.PacketHandlers;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class SkillpointsCommand : ChatCommand
    {
        public SkillpointsCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var game = Program.ResolveDependency<Game>();
            var playerManager = Program.ResolveDependency<PlayerManager>();

            playerManager.GetPeerInfo(peer).Champion.setSkillPoints(17);
            var skillUpResponse = new SkillUpPacket(playerManager.GetPeerInfo(peer).Champion.NetId, 0, 0, 17);
            game.PacketHandlerManager.sendPacket(peer, skillUpResponse, Channel.CHL_GAMEPLAY);
        }
    }
}

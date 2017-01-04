using System.Linq;
using ENet;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.Packets;
using LeagueSandbox.GameServer.Packets.PacketHandlers;
using LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class MobsCommand : ChatCommand
    {
        public MobsCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var game = Program.ResolveDependency<Game>();
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');
            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
                return;
            }
            int team;
            if (!int.TryParse(split[1], out team))
            {
                return;
            }
            var units = game.Map.GetObjects()
                .Where(xx => xx.Value.Team == CustomConvert.ToTeamId(team))
                .Where(xx => xx.Value is Minion || xx.Value is Monster);
            foreach (var unit in units)
            {
                var response = new AttentionPingAns(
                    playerManager.GetPeerInfo(peer),
                    new AttentionPing {
                        x = unit.Value.X,
                        y = unit.Value.Y,
                        targetNetId = 0,
                        type = Ping.Danger
                    });
                game.PacketHandlerManager.broadcastPacketTeam(
                    playerManager.GetPeerInfo(peer).Team,
                    response,
                    Channel.CHL_S2C
                );
            }
        }
    }
}

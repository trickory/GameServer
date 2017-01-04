using System;
using ENet;
using LeagueSandbox.GameServer.Packets.PacketHandlers;
using LeagueSandbox.GameServer.Players;
using Packet = LeagueSandbox.GameServer.Packets.Packet;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class PacketCommand : ChatCommand
    {
        public PacketCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner)
        {
        }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var game = Program.ResolveDependency<Game>();
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var s = arguments.Split(' ');
            if (s.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
                return;
            }

            var opcode = Convert.ToByte(s[1], 16);
            var packet = new Packet((PacketCmd) opcode);
            var buffer = packet.getBuffer();

            for (int i = 2; i < s.Length; i++)
            {
                if (s[i] == "netid")
                {
                    buffer.Write(playerManager.GetPeerInfo(peer).Champion.NetId);
                }
                else
                {
                    buffer.Write(Convert.ToByte(s[i], 16));
                }
            }

            game.PacketHandlerManager.sendPacket(peer, packet, Channel.CHL_S2C);
        }
    }
}

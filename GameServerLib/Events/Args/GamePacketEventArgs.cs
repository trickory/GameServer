using System;
using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class GamePacketEventArgs : EventArgs
    {
        public GamePacket GamePacket { get; private set; }
        public PacketProtocolFlags ProtocolFlag { get; private set; }
        public PacketChannel Channel { get; private set; }
        public byte[] RawPacketData { get; private set; }
        public byte[] PacketData { get; private set; }
        public short OpCode { get; private set; }
        public uint NetworkId { get; private set; }

        public GamePacketEventArgs(short opCode, uint networkId, byte[] packetData, byte[] rawPacketData,
            PacketChannel channel, PacketProtocolFlags flag)
        {
            OpCode = opCode;
            NetworkId = networkId;
            PacketData = packetData;
            RawPacketData = rawPacketData;
            Channel = channel;
            ProtocolFlag = flag;
        }
    }
}

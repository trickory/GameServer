using ENet;
using LeagueSandbox.GameServer.Core.Logic.PacketHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer
{
    public class HandlePacketArgs : EventArgs
    {
        private PacketCmd packetCmd;
        private byte[] data;
        private Channel packetChannel;
        private string wtf = "HI";

        public HandlePacketArgs(byte[] data, PacketCmd packetCmd, Channel packetChannel)
        {
            this.packetCmd = packetCmd;
            this.data = data;
            this.packetChannel = packetChannel;
        }
        
        public byte[] Data
        {
            get
            {
                return data;
            }
        }

        public string Wtf
        {
            get
            {
                return wtf;
            }
        }

        public PacketCmd PacketCMD
        {
            get
            {
                return packetCmd;
            }
        }

        public Channel PacketChannel
        {
            get
            {
                return packetChannel;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSandbox.GameServer.Core.Logic.PacketHandlers;
using System.Runtime.InteropServices;
using ENet;
using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Logic;
using BlowFishCS;
using LeagueSandbox.GameServer.Logic.Players;
using LeagueSandbox.GameServer.Logic.Packets;

namespace LeagueSandbox.GameServer.Core.Logic
{
    public class PacketHandlerManager
    {
        private Dictionary<PacketCmd, Dictionary<Channel, IPacketHandler>> _handlerTable;
        private List<Team> _teamsEnumerator;
        private Logger _logger;
        private BlowFish _blowfish;
        private Host _server;
        private PlayerManager _playerManager;

        public delegate void HandleKeyCheck(Peer sender, HandlePacketArgs args);
        public delegate void HandleQueryStatus(Peer sender, HandlePacketArgs args);
        public delegate void HandleLoadPing(Peer sender, HandlePacketArgs args);

        public event HandleKeyCheck OnHandleKeyCheck;
        public event HandleQueryStatus OnHandleQueryStatus;
        public event HandleLoadPing OnHandleLoadPing;

        

        public PacketHandlerManager(Logger logger, BlowFish blowfish, Host server, PlayerManager playerManager)
        {
            _logger = logger;
            _blowfish = blowfish;
            _server = server;
            _playerManager = playerManager;
            _teamsEnumerator = Enum.GetValues(typeof(Team)).Cast<Team>().ToList();
        }

        internal IPacketHandler TriggerHandler(PacketCmd cmd, Channel channelID)
        {
            var game = Program.ResolveDependency<Game>();
            var packetsHandledWhilePaused = new List<PacketCmd>
            {
                PacketCmd.PKT_UnpauseGame,
                PacketCmd.PKT_C2S_CharLoaded,
                PacketCmd.PKT_C2S_Click,
                PacketCmd.PKT_C2S_ClientReady,
                PacketCmd.PKT_C2S_Exit,
                PacketCmd.PKT_C2S_HeartBeat,
                PacketCmd.PKT_C2S_QueryStatusReq,
                PacketCmd.PKT_C2S_StartGame,
                PacketCmd.PKT_C2S_World_SendGameNumber,
                PacketCmd.PKT_ChatBoxMessage,
                PacketCmd.PKT_KeyCheck
            };
            if (game.IsPaused && !packetsHandledWhilePaused.Contains(cmd))
            {
                return null;
            }
            var handlers = _handlerTable[cmd];
            if (handlers.ContainsKey(channelID))
                return handlers[channelID];
            return null;
        }

        public bool sendPacket(Peer peer, GameServer.Logic.Packets.Packet packet, Channel channelNo, PacketFlags flag = PacketFlags.Reliable)
        {
            return sendPacket(peer, packet.GetBytes(), channelNo, flag);
        }

        private IntPtr allocMemory(byte[] data)
        {
            var unmanagedPointer = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, unmanagedPointer, data.Length);
            return unmanagedPointer;
        }

        private void releaseMemory(IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        public void printPacket(byte[] buffer, string str)
        {
            //string hex = BitConverter.ToString(buffer);
            // System.Diagnostics.Debug.WriteLine(str + hex.Replace("-", " "));
        }
        public bool sendPacket(Peer peer, byte[] source, Channel channelNo, PacketFlags flag = PacketFlags.Reliable)
        {
            ////PDEBUG_LOG_LINE(Logging," Sending packet:\n");
            //if(length < 300)
            //printPacket(source, "Sent: ");
            byte[] temp;
            if (source.Length >= 8)
                temp = _blowfish.Encrypt(source);
            else
                temp = source;

            return peer.Send((byte)channelNo, temp);
        }

        public bool broadcastPacket(byte[] data, Channel channelNo, PacketFlags flag = PacketFlags.Reliable)
        {
            ////PDEBUG_LOG_LINE(Logging," Broadcast packet:\n");
            //printPacket(data, "Broadcast: ");
            byte[] temp;
            if (data.Length >= 8)
                temp = _blowfish.Encrypt(data);
            else
                temp = data;

            var packet = new ENet.Packet();
            packet.Create(temp);
            _server.Broadcast((byte)channelNo, ref packet);
            return true;
        }

        public bool broadcastPacket(LeagueSandbox.GameServer.Logic.Packets.Packet packet, Channel channelNo, PacketFlags flag = PacketFlags.Reliable)
        {
            return broadcastPacket(packet.GetBytes(), channelNo, flag);
        }


        public bool broadcastPacketTeam(Team team, byte[] data, Channel channelNo, PacketFlags flag = PacketFlags.Reliable)
        {
            foreach (var ci in _playerManager.GetPlayers())
                if (ci.Item2.Peer != null && ci.Item2.Team == team)
                    sendPacket(ci.Item2.Peer, data, channelNo, flag);
            return true;
        }

        public bool broadcastPacketTeam(Team team, LeagueSandbox.GameServer.Logic.Packets.Packet packet, Channel channelNo, PacketFlags flag = PacketFlags.Reliable)
        {
            return broadcastPacketTeam(team, packet.GetBytes(), channelNo, flag);
        }

        public bool broadcastPacketVision(GameObject o, LeagueSandbox.GameServer.Logic.Packets.Packet packet, Channel channelNo, PacketFlags flag = PacketFlags.Reliable)
        {
            return broadcastPacketVision(o, packet.GetBytes(), channelNo, flag);
        }

        public bool broadcastPacketVision(GameObject o, byte[] data, Channel channelNo, PacketFlags flag = PacketFlags.Reliable)
        {
            var game = Program.ResolveDependency<Game>();
            foreach (var team in _teamsEnumerator)
            {
                if (team == Team.Neutral)
                    continue;
                broadcastPacket(data, channelNo, flag);
            }

            return true;
        }

        public bool handlePacket(Peer peer, byte[] data, Channel channelID)
        {
            var header = new PacketHeader(data);
            var packetArgs = new HandlePacketArgs(data, header.cmd, channelID);
            try
            {
                switch (header.cmd)
                {
                    case PacketCmd.PKT_C2S_StatsConfirm:
                    case PacketCmd.PKT_C2S_MoveConfirm:
                    case PacketCmd.PKT_C2S_ViewReq:
                        break;
                    case PacketCmd.PKT_KeyCheck:
                        HandleKeyCheck keyCheck = OnHandleKeyCheck;
                        keyCheck?.Invoke(peer, packetArgs);
                        break;
                    case PacketCmd.PKT_C2S_Ping_Load_Info:
                        HandleLoadPing loadPing = OnHandleLoadPing;
                        loadPing?.Invoke(peer, packetArgs);
                        break;
                    case PacketCmd.PKT_C2S_QueryStatusReq:
                        HandleQueryStatus queryStatus = OnHandleQueryStatus;
                        queryStatus?.Invoke(peer, packetArgs);
                        break;
                    default:
                        _logger.LogCoreWarning("Packet not Handled: " + header.cmd);
                        break;
                }

                printPacket(data, "Error: ");
            } catch (Exception e) { }
            _logger.LogCoreWarning("Packet Handled: " + header.cmd);
            return true;
        }

        public bool handlePacket(Peer peer, ENet.Packet packet, Channel channelID)
        {
            var data = new byte[(int)packet.Length];
            Marshal.Copy(packet.Data, data, 0, data.Length);

            if (data.Length >= 8)
                if (_playerManager.GetPeerInfo(peer) != null)
                    data = _blowfish.Decrypt(data);

            return handlePacket(peer, data, channelID);
        }
    }
}

using ENet;
using LeagueSandbox.GameServer.Chatbox;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleBlueTipClicked : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private ChatCommandManager _chatCommandManager = Program.ResolveDependency<ChatCommandManager>();
        private PlayerManager _playerManager = Program.ResolveDependency<PlayerManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var blueTipClicked = new BlueTipClicked(data);
            var removeBlueTip = new BlueTip(
                "",
                "",
                "",
                0,
                _playerManager.GetPeerInfo(peer).Champion.NetId,
                blueTipClicked.netid
            );
            _game.PacketHandlerManager.sendPacket(peer, removeBlueTip, Channel.CHL_S2C);

            _chatCommandManager.SendDebugMsgFormatted(
                ChatCommandManager.DebugMsgType.Normal,
                $"Clicked blue tip with netid: {blueTipClicked.netid}"
            );
            return true;
        }
    }
}

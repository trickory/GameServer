using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers.Handlers
{
    class HandleSwapItems : IPacketHandler
    {
        private Game _game = Program.ResolveDependency<Game>();
        private PlayerManager _playerManager = Program.ResolveDependency<PlayerManager>();

        public bool HandlePacket(Peer peer, byte[] data)
        {
            var request = new SwapItems(data);

            if (request.slotFrom > 6 || request.slotTo > 6)
                return false;

            // "Holy shit this needs refactoring" - Mythic, April 13th 2016
            _playerManager.GetPeerInfo(peer).Champion.getInventory().SwapItems(request.slotFrom, request.slotTo);
            _game.PacketNotifier.NotifyItemsSwapped(
                _playerManager.GetPeerInfo(peer).Champion,
                request.slotFrom,
                request.slotTo
            );

            return true;
        }
    }
}

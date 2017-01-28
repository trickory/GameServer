using System;
using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class GameNotifyEventArgs : EventArgs
    {
        public GameEventId EventId { get; private set; }
        public uint NetworkId { get; private set; }

        public GameNotifyEventArgs(GameEventId eventId, uint networkId)
        {
            EventId = eventId;
            NetworkId = networkId;
        }
    }
}

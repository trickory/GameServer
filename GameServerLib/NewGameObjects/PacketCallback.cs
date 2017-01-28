using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    class PacketCallback
    {
        public PacketCallbackType CallbackType { get; private set; }
        public Action<GamePacket> Action { get; private set; }

        public PacketCallback(PacketCallbackType type, Action<GamePacket> action)
        {
            CallbackType = type;
            Action = action;
        }
    }
}

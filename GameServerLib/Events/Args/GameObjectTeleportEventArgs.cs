using System;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class GameObjectTeleportEventArgs : EventArgs
    {
        public string RecallType { get; private set; }
        public string RecallName { get; private set; }

        public GameObjectTeleportEventArgs(string recallName, string recallType)
        {
            RecallName = recallName;
            RecallType = recallType;
        }
    }
}

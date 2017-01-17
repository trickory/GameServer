using System;

namespace LeagueSandbox.GameServer.Events
{
    class GameAfkEventArgs : EventArgs
    {
        public bool Process { get; private set; }

        public GameAfkEventArgs()
        {
            Process = false;
        }
    }
}

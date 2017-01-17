using System;

namespace LeagueSandbox.GameServer.Events.Args
{
    class CameraLockToggleEventArgs : EventArgs
    {
        public bool Process { get; private set; }

        public CameraLockToggleEventArgs()
        {
            Process = true;
        }
    }
}

using System;

namespace LeagueSandbox.GameServer.Events.Args
{
    class CameraSnapEventArgs : EventArgs
    {
        public bool Process { get; private set; }

        public CameraSnapEventArgs()
        {
            Process = true;
        }
    }
}

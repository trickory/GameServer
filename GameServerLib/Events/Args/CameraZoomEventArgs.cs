using System;

namespace LeagueSandbox.GameServer.Events.Args
{
    class CameraZoomEventArgs : EventArgs
    {
        public bool Process { get; private set; }

        public CameraZoomEventArgs()
        {
            Process = true;
        }
    }
}

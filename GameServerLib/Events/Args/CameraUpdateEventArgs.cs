using System;
using System.Numerics;

namespace LeagueSandbox.GameServer.Events.Args
{
    class CameraUpdateEventArgs : EventArgs
    {
        public bool Process { get; private set; }
        public Vector2 Mouse2D { get; private set; }

        public CameraUpdateEventArgs()
        {
            Process = true;
        }
    }
}

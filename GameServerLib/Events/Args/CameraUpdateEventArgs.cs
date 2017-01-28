using System;
using System.Numerics;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class CameraUpdateEventArgs : EventArgs
    {
        public Vector2 Mouse2D { get; private set; }
    }
}

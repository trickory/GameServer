using System;
using System.Numerics;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class GameObjectNewPathEventArgs : EventArgs
    {
        public Vector3[] Path { get; private set; }
        public float Speed { get; private set; }
        public bool IsDash { get; private set; }

        public GameObjectNewPathEventArgs(Vector3[] newPath, bool isDash, float speed)
        {
            Path = newPath;
            IsDash = isDash;
            Speed = speed;
        }
    }
}

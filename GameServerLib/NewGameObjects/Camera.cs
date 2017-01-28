using System.Numerics;
using LeagueSandbox.GameServer.Events;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    class Camera
    {
        public bool Locked { get; private set; }
        public float ZoomDistance { get; private set; }
        public Vector2 Yaw { get; private set; }
        public Vector2 ScreenPosition { get; private set; }
        public float ViewportDistance { get; private set; }
        public float YawY { get; private set; }
        public float YawX { get; private set; }
        public float Pitch { get; private set; }
        public float CameraY { get; private set; }
        public float CameraX { get; private set; }
        public static event CameraZoom OnZoom;
        public static event CameraUpdate OnUpdate;
        public static event CameraToggleLock OnToggleLock;
        public static event CameraSnap OnSnap;
    }
}

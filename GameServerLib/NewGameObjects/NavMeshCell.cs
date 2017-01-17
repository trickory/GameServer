using System.Numerics;
using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    class NavMeshCell
    {
        public short GridY { get; private set; }
        public short GridX { get; private set; }
        public CollisionFlags CollFlags { get; private set; }
        public Vector3 WorldPosition { get; private set; }
    }
}

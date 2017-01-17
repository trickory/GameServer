using System.Numerics;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    class GameObject
    {
        public bool IsDead { get; private set; }
        public float BoundingRadius { get; private set; }
        public GameObjectType Type { get; private set; }
        public GameObjectTeam Team { get; private set; }
        public bool IsValid { get; private set; }
        public Vector3 Position { get; private set; }
        public string Name { get; private set; }
        public BoundingBox BBox { get; private set; }
        public int NetworkId { get; private set; }
        public short Index { get; private set; }
        public static event GameObjectDelete OnDelete;
        public static event GameObjectCreate OnCreate;
    }
}

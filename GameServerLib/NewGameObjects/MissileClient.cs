using System.Numerics;
using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    class MissileClient : GameObject
    {
        public SpellSlot Slot { get; private set; }
        public GameObject Target { get; private set; }
        public Obj_AI_Base SpellCaster { get; private set; }
        public SpellData SpellData { get; private set; }
        public Vector3 EndPosition { get; private set; }
        public Vector3 StartPosition { get; private set; }
    }
}

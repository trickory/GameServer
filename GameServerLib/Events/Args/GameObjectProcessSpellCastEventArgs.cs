using System;
using System.Numerics;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    class GameObjectProcessSpellCastEventArgs : EventArgs
    {
        public bool Process { get; private set; }
        public bool IsToggle { get; private set; }
        public GameObject Target { get; private set; }
        public int CastedSpellCount { get; private set; }
        public Vector3 End { get; private set; }
        public Vector3 Start { get; private set; }
        public int Level { get; private set; }
        public SpellData SpellData { get; private set; }
        public float Time { get; private set; }
        public SpellSlot Slot { get; private set; }

        public GameObjectProcessSpellCastEventArgs(SpellData spellData, int level, Vector3 start, Vector3 end,
            GameObject target, int castedSpellCount, SpellSlot slot, bool isToggle)
        {
            SpellData = spellData;
            Level = level;
            Start = start;
            End = end;
            Target = target;
            CastedSpellCount = castedSpellCount;
            Slot = slot;
            IsToggle = isToggle;
            Process = true;
        }
    }
}

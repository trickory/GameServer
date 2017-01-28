using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Enums;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class SpellbookCastSpellEventArgs : EventArgs
    {
        public GameObject Target { get; private set; }
        public SpellSlot Slot { get; private set; }
        public Vector3 EndPosition { get; private set; }
        public Vector3 StartPositon { get; private set; }

        public SpellbookCastSpellEventArgs(Vector3 startPos, Vector3 endPos, GameObject target, SpellSlot slot)
        {
            StartPositon = startPos;
            EndPosition = endPos;
            Target = target;
            Slot = slot;
        }
    }
}

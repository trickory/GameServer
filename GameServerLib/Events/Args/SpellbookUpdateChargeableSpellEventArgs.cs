using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class SpellbookUpdateChargeableSpellEventArgs : EventArgs
    {
        public bool ReleaseCast { get; private set; }
        public Vector3 Position { get; private set; }
        public SpellSlot Slot { get; private set; }

        public SpellbookUpdateChargeableSpellEventArgs(SpellSlot slot, Vector3 position, bool releaseCast)
        {
            Slot = slot;
            Position = position;
            ReleaseCast = releaseCast;
        }
    }
}

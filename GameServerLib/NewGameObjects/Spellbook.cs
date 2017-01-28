using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Enums;
using LeagueSandbox.GameServer.Events;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    public class Spellbook
    {
        public bool HasSpellCaster { get; private set; }
        public bool IsCastingSpell { get; private set; }
        public bool SpellWasCast { get; private set; }
        public bool IsStopped { get; private set; }
        public bool IsCharging { get; private set; }
        public bool IsChanneling { get; private set; }
        public bool IsAutoAttacking { get; private set; }
        public float CastTime { get; private set; }
        public float CastEndTime { get; private set; }
        public Obj_AI_Base Owner { get; private set; }
        public SpellSlot SelectedSpellSlot { get; private set; }
        public SpellSlot ActiveSpellSlot { get; private set; }
        public List<SpellDataInst> Spells { get; private set; }
        public static event SpellbookPostCastSpell OnPostCastSpell;
        public static event SpellbookUpdateChargeableSpell OnUpdateChargeableSpell;
        public static event SpellbookStopCast OnStopCast;
        public static event SpellbookCastSpell OnCastSpell;

    }
}

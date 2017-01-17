using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    class OnHeroApplyCoolDownEventArgs : EventArgs
    {
        public float End { get; private set; }
        public float Start { get; private set; }
        public SpellSlot Slot { get; private set; }
        public SpellDataInst SpellData { get; private set; }
        public AIHeroClient Sender { get; private set; }

        public OnHeroApplyCoolDownEventArgs(float end, float start, SpellSlot slot, SpellDataInst sData, AIHeroClient sender)
        {
            End = end;
            Start = start;
            Slot = slot;
            SpellData = sData;
            Sender = sender;
        }
    }
}

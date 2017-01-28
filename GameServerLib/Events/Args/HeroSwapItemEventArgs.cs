using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class HeroSwapItemEventArgs : EventArgs
    {
        public int To { get; private set; }
        public int From { get; private set; }

        public HeroSwapItemEventArgs(AIHeroClient sender, int sourceSlotId, int targetSlotId)
        {
            To = targetSlotId;
            From = sourceSlotId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class OnHeroDeathEventArgs : EventArgs
    {
        public float DeathDuration { get; private set; }
        public Obj_AI_Base Sender { get; private set; }

        public OnHeroDeathEventArgs(Obj_AI_Base sender, float deathDuration)
        {
            DeathDuration = deathDuration;
            Sender = sender;
        }
    }
}

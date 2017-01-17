using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    class Obj_AI_BaseBuffGainEventArgs : EventArgs
    {
        public BuffInstance Buff { get; private set; }

        public Obj_AI_BaseBuffGainEventArgs(BuffInstance buff)
        {
            Buff = buff;
        }
    }
}

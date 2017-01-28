using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class Obj_AI_BaseLevelUpEventArgs : EventArgs
    {
        public int Level { get; private set; }

        public Obj_AI_BaseLevelUpEventArgs(int level)
        {
            Level = level;
        }
    }
}

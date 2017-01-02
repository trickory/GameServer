using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class Obj_BarracksDampener : Obj_AnimatedBuilding
    {
        public DampenerState State { get; set; }

        public Obj_BarracksDampener()
        {
        }

        public Obj_BarracksDampener(short index, uint networkId) : base(index, networkId)
        {
        }

        public enum DampenerState
        {
            Unknown = -1,
            Alive = 0,
            Destroyed = 1,
        }
    }
}

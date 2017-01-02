using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class Obj_AnimatedBuilding : Obj_Building
    {
        public Obj_AnimatedBuilding()
        {
        }

        public Obj_AnimatedBuilding(short index, uint networkId) : base(index, networkId)
        {
        }
    }
}

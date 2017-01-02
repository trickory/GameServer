using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class Obj_Barracks : AttackableUnit
    {
        public Obj_Barracks()
        {
        }

        public Obj_Barracks(short index, uint networkId) : base(index, networkId)
        {
        }
    }
}

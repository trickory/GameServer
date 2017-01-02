using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class Obj_Building : AttackableUnit
    {
        public Obj_Building()
        {
        }

        public Obj_Building(short index, uint networkId) : base(index, networkId)
        {
        }
    }
}

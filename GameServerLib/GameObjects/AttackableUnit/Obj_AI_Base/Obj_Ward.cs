using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.GameObjects
{
    class Obj_Ward : Obj_AI_Base
    {
        public WardType Type { get; set; }

        public Obj_Ward()
        {
        }

        public Obj_Ward(short index, uint networkId) : base(index, networkId)
        {
        }

        //may be outdated
        public enum WardType
        {
            StealthWard,
            VisionWard,
            WrigglesLantern,
            WardingTotem,
            GreaterTotem,
            GreaterStealthTotem,
            GreaterVisionTotem,
            Unknown,
        }
    }
}

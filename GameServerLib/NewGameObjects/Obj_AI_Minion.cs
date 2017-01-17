using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    class Obj_AI_Minion : Obj_AI_Base
    {
        public int RoamState { get; private set; }
        public int OriginalState { get; private set; }
        public int MinionLevel { get; private set; }
        public int CampNumber { get; private set; }
        public Vector3 LeashedPosition { get; private set; }
    }
}

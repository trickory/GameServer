using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class SpellbookStopCastEventArgs : EventArgs
    {
        public int Counter { get; private set; }
        public uint MissileNetworkId { get; private set; }
        public bool DestroyMissile { get; private set; }
        public bool ForceStop { get; private set; }
        public bool ExecuteCastFrame { get; private set; }
        public bool StopAnimation { get; private set; }

        public SpellbookStopCastEventArgs(bool stopAnimation, bool executeCastFrame, bool forceStop, bool destroyMissile, uint missileNetworkId, int counter)
        {
            Counter = counter;
            MissileNetworkId = missileNetworkId;
            DestroyMissile = destroyMissile;
            ForceStop = forceStop;
            ExecuteCastFrame = executeCastFrame;
            StopAnimation = stopAnimation;
        }
    }
}

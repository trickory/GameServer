using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class Obj_AI_UpdatePositionEventArgs : EventArgs
    {
        public Vector3 Position { get; private set; }
        public Obj_AI_Base Sender { get; private set; }

        public Obj_AI_UpdatePositionEventArgs(Vector3 position, Obj_AI_Base sender)
        {
            Position = position;
            Sender = sender;
        }
    }
}

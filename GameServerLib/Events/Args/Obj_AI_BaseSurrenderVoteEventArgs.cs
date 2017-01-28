using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class Obj_AI_BaseSurrenderVoteEventArgs : EventArgs
    {
        public SurrenderVoteType Type { get; private set; }

        public Obj_AI_BaseSurrenderVoteEventArgs(SurrenderVoteType type)
        {
            Type = type;
        }
    }
}

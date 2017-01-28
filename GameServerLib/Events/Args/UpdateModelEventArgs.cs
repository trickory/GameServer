using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class UpdateModelEventArgs : EventArgs
    {
        public int SkinId { get; private set; }
        public string Model { get; private set; }

        public UpdateModelEventArgs(string model, int skinId)
        {
            Model = model;
            SkinId = skinId;
        }
    }
}

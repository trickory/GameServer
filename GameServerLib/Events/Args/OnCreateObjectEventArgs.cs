using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Enums;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    class OnCreateObjectEventArgs : EventArgs
    {
        public GameObjectTeam Team { get; private set; }
        public int SkinId { get; private set; }
        public string Model { get; private set; }
        public Vector3 Position { get; private set; }

        public OnCreateObjectEventArgs(GameObject sender, string model, int skinId, Vector3 position)
        {
            Team = sender.Team;
            Model = model;
            SkinId = skinId;
            Position = position;
        }
    }
}

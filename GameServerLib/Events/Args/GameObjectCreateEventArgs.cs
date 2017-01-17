using System;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    class GameObjectCreateEventArgs : EventArgs
    {
        public GameObject GameObject { get; private set; }

        public GameObjectCreateEventArgs(GameObject sender)
        {
            GameObject = sender;
        }
    }
}

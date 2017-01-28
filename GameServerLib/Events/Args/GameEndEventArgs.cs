using System;
using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class GameEndEventArgs : EventArgs
    {
        public GameObjectTeam LosingTeam { get; private set; }
        public GameObjectTeam WinningTeam { get; private set; }

        public GameEndEventArgs(int winningTeam)
        {
            WinningTeam = (GameObjectTeam)winningTeam;
        }
    }
}

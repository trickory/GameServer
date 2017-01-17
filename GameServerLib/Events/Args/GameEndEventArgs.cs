using System;

namespace LeagueSandbox.GameServer.Events.Args
{
    class GameEndEventArgs : EventArgs
    {
        public GameObjectTeam LosingTeam { get; private set; }
        public GameObjectTeam WinningTeam { get; private set; }

        public GameEndEventArgs(int winningTeam)
        {
            WinningTeam = (GameObjectTeam)winningTeam;
        }
    }
}

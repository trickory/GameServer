using LeagueSandbox.GameServer.Enums;
using LeagueSandbox.GameServer.Events;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    class Game
    {
        public string BuildType { get; private set; }
        public string BuildTime { get; private set; }
        public string BuildDate { get; private set; }
        public string Version { get; private set; }
        public uint GameId { get; private set; }
        public bool IsCustomGame { get; private set; }
        public GameType Type { get; private set; }
        public GameMode Mode { get; private set; }
        public GameMapId MapId { get; private set; }
        public int Port { get; private set; }
        public string Region { get; private set; }
        public string IP { get; private set; }
        public int TicksPerSecond { get; private set; }
        public float Time { get; private set; }
        public static event GameNotify OnNotify;
        public static event GameAfk OnAfk;
        public static event GamePostTick OnPostTick;
        public static event GameTick OnTick;
        public static event GamePreTick OnPreTick;
        public static event GameProcessPacket OnProcessPacket;
        public static event GameEnd OnEnd;
        public static event GameUpdate OnUpdate;
    }
}

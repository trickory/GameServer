using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    public class BuffInstance
    {
        public bool IsVisible { get; private set; }
        public int CountAlt { get; private set; }
        public float StartTime { get; private set; }
        public float EndTime { get; private set; }
        public int Count { get; private set; }
        public bool IsInternal { get; private set; }
        public bool IsBlind { get; private set; }
        public bool IsDisarm { get; private set; }
        public bool IsFear { get; private set; }
        public bool IsKnockback { get; private set; }
        public bool IsKnockup { get; private set; }
        public bool IsRoot { get; private set; }
        public bool IsSilence { get; private set; }
        public bool IsSlow { get; private set; }
        public bool IsStunOrSuppressed { get; private set; }
        public bool IsSuppression { get; private set; }
        public bool IsPermanent { get; private set; }
        public bool IsValid { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsPositive { get; private set; }
        public GameObject Caster { get; private set; }
        public string SourceName { get; private set; }
        public string DisplayName { get; private set; }
        public BuffType Type { get; private set; }
        public string Name { get; private set; }
        public int Index { get; private set; }
    }
}

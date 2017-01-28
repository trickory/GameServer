namespace LeagueSandbox.GameServer.NewGameObjects
{
    public class Experience
    {
        public float XPNextLevelVisual { get; private set; }
        public float XPPercentage { get; private set; }
        public float XPToCurrentLevel { get; private set; }
        public float XPToNextLevel { get; private set; }
        public float XP { get; private set; }
        public int Level { get; private set; }
        public int SpellTrainingPoints { get; private set; }
    }
}

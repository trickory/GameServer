using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    public class Mastery
    {
        public byte Id { get; private set; }
        public MasteryPage Page { get; private set; }
        public byte Points { get; private set; }

        public Mastery(byte id, MasteryPage page, byte points)
        {
            Id = id;
            Page = page;
            Points = points;
        }
    }
}

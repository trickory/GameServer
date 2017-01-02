using LeagueSandbox.GameServer.Core.Logic;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class MovementVector
    {
        public short x;
        public short y;
        private Game _game = Program.ResolveDependency<Game>();

        public MovementVector() { }

        public MovementVector(short x, short y)
        {
            this.x = x;
            this.y = y;
        }

        public MovementVector(float x, float y)
        {
            /*this.x = FormatCoordinate(x, _game.Map.GetHeight() / 2);
            this.y = FormatCoordinate(y, _game.Map.GetWidth() / 2);*/
        }

        public static short FormatCoordinate(float coordinate, float origin)
        {
            return (short)((coordinate - origin) / 2f);
        }

        public static short TargetXToNormalFormat(float value)
        {
            var game = Program.ResolveDependency<Game>();
            //return FormatCoordinate(value, game.Map.GetWidth() / 2);
            return 0;
        }

        public static short TargetYToNormalFormat(float value)
        {
            var game = Program.ResolveDependency<Game>();
            //return FormatCoordinate(value, game.Map.GetHeight() / 2);
            return 0;
        }
    }
}

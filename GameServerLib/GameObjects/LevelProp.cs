using LeagueSandbox.GameServer.GameObjects;

namespace LeagueSandbox.GameServer.Logic.GameObjects
{
    public class LevelProp : Obj_AI_Base
    {
        public LevelProp()
        {
        }

        public LevelProp(short index, uint networkId) : base(index, networkId)
        {
        }
    }
}

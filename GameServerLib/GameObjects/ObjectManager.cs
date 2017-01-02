using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.GameObjects
{
    class ObjectManager
    {
        private static List<GameObject> cachedObjects = new List<GameObject>();

        public static IEnumerable<ObjectType> Get<ObjectType>() where ObjectType : GameObject, new()
        {
            if (typeof(GameObject) == typeof(ObjectType))
                return ObjectManager.cachedObjects.Cast<ObjectType>();
            return ObjectManager.cachedObjects.OfType<ObjectType>();
        }
    }
}

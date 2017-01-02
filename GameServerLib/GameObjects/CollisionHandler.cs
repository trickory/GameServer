using System;
using LeagueSandbox.GameServer.Core.Logic;
using System.Collections.Generic;
using System.Diagnostics;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class CollisionHandler
    {
        private Game _game = Program.ResolveDependency<Game>();

        private List<GameObject> _objects = new List<GameObject>();

        public CollisionHandler()
        {
            //Pathfinder.setMap(map);
            // Initialise the pathfinder.
        }

        public void AddObject(GameObject obj)
        {
            _objects.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            _objects.Remove(obj);
        }

        public void Update()
        {
            foreach (var obj in _objects)
            {
                if (obj is Obj_AI_Turret || obj is Obj_Building)
                {
                    continue;
                }

                //if (!_game.Map.AIMesh.isWalkable(obj.Position.X, obj.Position.Y))
               // {
               //     //obj.onCollision(null);
               // }

                foreach (var obj2 in _objects)
                {
                    if (obj == obj2)
                    {
                        continue;
                    }

                    //if (obj.IsCollidingWith(obj2))
                    //{
                    //    obj.onCollision(obj2);
                    //}
                }
            }
        }
    }
}

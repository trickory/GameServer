using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Enums;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class HeroIssueOrderEventArgs : EventArgs
    {
        public GameObject Target { get; private set; }
        public bool IsAttackMove { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public GameObjectOrder Order { get; private set; }

        public HeroIssueOrderEventArgs(GameObject target, bool isAttackMove, Vector3 targetPosition, GameObjectOrder order)
        {
            Target = target;
            IsAttackMove = isAttackMove;
            TargetPosition = targetPosition;
            Order = order;
        }
    }
}

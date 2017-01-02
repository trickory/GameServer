using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Logic.Items;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class Obj_AI_Turret : Obj_AI_Base
    {
        public Obj_AI_Turret()
        {
        }

        public Obj_AI_Turret(short index, uint networkId) : base(index, networkId)
        {

        }

        /*
        public string Name { get; private set; }
        protected float globalGold = 250.0f;
        protected float globalExp = 0.0f;

        public Obj_AI_Turret(
            string name,
            string model,
            float x = 0,
            float y = 0,
            TeamId team = Team.Order,
            uint netId = 0
        ) : base(model, new TurretStats(), 50, x, y, 1200, netId)
        {
            Name = name;
            SetTeam(team);
            Inventory = InventoryManager.CreateInventory(this);
        }

        public void CheckForTargets()
        {
            var objects = _game.Map.GetObjects();
            Obj_AI_Base nextTarget = null;
            var nextTargetPriority = 14;

            foreach (var it in objects)
            {
                var u = it.Value as Obj_AI_Base;

                if (u == null || u.IsDead || u.Team == Team || GetDistanceTo(u) > stats.Range.Total)
                    continue;

                // Note: this method means that if there are two champions within turret range,
                // The player to have been added to the game first will always be targeted before the others
                if (TargetUnit == null)
                {
                    var priority = (int)ClassifyTarget(u);
                    if (priority < nextTargetPriority)
                    {
                        nextTarget = u;
                        nextTargetPriority = priority;
                    }
                }
                else
                {
                    var targetIsChampion = TargetUnit as Obj_AI_Hero;

                    // Is the current target a champion? If it is, don't do anything
                    if (targetIsChampion != null)
                    {
                        // Find the next champion in range targeting an enemy champion who is also in range
                        var enemyChamp = u as Obj_AI_Hero;
                        if (enemyChamp != null && enemyChamp.TargetUnit != null)
                        {
                            var enemyChampTarget = enemyChamp.TargetUnit as Obj_AI_Hero;
                            if (enemyChampTarget != null && // Enemy Champion is targeting an ally
                                enemyChamp.GetDistanceTo(enemyChampTarget) <= enemyChamp.GetStats().Range.Total && // Enemy within range of ally
                                GetDistanceTo(enemyChampTarget) <= stats.Range.Total) // Enemy within range of this turret
                            {
                                nextTarget = enemyChamp; // No priority required
                                break;
                            }
                        }
                    }
                }
            }
            if (nextTarget != null)
            {
                TargetUnit = nextTarget;
                _game.PacketNotifier.NotifySetTarget(this, nextTarget);
            }
        }

        public override void update(float diff)
        {
            if (!IsAttacking)
            {
                CheckForTargets();
            }

            // Lose focus of the unit target if the target is out of range
            if (TargetUnit != null && GetDistanceTo(TargetUnit) > stats.Range.Total)
            {
                TargetUnit = null;
                _game.PacketNotifier.NotifySetTarget(this, null);
            }

            base.update(diff);
        }

        public override void die(Obj_AI_Base killer)
        {
            foreach (var player in _game.Map.GetAllChampionsFromTeam(killer.Team))
            {
                var goldEarn = globalGold;

                // Champions in Range within TURRET_RANGE * 1.5f will gain 150% more (obviously)
                if (player.GetDistanceTo(this) <= stats.Range.Total * 1.5f && !player.IsDead)
                {
                    goldEarn = globalGold * 2.5f;
                    if(globalExp > 0)
                        player.GetStats().Experience += globalExp;
                }


                player.GetStats().Gold += goldEarn;
                _game.PacketNotifier.NotifyAddGold(player, this, goldEarn);
            }
            _game.PacketNotifier.NotifyUnitAnnounceEvent(UnitAnnounces.TurretDestroyed, this, killer);
            base.die(killer);
        }

        public override void refreshWaypoints()
        {
        }

        public override float getMoveSpeed()
        {
            return 0;
        }
        */
    }
}

using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Core.Logic.RAF;
using LeagueSandbox.GameServer.Core.Logic;
using Newtonsoft.Json.Linq;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class Projectile : GameObject
    {
        public List<GameObject> ObjectsHit { get; private set; }
        public CastInfo CastInfo { get; private set; }
        public int ProjectileId { get; private set; }
        protected float _moveSpeed;
        protected int _flags;
        protected Spell _originSpell;
        private RAFManager _rafManager = Program.ResolveDependency<RAFManager>();
        private Logger _logger = Program.ResolveDependency<Logger>();

        public Projectile(
            int collisionRadius,
            CastInfo castInfo,
            Spell originSpell,
            float moveSpeed,
            string projectileName,
            int flags = 0,
            uint netId = 0
        )
        {
            CollisionRadius = collisionRadius;
            _originSpell = originSpell;
            _moveSpeed = moveSpeed;
            CastInfo = castInfo;
            ProjectileId = (int)_rafManager.GetHash(projectileName);
            if (!string.IsNullOrEmpty(projectileName))
            {
                JObject data;
                if (!_rafManager.ReadSpellData(projectileName, out data))
                {
                    _logger.LogCoreError("Couldn't find projectile stats for " + projectileName);
                    return;
                }
                VisionRadius = _rafManager.GetFloatValue(data, "SpellData", "MissilePerceptionBubbleRadius");
            }
            _flags = flags;
            ObjectsHit = new List<GameObject>();
            
            if (CastInfo.Target != null)
            {
                //((GameObject)target).incrementAttackerCount(); ?
            }

            //owner.incrementAttackerCount();
        }

        /*public override void update(float diff)
        {
            if (Target == null)
            {
                setToRemove();
                return;
            }

            base.update(diff);
        }

        public override void onCollision(GameObject collider)
        {
            base.onCollision(collider);
            if (Target != null && Target.IsSimpleTarget && !isToRemove())
            {
                CheckFlagsForUnit(collider as Obj_AI_Base);
            }
            else
            {
                if (Target == collider)
                {
                    CheckFlagsForUnit(collider as Obj_AI_Base);
                }
            }
        }

        public override float getMoveSpeed()
        {
            return _moveSpeed;
        }*/

        protected virtual void CheckFlagsForUnit(AttackableUnit unit)
        {
            if (CastInfo == null)
            {
                return;
            }

            //TODO: Replace with a CastInfo
            if (CastInfo.Target != null)
            { // Skillshot
                if (unit == null || ObjectsHit.Contains(unit))
                    return;

                if (unit.Team == Owner.Team
                    && !((_flags & (int)SpellFlag.SPELL_FLAG_AffectFriends) > 0))
                    return;

                if (unit.Team == Team.Neutral
                    && !((_flags & (int)SpellFlag.SPELL_FLAG_AffectNeutral) > 0))
                    return;

                if (unit.Team != Owner.Team
                    && unit.Team != Team.Neutral
                    && !((_flags & (int)SpellFlag.SPELL_FLAG_AffectEnemies) > 0))
                    return;


                if (unit.IsDead && !((_flags & (int)SpellFlag.SPELL_FLAG_AffectDead) > 0))
                    return;

                var m = unit as Obj_AI_Minion;
                if (m != null && !((_flags & (int)SpellFlag.SPELL_FLAG_AffectMinions) > 0))
                    return;
                
                var t = unit as Obj_AI_Turret;
                if (t != null && !((_flags & (int)SpellFlag.SPELL_FLAG_AffectTurrets) > 0))
                    return;

                var b = unit as Obj_Barracks;
                if ((b != null) && !((_flags & (int)SpellFlag.SPELL_FLAG_AffectBuildings) > 0))
                    return;

                var c = unit as Obj_AI_Hero;
                if (c != null && !((_flags & (int)SpellFlag.SPELL_FLAG_AffectHeroes) > 0))
                    return;

                ObjectsHit.Add(unit);
                //_originSpell.applyEffects(unit, this);
            }
            else
            {
                var u = CastInfo.Target;
                if (u != null)
                { // Autoguided spell
                    if (_originSpell != null)
                    {
                        //_originSpell.applyEffects(u, this);
                    }
                    else
                    { // auto attack
                        //Owner.AutoAttackHit(u);
                        //setToRemove();
                    }
                }
            }
        }
    }
}

using System;
using LeagueSandbox.GameServer.Enums;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class AttackableUnitDamageEventArgs : EventArgs
    {
        public DamageHitType HitType { get; private set; }
        public DamageType Type { get; private set; }
        public AttackableUnit Target { get; private set; }
        public AttackableUnit Source { get; private set; }
        public float Damage { get; private set; }

        public AttackableUnitDamageEventArgs(AttackableUnit source, AttackableUnit target, DamageHitType hitType,
            DamageType damageType, float damage, float gameTime)
        {
            Source = source;
            Target = target;
            HitType = hitType;
            Type = damageType;
            Damage = damage;
        }
    }
}

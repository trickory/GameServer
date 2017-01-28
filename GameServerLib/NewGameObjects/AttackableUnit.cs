using System.Numerics;
using LeagueSandbox.GameServer.Events;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    public class AttackableUnit : GameObject
    {
        public float PathfindingCollisionRadius { get; private set; }
        public float OverrideCollisionRadius { get; private set; }
        public float OverrideCollisionHeight { get; private set; }
        public string WeaponMaterial { get; private set; }
        public float ManaPercent { get; private set; }
        public float HealthPercent { get; private set; }
        public float MaxMana { get; private set; }
        public float MaxHealth { get; private set; }
        public float Mana { get; private set; }
        public float MagicShield { get; private set; }
        public bool MagicImmune { get; private set; }
        public bool IsZombie { get; private set; }
        public bool IsPhysicalImmune { get; private set; }
        public bool IsLifeStealImmune { get; private set; }
        public bool IsInvulnerable { get; private set; }
        public int IsBot { get; private set; }
        public float Health { get; private set; }
        public int HasBotAI { get; private set; }
        public float AttackShield { get; private set; }
        public float AllShield { get; private set; }
        public string ArmorMaterial { get; private set; }
        public bool IsTargetableToTeam { get; private set; }
        public bool IsTargetable { get; private set; }
        public bool IsAttackingPlayer { get; private set; }
        public bool IsRanged { get; private set; }
        public bool IsMelee { get; private set; }
        public Vector3 Direction { get; private set; }
        public static event AttackableUnitDamage OnDamage;
        public static event AttackableUnitModifyShield OnModifyShield;  
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class AttackableUnit : GameObject
    {
        public Vector3 Direction { get; set; }
        public int HasBotAI { get; set; }
        public bool IsAttackingPlayer { get; set; }
        public int IsBot { get; set; }
        public bool IsInvulnerable { get; set; }
        public bool IsMeele { get; set; }
        public bool IsRanged { get; set; }
        public bool IsTargetable { get; set; }
        public bool IsTargetableToTeam { get; set; }
        public bool IsZombie { get; set; }
        public float CollisionRadius { get; set; }

        public AttackableUnit()
        {
        }

        public AttackableUnit(short index, uint networkId) : base (index, networkId)
        {

        }
    }

    public enum DamageType : byte
    {
        DAMAGE_TYPE_PHYSICAL = 0,
        DAMAGE_TYPE_MAGICAL = 1,
        DAMAGE_TYPE_TRUE = 2
    }

    public enum DamageText : byte
    {
        DAMAGE_TEXT_INVULNERABLE = 0x00,
        DAMAGE_TEXT_DODGE = 0x02,
        DAMAGE_TEXT_CRITICAL = 0x03,
        DAMAGE_TEXT_NORMAL = 0x04,
        DAMAGE_TEXT_MISS = 0x05,
    }

    public enum DamageSource
    {
        DAMAGE_SOURCE_ATTACK,
        DAMAGE_SOURCE_SPELL,
        DAMAGE_SOURCE_SUMMONER_SPELL, //Ignite shouldn't destroy Banshee's
        DAMAGE_SOURCE_PASSIVE //Red/Thornmail shouldn't as well
    }

    public enum AttackType : byte
    {
        ATTACK_TYPE_RADIAL,
        ATTACK_TYPE_MELEE,
        ATTACK_TYPE_TARGETED
    }

    public enum MoveOrder
    {
        MOVE_ORDER_MOVE,
        MOVE_ORDER_ATTACKMOVE
    }

    public enum ShieldType : byte
    {
        GreenShield = 0x01,
        MagicShield = 0x02,
        NormalShield = 0x03
    }
}

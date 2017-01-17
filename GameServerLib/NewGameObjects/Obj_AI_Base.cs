using System.Collections.Generic;
using System.Collections.Specialized;
using System.Numerics;
using LeagueSandbox.GameServer.Enums;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    class Obj_AI_Base : AttackableUnit
    {
        public Vector3 FearLeashPoint { get; private set; }
        public Vector3 LastPausePosition { get; private set; }
        public int AI_LastPetSpawnedId { get; private set; }
        public float SpellCastBlockingAI { get; private set; }
        public float PetReturnRadius { get; private set; }
        public float PercentLocalGoldRewardMod { get; private set; }
        public float FlatGoldRewardMod { get; private set; }
        public float FlatExpRewardMod { get; private set; }
        public float PathfindingRadiusMod { get; private set; }
        public float AcquisitionRangeMod { get; private set; }
        public float _NonHealingFlatHPPoolMod { get; private set; }
        public float _PercentMagicPenetrationModPerLevel { get; private set; }
        public float _PercentMagicPenetrationMod { get; private set; }
        public float _FlatMagicPenetrationModPerLevel { get; private set; }
        public float _FlatMagicPenetrationMod { get; private set; }
        public float FlatGoldPer10Mod { get; private set; }
        public float _PercentTimeDeadModPerLevel { get; private set; }
        public float _PercentTimeDeadMod { get; private set; }
        public float _FlatTimeDeadModPerLevel { get; private set; }
        public float _FlatTimeDeadMod { get; private set; }
        public float _PercentCooldownModPerLevel { get; private set; }
        public float _PercentCooldownMod { get; private set; }
        public float _PercentArmorPenetrationModPerLevel { get; private set; }
        public float _PercentArmorPenetrationMod { get; private set; }
        public float _FlatArmorPenetrationModPerLevel { get; private set; }
        public float _FlatArmorPenetrationMod { get; private set; }
        public float _FlatDodgeModPerLevel { get; private set; }
        public float _FlatDodgeMod { get; private set; }
        public float _FlatCritDamageModPerLevel { get; private set; }
        public float _FlatCritChanceModPerLevel { get; private set; }
        public float _PercentAttackSpeedModPerLevel { get; private set; }
        public float _PercentMovementSpeedModPerLevel { get; private set; }
        public float _FlatMovementSpeedModPerLevel { get; private set; }
        public float _FlatMagicDamageModPerLevel { get; private set; }
        public float _FlatPhysicalDamageModPerLevel { get; private set; }
        public float _FlatMPRegenModPerLevel { get; private set; }
        public float _FlatHPRegenModPerLevel { get; private set; }
        public float _FlatSpellBlockModPerLevel { get; private set; }
        public float _FlatArmorModPerLevel { get; private set; }
        public float _FlatMPModPerLevel { get; private set; }
        public float _FlatHPModPerLevel { get; private set; }
        public float PercentBubbleRadiusMod { get; private set; }
        public float FlatBubbleRadiusMod { get; private set; }
        public float CastRange { get; private set; }
        public float AttackRange { get; private set; }
        public float MoveSpeed { get; private set; }
        public float PARRegenRate { get; private set; }
        public float PercentBasePARRegenMod { get; private set; }
        public float BasePARRegenRate { get; private set; }
        public float HPRegenRate { get; private set; }
        public float SpellBlock { get; private set; }
        public float Armor { get; private set; }
        public float Crit { get; private set; }
        public float Dodge { get; private set; }
        public float MissChance { get; private set; }
        public float ScaleSkinCoef { get; private set; }
        public float CritDamageMultiplier { get; private set; }
        public float BaseAbilityDamage { get; private set; }
        public float BaseAttackDamage { get; private set; }
        public float AttackSpeedMod { get; private set; }
        public float PercentGoldLostOnDeathMod { get; private set; }
        public float PercentRespawnTimeMod { get; private set; }
        public float PercentSpellVampMod { get; private set; }
        public float PercentLifeStealMod { get; private set; }
        public float PercentHealingAmountMod { get; private set; }
        public float PercentMultiplicativeAttackSpeedMod { get; private set; }
        public float PercentAttackSpeedMod { get; private set; }
        public float PercentCastRangeMod { get; private set; }
        public float FlatCastRangeMod { get; private set; }
        public float PercentAttackRangeMod { get; private set; }
        public float FlatAttackRangeMod { get; private set; }
        public float PercentEXPBonus { get; private set; }
        public float PercentMagicReduction { get; private set; }
        public float FlatMagicReduction { get; private set; }
        public float PercentPhysicalReduction { get; private set; }
        public float FlatPhysicalReduction { get; private set; }
        public float PercentMagicDamageMod { get; private set; }
        public float FlatMagicDamageMod { get; private set; }
        public float PercentPhysicalDamageMod { get; private set; }
        public float FlatPhysicalDamageMod { get; private set; }
        public float PercentCritDamageMod { get; private set; }
        public float FlatCritDamageMod { get; private set; }
        public float FlatCritChanceMod { get; private set; }
        public float FlatDodgeMod { get; private set; }
        public float FlatMissChanceMod { get; private set; }
        public float PercentSpellBlockMod { get; private set; }
        public float FlatSpellBlockMod { get; private set; }
        public float PercentBonusMagicPenetrationMod { get; private set; }
        public float PercentMagicPenetrationMod { get; private set; }
        public float FlatMagicPenetrationMod { get; private set; }
        public float PercentBonusArmorPenetrationMod { get; private set; }
        public float PercentArmorPenetrationMod { get; private set; }
        public float FlatArmorPenetrationMod { get; private set; }
        public float PercentArmorMod { get; private set; }
        public float FlatArmorMod { get; private set; }
        public float MoveSpeedFloorMod { get; private set; }
        public float PercentMultiplicativeMovementSpeedMod { get; private set; }
        public float PercentMovementSpeedSlowMod { get; private set; }
        public float FlatMovementSpeedSlowMod { get; private set; }
        public float PercentMovementSpeedHasteMod { get; private set; }
        public float FlatMovementSpeedHasteMod { get; private set; }
        public float PercentSlowResistMod { get; private set; }
        public float PercentCCReduction { get; private set; }
        public float PercentTenacityRuneMod { get; private set; }
        public float PercentTenacityMasteryMod { get; private set; }
        public float PercentTenacityItemMod { get; private set; }
        public float PercentTenacityCharacterMod { get; private set; }
        public float PercentTenacityCleanseMod { get; private set; }
        public float PercentPARRegenMod { get; private set; }
        public float FlatPARRegenMod { get; private set; }
        public float PercentBaseHPRegenMod { get; private set; }
        public float PercentHPRegenMod { get; private set; }
        public float FlatHPRegenMod { get; private set; }
        public float PercentPARPoolMod { get; private set; }
        public float FlatPARPoolMod { get; private set; }
        public float PercentHPPoolMod { get; private set; }
        public float FlatHPPoolMod { get; private set; }
        public float PercentCooldownMod { get; private set; }
        public float PassiveCooldownTotalTime { get; private set; }
        public float PassiveCooldownEndTime { get; private set; }
        public float FlatCooldownMod { get; private set; }
        public bool PlayerControlled { get; private set; }
        public float GoldTotal { get; private set; }
        public float Gold { get; private set; }
        public float ExpGiveRadius { get; private set; }
        public int EvolvePoints { get; private set; }
        public int AutoAttackTargettingFlags { get; private set; }
        public float AttackDelay { get; private set; }
        public float AttackCastDelay { get; private set; }
        public float DeathDuration { get; private set; }
        public float TotalMagicalDamage { get; private set; }
        public float TotalAttackDamage { get; private set; }
        public GameObject Pet { get; private set; }
        public Vector3 InfoComponentBasePosition { get; private set; }
        public Vector3[] Path { get; private set; }
        public SpellData BasicAttack { get; private set; }
        public bool CanAttack { get; private set; }
        public bool CanCast { get; private set; }
        public bool CanMove { get; private set; }
        public bool IsStealthed { get; private set; }
        public bool IsRevealSpecificUnit { get; private set; }
        public bool IsPacified { get; private set; }
        public bool IsStunned { get; private set; }
        public bool IsRooted { get; private set; }
        public bool IsTaunted { get; private set; }
        public bool IsCharmed { get; private set; }
        public bool IsFeared { get; private set; }
        public bool IsAsleep { get; private set; }
        public bool IsNearSight { get; private set; }
        public bool IsGhosted { get; private set; }
        public bool IsNoRender { get; private set; }
        public bool IsFleeing { get; private set; }
        public bool IsForceRenderParticles { get; private set; }
        public bool IsIgnoreCallForHelp { get; private set; }
        public bool IsSuppressCallForHelp { get; private set; }
        public bool IsCallForHelpSuppresser { get; private set; }
        public GameObjectCharacterState CharacterState { get; private set; }
        public float FlatDamageReductionFromBarracksMinionMod { get; private set; }
        public float PercentDamageToBarracksMinionMod { get; private set; }
        public BitVector32 Flags { get; private set; }
        public CharData CharData { get; private set; }
        public bool IsMonster { get; private set; }
        public bool IsHPBarRendered { get; private set; }
        public bool IsMinion { get; private set; }
        public Vector3 ServerPosition { get; private set; }
        public GameObjectCombatType CombatType { get; private set; }
        public float HPBarYOffset { get; private set; }
        public float HPBarXOffset { get; private set; }
        public Vector2 HPBarPosition { get; private set; }
        public string BaseSkinName { get; private set; }
        public new Vector3 Direction { get; private set; }
        public Spellbook Spellbook { get; private set; }
        public int SkinId { get; private set; }
        public string Model { get; private set; }
        public bool IsMoving { get; private set; }
        public InventorySlot[] InventoryItems { get; private set; }
        public List<BuffInstance> Buffs { get; private set; }
        public static event Obj_AI_BaseOnSurrenderVote OnSurrender;
        public static event Obj_AI_BaseOnBasicAttack OnBasicAttack;
        public static event Obj_AI_BaseDoCastSpell OnSpellCast;
        public static event Obj_AI_UpdatePosition OnUpdatePosition;
        public static event Obj_AI_UpdateModel OnUpdateModel;
        public static event Obj_AI_BaseLevelUp OnLevelUp;
        public static event Obj_AI_BaseBuffUpdate OnBuffUpdate;
        public static event Obj_AI_BaseBuffLose OnBuffLose;
        public static event Obj_AI_BaseBuffGain OnBuffGain;
        public static event Obj_AI_BasePlayAnimation OnPlayAnimation;
        public static event Obj_AI_BaseNewPath OnNewPath;
        public static event Obj_AI_BaseTeleport OnTeleport;
        public static event Obj_AI_ProcessSpellCast OnProcessSpellCast;
    }
}

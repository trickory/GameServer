using LeagueSandbox.GameServer.Core.Logic;
using System;
using System.Collections.Generic;
using System.Numerics;
using LeagueSandbox.GameServer.Core.Logic.RAF;
using NLua.Exceptions;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.Scripting.Lua;
using LeagueSandbox.GameServer.Logic.Items;
using LeagueSandbox.GameServer.Logic.Content;
using LeagueSandbox.GameServer.Logic.Players;

namespace LeagueSandbox.GameServer.GameObjects
{

    public class Obj_AI_Base : AttackableUnit
    {
        protected IScriptEngine _scriptEngine = new LuaScriptEngine();
        protected Logger _logger = Program.ResolveDependency<Logger>();

        public int AI_LastPetSpawnedId { get; set; }
        public float AttackCastDelay { get; set; }
        public float AttackDelay { get; set; }
        public float AttackRange { get; set; }
        public int AutoAttackTargettingFlags { get; set; }
        public string BaseSkinName { get; set; }
        public Stats Stats { get; set; }
        public List<BuffInstance> Buffs { get; set; }
        public bool CanAttack { get; set; }
        public bool CanCast { get; set; }
        public bool CanMove { get; set; }
        public float CastRange { get; set; }
        public CombatType CombatType { get; set; }
        public float DeathDuration { get; set; }
        public int EvolvePoints { get; set; }
        public float ExpGivenRadius { get; set; }
        public Vector3 FearLeashPoint { get; set; }
        public InventoryManager Inventory { get; set; }
        public bool IsAsleep { get; set; }
        public bool IsCharmed { get; set; }
        public bool IsFeared { get; set; }
        public bool IsFleeing { get; set; }
        public bool IsGhosted { get; set; }
        public bool IsMinion { get; set; }
        public bool IsMonster { get; set; }
        public bool IsMoving { get; set; }
        public bool IsNearSight { get; set; }
        public bool IsPacified { get; set; }
        public bool IsRooted { get; set; }
        public bool IsStealthed { get; set; }
        public bool IsStunned { get; set; }
        public bool IsTaunted { get; set; }
        public Vector3 LastPausePosition { get; set; }
        public string Model { get; set; }
        public List<Vector3> Path { get; set; } //Waypoints?
        public GameObject Pet { get; set; }
        public float PetReturnRadius { get; set; }
        public bool PlayerControlled { get; set; }
        public float ScaleSkinCoef { get; set; }
        public int SkinId { get; set; }


        //TODO: Event Args and Trigger
        public delegate void BasicAttack(Obj_AI_Base sender, EventArgs args);
        public delegate void BuffGain(Obj_AI_Base sender, EventArgs args);
        public delegate void BuffLose(Obj_AI_Base sender, EventArgs args);
        public delegate void BuffUpdate(Obj_AI_Base sender, EventArgs args);
        public delegate void LevelUp(Obj_AI_Base sender, EventArgs args);
        public delegate void NewPath(Obj_AI_Base sender, EventArgs args);
        public delegate void PlayAnimation(Obj_AI_Base sender, EventArgs args);
        public delegate void ProcessSpellCast(Obj_AI_Base sender, EventArgs args);
        public delegate void SpellCast(Obj_AI_Base sender, EventArgs args);
        public delegate void Surrender(Obj_AI_Base sender, EventArgs args);
        public delegate void Teleport(Obj_AI_Base sender, EventArgs args);
        public delegate void UpdateModel(Obj_AI_Base sender, EventArgs args);
        public delegate void UpdatePosition(Obj_AI_Base sender, EventArgs args);

        public event BasicAttack OnBasicAttack;
        public event BuffGain OnBuffGain;
        public event BuffLose OnBuffLose;
        public event BuffUpdate OnBuffUpdate;
        public event LevelUp OnLevelUp;
        public event NewPath OnNewPath;
        public event PlayAnimation OnPlayAnimation;
        public event ProcessSpellCast OnProcessSpellCast;
        public event SpellCast OnSpellCast;
        public event Surrender OnSurrender;
        public event Teleport OnTeleport;
        public event UpdateModel OnUpdateModel;
        public event UpdatePosition OnUpdatePosition;

        public Obj_AI_Base()
        {

        }

        public Obj_AI_Base(short index, uint networkId) : base(index, networkId)
        {
        }

        public BuffInstance GetBuff(string name)
        {
            return null;
        }

        public int GetBuffCount(string name)
        {
            return 0;
        }

        public bool HasBuff(string name)
        {
            return false;
        }

        public bool HasBuffOfType(BuffType type)
        {
            return false;
        }

        public bool SetModel(string model)
        {
            return false;
        }
        
        public bool SetSkin(string model, int skinId)
        {
            return false;
        }

        public bool SetSkinId(int skinId)
        {
            return false;
        }

        /*
        public virtual void LoadLua()
        {
            _scriptEngine = new LuaScriptEngine();

            _scriptEngine.Execute("package.path = 'LuaLib/?.lua;' .. package.path");
            _scriptEngine.Execute(@"
                function onAutoAttack(target)
                end");
            _scriptEngine.Execute(@"
                function onUpdate(diff)
                end");
            _scriptEngine.Execute(@"
                function onDealDamage(target, damage, type, source)
                end");
            _scriptEngine.Execute(@"
                function onDie(killer)
                end");
            _scriptEngine.Execute(@"
                function onDamageTaken(attacker, damage, type, source)
                end");
            _scriptEngine.Execute(@"
                function onCollide(collider)
                end");
            _scriptEngine.Execute(@"
                function onCollideWithTerrain()
                end");

            ApiFunctionManager.AddBaseFunctionToLuaScript(_scriptEngine);
        }

        public Stats GetStats()
        {
            return stats;
        }

        public override void update(float diff)
        {
            _timerUpdate += diff;
            if (_timerUpdate >= UPDATE_TIME)
            {
                if (_scriptEngine.IsLoaded())
                {
                    try
                    {
                        _scriptEngine.RunFunction("onUpdate", _timerUpdate);
                    }
                    catch (LuaScriptException e)
                    {
                        _logger.LogCoreError("LUA ERROR : " + e.Message);
                    }
                }
                _timerUpdate = 0;
            }

            UpdateAutoAttackTarget(diff);

            base.update(diff);

            _statUpdateTimer += diff;
            if (_statUpdateTimer >= 500)
            { // update stats (hpregen, manaregen) every 0.5 seconds
                stats.update(_statUpdateTimer);
                _statUpdateTimer = 0;
            }
        }

        public void UpdateAutoAttackTarget(float diff)
        {
            if (IsDead)
            {
                if (TargetUnit != null)
                {
                    SetTargetUnit(null);
                    AutoAttackTarget = null;
                    IsAttacking = false;
                    _game.PacketNotifier.NotifySetTarget(this, null);
                    _hasMadeInitialAttack = false;
                }
                return;
            }

            if (TargetUnit != null)
            {
                if (TargetUnit.IsDead || !_game.Map.TeamHasVisionOn(Team, TargetUnit))
                {
                    SetTargetUnit(null);
                    IsAttacking = false;
                    _game.PacketNotifier.NotifySetTarget(this, null);
                    _hasMadeInitialAttack = false;

                }
                else if (IsAttacking && AutoAttackTarget != null)
                {
                    _autoAttackCurrentDelay += diff / 1000.0f;
                    if (_autoAttackCurrentDelay >= AutoAttackDelay / stats.AttackSpeedMultiplier.Total)
                    {
                        if (!IsMelee)
                        {
                            var p = new Projectile(
                                X,
                                Y,
                                5,
                                this,
                                AutoAttackTarget,
                                null,
                                AutoAttackProjectileSpeed,
                                "",
                                0,
                                _autoAttackProjId
                            );
                            _game.Map.AddObject(p);
                            _game.PacketNotifier.NotifyShowProjectile(p);
                        }
                        else
                        {
                            AutoAttackHit(AutoAttackTarget);
                        }
                        _autoAttackCurrentCooldown = 1.0f / (stats.GetTotalAttackSpeed());
                        IsAttacking = false;
                    }

                }
                else if (GetDistanceTo(TargetUnit) <= stats.Range.Total)
                {
                    refreshWaypoints();
                    _isNextAutoCrit = random.Next(0, 100) < stats.CriticalChance.Total * 100;
                    if (_autoAttackCurrentCooldown <= 0)
                    {
                        IsAttacking = true;
                        _autoAttackCurrentDelay = 0;
                        _autoAttackProjId = _networkIdManager.GetNewNetID();
                        AutoAttackTarget = TargetUnit;

                        if (!_hasMadeInitialAttack)
                        {
                            _hasMadeInitialAttack = true;
                            _game.PacketNotifier.NotifyBeginAutoAttack(
                                this,
                                TargetUnit,
                                _autoAttackProjId,
                                _isNextAutoCrit
                            );
                        }
                        else
                        {
                            _nextAttackFlag = !_nextAttackFlag; // The first auto attack frame has occurred
                            _game.PacketNotifier.NotifyNextAutoAttack(
                                this,
                                TargetUnit,
                                _autoAttackProjId,
                                _isNextAutoCrit,
                                _nextAttackFlag
                                );
                        }

                        var attackType = IsMelee ? AttackType.ATTACK_TYPE_MELEE : AttackType.ATTACK_TYPE_TARGETED;
                        _game.PacketNotifier.NotifyOnAttack(this, TargetUnit, attackType);
                    }

                }
                else
                {
                    refreshWaypoints();
                }

            }
            else if (IsAttacking)
            {
                if (AutoAttackTarget == null
                    || AutoAttackTarget.IsDead
                    || !_game.Map.TeamHasVisionOn(Team, AutoAttackTarget)
                )
                {
                    IsAttacking = false;
                    _hasMadeInitialAttack = false;
                    AutoAttackTarget = null;
                }
            }

            if (_autoAttackCurrentCooldown > 0)
            {
                _autoAttackCurrentCooldown -= diff / 1000.0f;
            }
        }

        public override float getMoveSpeed()
        {
            return stats.MoveSpeed.Total;
        }

        public Dictionary<string, Buff> GetBuffs()
        {
            var toReturn = new Dictionary<string, Buff>();
            lock (_buffsLock)
            {
                foreach (var buff in _buffs)
                    toReturn.Add(buff.Key, buff.Value);

                return toReturn;
            }
        }

        public int GetBuffsCount()
        {
            return _buffs.Count;
        }

        public override void onCollision(GameObject collider)
        {
            base.onCollision(collider);

            if (!_scriptEngine.IsLoaded())
            {
                return;
            }

            try
            {
                if (collider == null)
                {
                    _scriptEngine.RunFunction("onCollideWithTerrain");
                }
                else
                {
                    _scriptEngine.RunFunction("onCollide", collider);
                }
            }
            catch (LuaException e)
            {
                _logger.LogCoreError("LUA ERROR : " + e.Message);
            }
        }

        /// <summary>
        /// This is called by the AA projectile when it hits its target
        /// </summary>
        public virtual void AutoAttackHit(Obj_AI_Base target)
        {
            var damage = stats.AttackDamage.Total;
            if (_isNextAutoCrit)
            {
                damage *= stats.getCritDamagePct();
            }

            dealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL,
                                             DamageSource.DAMAGE_SOURCE_ATTACK,
                                             _isNextAutoCrit);

            if (_scriptEngine.IsLoaded())
            {
                try
                {
                    _scriptEngine.RunFunction("onAutoAttack", target);
                }
                catch (LuaScriptException e)
                {
                    _logger.LogCoreError("LUA ERROR : " + e.Message);
                }
            }
        }

        public virtual void dealDamageTo(Obj_AI_Base target, float damage, DamageType type, DamageSource source, bool isCrit)
        {
            var text = DamageText.DAMAGE_TEXT_NORMAL;

            if (isCrit)
            {
                text = DamageText.DAMAGE_TEXT_CRITICAL;
            }

            if (_scriptEngine.IsLoaded())
            {
                try
                {
                    _scriptEngine.RunFunction("onDealDamage", target, damage, type, source);
                }
                catch (LuaScriptException e)
                {
                    _logger.LogCoreError("ERROR LUA : " + e.Message);
                }
            }

            float defense = 0;
            float regain = 0;
            switch (type)
            {
                case DamageType.DAMAGE_TYPE_PHYSICAL:
                    defense = target.GetStats().Armor.Total;
                    defense = (1 - stats.ArmorPenetration.PercentBonus) * defense - stats.ArmorPenetration.FlatBonus;

                    break;
                case DamageType.DAMAGE_TYPE_MAGICAL:
                    defense = target.GetStats().MagicPenetration.Total;
                    defense = (1 - stats.MagicPenetration.PercentBonus)*defense - stats.MagicPenetration.FlatBonus;
                    break;
            }

            switch (source)
            {
                case DamageSource.DAMAGE_SOURCE_SPELL:
                    regain = stats.SpellVamp.Total;
                    break;
                case DamageSource.DAMAGE_SOURCE_ATTACK:
                    regain = stats.LifeSteal.Total;
                    break;
            }

            //Damage dealing. (based on leagueoflegends' wikia)
            damage = defense >= 0 ? (100 / (100 + defense)) * damage : (2 - (100 / (100 - defense))) * damage;
            if (target._scriptEngine.IsLoaded())
            {
                try
                {
                    target._scriptEngine.Execute(@"
                        function modifyIncomingDamage(value)
                            damage = value
                        end");
                    target._scriptEngine.RunFunction("onDamageTaken", this, damage, type, source);
                }
                catch (LuaScriptException e)
                {
                    _logger.LogCoreError("LUA ERROR : " + e);
                }
            }

            target.GetStats().CurrentHealth = Math.Max(0.0f, target.GetStats().CurrentHealth - damage);
            if (!target.IsDead && target.GetStats().CurrentHealth <= 0)
            {
                target.IsDead = true;
                target.die(this);
            }
            _game.PacketNotifier.NotifyDamageDone(this, target, damage, type, text);
            _game.PacketNotifier.NotifyUpdatedStats(target, false);

            //Get health from lifesteal/spellvamp
            if (regain != 0)
            {
                stats.CurrentHealth = Math.Min(stats.HealthPoints.Total, stats.CurrentHealth + regain * damage);
                _game.PacketNotifier.NotifyUpdatedStats(this, false);
            }
        }

        public virtual void die(Obj_AI_Base killer)
        {
            if (_scriptEngine.IsLoaded())
            {
                try
                {
                    _scriptEngine.RunFunction("onDie", killer);
                }
                catch (LuaScriptException e)
                {
                    _logger.LogCoreError(string.Format("LUA ERROR : {0}", e.Message));
                }
            }

            setToRemove();
            _game.Map.StopTargeting(this);

            _game.PacketNotifier.NotifyNpcDie(this, killer);

            float exp = _game.Map.GetExperienceFor(this);
            var champs = _game.Map.GetChampionsInRange(this, EXP_RANGE, true);
            //Cull allied champions
            champs.RemoveAll(l => l.Team == Team);

            if (champs.Count > 0)
            {
                float expPerChamp = exp / champs.Count;
                foreach (var c in champs)
                {
                    c.GetStats().Experience += expPerChamp;
                    _game.PacketNotifier.NotifyAddXP(c, expPerChamp);
                }
            }

            if (killer != null)
            {
                var cKiller = killer as Obj_AI_Hero;

                if (cKiller == null)
                    return;

                float gold = _game.Map.GetGoldFor(this);
                if (gold <= 0)
                    return;

                cKiller.GetStats().Gold += gold;
                _game.PacketNotifier.NotifyAddGold(cKiller, this, gold);

                if (cKiller.KillDeathCounter < 0)
                {
                    cKiller.ChampionGoldFromMinions += gold;
                    _logger.LogCoreInfo(string.Format(
                        "Adding gold form minions to reduce death spree: {0}",
                        cKiller.ChampionGoldFromMinions
                    ));
                }

                if (cKiller.ChampionGoldFromMinions >= 50 && cKiller.KillDeathCounter < 0)
                {
                    cKiller.ChampionGoldFromMinions = 0;
                    cKiller.KillDeathCounter += 1;
                }
            }

            if (IsDashing)
            {
                IsDashing = false;
            }
        }

        public void AddBuff(Buff b)
        {
            lock (_buffsLock)
            {
                if (!_buffs.ContainsKey(b.Name))
                {
                    _buffs.Add(b.Name, b);
                }
                else
                {
                    _buffs[b.Name].TimeElapsed = 0; // if buff already exists, just restart its timer
                }
            }
        }

        public void RemoveBuff(Buff b)
        {
            //TODO add every stat
            RemoveBuff(b.Name);
        }

        public void RemoveBuff(string b)
        {
            lock (_buffsLock)
                _buffs.Remove(b);
        }

        public virtual bool isInDistress()
        {
            return false; //return DistressCause;
        }

        //todo: use statmods
        public Buff GetBuff(string name)
        {
            lock (_buffsLock)
            {
                if (_buffs.ContainsKey(name))
                    return _buffs[name];
                return null;
            }
        }

        public void SetTargetUnit(Obj_AI_Base target)
        {
            if (target == null) // If we are unsetting the target (moving around)
            {
                if (TargetUnit != null) // and we had a target
                    TargetUnit.DistressCause = null; // Unset the distress call
                                                      // TODO: Replace this with a delay?

                IsAttacking = false;
            }
            else
            {
                target.DistressCause = this; // Otherwise set the distress call
            }

            TargetUnit = target;
            refreshWaypoints();
        }

        public virtual void refreshWaypoints()
        {
            if (TargetUnit == null || (GetDistanceTo(TargetUnit) <= stats.Range.Total && Waypoints.Count == 1))
                return;

            if (GetDistanceTo(TargetUnit) <= stats.Range.Total - 2.0f)
            {
                SetWaypoints(new List<Vector2> { new Vector2(X, Y) });
            }
            else
            {
                var t = new Target(Waypoints[Waypoints.Count - 1]);
                if (t.GetDistanceTo(TargetUnit) >= 25.0f)
                {
                    SetWaypoints(new List<Vector2> { new Vector2(X, Y), new Vector2(TargetUnit.X, TargetUnit.Y) });
                }
            }
        }

        public ClassifyUnit ClassifyTarget(Obj_AI_Base target)
        {
            if (target.TargetUnit != null && target.TargetUnit.isInDistress()) // If an ally is in distress, target this unit. (Priority 1~5)
            {
                if (target is Obj_AI_Hero && target.TargetUnit is Obj_AI_Hero) // If it's a champion attacking an allied champion
                {
                    return ClassifyUnit.ChampionAttackingChampion;
                }

                if (target is Obj_AI_Minion && target.TargetUnit is Obj_AI_Hero) // If it's a minion attacking an allied champion.
                {
                    return ClassifyUnit.MinionAttackingChampion;
                }

                if (target is Obj_AI_Minion && target.TargetUnit is Obj_AI_Minion) // Minion attacking minion
                {
                    return ClassifyUnit.MinionAttackingMinion;
                }

                if (target is Obj_AI_Turret && target.TargetUnit is Obj_AI_Minion) // Turret attacking minion
                {
                    return ClassifyUnit.TurretAttackingMinion;
                }

                if (target is Obj_AI_Hero && target.TargetUnit is Obj_AI_Minion) // Champion attacking minion
                {
                    return ClassifyUnit.ChampionAttackingMinion;
                }
            }

            var p = target as Placeable;
            if (p != null)
            {
                return ClassifyUnit.Placeable;
            }

            var m = target as Obj_AI_Minion;
            if (m != null)
            {
                switch (m.getType())
                {
                    case MinionSpawnType.MINION_TYPE_MELEE:
                        return ClassifyUnit.MeleeMinion;
                    case MinionSpawnType.MINION_TYPE_CASTER:
                        return ClassifyUnit.CasterMinion;
                    case MinionSpawnType.MINION_TYPE_CANNON:
                    case MinionSpawnType.MINION_TYPE_SUPER:
                        return ClassifyUnit.SuperOrCannonMinion;
                }
            }

            if (target is Obj_AI_Turret)
            {
                return ClassifyUnit.Turret;
            }

            if (target is Obj_AI_Hero)
            {
                return ClassifyUnit.Champion;
            }

            if (target is Inhibitor && !target.IsDead)
            {
                return ClassifyUnit.Inhibitor;
            }

            if (target is Nexus)
            {
                return ClassifyUnit.Nexus;
            }

            return ClassifyUnit.Default;
        }*/

    }

    public enum UnitAnnounces : byte
    {
        Death = 0x04,
        InhibitorDestroyed = 0x1F,
        InhibitorAboutToSpawn = 0x20,
        InhibitorSpawned = 0x21,
        TurretDestroyed = 0x24,
        SummonerDisconnected = 0x47,
        SummonerReconnected = 0x48
    }

    public enum ClassifyUnit
    {
        ChampionAttackingChampion = 1,
        MinionAttackingChampion = 2,
        MinionAttackingMinion = 3,
        TurretAttackingMinion = 4,
        ChampionAttackingMinion = 5,
        Placeable = 6,
        MeleeMinion = 7,
        CasterMinion = 8,
        SuperOrCannonMinion = 9,
        Turret = 10,
        Champion = 11,
        Inhibitor = 12,
        Nexus = 13,
        Default = 14
    }
}

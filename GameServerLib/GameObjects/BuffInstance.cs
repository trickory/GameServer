using System.Collections.Generic;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Core.Logic.PacketHandlers;
using LeagueSandbox.GameServer.Logic.API;
using NLua.Exceptions;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.Scripting.Lua;
using LeagueSandbox.GameServer.Logic;

namespace LeagueSandbox.GameServer.GameObjects
{
    public enum BuffType : byte
    {
        Internal,
        Aura,
        CombatEnchancer,
        CombatDehancer,
        SpellShield,
        Stun,
        Invisibility,
        Silence,
        Taunt,
        Polymorph,
        Slow,
        Snare,
        Damage,
        Heal,
        Haste,
        SpellImmunity,
        PhysicalImmunity,
        Invulnerability,
        Sleep,
        NearSight,
        Frenzy,
        Fear,
        Charm,
        Poison,
        Suppression,
        Blind,
        Counter,
        Shred,
        Flee,
        Knockup,
        Knockback,
        Disarm
    }

    public class BuffInstance
    {
        private Logger _logger = Program.ResolveDependency<Logger>();

        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string SourceName { get; set; }

        public GameObject Caster { get; set; }
        public GameObject Target { get; set; }

        public BuffType Type { get; set; }

        public float StartTime { get; set; }
        public float EndTime { get; set; }
        public byte Slot { get; set; }

        public int Count { get; set; }
        public int CountAlt { get; set; }
        public int Index { get; }

        public bool IsActive { get; set; }
        public bool IsBlind { get; set; }
        public bool IsDisarm { get; set; }
        public bool IsFear { get; set; }
        public bool IsInternal { get; set; }
        public bool IsKnockback { get; set; }
        public bool IsKnockup { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsPositive { get; set; }
        public bool IsSilence { get; set; }
        public bool IsSlow { get; set; }
        public bool IsStunOrSuppressed { get; set; }
        public bool IsValid { get; set; }
        public bool IsVisible { get; set; }

        protected Dictionary<Pair<MasterMask, FieldMask>, float> StatsModified = new Dictionary<Pair<MasterMask, FieldMask>, float>();

        public BuffInstance()
        {
            /**
            _game = game;
            Duration = dur;
            Stacks = stacks;
            Slot = 0x01;
            Name = buffName;
            TimeElapsed = 0;
            _remove = false;
            TargetUnit = onto;
            SourceUnit = from;
            BuffType = BuffType.Aura;
            LoadLua();
            try
            {
                _scriptEngine.RunFunction("onAddBuff");
            }
            catch (LuaException e)
            {
                _logger.LogCoreError("LUA ERROR : " + e.Message);
            }
            **/
        }
        
        /*
        public void LoadLua()
        {
            var scriptLoc = _game.Config.ContentManager.GetBuffScriptPath(Name);
            _logger.LogCoreInfo("Loading buff from " + scriptLoc);

            _scriptEngine.Execute("package.path = 'LuaLib/?.lua;' .. package.path");
            _scriptEngine.Execute(@"
                function onAddBuff()
                end");
            _scriptEngine.Execute(@"
                function onUpdate(diff)
                end");
            _scriptEngine.Execute(@"
                function onBuffEnd()
                end");
            _scriptEngine.RegisterFunction("getSourceUnit", this, typeof(Buff).GetMethod("GetSourceUnit"));
            _scriptEngine.RegisterFunction("getUnit", this, typeof(Buff).GetMethod("GetUnit"));
            _scriptEngine.RegisterFunction("getStacks", this, typeof(Buff).GetMethod("GetStacks"));
            _scriptEngine.RegisterFunction("addStat", this, typeof(Buff).GetMethod("GetStacks"));
            _scriptEngine.RegisterFunction("substractStat", this, typeof(Buff).GetMethod("GetStacks"));
            _scriptEngine.RegisterFunction("setStat", this, typeof(Buff).GetMethod("GetStacks"));

            ApiFunctionManager.AddBaseFunctionToLuaScript(_scriptEngine);

            _scriptEngine.Load(scriptLoc);
        }

        public void Update(float diff)
        {
            TimeElapsed += (float)diff / 1000.0f;

            if (_scriptEngine.IsLoaded())
            {
                try
                {
                    _scriptEngine.RunFunction("onUpdate", diff);
                }
                catch (LuaException e)
                {
                    _logger.LogCoreError("LUA ERROR : " + e.Message);
                }
            }

            if (Duration != 0.0f)
            {
                if (TimeElapsed >= Duration)
                {
                    try
                    {
                        _scriptEngine.RunFunction("onBuffEnd");
                    }
                    catch (LuaException e)
                    {
                        _logger.LogCoreError("LUA ERROR : " + e.Message);
                    }
                    _remove = true;
                }
            }
        }*/
    }
}

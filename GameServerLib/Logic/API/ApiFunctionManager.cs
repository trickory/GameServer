using System;
using System.Collections.Generic;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Core.Logic.PacketHandlers;
using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Packets;
using System.Linq;
using LeagueSandbox.GameServer.Logic.Scripting;
using System.Numerics;
using LeagueSandbox.GameServer.GameObjects;

namespace LeagueSandbox.GameServer.Logic.API
{
    public static class ApiFunctionManager
    {
        private static Game _game;

        public static byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace(" ", string.Empty);
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        internal static void SetGame(Game game)
        {
            _game = game;
        }

        public static void TeleportTo(Obj_AI_Base unit, float x, float y)
        {
            var coords = new Vector3(x, y, 0);
            //var truePos = _game.Map.AIMesh.getClosestTerrainExit(coords);
            //_game.PacketNotifier.NotifyTeleport(unit, truePos.X, truePos.Y);
        }

        public static bool IsWalkable(float x, float y)
        {
            return true;
        }

        public static void AddBuff(string buffName, float duration, int stacks, Obj_AI_Base onto, Obj_AI_Base from)
        {
           //var buff = new BuffInstance(_game, buffName, duration, stacks, onto, from);
           //onto.AddBuff(buff);
           //_game.PacketNotifier.NotifyAddBuff(buff);
        }

        public static void AddParticle(Obj_AI_Hero champion, string particle, float toX, float toY, float size = 1.0f, string bone = "")
        {
            var t = new CastInfo(champion, null, new Vector3(toX, toY, 0), new Vector3(0,0,0));
            _game.PacketNotifier.NotifyParticleSpawn(new Particle(t, particle, size, bone));
        }

        public static void AddParticleTarget(Obj_AI_Hero champion, string particle, Obj_AI_Base target, float size = 1.0f, string bone = "")
        {
            var t = new CastInfo(champion, target, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            _game.PacketNotifier.NotifyParticleSpawn(new Particle(t, particle, size, bone));
        }

        public static void PrintChat(string msg)
        {
            var dm = new DebugMessage(msg);
            _game.PacketHandlerManager.broadcastPacket(dm, Channel.CHL_S2C);
        }

        public static List<Obj_AI_Base> GetUnitsInRange(Vector3 target, float range, bool isAlive)
        {
            //return _game.Map.GetUnitsInRange(target.X, target.Y, range, isAlive);
            return null;
        }

        public static List<Obj_AI_Hero> GetChampionsInRange(Vector3 target, float range, bool isAlive)
        {
            //return _game.Map.GetChampionsInRange(target.X, target.Y, range, isAlive);
            return null;
        }

        public static void SetChampionModel(Obj_AI_Hero champion, string model)
        {
            champion.Model = model;
        }

        public static void DashToUnit(CastInfo castInfo,
                                  float dashSpeed,
                                  bool keepFacingLastDirection,
                                  string animation = null,
                                  float leapHeight = 0.0f,
                                  float followTargetMaxDistance = 0.0f,
                                  float backDistance = 0.0f,
                                  float travelTime = 0.0f
                                  )
        {
            if (animation != null)
            {
                var animList = new List<string> {"RUN", animation};
                _game.PacketNotifier.NotifySetAnimation(castInfo.Caster, animList);
            }

            if (castInfo.Target != null)
            {
                //var newCoords = _game.Map.AIMesh.getClosestTerrainExit(castInfo.Caster.Position);
                /*castInfo.Caster.DashToTarget(newCoords, dashSpeed, followTargetMaxDistance, backDistance, travelTime);
                _game.PacketNotifier.NotifyDash(
                    castInfo,
                    dashSpeed,
                    keepFacingLastDirection,
                    leapHeight,
                    followTargetMaxDistance,
                    backDistance,
                    travelTime
                );*/
            }
            else
            {
                /*castInfo.Caster.DashToTarget(castInfo.Position, dashSpeed, followTargetMaxDistance, backDistance, travelTime);
                _game.PacketNotifier.NotifyDash(
                    castInfo,
                    dashSpeed,
                    keepFacingLastDirection,
                    leapHeight,
                    followTargetMaxDistance,
                    backDistance,
                    travelTime
                );*/
            }
            //castInfo.Caster.TargetUnit = null;
        }

        public static void DashToLocation(CastInfo castInfo,
                                 float dashSpeed,
                                 bool keepFacingLastDirection,
                                 string animation = null,
                                 float leapHeight = 0.0f,
                                 float followTargetMaxDistance = 0.0f,
                                 float backDistance = 0.0f,
                                 float travelTime = 0.0f
                                 )
        {
            DashToUnit(
                castInfo,
                dashSpeed,
                keepFacingLastDirection,
                animation,
                leapHeight,
                followTargetMaxDistance,
                backDistance,
                travelTime
            );
        }

        public static Team GetTeam(GameObject gameObject)
        {
            return gameObject.Team;
        }

        public static bool IsDead(Obj_AI_Base unit)
        {
            return unit.IsDead;
        }

        public static void SendPacket(string packetString)
        {
            var packet = StringToByteArray(packetString);
            _game.PacketHandlerManager.broadcastPacket(packet, Channel.CHL_S2C);
        }

        public static bool UnitIsChampion(GameObject unit)
        {
            return unit is Obj_AI_Hero;
        }

        public static bool UnitIsMinion(GameObject unit)
        {
            return unit is Obj_AI_Minion;
        }

        public static bool UnitIsTurret(GameObject unit)
        {
            return unit is Obj_AI_Turret;
        }

        public static bool UnitIsBuilding(GameObject unit)
        {
            return unit is Obj_Building;
        }

        public static void AddBaseFunctionToLuaScript(IScriptEngine scriptEngine)
        {
            if (scriptEngine == null)
                return;
            scriptEngine.RegisterFunction("setChampionModel", null, typeof(ApiFunctionManager).GetMethod("SetChampionModel", new Type[] { typeof(Obj_AI_Hero), typeof(string) }));
            scriptEngine.RegisterFunction("teleportTo", null, typeof(ApiFunctionManager).GetMethod("TeleportTo", new Type[] { typeof(Obj_AI_Base), typeof(float), typeof(float) }));
            scriptEngine.RegisterFunction("addParticle", null, typeof(ApiFunctionManager).GetMethod("AddParticle", new Type[] { typeof(Obj_AI_Hero), typeof(string), typeof(float), typeof(float), typeof(float), typeof(string) }));
            scriptEngine.RegisterFunction("addParticleTarget", null, typeof(ApiFunctionManager).GetMethod("AddParticleTarget", new Type[] { typeof(Obj_AI_Hero), typeof(string), typeof(AttackableUnit), typeof(float), typeof(string) }));
            scriptEngine.RegisterFunction("addBuff", null, typeof(ApiFunctionManager).GetMethod("AddBuff", new Type[] { typeof(string), typeof(float), typeof(int), typeof(Obj_AI_Base), typeof(Obj_AI_Base) }));
            scriptEngine.RegisterFunction("printChat", null, typeof(ApiFunctionManager).GetMethod("PrintChat", new Type[] { typeof(string) }));
            scriptEngine.RegisterFunction("getUnitsInRange", null, typeof(ApiFunctionManager).GetMethod("GetUnitsInRange", new Type[] { typeof(AttackableUnit), typeof(float), typeof(bool) }));
            scriptEngine.RegisterFunction("getChampionsInRange", null, typeof(ApiFunctionManager).GetMethod("GetChampionsInRange", new Type[] { typeof(AttackableUnit), typeof(float), typeof(bool) }));
            scriptEngine.RegisterFunction("dashToLocation", null, typeof(ApiFunctionManager).GetMethod("DashToLocation", new Type[] { typeof(Obj_AI_Base), typeof(float), typeof(float), typeof(float), typeof(bool), typeof(string), typeof(float), typeof(float), typeof(float), typeof(float) }));
            scriptEngine.RegisterFunction("dashToUnit", null, typeof(ApiFunctionManager).GetMethod("DashToUnit", new Type[] { typeof(Obj_AI_Base), typeof(AttackableUnit), typeof(float), typeof(bool), typeof(string), typeof(float), typeof(float), typeof(float), typeof(float) }));
            scriptEngine.RegisterFunction("getTeam", null, typeof(ApiFunctionManager).GetMethod("GetTeam", new Type[] { typeof(GameObject) }));
            scriptEngine.RegisterFunction("isDead", null, typeof(ApiFunctionManager).GetMethod("IsDead", new Type[] { typeof(Obj_AI_Base) }));
            scriptEngine.RegisterFunction("sendPacket", null, typeof(ApiFunctionManager).GetMethod("SendPacket", new Type[] { typeof(string) }));
            scriptEngine.RegisterFunction("unitIsChampion", null, typeof(ApiFunctionManager).GetMethod("UnitIsChampion", new Type[] { typeof(GameObject) }));
            scriptEngine.RegisterFunction("unitIsMinion", null, typeof(ApiFunctionManager).GetMethod("UnitIsMinion", new Type[] { typeof(GameObject) }));
            scriptEngine.RegisterFunction("unitIsTurret", null, typeof(ApiFunctionManager).GetMethod("UnitIsTurret", new Type[] { typeof(GameObject) }));
            scriptEngine.RegisterFunction("unitIsInhibitor", null, typeof(ApiFunctionManager).GetMethod("UnitIsInhibitor", new Type[] { typeof(GameObject) }));
            scriptEngine.RegisterFunction("unitIsNexus", null, typeof(ApiFunctionManager).GetMethod("UnitIsNexus", new Type[] { typeof(GameObject) }));
            scriptEngine.RegisterFunction("unitIsPlaceable", null, typeof(ApiFunctionManager).GetMethod("UnitIsPlaceable", new Type[] { typeof(GameObject) }));
            scriptEngine.RegisterFunction("unitIsMonster", null, typeof(ApiFunctionManager).GetMethod("UnitIsMonster", new Type[] { typeof(GameObject) }));
        }
    }
}

using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Core.Logic.PacketHandlers;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.Logic.Content;
using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Players;
using System.Collections.Generic;
using static LeagueSandbox.GameServer.GameObjects.Obj_BarracksDampener;

namespace LeagueSandbox.GameServer.Logic.Packets
{
    public class PacketNotifier
    {
        private Game _game;
        private PlayerManager _playerManager;
        private NetworkIdManager _networkIdManager;

        public PacketNotifier(Game game, PlayerManager playerManager, NetworkIdManager networkIdManager)
        {
            _game = game;
            _playerManager = playerManager;
            _networkIdManager = networkIdManager;
        }

        public void NotifyMinionSpawned(Obj_AI_Minion m, Team team)
        {
            var ms = new MinionSpawn(m);
            _game.PacketHandlerManager.broadcastPacketTeam(team, ms, Channel.CHL_S2C);
            NotifySetHealth(m);
        }

        public void NotifySetHealth(Obj_AI_Base u)
        {
            var sh = new SetHealth(u);
            _game.PacketHandlerManager.broadcastPacketVision(u, sh, Channel.CHL_S2C);
        }

        public void NotifyGameEnd(Obj_Barracks nexus)
        {
            var losingTeam = nexus.Team;

            foreach (var p in _playerManager.GetPlayers())
            {
                /*var coords = _game.Map.GetEndGameCameraPosition(losingTeam);
                var cam = new MoveCamera(p.Item2.Champion, coords[0], coords[1], coords[2], 2);
                _game.PacketHandlerManager.sendPacket(p.Item2.Peer, cam, Channel.CHL_S2C);
                _game.PacketHandlerManager.sendPacket(p.Item2.Peer, new HideUi(), Channel.CHL_S2C);*/
            }

            _game.PacketHandlerManager.broadcastPacket(new ExplodeNexus(nexus), Channel.CHL_S2C);

            var timer = new System.Timers.Timer(5000) { AutoReset = false };
            timer.Elapsed += (a, b) =>
            {
                var gameEndPacket = new GameEnd(losingTeam != Team.Order);
                _game.PacketHandlerManager.broadcastPacket(gameEndPacket, Channel.CHL_S2C);
            };
            timer.Start();
            Program.SetToExit();
        }

        public void NotifyUpdatedStats(Obj_AI_Base u, bool partial = true)
        {
            var us = new UpdateStats(u, partial);
            _game.PacketHandlerManager.broadcastPacketVision(u, us, Channel.CHL_LOW_PRIORITY, ENet.PacketFlags.Unsequenced);
        }

        public void NotifyInhibitorState(Obj_BarracksDampener inhibitor, GameObject killer = null, List<Obj_AI_Hero> assists = null)
        {
            UnitAnnounce announce;
            switch (inhibitor.State)
            {
                case DampenerState.Destroyed:
                    announce = new UnitAnnounce(UnitAnnounces.InhibitorDestroyed, inhibitor, killer, assists);
                    _game.PacketHandlerManager.broadcastPacket(announce, Channel.CHL_S2C);

                    var anim = new InhibitorDeathAnimation(inhibitor, killer);
                    _game.PacketHandlerManager.broadcastPacket(anim, Channel.CHL_S2C);
                    break;
                case DampenerState.Alive:
                    announce = new UnitAnnounce(UnitAnnounces.InhibitorSpawned, inhibitor, killer, assists);
                    _game.PacketHandlerManager.broadcastPacket(announce, Channel.CHL_S2C);
                    break;
            }
            var packet = new InhibitorStateUpdate(inhibitor);
            _game.PacketHandlerManager.broadcastPacket(packet, Channel.CHL_S2C);
        }

        public void NotifyInhibitorSpawningSoon(Obj_BarracksDampener inhibitor)
        {
            var packet = new UnitAnnounce(UnitAnnounces.InhibitorAboutToSpawn, inhibitor);
            _game.PacketHandlerManager.broadcastPacket(packet, Channel.CHL_S2C);
        }

        public void NotifyAddBuff(BuffInstance b)
        {
            var add = new AddBuff(b.Target, b.Caster, b.Count, b.EndTime, BuffType.Aura, b.Name, b.Slot);
            _game.PacketHandlerManager.broadcastPacket(add, Channel.CHL_S2C);
        }

        public void NotifyRemoveBuff(Obj_AI_Base u, string buffName, byte slot = 0x01)
        {
            var remove = new RemoveBuff(u, buffName, slot);
            _game.PacketHandlerManager.broadcastPacket(remove, Channel.CHL_S2C);
        }

        public void NotifyTeleport(Obj_AI_Base u, float _x, float _y)
        {
            // Can't teleport to this point of the map
            if (true)
            {
                _x = MovementVector.TargetXToNormalFormat(u.Position.X);
                _y = MovementVector.TargetYToNormalFormat(u.Position.Y);
            }
            else
            {
                u.Position = new System.Numerics.Vector3(_x, _y, 0);

                //TeleportRequest first(u.NetId, u.teleportToX, u.teleportToY, true);
                //sendPacket(currentPeer, first, Channel.CHL_S2C);

                _x = MovementVector.TargetXToNormalFormat(_x);
                _y = MovementVector.TargetYToNormalFormat(_y);
            }

            var second = new TeleportRequest(u.NetworkID, _x, _y, false);
            _game.PacketHandlerManager.broadcastPacketVision(u, second, Channel.CHL_S2C);
        }

        public void NotifyMovement(Obj_AI_Base o)
        {
            var answer = new MovementAns(o);
            _game.PacketHandlerManager.broadcastPacketVision(o, answer, Channel.CHL_LOW_PRIORITY);
        }

        public void NotifyDamageDone(Obj_AI_Base source, Obj_AI_Base target, float amount, DamageType type, DamageText damagetext)
        {
            var dd = new DamageDone(source, target, amount, type, damagetext);
            _game.PacketHandlerManager.broadcastPacket(dd, Channel.CHL_S2C);
        }

        public void NotifyModifyShield(Obj_AI_Base unit, float amount, ShieldType type)
        {
            var ms = new ModifyShield(unit, amount, type);
            _game.PacketHandlerManager.broadcastPacket(ms, Channel.CHL_S2C);
        }

        public void NotifyBeginAutoAttack(Obj_AI_Base attacker, Obj_AI_Base victim, uint futureProjNetId, bool isCritical)
        {
            var aa = new BeginAutoAttack(attacker, victim, futureProjNetId, isCritical);
            _game.PacketHandlerManager.broadcastPacket(aa, Channel.CHL_S2C);
        }

        public void NotifyNextAutoAttack(Obj_AI_Base attacker, Obj_AI_Base target, uint futureProjNetId, bool isCritical, bool nextAttackFlag)
        {
            var aa = new NextAutoAttack(attacker, target, futureProjNetId, isCritical, nextAttackFlag);
            _game.PacketHandlerManager.broadcastPacket(aa, Channel.CHL_S2C);
        }

        public void NotifyOnAttack(Obj_AI_Base attacker, Obj_AI_Base attacked, AttackType attackType)
        {
            var oa = new OnAttack(attacker, attacked, attackType);
            _game.PacketHandlerManager.broadcastPacket(oa, Channel.CHL_S2C);
        }

        public void NotifyProjectileSpawn(Projectile p)
        {
            var sp = new SpawnProjectile(p);
            _game.PacketHandlerManager.broadcastPacket(sp, Channel.CHL_S2C);
        }

        public void NotifyProjectileDestroy(Projectile p)
        {
            var dp = new DestroyProjectile(p);
            _game.PacketHandlerManager.broadcastPacket(dp, Channel.CHL_S2C);
        }

        public void NotifyParticleSpawn(Particle particle)
        {
            var sp = new SpawnParticle(particle);
            _game.PacketHandlerManager.broadcastPacket(sp, Channel.CHL_S2C);
        }

        public void NotifyModelUpdate(Obj_AI_Base obj)
        {
            var mp = new UpdateModel(obj.NetworkID, obj.Model);
            _game.PacketHandlerManager.broadcastPacket(mp, Channel.CHL_S2C);
        }

        public void NotifyItemBought(Obj_AI_Base u, Item i)
        {
            var response = new BuyItemAns(u, i);
            _game.PacketHandlerManager.broadcastPacketVision(u, response, Channel.CHL_S2C);
        }

        public void NotifyFogUpdate2(Obj_AI_Base u)
        {
            var fog = new FogUpdate2(u);
            _game.PacketHandlerManager.broadcastPacketTeam(u.Team, fog, Channel.CHL_S2C);
        }

        public void NotifyItemsSwapped(Obj_AI_Hero c, byte fromSlot, byte toSlot)
        {
            var sia = new SwapItems(c, fromSlot, toSlot);
            _game.PacketHandlerManager.broadcastPacketVision(c, sia, Channel.CHL_S2C);
        }

        public void NotifyLevelUp(Obj_AI_Hero c)
        {
            var lu = new LevelUp(c);
            _game.PacketHandlerManager.broadcastPacket(lu, Channel.CHL_S2C);
        }

        public void NotifyRemoveItem(Obj_AI_Hero c, byte slot, byte remaining)
        {
            var ri = new RemoveItem(c, slot, remaining);
            _game.PacketHandlerManager.broadcastPacketVision(c, ri, Channel.CHL_S2C);
        }

        public void NotifySetTarget(Obj_AI_Base attacker, Obj_AI_Base target)
        {
            var st = new SetTarget(attacker, target);
            _game.PacketHandlerManager.broadcastPacket(st, Channel.CHL_S2C);

            var st2 = new SetTarget2(attacker, target);
            _game.PacketHandlerManager.broadcastPacket(st2, Channel.CHL_S2C);
        }

        public void NotifyChampionDie(Obj_AI_Hero die, Obj_AI_Base killer, int goldFromKill)
        {
            var cd = new ChampionDie(die, killer, goldFromKill);
            _game.PacketHandlerManager.broadcastPacket(cd, Channel.CHL_S2C);

            NotifyChampionDeathTimer(die);
        }

        public void NotifyChampionDeathTimer(Obj_AI_Hero die)
        {
            var cdt = new ChampionDeathTimer(die);
            _game.PacketHandlerManager.broadcastPacket(cdt, Channel.CHL_S2C);
        }

        public void NotifyChampionRespawn(Obj_AI_Hero c)
        {
            var cr = new ChampionRespawn(c);
            _game.PacketHandlerManager.broadcastPacket(cr, Channel.CHL_S2C);
        }

        public void NotifyShowProjectile(Projectile p)
        {
            var sp = new ShowProjectile(p);
            _game.PacketHandlerManager.broadcastPacket(sp, Channel.CHL_S2C);
        }

        public void NotifyNpcDie(Obj_AI_Base die, Obj_AI_Base killer)
        {
            var nd = new NpcDie(die, killer);
            _game.PacketHandlerManager.broadcastPacket(nd, Channel.CHL_S2C);
        }

        public void NotifyAddGold(Obj_AI_Hero c, Obj_AI_Base died, float gold)
        {
            var ag = new AddGold(c, died, gold);
            _game.PacketHandlerManager.broadcastPacket(ag, Channel.CHL_S2C);
        }

        public void NotifyAddXP(Obj_AI_Hero champion, float experience)
        {
            var xp = new AddXP(champion, experience);
            _game.PacketHandlerManager.broadcastPacket(xp, Channel.CHL_S2C);
        }

        public void NotifyStopAutoAttack(Obj_AI_Base attacker)
        {
            var saa = new StopAutoAttack(attacker);
            _game.PacketHandlerManager.broadcastPacket(saa, Channel.CHL_S2C);
        }

        public void NotifyDebugMessage(string htmlDebugMessage)
        {
            var dm = new DebugMessage(htmlDebugMessage);
            _game.PacketHandlerManager.broadcastPacket(dm, Channel.CHL_S2C);
        }

        public void NotifyPauseGame(int seconds, bool showWindow)
        {
            var pg = new PauseGame(seconds, showWindow);
            _game.PacketHandlerManager.broadcastPacket(pg, Channel.CHL_S2C);
        }

        public void NotifyResumeGame(Obj_AI_Base unpauser, bool showWindow)
        {
            UnpauseGame upg;
            if (unpauser == null)
            {
                upg = new UnpauseGame(0, showWindow);
            }
            else
            {
                upg = new UnpauseGame(unpauser.NetworkID, showWindow);
            }
            _game.PacketHandlerManager.broadcastPacket(upg, Channel.CHL_S2C);
        }

        public void NotifySpawn(Obj_AI_Base u)
        {
            var m = u as Obj_AI_Minion;
            if (m != null)
                NotifyMinionSpawned(m, CustomConvert.GetEnemyTeam(m.Team));

            var c = u as Obj_AI_Hero;
            if (c != null)
                NotifyChampionSpawned(c, CustomConvert.GetEnemyTeam(c.Team));

            NotifySetHealth(u);
        }

        /*private void NotifyAzirTurretSpawned(AzirTurret azirTurret)
        {
            var spawnPacket = new SpawnAzirTurret(azirTurret);
            _game.PacketHandlerManager.broadcastPacketVision(azirTurret, spawnPacket, Channel.CHL_S2C);
        }

        private void NotifyPlaceableSpawned(Placeable placeable)
        {
            var spawnPacket = new SpawnPlaceable(placeable);
            _game.PacketHandlerManager.broadcastPacketVision(placeable, spawnPacket, Channel.CHL_S2C);
        }

        private void NotifyMonsterSpawned(Monster m)
        {
            var sp = new SpawnMonster(m);
            _game.PacketHandlerManager.broadcastPacketVision(m, sp, Channel.CHL_S2C);
        }*/

        public void NotifyLeaveVision(GameObject o, Team team)
        {
            var lv = new LeaveVision(o);
            _game.PacketHandlerManager.broadcastPacketTeam(team, lv, Channel.CHL_S2C);

            // Not exactly sure what this is yet
            var c = o as Obj_AI_Hero;
            if (o == null)
            {
                var deleteObj = new DeleteObjectFromVision(o);
                _game.PacketHandlerManager.broadcastPacketTeam(team, deleteObj, Channel.CHL_S2C);
            }
        }

        public void NotifyEnterVision(GameObject o, Team team)
        {
            var m = o as Obj_AI_Minion;
            if (m != null)
            {
                var eva = new EnterVisionAgain(m);
                _game.PacketHandlerManager.broadcastPacketTeam(team, eva, Channel.CHL_S2C);
                NotifySetHealth(m);
                return;
            }

            var c = o as Obj_AI_Hero;
            // TODO: Fix bug where enemy champion is not visible to user when vision is acquired until the enemy champion moves
            if (c != null)
            {
                var eva = new EnterVisionAgain(c);
                _game.PacketHandlerManager.broadcastPacketTeam(team, eva, Channel.CHL_S2C);
                NotifySetHealth(c);
            }
        }

        public void NotifyChampionSpawned(Obj_AI_Hero c, Team team)
        {
            var hs = new HeroSpawn2(c);
            _game.PacketHandlerManager.broadcastPacketTeam(team, hs, Channel.CHL_S2C);
        }

        public void NotifySetCooldown(Obj_AI_Hero c, byte slotId, float currentCd, float totalCd)
        {
            var cd = new SetCooldown(c.NetworkID, slotId, currentCd, totalCd);
            _game.PacketHandlerManager.broadcastPacket(cd, Channel.CHL_S2C);
        }

        public void NotifyGameTimer()
        {
            var gameTimer = new GameTimer(5 / 1000.0f);
            _game.PacketHandlerManager.broadcastPacket(gameTimer, Channel.CHL_S2C);
        }

        public void NotifyUnitAnnounceEvent(UnitAnnounces messageId, Obj_AI_Base target, GameObject killer = null, List<Obj_AI_Hero> assists = null)
        {
            var announce = new UnitAnnounce(messageId, target, killer, assists);
            _game.PacketHandlerManager.broadcastPacket(announce, Channel.CHL_S2C);
        }

        public void NotifyAnnounceEvent(Announces messageId, bool isMapSpecific)
        {
            var announce = new Announce(messageId, isMapSpecific ? 1 : 1);
            _game.PacketHandlerManager.broadcastPacket(announce, Channel.CHL_S2C);
        }

        public void NotifySpellAnimation(Obj_AI_Base u, string animation)
        {
            var sa = new SpellAnimation(u, animation);
            _game.PacketHandlerManager.broadcastPacketVision(u, sa, Channel.CHL_S2C);
        }

        public void NotifySetAnimation(Obj_AI_Base u, List<string> animationPairs)
        {
            var setAnimation = new SetAnimation(u, animationPairs);
            _game.PacketHandlerManager.broadcastPacketVision(u, setAnimation, Channel.CHL_S2C);
        }

        public void NotifyDash(Obj_AI_Base u,
                               Obj_AI_Base t,
                               float dashSpeed,
                               bool keepFacingLastDirection,
                               float leapHeight,
                               float followTargetMaxDistance,
                               float backDistance,
                               float travelTime)
        {
            var dash = new Dash(u,
                                t,
                                dashSpeed,
                                keepFacingLastDirection,
                                leapHeight,
                                followTargetMaxDistance,
                                backDistance,
                                travelTime);
            _game.PacketHandlerManager.broadcastPacketVision(u, dash, Channel.CHL_S2C);
        }
    }
}

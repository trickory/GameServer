using BlowFishCS;
using ENet;
using LeagueSandbox.GameServer.Core.Logic.PacketHandlers;
using LeagueSandbox.GameServer.Exceptions;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.Logic;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Content;
using LeagueSandbox.GameServer.Logic.Packets;
using LeagueSandbox.GameServer.Logic.Players;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Timer = System.Timers.Timer;

namespace LeagueSandbox.GameServer.Core.Logic
{
    public class Game
    {
        private Host _server;
        public BlowFish Blowfish { get; private set; }

        public bool IsRunning { get; private set; }

        public bool IsPaused { get; set; }
        private Timer _pauseTimer;
        public long PauseTimeLeft { get; private set; }
        private bool _autoResumeCheck;

        public int PlayersReady { get; private set; }
        
        public PacketNotifier PacketNotifier { get; private set; }
        public PacketHandlerManager PacketHandlerManager { get; private set; }
        public Config Config { get; protected set; }
        protected const int PEER_MTU = 996;
        protected const double REFRESH_RATE = 1000.0 / 30.0; // 30 fps
        private Logger _logger;
        // Object managers
        private ItemManager _itemManager;
        // Other managers
        private PlayerManager _playerManager;
        private NetworkIdManager _networkIdManager;
        private Stopwatch _lastMapDurationWatch;
        
        public delegate void Update(float sinceLastMapTime, EventArgs args);

        public event Update OnUpdate;

        public Game(
            ItemManager itemManager,
            NetworkIdManager networkIdManager,
            PlayerManager playerManager,
            Logger logger
        )
        {
            _itemManager = itemManager;
            _networkIdManager = networkIdManager;
            _playerManager = playerManager;
            _logger = logger;
        }

        public void Initialize(Address address, string blowfishKey, Config config)
        {
            _logger.LogCoreInfo("Loading Config.");
            Config = config;
            
            _server = new Host();
            _server.Create(address, 32, 32, 0, 0);

            var key = Convert.FromBase64String(blowfishKey);
            if (key.Length <= 0)
            {
                throw new InvalidKeyException("Invalid blowfish key supplied");
            }

            Blowfish = new BlowFish(key);
            PacketHandlerManager = new PacketHandlerManager(_logger, Blowfish, _server, _playerManager);
            
            PacketNotifier = new PacketNotifier(this, _playerManager, _networkIdManager);
            ApiFunctionManager.SetGame(this);
            IsRunning = false;

            foreach (var p in Config.Players)
            {
                _playerManager.AddPlayer(p);
            }

            _pauseTimer = new Timer
            {
                AutoReset = true,
                Enabled = false,
                Interval = 1000
            };
            _pauseTimer.Elapsed += (sender, args) => PauseTimeLeft--;
            PauseTimeLeft = 30 * 60; // 30 minutes

            _logger.LogCoreInfo("Game is ready.");
        }
        
        public void NetLoop()
        {
            var enetEvent = new Event();

            _lastMapDurationWatch = new Stopwatch();
            _lastMapDurationWatch.Start();
            using (PreciseTimer.SetResolution(1))
            {
                while (!Program.IsSetToExit)
                {
                    while (_server.Service(0, out enetEvent) > 0)
                    {
                        switch (enetEvent.Type)
                        {
                            case EventType.Connect:
                                // Set some defaults
                                enetEvent.Peer.Mtu = PEER_MTU;
                                enetEvent.Data = 0;
                                break;

                            case EventType.Receive:
                                PacketHandlerManager.handlePacket(enetEvent.Peer, enetEvent.Packet, (Channel)enetEvent.ChannelID);
                                // Clean up the packet now that we're done using it.
                                enetEvent.Packet.Dispose();
                                break;

                            case EventType.Disconnect:
                                HandleDisconnect(enetEvent.Peer);
                                break;
                        }
                    }

                    if (_lastMapDurationWatch.Elapsed.TotalMilliseconds + 1.0 > REFRESH_RATE)
                    {
                        double sinceLastMapTime = _lastMapDurationWatch.Elapsed.TotalMilliseconds;
                        _lastMapDurationWatch.Restart();
                        if (IsRunning)
                        {
                            OnUpdate((float)sinceLastMapTime, new EventArgs());
                        }
                    }
                    Thread.Sleep(1);
                }
            }
        }

        public void IncrementReadyPlayers()
        {
            PlayersReady++;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Pause()
        {
            if (PauseTimeLeft <= 0)
            {
                return;
            }
            IsPaused = true;
            PacketNotifier.NotifyPauseGame((int)PauseTimeLeft, true);
        }

        public void Unpause()
        {
            _lastMapDurationWatch.Start();
            IsPaused = false;
            _pauseTimer.Enabled = false;
        }

        private bool HandleDisconnect(Peer peer)
        {
            var peerinfo = _playerManager.GetPeerInfo(peer);
            if (peerinfo != null)
            {
                if (!peerinfo.IsDisconnected)
                {
                    PacketNotifier.NotifyUnitAnnounceEvent(UnitAnnounces.SummonerDisconnected, peerinfo.Champion);
                }
                peerinfo.IsDisconnected = true;
            }
            return true;
        }
    }

    public enum GameEventId
    {
        OnDelete,
        OnSpawn,
        OnDie,
        OnKill,
        OnChampionDie,
        OnChampionLevelUp,
        OnChampionKillPre,
        OnChampionKill,
        OnChampionKillPost,
        OnChampionSingleKill,
        OnChampionDoubleKill,
        OnChampionTripleKill,
        OnChampionQuadraKill,
        OnChampionPentaKill,
        OnChampionUnrealKill,
        OnFirstBlood,
        OnDamageTaken,
        OnDamageGiven,
        OnSpellCast1,
        OnSpellCast2,
        OnSpellCast3,
        OnSpellCast4,
        OnSpellAvatarCast1,
        OnSpellAvatarCast2,
        OnGoldSpent,
        OnGoldEarned,
        OnItemConsumeablePurchased,
        OnCriticalStrike,
        OnAce,
        OnReincarnate,
        OnChangeChampion,
        OnDampenerKill,
        OnDampenerDie,
        OnDampenerRespawnSoon,
        OnDampenerRespawn,
        OnDampenerDamage,
        OnTurretKill,
        OnTurretDie,
        OnTurretDamage,
        OnMinionKill,
        OnMinionDenied,
        OnNeutralMinionKill,
        OnSuperMonsterKill,
        OnAcquireRedBuffFromNeutral,
        OnAcquireBlueBuffFromNeutral,
        OnHQKill,
        OnHQDie,
        OnHeal,
        OnCastHeal,
        OnBuff,
        OnCrowdControlDealt,
        OnKillingSpree,
        OnKillingSpreeSet1,
        OnKillingSpreeSet2,
        OnKillingSpreeSet3,
        OnKillingSpreeSet4,
        OnKillingSpreeSet5,
        OnKillingSpreeSet6,
        OnKilledUnitOnKillingSpree,
        OnKilledUnitOnKillingSpreeSet1,
        OnKilledUnitOnKillingSpreeSet2,
        OnKilledUnitOnKillingSpreeSet3,
        OnKilledUnitOnKillingSpreeSet4,
        OnKilledUnitOnKillingSpreeSet5,
        OnKilledUnitOnKillingSpreeSet6,
        OnKilledUnitOnKillingSpreeDoubleKill,
        OnKilledUnitOnKillingSpreeTripleKill,
        OnKilledUnitOnKillingSpreeQuadraKill,
        OnKilledUnitOnKillingSpreePentaKill,
        OnKilledUnitOnKillingSpreeUnrealKill,
        OnDeathAssist,
        OnQuit,
        OnLeave,
        OnReconnect,
        OnGameStart,
        OnAssistingSpreeSet1,
        OnAssistingSpreeSet2,
        OnChampionTripleAssist,
        OnChampionPentaAssist,
        OnPing,
        OnPingPlayer,
        OnPingBuilding,
        OnPingOther,
        OnEndGame,
        OnSpellLevelup1,
        OnSpellLevelup2,
        OnSpellLevelup3,
        OnSpellLevelup4,
        OnSpellEvolve1,
        OnSpellEvolve2,
        OnSpellEvolve3,
        OnSpellEvolve4,
        OnItemPurchased,
        OnItemSold,
        OnItemRemoved,
        OnItemUndo,
        OnItemCallout,
        OnItemGroupChange,
        OnItemChange,
        OnUndoEnabledChange,
        OnShopItemSubstitutionChange,
        OnShopMenuOpen,
        OnShopMenuClose,
        OnSurrenderVoteStart,
        OnSurrenderVote,
        OnSurrenderVoteAlready,
        OnSurrenderFailedVotes,
        OnSurrenderTooEarly,
        OnSurrenderAgreed,
        OnSurrenderSpam,
        OnSurrenderEarlyAllowed,
        OnEqualizeVoteStart,
        OnEqualizeVote,
        OnEqualizeVoteAlready,
        OnEqualizeFailedVotes,
        OnEqualizeTooEarly,
        OnEqualizeNotEnoughGold,
        OnEqualizeNotEnoughLevels,
        OnEqualizeAgreed,
        OnEqualizeSpam,
        OnPause,
        OnPauseResume,
        OnMinionsSpawn,
        OnStartGameMessage1,
        OnStartGameMessage2,
        OnStartGameMessage3,
        OnStartGameMessage4,
        OnStartGameMessage5,
        OnAlert,
        OnScoreboardOpen,
        OnAudioEventFinished,
        OnNexusCrystalStart,
        OnCapturePointNeutralized_A,
        OnCapturePointNeutralized_B,
        OnCapturePointNeutralized_C,
        OnCapturePointNeutralized_D,
        OnCapturePointNeutralized_E,
        OnCapturePointCaptured_A,
        OnCapturePointCaptured_B,
        OnCapturePointCaptured_C,
        OnCapturePointCaptured_D,
        OnCapturePointCaptured_E,
        OnCapturePointFiveCap,
        OnVictoryPointThreshold1,
        OnVictoryPointThreshold2,
        OnVictoryPointThreshold3,
        OnVictoryPointThreshold4,
        OnMinionKillVictoryThreshold1,
        OnMinionKillVictoryThreshold2,
        OnTurretKillVictoryThreshold1,
        OnTurretKillVictoryThreshold2,
        OnReplayFastForwardStart,
        OnReplayFastForwardEnd,
        OnReplayOnKeyframeFinished,
        OnReplayDestroyAllObjects,
        OnKillDragon,
        OnKillDragon_Spectator,
        OnKillDragonSteal,
        OnKillWorm,
        OnKillWorm_Spectator,
        OnKillWormSteal,
        OnKillSpiderBoss,
        OnKillSpiderBoss_Spectator,
        OnCaptureAltar,
        OnCaptureAltar_Spectator,
        OnPlaceWard,
        OnKillWard,
        OnMinionAscended,
        OnChampionAscended,
        OnClearAscended,
        OnGameStatEvent,
        OnRelativeTeamColorChange,
    }
}

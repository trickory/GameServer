using System.Collections.Generic;
using LeagueSandbox.GameServer.Enums;
using LeagueSandbox.GameServer.Events;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    public class AIHeroClient : Obj_AI_Base
    {
        public int WardsPlaced { get; private set; }
        public int WardsKilled { get; private set; }
        public int UnrealKills { get; private set; }
        public int TurretsKilled { get; private set; }
        public int TripleKills { get; private set; }
        public float TotalTimeCrowdControlDealt { get; private set; }
        public float TotalHeal { get; private set; }
        public int SuperMonsterKilled { get; private set; }
        public int QuadraKills { get; private set; }
        public float PhysicalDamageTaken { get; private set; }
        public float PhysicalDamageDealtPlayer { get; private set; }
        public int PentaKills { get; private set; }
        public float ObjectivePlayerScore { get; private set; }
        public int NodesNeutralized { get; private set; }
        public int NodesCaptured { get; private set; }
        public int MinionsKilled { get; private set; }
        public float MagicDamageTaken { get; private set; }
        public float MagicDamageDealtPlayer { get; private set; }
        public float LongestTimeSpentLiving { get; private set; }
        public int LargestKillingSpree { get; private set; }
        public float LargestCriticalStrike { get; private set; }
        public int HQKilled { get; private set; } // wut?
        public int DoubleKills { get; private set; }
        public int Deaths { get; private set; }
        public int CombatPlayerScore { get; private set; }
        public int ChampionsKilled { get; private set; }
        public int BarracksKilled { get; private set; }
        public int Assists { get; private set; }
        public Experience Experience { get; private set; }
        public Mastery[] Masteries { get; private set; }
        public new float GoldTotal { get; private set; }
        public new float Gold { get; private set; }
        public float NeutralMinionsKilled { get; private set; }
        public string ChampionName { get; private set; }
        public Champion Hero { get; private set; }
        public List<int> Runes { get; private set; }
        public int SpellTrainingPoints { get; private set; }
        public int Level { get; private set; }
        public static event AIHeroClientSpawn OnSpawn;
        public static event AIHeroClientDeath OnDeath;

    }
}

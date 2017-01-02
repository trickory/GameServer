using LeagueSandbox.GameServer.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSandbox.GameServer
{
    public class GameObject
    {
        public uint NetworkID { get; set; }
        public short Index { get; set; }
        public string Name{ get; set; }
        public Vector3 Position { get; set; }
        public Team Team { get; set; }
        public ObjectType Type { get; set; }
        public bool IsDead { get; set; }
        public float VisionRadius { get; set; }
        public int CollisionRadius { get; set; }
        public GameObject Owner { get; set; }
        //TODO: Stats

        public delegate void Create(GameObject sender, EventArgs args);
        public delegate void Delete(GameObject sender, EventArgs args);

        public event Create OnCreate;
        public event Delete OnDelete;

        public GameObject()
        {
        }

        public GameObject(short index, uint networkId)
        {
            this.Index = index;
            this.NetworkID = networkId;
        }
    }
    
    [Flags]
    public enum CharacterState
    {
        CanAttack = 1,
        CanCast = 2,
        CanMove = 4,
        Immovable = 8,
        IsStealth = 16,
        RevealSpecificUnit = 32,
        Taunted = 64,
        Feared = 128,
        Fleeing = 256,
        Surpressed = 512,
        Asleep = 1024,
        NearSight = 2048,
        Ghosted = 4096,
        GhostProof = 8192,
        Charmed = 16384,
        NoRender = 32768,
        DodgePiercing = 131072,
        DisableAmbientGold = 262144,
        DisableAmbientXP = 524288,
        ForceRenderParticles = 65536,
    }

    public enum CombatType
    {
        Melee = 1,
        Ranged = 2,
    }

    public enum ObjectType
    {
        NeutralMinionCamp,
        obj_AI_Base,
        FollowerObject,
        FollowerObjectWithLerpMovement,
        AIHeroClient,
        obj_AI_Marker,
        obj_AI_Minion,
        LevelPropAI,
        obj_AI_Turret,
        obj_GeneralParticleEmitter,
        MissileClient,
        DrawFX,
        UnrevealedTarget,
        obj_LampBulb,
        obj_Barracks,
        obj_BarracksDampener,
        obj_AnimatedBuilding,
        obj_Building,
        obj_Levelsizer,
        obj_NavPoint,
        obj_SpawnPoint,
        obj_Lake,
        obj_HQ,
        obj_InfoPoint,
        LevelPropGameObject,
        LevelPropSpawnerPoint,
        obj_Shop,
        obj_Turret,
        GrassObject,
        obj_Ward,
        GameObject,
        Unknown,
    }

    public enum Team
    {
        Unknown = 0,
        Order = 100,
        Chaos = 200,
        Neutral = 300
    }
}

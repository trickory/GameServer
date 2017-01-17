using System;
using System.Collections.Generic;   

namespace LeagueSandbox.GameServer.Events.Args
{
    class GameObjectPlayAnimationEventArgs : EventArgs
    {
        private Dictionary<int, string> _animationsHashDictionary = new Dictionary<int, string>
        {
            { 0x2ACD4ECA, "Run" },
            { -0x3CFE306D, "Idle" },
            { -0x3DE1CBBA, "Joke" },
            { -0x496A3342, "Laugh" },
            { -0x43637B9D, "Taunt" },
            { -0x7EAD764, "Dance" },
            { 0x56B1E924, "Attack1" },
            { 0x59B1EDDD, "Attack2" },
            { 0x5A81BDB0, "Recall" },
            { 0x3A224D98, "Spawn" },
            { -0x4D09C798, "Spell1" },
            { -0x3D65C9D5, "Spell1a" },
            { -0x3C65C842, "Spell1b" },
            { -0x3B65C6AF, "Spell1c" },
            { -0x4A09C2DF, "Spell2" },
            { -0x4D5D9440, "Spell2a" },
            { -0x4A5D8F87, "Spell2b" },
            { -0x4B5D911A, "Spell2c" },
            { -0x4B09C472, "Spell3" },
            { -0x315FA6C3, "Spell3a" },
            { -0x345FAB7C, "Spell3b" },
            { -0x335FA9E9, "Spell3c" },
            { -0x4809BFB9, "Spell4" },
            { -0x4159042E, "Spell4a" },
            { -0x425905C1, "Spell4b" },
            { -0x43590754, "Spell4c" },
            { -0x42D742B3, "Death" },
            { -0x42A6F624, "AzirSoldierSpawn" },
            { -0x714BF429, "AzirSoldierIdle" },
            { 0x45F47B73, "AzirSoldierActive" },
            { 0x777270D5, "AzirSoldierAutoAttack1" },
            { 0x74726C1C, "AzirSoldierAutoAttack2" },
            { -0x69ABFA73, "Crit" },
            { -0x66506ADA, "TurretFirstBreak" },
            { 0x233951B2, "TurretSecondBreak" },
            { -0x460ABFB2, "TurretExplosion" },
            { 0x34EDFA7B, "Spell4_Windup" },
            { -0x5672536A, "Spell4_Loop" },
            { 0x4B07AE2E, "Spell4_Winddown" },
            { -0x622623FA, "Idle1" },
            { -0x10FE50A8, "Inactive" },
            { 0x1F4D30B1, "Reactivate" },
            { -0x6C9515F1, "Run_Exit" },
            { -0x4909C14C, "Spell5" },
            { -0x6A5D5EB1, "Spell3withReload" }
        };
        public bool Process { get; private set; }

        public string Animation
        {
            get
            {
                if (_animationsHashDictionary.ContainsKey(AnimationHash))
                {
                    return _animationsHashDictionary[AnimationHash];
                }
                return "Unknown";
            }
        }

        public int AnimationHash { get; private set; }

        public GameObjectPlayAnimationEventArgs(int animationHash)
        {
            AnimationHash = animationHash;
        }
    }
}

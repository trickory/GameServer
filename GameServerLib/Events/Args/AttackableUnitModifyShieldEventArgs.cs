using System;

namespace LeagueSandbox.GameServer.Events.Args
{
    class AttackableUnitModifyShieldEventArgs : EventArgs
    {
        public float AttackShield { get; private set; }
        public float MagicShield { get; private set; }

        public AttackableUnitDamageEventArgs(float magicShield, float attackShield)
        {
            MagicShield = magicShield;
            AttackShield = attackShield;
        }
    }
}

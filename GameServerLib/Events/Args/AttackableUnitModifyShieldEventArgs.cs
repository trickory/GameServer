using System;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class AttackableUnitModifyShieldEventArgs : EventArgs
    {
        public float AttackShield { get; private set; }
        public float MagicShield { get; private set; }

        public AttackableUnitModifyShieldEventArgs(float magicShield, float attackShield)
        {
            MagicShield = magicShield;
            AttackShield = attackShield;
        }
    }
}

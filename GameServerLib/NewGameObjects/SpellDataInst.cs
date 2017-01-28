using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Enums;
using SpellState = LeagueSandbox.GameServer.GameObjects.SpellState;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    public class SpellDataInst
    {
        public SpellData SpellData { get; private set; }
        public SpellState State { get; private set; }
        public SpellSlot Slot { get; private set; }
        public bool IsOnCooldown { get; private set; }
        public bool IsLearned { get; private set; }
        public bool IsReady { get; private set; }
        public string Name { get; private set; }
        public bool IsUpgradeable { get; private set; }
        public int Level { get; private set; }
        public int ToggleState { get; private set; }
        public float CooldownExpires { get; private set; }
        public float Cooldown { get; private set; }
        public float AmmoRechargeStart { get; private set; }
        public int Ammo { get; private set; }
    }
}

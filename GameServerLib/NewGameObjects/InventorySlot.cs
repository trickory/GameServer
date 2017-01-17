namespace LeagueSandbox.GameServer.NewGameObjects
{
    class InventorySlot
    {
        public bool IsWard { get; private set; }
        public SpellSlot SpellSlot { get; private set; }
        public string Description { get; private set; }
        public string ToolTip { get; private set; }
        public string DisplayName { get; private set; }
        public string Name { get; private set; }
        public int Slot { get; private set; }
        public int Price { get; private set; }
        public ItemId Id { get; private set; }
        public int Stacks { get; private set; }
        public int Charges { get; private set; }

    }
}

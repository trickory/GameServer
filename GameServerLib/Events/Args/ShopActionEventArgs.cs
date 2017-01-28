using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class ShopActionEventArgs : EventArgs
    {
        public int[] RecipeItemIds { get; private set; }
        public string Name { get; private set; }
        public int MaxStacks { get; private set; }
        public int Price { get; private set; }
        public int Id { get; private set; }

        public ShopActionEventArgs(AIHeroClient sender, int managedItemId, int price, int maxStacks, string name, int[] recipeItemIds)
        {
            Id = managedItemId;
            Price = price;
            MaxStacks = maxStacks;
            Name = name;
            RecipeItemIds = recipeItemIds;
        }
    }
}

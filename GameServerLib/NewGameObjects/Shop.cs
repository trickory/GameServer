using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Events;

namespace LeagueSandbox.GameServer.NewGameObjects
{
    class Shop
    {
        public bool CanShop(AIHeroClient hero)
        {
            return default(bool); // todo
        }

        public static event ShopSellItem OnSellItem;
        public static event ShopBuyItem OnBuyItem;
    }
}

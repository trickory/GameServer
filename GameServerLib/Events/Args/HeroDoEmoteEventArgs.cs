using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Enums;
using LeagueSandbox.GameServer.NewGameObjects;

namespace LeagueSandbox.GameServer.Events.Args
{
    public class HeroDoEmoteEventArgs : EventArgs
    {
        public Emote EmoteId { get; private set; }

        public HeroDoEmoteEventArgs(AIHeroClient sender, short emoteId)
        {
            EmoteId = (Emote)emoteId;
        }
    }
}

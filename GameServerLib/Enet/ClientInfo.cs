using ENet;
using LeagueSandbox.GameServer.Logic.GameObjects;

namespace LeagueSandbox.GameServer.Enet
{
    public class ClientInfo
    {
        public long UserId { get; private set; }
        public bool IsMatchingVersion { get; set; }
        public bool IsDisconnected { get; set; }
        public int SkinNo { get; private set; }
        public string[] SummonerSkills { get; private set; }
        public string Name { get; private set; }
        public string Rank { get; private set; }
        public short Ribbon { get; private set; }
        public int Icon { get; private set; }
        public Team Team { get; private set; }

        private Champion _champion;
        public Champion Champion
        {
            get { return _champion; }
            set
            {
                _champion = value;
                _champion.Skin = SkinNo;
            }
        }

        public Peer Peer { get; set; }

        public ClientInfo(string rank,
            Team team,
            short ribbon,
            int icon,
            int skinNo,
            string name,
            string[] summonerSkills,
            long userId)
        {
            Rank = rank;
            Team = team;
            Ribbon = ribbon;
            Icon = icon;
            SkinNo = skinNo;
            IsMatchingVersion = true;
            IsDisconnected = false;
            Name = name;
            SummonerSkills = summonerSkills;
            UserId = userId;
        }
    }
    public enum Team
    {
        Order = 0x64,
        Chaos = 0xC8,
        Neutral = 0x12C
    }
}

namespace LeagueSandbox.GameServer.GameObjects
{
    public class Particle
    {
        public CastInfo CastInfo { get; private set; }
        public string Name { get; private set; }
        public string BoneName { get; private set; }
        public float Size { get; private set; }
        public uint NetworkID { get; private set; }

        public Particle(CastInfo castInfo, string particleName, float size = 1.0f, string boneName = "", uint netId = 0)
        {
            CastInfo = castInfo;
            Name = particleName;
            BoneName = boneName;
            NetworkID = netId;
            Size = size;
        }
    }
}

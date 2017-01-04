using LeagueSandbox.GameServer.Enet;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Stats;

namespace LeagueSandbox.GameServer.GameObjects
{
    public class Nexus : Unit
    {
        public Nexus(
            string model,
            Team team,
            int collisionRadius = 40,
            float x = 0,
            float y = 0,
            int visionRadius = 0,
            uint netId = 0
        ) : base(model, new BuildingStats(), collisionRadius, x, y, visionRadius, netId)
        {
            stats.CurrentHealth = 5500;
            stats.HealthPoints.BaseValue = 5500;

            SetTeam(team);
        }

        public override void die(Unit killer)
        {
            _game.Stop();
            _game.PacketNotifier.NotifyGameEnd(this);
        }

        public override void RefreshWaypoints()
        {

        }

        public override void setToRemove()
        {

        }

        public override float GetMoveSpeed()
        {
            return 0;
        }
    }
}

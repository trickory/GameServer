using System;
using System.Threading;
using System.Threading.Tasks;
using ENet;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Enet;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Packets;
using LeagueSandbox.GameServer.Packets.PacketHandlers;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class RainbowCommand : ChatCommand
    {
        public RainbowCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        private static Champion _me;
        private static bool _run;
        private static float _a = 0.5f;
        private static float _speed = 0.25f;
        private static int _delay = 250;

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');

            _me = playerManager.GetPeerInfo(peer).Champion;

            if (split.Length > 1)
            {
                float.TryParse(split[1], out _a);
            }

            if (split.Length > 2)
            {
                float.TryParse(split[2], out _speed);
                _delay = (int)(_speed * 1000);
            }

            _run = !_run;
            if (_run)
            {
                Task.Run(() => TaskRainbow());
            }
        }

        public void TaskRainbow()
        {
            while (_run)
            {
                var rainbow = new byte[4];
                new Random().NextBytes(rainbow);
                Thread.Sleep(_delay);
                BroadcastTint(_me.Team, false, 0.0f, 0, 0, 0, 1f);
                BroadcastTint(_me.Team, true, _speed, rainbow[1], rainbow[2], rainbow[3], _a);
            }
            Thread.Sleep(_delay);
            BroadcastTint(_me.Team, false, 0.0f, 0, 0, 0, 1f);
        }

        public void BroadcastTint(Team team, bool enable, float speed, byte r, byte g, byte b, float a)
        {
            var game = Program.ResolveDependency<Game>();
            var tint = new SetScreenTint(team, enable, speed, r, g, b, a);
            game.PacketHandlerManager.broadcastPacket(tint, Channel.CHL_S2C);
        }
    }
}

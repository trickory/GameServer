using System;
using System.Collections.Generic;
using System.Numerics;
using ENet;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Enet;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class SpawnCommand : ChatCommand
    {
        public SpawnCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var game = Program.ResolveDependency<Game>();
            var playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.ToLower().Split(' ');

            if (split.Length < 2)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
            }
            else if (split[1] == "minions")
            {
                var champion = playerManager.GetPeerInfo(peer).Champion;
                var random = new Random();

                var caster = new Minion(MinionSpawnType.MINION_TYPE_CASTER, MinionSpawnPosition.SPAWN_RED_BOT);
                var cannon = new Minion(MinionSpawnType.MINION_TYPE_CANNON, MinionSpawnPosition.SPAWN_RED_BOT);
                var melee = new Minion(MinionSpawnType.MINION_TYPE_MELEE, MinionSpawnPosition.SPAWN_RED_BOT);
                var super = new Minion(MinionSpawnType.MINION_TYPE_SUPER, MinionSpawnPosition.SPAWN_RED_BOT);

                const int x = 400;
                caster.setPosition(champion.X + random.Next(-x, x), champion.Y + random.Next(-x, x));
                cannon.setPosition(champion.X + random.Next(-x, x), champion.Y + random.Next(-x, x));
                melee.setPosition(champion.X + random.Next(-x, x), champion.Y + random.Next(-x, x));
                super.setPosition(champion.X + random.Next(-x, x), champion.Y + random.Next(-x, x));

                caster.PauseAI(true);
                cannon.PauseAI(true);
                melee.PauseAI(true);
                super.PauseAI(true);

                caster.SetWaypoints(new List<Vector2> { new Vector2(caster.X, caster.Y), new Vector2(caster.X, caster.Y) });
                cannon.SetWaypoints(new List<Vector2> { new Vector2(cannon.X, cannon.Y), new Vector2(cannon.X, cannon.Y) });
                melee.SetWaypoints(new List<Vector2> { new Vector2(melee.X, melee.Y), new Vector2(melee.X, melee.Y) });
                super.SetWaypoints(new List<Vector2> { new Vector2(super.X, super.Y), new Vector2(super.X, super.Y) });

                caster.SetVisibleByTeam(Team.Order, true);
                cannon.SetVisibleByTeam(Team.Order, true);
                melee.SetVisibleByTeam(Team.Order, true);
                super.SetVisibleByTeam(Team.Order, true);

                game.Map.AddObject(caster);
                game.Map.AddObject(cannon);
                game.Map.AddObject(melee);
                game.Map.AddObject(super);
            }
        }
    }
}

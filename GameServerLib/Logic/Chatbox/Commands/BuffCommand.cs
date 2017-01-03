using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENet;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Players;

namespace LeagueSandbox.GameServer.Logic.Chatbox.Commands
{
    class BuffCommand : ChatCommand
    {
        public BuffCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner)
        {
        }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            Game _game = Program.ResolveDependency<Game>();
            PlayerManager _playerManager = Program.ResolveDependency<PlayerManager>();

            var split = arguments.Split(' ');
            byte slot, type;
            float duration;
            int stacks;
            if (split.Length >= 6)
            {
                if (byte.TryParse(split[1], out slot) && byte.TryParse(split[2], out type) &&
                    float.TryParse(split[4], out duration) && int.TryParse(split[5], out stacks))
                {
                    Buff _b = new Buff(_game, slot, type, split[3], duration, stacks, _playerManager.GetPeerInfo(peer).Champion);
                    _playerManager.GetPeerInfo(peer).Champion.AddBuff(_b);
                    _game.PacketNotifier.NotifyAddBuff(_b);
                }
                
            }
        }
    }
}

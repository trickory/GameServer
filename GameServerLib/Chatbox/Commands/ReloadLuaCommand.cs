using ENet;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    public class ReloadLuaCommand : ChatCommand
    {
        public ReloadLuaCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var playerManager = Program.ResolveDependency<PlayerManager>();
            var champ = playerManager.GetPeerInfo(peer).Champion;
            foreach (var spell in champ.Spells.Values)
            {
                spell.ReloadLua();
            }
            champ.LoadLua();
            _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.Info, "Successfully reloaded luas for your champion!");
        }
    }
}

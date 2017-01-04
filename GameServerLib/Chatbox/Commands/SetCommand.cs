using ENet;

namespace LeagueSandbox.GameServer.Chatbox.Commands
{
    class SetCommand : ChatCommand
    {
        public SetCommand(string command, string syntax, ChatCommandManager owner) : base(command, syntax, owner) { }

        public override void Execute(Peer peer, bool hasReceivedArguments, string arguments = "")
        {
            var split = arguments.ToLower().Split(' ');
            if (split.Length < 4)
            {
                _owner.SendDebugMsgFormatted(ChatCommandManager.DebugMsgType.SyntaxError);
                ShowSyntax();
                return;
            }
            int blockNo, fieldNo;
            float value;
            if (int.TryParse(split[1], out blockNo))
            {
                if (int.TryParse(split[2], out fieldNo))
                {
                    if (float.TryParse(split[3], out value))
                    {
                        //game.GetPeerInfo(peer).Champion.GetStats().setStat((MasterMask)blockNo, (FieldMask)fieldNo, value);
                    }
                }
            }
        }
    }
}

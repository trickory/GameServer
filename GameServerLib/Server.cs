using System;
using ENet;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic;
using LeagueSandbox.GameServer.GameObjects;
using Ninject;
using LeagueSandbox.GameServer.Logic.Packets;
using LeagueSandbox.GameServer.Core.Logic.PacketHandlers;
using LeagueSandbox.GameServer.Logic.Players;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.Scripting.Lua;

namespace LeagueSandbox.GameServer
{
    public class Server : IDisposable
    {
        private string BLOWFISH_KEY = "17BLOhi6KZsTtldTsizvHg==";
        private uint SERVER_HOST = Address.IPv4HostAny;
        private ushort SERVER_PORT = Program.ServerPort;
        private string SERVER_VERSION = "0.2.0";
        private Logger _logger;
        private ServerContext _serverContext;
        private Game _game;
        private Config _config;
        private IScriptEngine _scriptEngine;
        private static StandardKernel _kernel;

        public Server(Logger logger, ServerContext serverContext, Game game)
        {
            _logger = logger;
            _serverContext = serverContext;
            _game = game;
            _config = Config.LoadFromJson(Program.ConfigJson);
        }

        public void Start()
        {
            _logger.LogCoreInfo($"Yorick {SERVER_VERSION}");
            _logger.LogCoreInfo("Game started on port: {0}", SERVER_PORT);
            _game.Initialize(new Address(SERVER_HOST, SERVER_PORT), BLOWFISH_KEY, _config);
            _scriptEngine = new LuaScriptEngine();
            _scriptEngine.Load("whatever.lua");
            _scriptEngine.RunFunction("onServerStart");
            // Handle here every shit.
            _game.PacketHandlerManager.OnHandleKeyCheck += PacketHandlerManager_OnHandleKeyCheck;
            _game.PacketHandlerManager.OnHandleQueryStatus += PacketHandlerManager_OnHandleQueryStatus; ;
            _game.PacketHandlerManager.OnHandleLoadPing += PacketHandlerManager_OnHandleLoadPing;

            _game.NetLoop();
        }

        private void PacketHandlerManager_OnHandleQueryStatus(Peer peer, HandlePacketArgs args)
        {
            var response = new QueryStatus();
            _game.PacketHandlerManager.sendPacket(peer, response, Channel.CHL_S2C);
        }

        private void PacketHandlerManager_OnHandleLoadPing(Peer peer, HandlePacketArgs args)
        {
            var _playerManager = Program.ResolveDependency<PlayerManager>();
            var loadInfo = new PingLoadInfo(args.Data);
            var peerInfo = _playerManager.GetPeerInfo(peer);
            if (peerInfo == null)
                return;
            var response = new PingLoadInfo(loadInfo, peerInfo.UserId);

            //Logging->writeLine("loaded: %f, ping: %f, %f", loadInfo->loaded, loadInfo->ping, loadInfo->f3);
            _game.PacketHandlerManager.broadcastPacket(response, Channel.CHL_LOW_PRIORITY, PacketFlags.None);
        }

        private void PacketHandlerManager_OnHandleKeyCheck(Peer peer, HandlePacketArgs args)
        {
            var _playerManager = Program.ResolveDependency<PlayerManager>();
            var keyCheck = new KeyCheck(args.Data);
            var userId = _game.Blowfish.Decrypt(keyCheck.checkId);

            if (userId != keyCheck.userId)
                return;

            var playerNo = 0;

            foreach (var p in _playerManager.GetPlayers())
            {
                var player = p.Item2;
                if (player.UserId == userId)
                {
                    if (player.Peer != null)
                    {
                        if (!player.IsDisconnected)
                        {
                            _logger.LogCoreWarning("Ignoring new player " + userId + ", already connected!");
                            return;
                        }
                    }

                    //TODO: add at least port or smth
                    p.Item1 = peer.Address.port;
                    player.Peer = peer;
                    var response = new KeyCheck(keyCheck.userId, playerNo);
                    bool bRet = _game.PacketHandlerManager.sendPacket(peer, response, Channel.CHL_HANDSHAKE);
                    //handleGameNumber(player, peer, _game);//Send 0x91 Packet?
                    return;
                }
                ++playerNo;
            }
        }

        public void Dispose()
        {
            PathNode.DestroyTable();
        }
    }
}

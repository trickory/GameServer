using LeagueSandbox.GameServer.Chatbox;
using LeagueSandbox.GameServer.Content;
using LeagueSandbox.GameServer.Core.Logic.RAF;
using LeagueSandbox.GameServer.Scripting;
using LeagueSandbox.GameServer.Packets;
using LeagueSandbox.GameServer.Players;
using LeagueSandbox.GameServer.Scripting.Lua;
using Ninject.Modules;

namespace LeagueSandbox.GameServer
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            // Singletons - Only one instance of these objects will ever be created.
            Bind<Server>().To<Server>().InSingletonScope();

            Bind<Logger>().To<Logger>().InSingletonScope();
            Bind<ServerContext>().To<ServerContext>().InSingletonScope();
            Bind<RAFManager>().To<RAFManager>().InSingletonScope();
            Bind<Game>().To<Game>().InSingletonScope();

            Bind<ItemManager>().To<ItemManager>().InSingletonScope();
            Bind<ChatCommandManager>().To<ChatCommandManager>().InSingletonScope();
            Bind<PlayerManager>().To<PlayerManager>().InSingletonScope();
            Bind<NetworkIdManager>().To<NetworkIdManager>().InSingletonScope();

            // Other bindings
            Bind<IScriptEngine>().To<LuaScriptEngine>();
        }
    }
}
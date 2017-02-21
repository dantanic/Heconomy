
/*
    ooooo   ooooo                     .o8        .ooooo.   
    `888'   `888'                    "888       888' `Y88. 
     888     888   .ooooo.  oooo d8b  888oooo.  888    888 
     888ooooo888  d88' `88b `888""8P  d88' `88b  `Vbood888 
     888     888  888ooo888  888      888   888       888' 
     888     888  888    .o  888      888   888     .88P'  
    o888o   o888o `Y8bod8P' d888b     `Y8bod8P'   .oP'     

    Directed by Herb9.
*/

using HeconomyAPI.Command;
using HeconomyAPI.Handler;

using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;

using System.IO;

namespace HeconomyAPI.Assist
{

    [Plugin(PluginName = "HeconomyAPI", Description = "An advanced economy plugin for MiNET.", PluginVersion = "1.0", Author = "Herb9")]
    public class Loader : Plugin
    {

        private HeconomyAPI Plugin { get; set; }

        private Inspector Inspector { get; set; }

        private Config Config { get; set; }

        protected override void OnEnable()
        {
            Plugin = HeconomyAPI.GetAPI();

            Inspector = new Inspector();

            Config = new Config();

            Context.Server.PlayerFactory.PlayerCreated += (sender, args) =>
            {
                Player player = args.Player;

                player.PlayerJoin += new PlayerJoin(Plugin).handle;
            };

            RegisterCommands();

            @Directory.CreateDirectory(Plugin.GetPluginFolder());
            @Directory.CreateDirectory(Plugin.GetPluginFolder() + @"\players");
        }

        private void RegisterCommands()
        {
            Context.PluginManager.LoadCommands(new Money(Plugin));
            Context.PluginManager.LoadCommands(new Pay(Plugin));
        }
    }
}

/*
    888    888                  888       .d8888b.  
    888    888                  888      d88P  Y88b 
    888    888                  888      888    888 
    8888888888  .d88b.  888d888 88888b.  Y88b. d888 
    888    888 d8P  Y8b 888P"   888 "88b  "Y888P888 
    888    888 88888888 888     888  888        888 
    888    888 Y8b.     888     888 d88P Y88b  d88P 
    888    888  "Y8888  888     88888P"   "Y8888P"   

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

        protected override void OnEnable()
        {
            Plugin = HeconomyAPI.GetAPI();

            Inspector = new Inspector();

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


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

using Heconomy.Command;

using MiNET.Plugins;
using MiNET.Plugins.Attributes;

namespace Heconomy
{

    [Plugin(
        PluginName = "Heconomy",
        Description = "An advanced economy plugin for MiNET.",
        PluginVersion = "1.0",
        Author = "Herb9"
        )]
    public class PluginLoader : Plugin
    {

        private string Prefix;

        protected override void OnEnable()
        {
            Prefix = Heconomy.Prefix;

            CheckUpdate();

            RegisterCommands();
        }

        private void CheckUpdate()
        {

        }

        private void RegisterCommands()
        {
            Heconomy plugin = Heconomy.GetAPI();

            Context.PluginManager.LoadCommands(new Money(plugin));
            
        }
    }
}

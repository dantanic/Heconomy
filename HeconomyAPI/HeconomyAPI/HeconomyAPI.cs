
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

using HeconomyAPI.Commands;
using HeconomyAPI.Utils;

using MiNET;
using MiNET.Plugins;

namespace HeconomyAPI
{

    public class HeconomyAPI : Plugin
    {

        public const string Prefix = "\x5b\x48\x65\x63\x6f\x6e\x6f\x6d\x79\x5d";

        private Updater Updater;

        protected override void OnEnable()
        {
            Updater = new Updater() { Version = 1.0f };

            RegisterCommands();
        }

        private void RegisterCommands()
        {
            var plugin = new HeconomyAPI();

            Context.PluginManager.LoadCommands(new Money(plugin));
            Context.PluginManager.LoadCommands(new Pay(plugin));
        }

        public static HeconomyAPI GetAPI()
        {
            return new HeconomyAPI();
        }

        public void IsRegisteredPlayer()
        {

        }

        public void RegisterPlayer()
        {

        }

        public void GetMoneySymbol()
        {

        }

        public void GetDefaultMoney()
        {

        }

        public void GetMoney()
        {

        }

        public void SetMoney()
        {

        }

        public void GetPlayer()
        {

        }
    }
}

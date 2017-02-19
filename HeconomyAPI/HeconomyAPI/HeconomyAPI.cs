
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
using HeconomyAPI.Handlers;
using HeconomyAPI.Utils;

using MiNET;
using MiNET.Plugins;

using Newtonsoft.Json.Linq;

using System;
using System.IO;
using System.Reflection;

namespace HeconomyAPI
{

    public class HeconomyAPI : Plugin
    {

        public const string Prefix = "\x5b\x48\x65\x63\x6f\x6e\x6f\x6d\x79\x5d";

        private static string assembly = Assembly.GetExecutingAssembly().GetName().CodeBase;
        private static string path = Path.Combine(new Uri(Path.GetDirectoryName(assembly)).LocalPath, "HeconomyAPI\\");

        private Inspector Inspector;

        protected override void OnEnable()
        {
            Inspector = new Inspector() { Version = 1.0 };

            var plugin = new HeconomyAPI();

            Context.Server.PlayerFactory.PlayerCreated += (sender, args) =>
            {
                Player player = args.Player;

                player.PlayerJoin += new PlayerJoin(plugin).handle;
            };

            RegisterCommands();

            @Directory.CreateDirectory(path);
            @Directory.CreateDirectory(path + "\\players");
        }

        public override void OnDisable()
        {

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

        public bool IsRegisteredPlayer(string player)
        {
            string data = path + "\\players\\" + player.ToLower() + ".json";

            return File.Exists(data);
        }

        public void RegisterPlayer(Player player)
        {
            string data = path + "\\players\\" + player.Username.ToLower() + ".json";

            JObject file = new JObject(
                new JProperty("Money", 10)
                );
            File.WriteAllText(data, file.ToString());
        }

        public string GetMoneySymbol()
        {
            return "$";
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

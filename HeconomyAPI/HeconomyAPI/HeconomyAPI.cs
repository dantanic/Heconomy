
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
using HeconomyAPI.Assist;

using MiNET;
using MiNET.Worlds;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using System;
using System.Linq;
using System.IO;
using System.Reflection;

namespace HeconomyAPI
{

    [Plugin(PluginName = "HeconomyAPI", Description = "An advanced economy plugin for MiNET.", PluginVersion = "beta1", Author = "Herb9")]
    public class HeconomyAPI : Plugin
    {

        public const string Prefix = "\x5b\x48\x65\x63\x6f\x6e\x6f\x6d\x79\x5d";

        private static string assembly = Assembly.GetExecutingAssembly().GetName().CodeBase;
        private static string path = Path.Combine(new Uri(Path.GetDirectoryName(assembly)).LocalPath, @"HeconomyAPI\");

        private Inspector Inspector;

        protected override void OnEnable()
        {
            Inspector = new Inspector();

            var plugin = new HeconomyAPI();

            Context.Server.PlayerFactory.PlayerCreated += (sender, args) =>
            {
                Player player = args.Player;

                player.PlayerJoin += new PlayerJoin(plugin).handle;
            };

            RegisterCommands();

            @Directory.CreateDirectory(path);
            @Directory.CreateDirectory(path + @"\players");
        }

        private void RegisterCommands()
        {
            var plugin = new HeconomyAPI();

            Context.PluginManager.LoadCommands(new Money(plugin));
            Context.PluginManager.LoadCommands(new Pay(plugin));
        }

        private void Save(JObject item, string path)
        {
            string data = JsonConvert.SerializeObject(item, Formatting.Indented);

            File.WriteAllText(data, data);
        }

        public static HeconomyAPI GetAPI()
        {
            return new HeconomyAPI();
        }

        public bool IsRegisteredPlayer(string player)
        {
            string data = path + @"\players\" + player.ToLower() + ".json";

            return File.Exists(data);
        }

        public void RegisterPlayer(Player player)
        {
            string data = path + @"\players\" + player.Username.ToLower() + ".json";

            JObject item = new JObject(
                new JProperty("Money", GetDefaultMoney())
                );

            File.WriteAllText(data, item.ToString());
        }

        public Player GetPlayer(string var, Level level)
        {
            return level.Players.ToList().Find(x => x.Value.Username.Contains(var)).Value ?? null;
        }

        public string GetMoneySymbol()
        {
            return "$";
        }

        public int GetDefaultMoney()
        {
            return 10;
        }

        public int GetMoney(string player)
        {
            string data = path + @"\players\" + player.ToLower() + ".json";

            JObject item = JObject.Parse(File.ReadAllText(data));

            return int.Parse(item["Money"].ToString());
        }

        public void SetMoney(string player, int amount)
        {
            string data = path + @"\players\" + player.ToLower() + ".json";

            JObject item = JObject.Parse(File.ReadAllText(data));

            item["Money"] = amount;

            Save(item, data);
        }
    }
}
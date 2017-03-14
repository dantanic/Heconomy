
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

using HeconomyAPI.Assist;
using HeconomyAPI.Command;
using HeconomyAPI.Package;

using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using MiNET.Worlds;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HeconomyAPI
{

    [Plugin(PluginName = "HeconomyAPI", Description = "An advanced economy plugin for MiNET.", PluginVersion = "1.2", Author = "Herb9")]
    public class HeconomyAPI : Plugin
    {

        public const string Prefix = "\x5b\x48\x65\x63\x6f\x6e\x6f\x6d\x79\x5d";

        private static HeconomyAPI Instance;

        private AutoUpdater AutoUpdater;
        private Config Config;

        private Connect Connect;

        protected override void OnEnable()
        {
            Instance = this;

            @Directory.CreateDirectory(GetDataFolder());
            @Directory.CreateDirectory(GetDataFolder() + @"users\");

            AutoUpdater = new AutoUpdater(this);

            Config = new Config(this);

            RegisterCommands();

            RegisterPackages();
        }

        private void RegisterCommands()
        {
            Context.PluginManager.LoadCommands(new Money(this));
            Context.PluginManager.LoadCommands(new Pay(this));
            Context.PluginManager.LoadCommands(new View(this));
            Context.PluginManager.LoadCommands(new Top(this));
        }

        private void RegisterPackages()
        {
            Connect = new Connect(this);

            Context.Server.PlayerFactory.PlayerCreated += (sender, args) =>
            {
                args.Player.PlayerJoin += Connect.Package;
            };
        }

        public string GetDataFolder() 
            => Path.Combine(new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath, @"HeconomyAPI\");

        public bool IsRegisteredPlayer(string player) 
            => File.Exists(GetDataFolder() + @"users\" + player.ToLower() + ".json");

        public Player GetPlayer(string player, Level level) 
            => level.Players.ToList().Find(x => x.Value.Username.ToLower().Contains(player)).Value ?? null;

        public void RegisterPlayer(Player player)
        {
            string data = GetDataFolder() + @"users\" + player.Username.ToLower() + ".json";

            JObject item = new JObject(
                new JProperty("Money", GetDefaultMoney())
                );

            File.WriteAllText(data, item.ToString());
        }

        /*
                  .o.       ooooooooo.   ooooo 
                 .888.      `888   `Y88. `888' 
                .8"888.      888   .d88'  888  
               .8' `888.     888ooo88P'   888  
              .88ooo8888.    888          888  
             .8'     `888.   888          888  
            o88o     o8888o o888o        o888o 

            You can using HeconomyAPI public functions.
        */

        public static HeconomyAPI GetInstance() 
            => Instance;

        public string GetMoneySymbol() 
            => Config.GetProperty("Symbol");

        public int GetDefaultMoney() 
            => int.Parse(Config.GetProperty("DefaultMoney"));

        public int GetMinimumMoney() 
            => int.Parse(Config.GetProperty("MinMoney"));

        public int GetMoney(string player)
        {
            string data = GetDataFolder() + @"users\" + player.ToLower() + ".json";

            JObject item = JObject.Parse(File.ReadAllText(data));

            return int.Parse(item["Money"].ToString());
        }

        public void SetMoney(string player, int amount)
        {
            string data = GetDataFolder() + @"users\" + player.ToLower() + ".json";

            JObject item = JObject.Parse(File.ReadAllText(data));

            item["Money"] = amount;

            string input = JsonConvert.SerializeObject(item, Formatting.Indented);

            File.WriteAllText(data, input);
        }
    }
}
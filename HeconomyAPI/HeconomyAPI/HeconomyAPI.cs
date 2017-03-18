
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
using HeconomyAPI.Util;
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

        public static HeconomyAPI Instance { get; private set; }

        public UpdateInfo UpdateInfo { get; private set; }
        public Config Config { get; private set; }

        public string Symbol { get; set; }

        public int MinimumMoney { get; set; }
        public int DefaultMoney { get; set; }

        protected override void OnEnable()
        {
            @Directory.CreateDirectory(GetDataFolder());
            @Directory.CreateDirectory(GetDataFolder() + @"\users");
            RegisterCommands();

            UpdateInfo = new UpdateInfo(this);
            Config = new Config(this);
            Instance = this;

            Symbol = Config.GetProperty("Symbol");

            MinimumMoney = int.Parse(Config.GetProperty("MinimumMoney"));
            DefaultMoney = int.Parse(Config.GetProperty("DefaultMoney"));

            Context.Server.PlayerFactory.PlayerCreated += (sender, args) =>
            {
                args.Player.PlayerJoin += PlayerJoin;
            };
        }

        private void PlayerJoin(object sender, PlayerEventArgs eventArgs)
        {
            Player player = eventArgs.Player;

            if (!IsRegisteredPlayer(player.Username))
            {
                RegisterPlayer(player);

                Console.WriteLine(Prefix + " Could not find " + player.Username + "'s data, registering " + player.Username + "'s data...");
            }
        }

        private void RegisterCommands()
        {
            Context.PluginManager.LoadCommands(new Money(this));
            Context.PluginManager.LoadCommands(new Pay(this));
            // To be continue...
            // Context.PluginManager.LoadCommands(new Top(this));
            Context.PluginManager.LoadCommands(new View(this));
        }
         
        public string GetDataFolder() => Path.Combine(new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath, "HeconomyAPI");

        public bool IsRegisteredPlayer(string player) => File.Exists(GetDataFolder() + @"\users\" + player.ToLower() + ".json");

        public void RegisterPlayer(Player player)
        {
            string path = GetDataFolder() + @"\users\" + player.Username.ToLower() + ".json";
            JObject data = new JObject(
                new JProperty("Money", GetDefaultMoney())
                );

            File.WriteAllText(path, data.ToString());
        }

        public Player GetPlayer(string player, Level level) => level.Players.ToList().Find(x => x.Value.Username.ToLower().Contains(player)).Value;

        public static HeconomyAPI GetInstance() => Instance;

        public string GetMoneySymbol() => Config.GetProperty("Symbol");

        public int GetMinimumMoney() => int.Parse(Config.GetProperty("MinimumMoney"));

        public int GetDefaultMoney() => int.Parse(Config.GetProperty("DefaultMoney"));

        public int GetMoney(string player)
        {
            string path = GetDataFolder() + @"\users\" + player.ToLower() + ".json";
            JObject data = JObject.Parse(File.ReadAllText(path));

            return int.Parse(data["Money"].ToString());
        }

        public void SetMoney(string player, int amount)
        {
            string path = GetDataFolder() + @"\users\" + player.ToLower() + ".json";
            JObject data = JObject.Parse(File.ReadAllText(path));
            data["Money"] = amount;

            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}
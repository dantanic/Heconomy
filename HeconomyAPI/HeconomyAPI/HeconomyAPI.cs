
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

    [Plugin(PluginName = "HeconomyAPI", Description = "An advanced economy plugin for MiNET.", PluginVersion = "1.1", Author = "Herb9")]
    public class HeconomyAPI : Plugin
    {

        public const string Prefix = "\x5b\x48\x65\x63\x6f\x6e\x6f\x6d\x79\x5d";

        private static HeconomyAPI Object;

        private AutoUpdater AutoUpdater;

        private Resource Resource;

        protected override void OnEnable()
        {
            Object = this;

            RegisterCommands();

            SetPluginSource();

            AutoUpdater = new AutoUpdater(this);

            Resource = new Resource(this);

            AutoUpdater.Identify();

            Resource.CreateObject();

            Context.Server.PlayerFactory.PlayerCreated += (sender, args) =>
            {
                Player player = args.Player;

                player.PlayerJoin += new PlayerListener(this).CallEvent;
            };
        }

        private void RegisterCommands()
        {
            Context.PluginManager.LoadCommands(new Money(this));
            Context.PluginManager.LoadCommands(new Pay(this));
        }

        private void SetPluginSource()
        {
            @Directory.CreateDirectory(GetPluginSource());
            @Directory.CreateDirectory(GetPluginSource() + "\\users");
        }

        public string GetPluginSource()
        {
            string assembly = Assembly.GetExecutingAssembly().GetName().CodeBase;

            return Path.Combine(new Uri(Path.GetDirectoryName(assembly)).LocalPath, "HeconomyAPI");
        }

        private void SavePlayerData(string path, JObject jobject)
        {
            string data = JsonConvert.SerializeObject(jobject, Formatting.Indented);

            File.WriteAllText(path, data);
        }

        public bool IsRegisteredPlayer(string player)
        {
            string data = GetPluginSource() + "\\users\\" + player.ToLower() + ".json";

            return File.Exists(data);
        }

        public void RegisterPlayer(Player player)
        {
            string data = GetPluginSource() + "\\users\\" + player.Username.ToLower() + ".json";

            JObject item = new JObject(
                new JProperty("Money", GetDefaultMoney())
                );

            File.WriteAllText(data, item.ToString());
        }

        public Player GetPlayer(string player, Level level)
        {
            return level.Players.ToList().Find(x => x.Value.Username.ToLower().Contains(player)).Value ?? null;
        }

        /*
                  .o.       ooooooooo.   ooooo 
                 .888.      `888   `Y88. `888' 
                .8"888.      888   .d88'  888  
               .8' `888.     888ooo88P'   888  
              .88ooo8888.    888          888  
             .8'     `888.   888          888  
            o88o     o8888o o888o        o888o 
        */

        public static HeconomyAPI GetAPI()
        {
            return Object;
        }

        public string GetMoneySymbol()
        {
            return Resource.GetProperty("Symbol");
        }

        public int GetDefaultMoney()
        {
            return int.Parse(Resource.GetProperty("DefaultMoney"));
        }

        public int GetMinimumMoney()
        {
            return int.Parse(Resource.GetProperty("MinMoney"));
        }

        public int GetMoney(string player)
        {
            string path = GetPluginSource() + "\\users\\" + player.ToLower() + ".json";

            JObject data = JObject.Parse(File.ReadAllText(path));

            return int.Parse(data["Money"].ToString());
        }

        public void SetMoney(string player, int amount)
        {
            string path = GetPluginSource() + "\\users\\" + player.ToLower() + ".json";

            JObject data = JObject.Parse(File.ReadAllText(path));

            data["Money"] = amount;

            SavePlayerData(path, data);
        }
    }
}
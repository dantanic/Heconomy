
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

using MiNET;
using MiNET.Worlds;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HeconomyAPI
{

    public class HeconomyAPI
    {

        public const string Prefix = "\x5b\x48\x65\x63\x6f\x6e\x6f\x6d\x79\x5d";

        protected HeconomyAPI()
        {

        }

        public Player GetPlayer(string player, Level level)
        {
            return level.Players.ToList().Find(x => x.Value.Username.ToLower().Contains(player)).Value ?? null;
        }

        public string GetPluginFolder()
        {
            string assembly = Assembly.GetExecutingAssembly().GetName().CodeBase;

            return Path.Combine(new Uri(Path.GetDirectoryName(assembly)).LocalPath, @"HeconomyAPI\");
        }

        public bool IsRegisteredPlayer(string player)
        {
            string data = GetPluginFolder() + @"\players\" + player.ToLower() + ".json";

            return File.Exists(data);
        }

        public void RegisterPlayer(Player player)
        {
            string data = GetPluginFolder() + @"\players\" + player.Username.ToLower() + ".json";

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

            You can using api functions.
        */

        public static HeconomyAPI GetAPI()
        {
            return new HeconomyAPI();
        }

        public string GetMoneySymbol()
        {
            return "$";
        }

        public int GetDefaultMoney()
        {
            return 10;
        }

        public int GetMinimumMoney()
        {
            return 0;
        }

        public int GetMoney(string player)
        {
            string path = GetPluginFolder() + @"\players\" + player.ToLower() + ".json";

            JObject item = JObject.Parse(File.ReadAllText(path));

            return int.Parse(item["Money"].ToString());
        }

        public void SetMoney(string player, int amount)
        {
            string path = GetPluginFolder() + @"\players\" + player.ToLower() + ".json";

            JObject item = JObject.Parse(File.ReadAllText(path));

            item["Money"] = amount;

            string output = JsonConvert.SerializeObject(item, Formatting.Indented);

            File.WriteAllText(path, output);
        }
    }
}
 
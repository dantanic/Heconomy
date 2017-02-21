
/*
                                                    ..                   
             .xHL                                 . uW8"                     
          .-`8888hxxx~                  .u    .   `t888         .xn!~%x.     
       .H8X  `%888*"           .u     .d88B :@8c   8888   .    x888   888.   
       888X     ..x..       ud8888.  ="8888f8888r  9888.z88N  X8888   8888:  
      '8888k .x8888888x   :888'8888.   4888>'88"   9888  888E 88888   X8888  
       ?8888X    "88888X  d888 '88%"   4888> '     9888  888E 88888   88888> 
        ?8888X    '88888> 8888.+"      4888>       9888  888E `8888  :88888X 
     H8H %8888     `8888> 8888L       .d888L .+    9888  888E   `"**~ 88888> 
    '888> 888"      8888  '8888c. .+  ^"8888*"    .8888  888"  .xx.   88888  
     "8` .8" ..     88*    "88888%       "Y"       `%888*%"   '8888>  8888~  
        `  x8888h. d*"       "YP'                     "`       888"  :88%    
          !""*888%~                                             ^"===""      
          !   `"  .                                                          
          '-....:~ 
*/

using HeconomyAPI.Assist;

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
                 ..                  ....      ..         .....     .  
              :**888H: `: .xH""    +^""888h. ~"888h     .d88888Neu. 'L 
             X   `8888k XX888     8X.  ?8888X  8888f    F""""*8888888F 
           '8hx  48888 ?8888    '888x  8888X  8888~   *      `"*88*"  
           '8888 '8888 `8888    '88888 8888X   "88x:   -....    ue=:. 
             %888>'8888  8888     `8888 8888X  X88x.           :88N  ` 
               "8 '888"  8888       `*` 8888X '88888X          9888L   
              .-` X*"    8888      ~`...8888X  "88888   uzu.   `8888L  
                .xhx.    8888       x8888888X.   `%8" ,""888i   ?8888  
              .H88888h.~`8888.>    '%"*8888888h.   "  4  9888L   %888> 
             .~  `%88!` '888*~     ~    888888888!`   '  '8888   '88%  
                   `"     ""            X888^"""           "*8Nu.z*"   
                                        `88f                           
                                         88                            
                                         ""    
        */

        public static HeconomyAPI GetAPI()
        {
            return new HeconomyAPI();
        }

        public string GetMoneySymbol()
        {
            Config config = new Config();

            return config.GetProperty("symbol");
        }

        public int GetDefaultMoney()
        {
            Config config = new Config();

            return int.Parse(config.GetProperty("default"));
        }

        public int GetMinimumMoney()
        {
            Config config = new Config();

            return int.Parse(config.GetProperty("minimum"));
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
 

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
using HeconomyAPI.Command;

using MiNET;
using MiNET.Plugins;
using MiNET.Worlds;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HeconomyAPI
{

    public class HeconomyAPI : Plugin
    {

        public const string Prefix = "\x5b\x48\x65\x63\x6f\x6e\x6f\x6d\x79\x5d";

        private static dynamic Object;

        private dynamic AutoUpdater;

        private dynamic Resource;

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
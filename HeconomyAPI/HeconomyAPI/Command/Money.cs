
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

using MiNET;
using MiNET.Plugins.Attributes;

namespace HeconomyAPI.Command
{

    public class Money
    {

        private dynamic Plugin;

        public Money(HeconomyAPI plugin)
        {
            Plugin = plugin;
        }

        [Command(Name = "money", Description = "Shows player's money amount or you.", Permission = "heconomyapi.command.money")]
        public void Execute(Player sender)
        {
            int amount = Plugin.GetMoney(sender.Username);

            string symbol = Plugin.GetMoneySymbol();

            sender.SendMessage(HeconomyAPI.Prefix + " Your money amount: " + amount + symbol);
        }

        [Command(Name = "money", Description = "Shows your money amount or you.", Permission = "heconomyapi.command.money")]
        public void Execute(Player sender, string player)
        {
            string symbol = Plugin.GetMoneySymbol();

            if (Plugin.IsRegisteredPlayer(player))
            {
                int amount = Plugin.GetMoney(player);

                if(Plugin.GetPlayer(player, sender.Level) != null)
                {
                    Player victim = Plugin.GetPlayer(player, sender.Level);

                    sender.SendMessage(HeconomyAPI.Prefix + " " + victim.Username + "'s money amount: " + amount + symbol);

                    return;
                }

                sender.SendMessage(HeconomyAPI.Prefix + " " + player + "'s money amount: " + amount + symbol);
            }
        }
    }
}

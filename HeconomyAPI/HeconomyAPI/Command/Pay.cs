
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

    public class Pay
    {

        private HeconomyAPI Plugin { get; set; }

        public Pay(HeconomyAPI plugin)
        {
            Plugin = plugin;
        }

        [Command(Name = "pay", Description = "Pays money to player.", Permission = "heconomyapi.command.pay")]
        public void execute(Player sender, string player, double amount)
        {
            string symbol = Plugin.GetMoneySymbol();

            if((Plugin.IsRegisteredPlayer(player)) && (Plugin.GetPlayer(player, sender.Level) != null))
            {
                Player receiver = Plugin.GetPlayer(player, sender.Level);

                Plugin.SetMoney(sender.Username, Plugin.GetMoney(sender.Username) - amount);
                Plugin.SetMoney(receiver.Username, Plugin.GetMoney(receiver.Username) + amount);

                sender.SendMessage(HeconomyAPI.Prefix + $" You paid {amount}{symbol} to {receiver.Username}.");

                receiver.SendMessage(HeconomyAPI.Prefix + $" You have received {amount}{symbol} from {sender.Username}.");
            }
        }
    }
}

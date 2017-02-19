
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

using System;

using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;

namespace HeconomyAPI.Command
{

    public class Pay
    {

        protected HeconomyAPI Plugin { get; set; }

        public Pay(HeconomyAPI plugin)
        {
            Plugin = plugin;
        }

        [Command(Name = "pay", Description = "Pays money to player.", Permission = "heconomyapi.command.pay")]
        public void execute(Player sender, string var, int amount)
        {
            string symbol = Plugin.GetMoneySymbol();

            if(Plugin.IsRegisteredPlayer(var))
            {
                sender.SendMessage(HeconomyAPI.Prefix + $" You paid {amount}{symbol} to {var}.");

                Plugin.SetMoney(sender.Username, Plugin.GetMoney(sender.Username) - amount);
                Plugin.SetMoney(var, Plugin.GetMoney(var) + amount);

                if(Plugin.GetPlayer(var, sender.Level) != null)
                {
                    Player receiver = Plugin.GetPlayer(var, sender.Level);

                    receiver.SendMessage(HeconomyAPI.Prefix + $" You have received {amount}{symbol} from {sender.Username}");
                }
            }
        }
    }
}

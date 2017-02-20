
/*
    888    888                  888       .d8888b.  
    888    888                  888      d88P  Y88b 
    888    888                  888      888    888 
    8888888888  .d88b.  888d888 88888b.  Y88b. d888 
    888    888 d8P  Y8b 888P"   888 "88b  "Y888P888 
    888    888 88888888 888     888  888        888 
    888    888 Y8b.     888     888 d88P Y88b  d88P 
    888    888  "Y8888  888     88888P"   "Y8888P"   

    Directed by Herb9.
*/

using MiNET;
using MiNET.Plugins.Attributes;

namespace HeconomyAPI.Command
{

    public class Money
    {

        protected HeconomyAPI Plugin { get; set; }

        public Money(HeconomyAPI plugin)
        {
            Plugin = plugin;
        }

        [Command(Name = "money", Description = "Shows player money amount or you.", Permission = "heconomyapi.command.money")]
        public void execute(Player sender)
        {
            int amount = Plugin.GetMoney(sender.Username);

            string symbol = Plugin.GetMoneySymbol();

            sender.SendMessage(HeconomyAPI.Prefix + $" Your money amount: {amount}{symbol}");
        }
    }
}


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

    public class Pay
    {

        private HeconomyAPI Plugin { get; set; }

        public Pay(HeconomyAPI plugin)
        {
            Plugin = plugin;
        }

        [Command(Name = "pay", Description = "Pays money to player.", Permission = "heconomyapi.command.pay")]
        public void execute(Player sender, string player, int amount)
        {
            string symbol = Plugin.GetMoneySymbol();

            if(Plugin.IsRegisteredPlayer(player))
            {
                sender.SendMessage(HeconomyAPI.Prefix + $" You paid {amount}{symbol} to {player}.");

                Plugin.SetMoney(sender.Username, Plugin.GetMoney(sender.Username) - amount);
                Plugin.SetMoney(player, Plugin.GetMoney(player) + amount);

                if(Plugin.GetPlayer(player, sender.Level) != null)
                {
                    Player receiver = Plugin.GetPlayer(player, sender.Level);

                    receiver.SendMessage(HeconomyAPI.Prefix + $" You have received {amount}{symbol} from {sender.Username}.");
                }
            }
        }
    }
}


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
using MiNET.Plugins;
using MiNET.Plugins.Attributes;

namespace Heconomy.Command
{

    public class Pay
    {

        private string Prefix;

        private Heconomy Plugin;

        public Pay(Heconomy plugin)
        {
            Plugin = plugin;
        }

        [Command(
            Name = "pay",
            Description = "Pays money to player."
            )]
        public void execute(Player sender, string player, float amount)
        {
            if(Plugin.IsRegisteredPlayer(player) && amount > 0)
            {
                string symbol = Plugin.GetMoneySymbol();

                Plugin.SetMoney(player, Plugin.GetMoney(player) + amount);

                Plugin.SetMoney(sender.Username, Plugin.GetMoney(sender.Username) - amount);

                sender.SendMessage($"{Prefix} You paid {amount}{symbol} to {player}.");

                if(Plugin.GetNameByPlayer(player, sender.Level) != null)
                {
                    Player receiver = Plugin.GetNameByPlayer(player, sender.Level);

                    receiver.SendMessage($"{Prefix} You have received {amount}{symbol} from {sender.Username}");
                }
            }
        }
    }
}

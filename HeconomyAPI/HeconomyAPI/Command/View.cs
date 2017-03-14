
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
using MiNET.Plugins.Attributes;
using MiNET.Utils;

namespace HeconomyAPI.Command
{
    public class View
    {
        private HeconomyAPI Plugin;

        public View(HeconomyAPI plugin)
        {
            Plugin = plugin;
        }

        [Command(Name = "view", Description = "Shows other player's money amount.", Permission = "heconomyapi.command.view")]
        public void Execute(Player sender)
        {
            sender.SendMessage(ChatColors.Green + "Usage: /view <player: string>");
        }

        [Command(Name = "view", Description = "Shows other player's money amount.", Permission = "heconomyapi.command.view")]
        public void Execute(Player sender, string player)
        {
            string symbol = Plugin.GetMoneySymbol();
            int amount = Plugin.GetMoney(player);
            if (!Plugin.IsRegisteredPlayer(player)) sender.SendMessage(ChatColors.Red + "Invaild player.");
            else sender.SendMessage(ChatColors.Blue + player + "'s money amount: " + amount + symbol);
        }
    }
}

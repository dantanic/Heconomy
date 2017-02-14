
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

    public class Money
    {

        private string Prefix { get; set; }

        private Heconomy Plugin { get; set; }

        public Money(Heconomy plugin)
        {
            Plugin = plugin;

            Prefix = Heconomy.Prefix;
        }

        [Command(
            Name = "money", 
            Description = "Shows your money amount."
            )]
        public void execute(Player sender)
        {
            float amount = Plugin.GetMoney(sender.Username);

            string symbol = Plugin.GetMoneySymbol();

            sender.SendMessage($"{Prefix} Your money amount: {amount}{symbol}");
        }

        [Command(
            Name = "money",
            Description = "Shows your money amount."
            )]
        public void execute(Player sender, string player)
        {
            if (Plugin.IsRegisteredPlayer(player))
            {
                float amount = Plugin.GetMoney(player);

                string symbol = Plugin.GetMoneySymbol();

                sender.SendMessage($"{Prefix} Player {player}'s money amount: {amount}{symbol}");
            }
        }
    }
}

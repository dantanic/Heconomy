
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
    public class Money
    {
        public HeconomyAPI Plugin { get; set; }

        public string Symbol { get; set; }

        public Money(HeconomyAPI plugin)
        {
            Plugin = plugin;

            Symbol = Plugin.GetMoneySymbol();
        }

        [Command(Name = "money", Description = "Shows your money.", Permission = "heconomyapi.command.money")]
        public void Execute(Player sender)
        {
            sender.SendMessage(ChatColors.DarkGreen + "Your money: " + Plugin.GetMoney(sender.Username) + Symbol);
        }
    }
}

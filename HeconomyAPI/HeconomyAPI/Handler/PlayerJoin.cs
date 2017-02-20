
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

namespace HeconomyAPI.Handler
{

    public class PlayerJoin
    {

        private HeconomyAPI Plugin { get; set; }

        public PlayerJoin(HeconomyAPI plugin)
        {
            Plugin = plugin;
        }

        public void handle(object sender, PlayerEventArgs eventArgs)
        {
            Player player = eventArgs.Player;

            if (!Plugin.IsRegisteredPlayer(player.Username))
            {
                Plugin.RegisterPlayer(player);
            }
        }
    }
}

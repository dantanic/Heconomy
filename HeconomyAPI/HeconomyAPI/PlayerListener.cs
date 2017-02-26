
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

using System;

namespace HeconomyAPI
{

    public class PlayerListener
    {

        private HeconomyAPI Plugin;

        public PlayerListener(HeconomyAPI plugin)
        {
            Plugin = plugin;
        }

        public void CallEvent(object sender, PlayerEventArgs eventArgs)
        {
            Player player = eventArgs.Player;

            if (!Plugin.IsRegisteredPlayer(player.Username))
                Plugin.RegisterPlayer(player);
        }
    }
}
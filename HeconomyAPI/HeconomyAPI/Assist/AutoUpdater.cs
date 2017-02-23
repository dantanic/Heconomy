
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

using Newtonsoft.Json.Linq;

using System;
using System.Net;
using System.Text;

namespace HeconomyAPI.Assist
{

    public class AutoUpdater
    {

        private HeconomyAPI Plugin;

        private byte[] Source = Convert.FromBase64String("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL2RhbnRhbmljL2pzb24vbWFzdGVyL2NhbGwuanNvbg==");

        private dynamic Version;

        public AutoUpdater(HeconomyAPI plugin)
        {
            Plugin = plugin;

            Version = JObject.Parse(GetVersionString());
        }

        private string GetVersionString()
        {
            return new WebClient().DownloadString(Encoding.UTF8.GetString(Source));
        }

        public void Identify()
        {
            if (Version.HeconomyAPI > 1.0)
                Console.WriteLine(HeconomyAPI.Prefix + " New version has been found, Please download new version or inquire developer.");

            else
                Console.WriteLine(HeconomyAPI.Prefix + " You are currently using HeconomyAPI v1.0");
        }
    }
}
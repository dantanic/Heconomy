
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

using Newtonsoft.Json.Linq;

using System;
using System.Net;
using System.Text;

namespace HeconomyAPI.Assist
{

    public class AutoUpdater
    {

        private HeconomyAPI Plugin;

        private byte[] Path = Convert.FromBase64String("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL2RhbnRhbmljL2pzb24vbWFzdGVyL2NhbGwuanNvbg==");

        private dynamic Version;

        public AutoUpdater(HeconomyAPI plugin)
        {
            Plugin = plugin;

            Version = JObject.Parse(GetVersionString());

            Console.WriteLine(HeconomyAPI.Prefix + " AutoUpdater has been enabled, checking updates...");
        }

        private string GetVersionString()
        {
            return new WebClient().DownloadString(Encoding.UTF8.GetString(Path));
        }

        public void Identify()
        {
            if (Version.HeconomyAPI > 1.0)
                Console.WriteLine(HeconomyAPI.Prefix + " New version has been found, please download new version or inquire developer.");

            else
                Console.WriteLine(HeconomyAPI.Prefix + " You are currently using HeconomyAPI v1.1");
        }
    }
}

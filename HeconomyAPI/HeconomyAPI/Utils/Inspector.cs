
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

namespace HeconomyAPI.Utils
{

    public class Inspector
    {

        public double Version { get; set; }

        public Inspector()
        {
            byte[] path = Convert.FromBase64String("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0hlcmJQbHVnaW5zL0hlY29ub215L21hc3Rlci9IZWNvbm9teUFQSS9IZWNvbm9teUFQSS9wbHVnaW4uanNvbg==");

            dynamic plugin = JObject.Parse(new WebClient().DownloadString(Encoding.UTF8.GetString(path)));

            if (plugin.Version > Version)
                Console.WriteLine(HeconomyAPI.Prefix + " New version has been found, Please inquire developer or download new version.");

            else
                Console.WriteLine(HeconomyAPI.Prefix + " Heconomy v1.0 successfully enabled.");
        }
    }
}
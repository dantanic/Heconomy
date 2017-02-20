
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

using Newtonsoft.Json.Linq;

using System;
using System.Net;
using System.Text;

namespace HeconomyAPI.Assist
{

    public class Inspector
    {

        public Inspector()
        {
            byte[] path = Convert.FromBase64String("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0hlcmJQbHVnaW5zL0hlY29ub215L21hc3Rlci9IZWNvbm9teUFQSS9IZWNvbm9teUFQSS9wbHVnaW4uanNvbg==");

            dynamic plugin = JObject.Parse(new WebClient().DownloadString(Encoding.UTF8.GetString(path)));

            if (plugin.Version > 1.0)
                Console.WriteLine(HeconomyAPI.Prefix + " New version has been found, Please inquire developer or download new version.");

            else
                Console.WriteLine(HeconomyAPI.Prefix + " HeconomyAPI v1.0 successfully enabled.");
        }
    }
}
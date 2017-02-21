
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

using System;
using System.Text;
using System.Net;
using System.IO;

namespace HeconomyAPI.Assist
{

    public class Config
    {

        public void Create()
        {
            byte[] url = Convert.FromBase64String("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0hlcmJQbHVnaW5zL0hlY29ub215L21hc3Rlci9IZWNvbm9teUFQSS9IZWNvbm9teUFQSS9SZXNvdXJjZXMvc2V0dGluZ3MuY29uZg==");

            string data = File.ReadAllText(new WebClient().DownloadString(Encoding.UTF8.GetString(url)));

            string path = HeconomyAPI.GetAPI().GetPluginFolder();

            if (File.Exists(path + @"\settings.conf"))
            {
                File.Create(path + @"\settings.conf");

                File.WriteAllText(path + @"\settings.conf", data);
            }
        }

        public string GetProperty(string property)
        {
            return "";
        }
    }
}

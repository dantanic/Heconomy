
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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HeconomyAPI.Util
{
    public class Config
    {
        private HeconomyAPI Plugin;

        private byte[] B64Path = Convert.FromBase64String("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0hlcmJQbHVnaW5zL0hlY29ub215L21hc3Rlci9IZWNvbm9teUFQSS9IZWNvbm9teUFQSS9SZXNvdXJjZXMvc2V0dGluZ3MuY29uZg==");

        private Dictionary<string, string> Contents = new Dictionary<string, string>();

        public Config(HeconomyAPI plugin)
        {
            Plugin = plugin;
            var path = Plugin.GetDataFolder() + "settings.conf";

            if (!File.Exists(path)) File.WriteAllText(path, new WebClient().DownloadString(Encoding.UTF8.GetString(B64Path)));

            string[] data = File.ReadAllLines(path);

            foreach (string datum in data) LoadResource(datum);
        }

        private void LoadResource(string datum)
        {
            foreach (string data in datum.Split(new[] { "\r\n", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string contents = data.Trim();

                if ((!contents.Contains("=")) && (contents.StartsWith("#"))) continue;

                string[] property = contents.Split('=');

                Contents.Add(property[0], property[1]);
            }
        }

        public string GetProperty(string property) => Contents[property];
    }
}
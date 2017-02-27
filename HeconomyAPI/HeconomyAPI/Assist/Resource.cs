
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

namespace HeconomyAPI.Assist
{

    public class Resource
    {

        private dynamic Plugin;

        private byte[] Path = Convert.FromBase64String("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0hlcmJQbHVnaW5zL0hlY29ub215L21hc3Rlci9IZWNvbm9teUFQSS9IZWNvbm9teUFQSS9SZXNvdXJjZXMvc2V0dGluZ3MuY29uZg==");

        private Dictionary<string, string> Contents = new Dictionary<string, string>();

        private string Source;

        public Resource(HeconomyAPI plugin)
        {
            Plugin = plugin;

            Source = Plugin.GetPluginSource();
        }

        private string GetResourceString()
        {
            return new WebClient().DownloadString(Encoding.UTF8.GetString(Path));
        }

        private void LoadResource(string data)
        {
            foreach (string token in data.Split(new[] { "\r\n", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string trim = token.Trim();

                if ((!trim.Contains("=")) && (trim.StartsWith("#")))
                    continue;

                string[] item = trim.Split('=');

                string key = item[0];
                string result = item[1];

                Contents.Add(key, result);
            }
        }

        public string GetProperty(string property)
        {
            if (!Contents.ContainsKey(property))
                return null;

            return Contents[property];
        }

        public void CreateObject()
        {
            var path = Source + "\\settings.conf";

            if (!File.Exists(path))
                File.WriteAllText(path, GetResourceString());

            string[] data = File.ReadAllLines(path);

            foreach (string item in data)
                LoadResource(item);

            Console.WriteLine(HeconomyAPI.Prefix + " settings.conf successfully loaded.");
        }
    }
}
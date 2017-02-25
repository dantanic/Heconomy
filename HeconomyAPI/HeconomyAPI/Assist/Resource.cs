
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

        private byte[] Path = Convert.FromBase64String("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0hlcmJQbHVnaW5zL0hlY29ub215L21hc3Rlci9IZWNvbm9teUFQSS9IZWNvbm9teUFQSS9IZWNvbm9teUFQSS9SZXNvdXJjZXMvc2V0dGluZ3MuY29uZg==");

        private Dictionary<string, string> Contents = new Dictionary<string, string>();

        private string Source;
        
        public Resource(HeconomyAPI plugin)
        {
            Plugin = plugin;

            Source = Plugin.GetPluginSource();

            Console.WriteLine(HeconomyAPI.Prefix + " Loading settings.conf...");
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

                Contents.Add(item[0], item[1]);

                Console.WriteLine(HeconomyAPI.Prefix + " settings.conf successfully loaded.");
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

            foreach(string item in data)
                LoadResource(item);
        }
    }
}

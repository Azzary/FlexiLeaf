using Giny.Core.DesignPattern;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.StealthRunner
{
    public class AppSettings : Singleton<AppSettings>
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }


       public void LoadConfiguration()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                AppSettings config = JsonConvert.DeserializeObject<AppSettings>(json);
                IPAddress = config.IPAddress;
                Port = config.Port;
            }
        }
    }

}

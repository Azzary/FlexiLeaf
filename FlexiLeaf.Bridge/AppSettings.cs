using Giny.Core.DesignPattern;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.Bridge
{
    public class AppSettings: Singleton<AppSettings>
    {
        public string IPAddress { get; set; }
        public int PortStealthRunner { get; set; }
        public int PortControlHub { get; set; }
        public string Password { get; set; }


        public void LoadConfiguration()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                var Config = JsonConvert.DeserializeObject<AppSettings>(json);

                IPAddress = Config.IPAddress;
                PortStealthRunner = Config.PortStealthRunner;
                PortControlHub = Config.PortControlHub;
                Password = Config.Password;
            }
        }
    }
}

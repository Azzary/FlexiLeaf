using Giny.Core.DesignPattern;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiLeaf.ControlHub
{
    public class AppSettings : Singleton<AppSettings>
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }

        public void LoadConfiguration()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                var config = JsonConvert.DeserializeObject<AppSettings>(json);
                AppSettings.Instance.IPAddress = config.IPAddress;
                AppSettings.Instance.Port = config.Port;
                AppSettings.Instance.Password = config.Password;
            }
        }
    }

}

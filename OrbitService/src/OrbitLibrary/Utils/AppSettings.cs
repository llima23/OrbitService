using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrbitLibrary.Utils
{

    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string DbServerType { get; set; }
        public string DataBaseName { get; set; }

        public static AppSettings GetAppSettings()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("C:\\4TAX_Services\\Orbit_GeradorStringConexao");
            sb.Append(@"\connector.json");

            using (StreamReader r = new StreamReader(sb.ToString()))
            {
                string encryptedJson = r.ReadToEnd();
                string json = new SettingsCrypto().Decrypt(encryptedJson);
                AppSettings config = JsonConvert.DeserializeObject<AppSettings>(json);
                return config;
            }
        }

        public static List<AppSettings> GetListAppSettings()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("C:\\4TAX_Services\\Orbit_GeradorStringConexao");
            sb.Append(@"\connector.json");

            using (StreamReader r = new StreamReader(sb.ToString()))
            {
                string json = "";
                try
                {
                    string encryptedJson = r.ReadToEnd();
                    json = new SettingsCrypto().Decrypt(encryptedJson);
                    List<AppSettings> config = JsonConvert.DeserializeObject<List<AppSettings>>(json);
                    return config;
                }
                catch
                {
                    throw new Exception(json);
                }
            }
        }



    }
}

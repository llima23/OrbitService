using Newtonsoft.Json;
using OrbitLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Windows.Forms;

namespace ConnectionStringGenerator.Application.ConnectionString
{
    public class ConnectionString
    {
        private List<ObjConnectionString> listobjConnectionStrings;
        private ObjConnectionString objConnectionString;
        private StringBuilder sbConnectionString;
        public string connectionStringEncrypt;
        public string DbServerTypeName;

        public string EncryptConnectionString(string dbType, string serverLicense, string user, string password, DataGridView dataGridView1)
        {
            try
            {
                
                listobjConnectionStrings = new List<ObjConnectionString>();
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    objConnectionString = new ObjConnectionString();
                    sbConnectionString = new StringBuilder();
                    if (!string.IsNullOrEmpty(Convert.ToString(item.Cells[0].Value)))
                    {
                        if (dbType == "0") //Hana
                        {
                            DbServerTypeName = "HANA";
                            sbConnectionString.Append("DRIVER={HDBODBC};");
                            sbConnectionString.Append($"UID={user};");
                            sbConnectionString.Append($"PWD={password};");
                            sbConnectionString.Append($"SERVERNODE={serverLicense};");
                            sbConnectionString.Append($"CS={Convert.ToString(item.Cells[0].Value)};");
                        }
                        else
                        {
                            DbServerTypeName = "SQL";
                            sbConnectionString.Append("Driver={SQL Server};");
                            sbConnectionString.Append($"Uid={user};");
                            sbConnectionString.Append($"Pwd={password};");
                            sbConnectionString.Append($"Server={serverLicense};");
                            sbConnectionString.Append($"Database={Convert.ToString(item.Cells[0].Value)};");
                        }
                        objConnectionString.ConnectionString = sbConnectionString.ToString();
                        objConnectionString.DbServerType = DbServerTypeName;
                        objConnectionString.DataBaseName = Convert.ToString(item.Cells[0].Value);
                        listobjConnectionStrings.Add(objConnectionString);

                    }
                   
                }
                string teste = JsonConvert.SerializeObject(listobjConnectionStrings);
                return connectionStringEncrypt = new SettingsCrypto().Encrypt(JsonConvert.SerializeObject(listobjConnectionStrings));
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}

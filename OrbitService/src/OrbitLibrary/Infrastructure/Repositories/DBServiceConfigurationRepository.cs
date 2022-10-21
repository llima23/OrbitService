using System;
using System.Data;
using OrbitLibrary.Common;
using OrbitLibrary.Data;
using OrbitLibrary.Repositories;
using OrbitLibrary.Utils;

namespace OrbitLibrary.Infrastructure.Repositories
{
    public class DBServiceConfigurationRepository: ServiceConfigurationRepository
    {
        public const string TABLE_NAME = "TAX4_CONFIGADDON";
        private IWrapper wrapper;
        public DBServiceConfigurationRepository(IWrapper wrapper)
        {
            this.wrapper = wrapper;
            
        }

        public ServiceConfiguration GetConfiguration()
        {
            try
            {
                if (VerifyIfTableConfigAddonExist())
                {
                    DataSet queryResult = wrapper.ExecuteQuery(@$"SELECT * FROM ""@{TABLE_NAME}""");
                    if (queryResult.Tables[0].Rows.Count == 0)
                    {
                        return new ServiceConfiguration(null, null, null, null, new Guid());
                    }

                    string baseURIString = queryResult.Tables[0].Rows[0]["U_TAX4_UrlOrbit"].ToString();
                    string username = queryResult.Tables[0].Rows[0]["U_TAX4_usr4TAX"].ToString();
                    string password = queryResult.Tables[0].Rows[0]["U_TAX4_pass4TAX"].ToString();
                    string tenant = queryResult.Tables[0].Rows[0]["U_TAX4_TenantId"].ToString();
                    string ativo = queryResult.Tables[0].Rows[0]["U_TAX4_Ativo"].ToString();
                    ServiceConfiguration sConfig = new ServiceConfiguration(new Uri(baseURIString), username, password, ativo, new Guid(tenant));
                    sConfig.CredentialsProvider = Defaults.GetCredentialsProvider(sConfig, Defaults.GetCommunicationProvider());

                    return sConfig;
                }
                else
                {
                    return new ServiceConfiguration(null, null, null, null, new Guid());
                }
            }
            catch
            {
                return new ServiceConfiguration(null, null, null, null, new Guid());
            }
           
       
        }

        public bool VerifyIfTableConfigAddonExist()
        {
            DataSet queryResult = wrapper.ExecuteQuery(@$"select * from OUTB where ""TableName"" = '{TABLE_NAME}'");
            if (queryResult.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
           
        }
    }
}

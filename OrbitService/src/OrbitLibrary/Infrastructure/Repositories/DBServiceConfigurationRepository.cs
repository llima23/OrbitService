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
                        return new ServiceConfiguration(null, null, null, false, new Guid(), false,false);
                    }

                    //string baseURIString = "https://dev.api.orbitspot.com/"; //queryResult.Tables[0].Rows[0]["U_TAX4_UrlOrbit"].ToString();
                    //string username = "juliano.moura@seidor.com.br"; //queryResult.Tables[0].Rows[0]["U_TAX4_usr4TAX"].ToString();
                    //string password = "6s2v5399ai$Z2ZYV";//queryResult.Tables[0].Rows[0]["U_TAX4_pass4TAX"].ToString();
                    //string tenant = "2f3b81f0-c5a6-49a6-8e99-3083ce1d9d5c";//queryResult.Tables[0].Rows[0]["U_TAX4_TenantId"].ToString();
                    //bool ativo = queryResult.Tables[0].Rows[0]["U_TAX4_Ativo"].ToString() == "Y" ? true : false;
                    //bool integraDocDFe = queryResult.Tables[0].Rows[0]["U_TAX4_IntegraDocDFe"].ToString() == "Y" ? true : false;
                    //bool integraDocFiscal = queryResult.Tables[0].Rows[0]["U_TAX4_IntegraDocFisc"].ToString() == "Y" ? true : false;

                    string baseURIString = queryResult.Tables[0].Rows[0]["U_TAX4_UrlOrbit"].ToString();
                    string username = queryResult.Tables[0].Rows[0]["U_TAX4_usr4TAX"].ToString();
                    string password = queryResult.Tables[0].Rows[0]["U_TAX4_pass4TAX"].ToString();
                    string tenant = queryResult.Tables[0].Rows[0]["U_TAX4_TenantId"].ToString();
                    bool ativo = queryResult.Tables[0].Rows[0]["U_TAX4_Ativo"].ToString() == "Y" ? true : false;
                    bool integraDocDFe = queryResult.Tables[0].Rows[0]["U_TAX4_IntegraDocDFe"].ToString() == "Y" ? true : false;
                    bool integraDocFiscal = queryResult.Tables[0].Rows[0]["U_TAX4_IntegraDocFisc"].ToString() == "Y" ? true : false;
                    ServiceConfiguration sConfig = new ServiceConfiguration(new Uri(baseURIString), username, password, ativo, new Guid(tenant), integraDocDFe, integraDocFiscal);
                    sConfig.CredentialsProvider = Defaults.GetCredentialsProvider(sConfig, Defaults.GetCommunicationProvider());

                    return sConfig;
                }
                else
                {
                    return new ServiceConfiguration(null, null, null, false, new Guid(),false,false);
                }
            }
            catch
            {
                return new ServiceConfiguration(null, null, null, false, new Guid(),false,false);
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

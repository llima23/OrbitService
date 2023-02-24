using AccountService_CentroDeCusto.CentroDeCusto.infrastructure.documents.entities;
using AccountService_CentroDeCusto.CentroDeCusto.repository;
using AccountService_CentroDeCusto.CentroDeCusto.service;
using Newtonsoft.Json;
using OrbitLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccountService_CentroDeCusto.CentroDeCusto.infrastructure.repositories
{
    public class DBCentroDeCustoRepository : IDBCentroDeCustoRepository
    {
        private IWrapper Wrapper;
        public string QUERY = @"SELECT  
		                            ""PrcCode"", 
		                            ""PrcName"",
	  	                            ""U_TAX4_LIDO"", 
	  	                            COALESCE(""U_TAX4_IdRet"",'') AS ""U_TAX4_IdRet"" 
	  	                            FROM OPRC 
	  	                            WHERE ""U_TAX4_LIDO"" = 'N'";
        private string jsonConvert;
        private DataSet queryResult;

        public DBCentroDeCustoRepository(IWrapper wrapper)
        {
            this.Wrapper = wrapper;
        }
        public List<CentroDeCustoB1> ReturnListOfAccountToOrbit()
        {
            List<CentroDeCustoB1> result = new List<CentroDeCustoB1>();
            queryResult = Wrapper.ExecuteQuery(QUERY);
            jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
            result = JsonConvert.DeserializeObject<List<CentroDeCustoB1>>(jsonConvert);
            return result;
        }

        public int UpdateAccountStatusSucess(CentroDeCustoB1 account, CentroDeCustoOutput output)
        {
            return Wrapper.ExecuteNonQuery(@$"UPDATE OPRC SET ""U_TAX4_LIDO"" = 'S', ""U_TAX4_IdRet"" = '{output.id}' WHERE ""PrcCode"" = '{account.PrcCode}'");
        }
        public int UpdateAccountStatusError(CentroDeCustoB1 account, CentroDeCustoError output)
        {
            string messageError = string.Empty;

            if (output.errors == null)
            {
                messageError = output.message;
            }
            else
            {
                foreach (Error item in output.errors)
                {
                    messageError += item.msg + " - " + item.param + " - ";
                }
            }
            return Wrapper.ExecuteNonQuery(@$"UPDATE OPRC SET ""U_TAX4_LIDO"" = 'N' WHERE ""PrcCode"" = '{account.PrcCode}'");
        }
    }
}

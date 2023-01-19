using AccountService_PlanoDeContas.PlanoDeContas.Infrastructure.documents.entities;
using AccountService_PlanoDeContas.PlanoDeContas.repository;
using AccountService_PlanoDeContas.PlanoDeContas.service;
using AccountService_PlanoDeContas.PlanoDeContas.service.Create;
using Newtonsoft.Json;
using OrbitLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AccountService_PlanoDeContas.PlanoDeContas.Infrastructure.repositories
{
    public class DBPlanAccountRepository : IDBPlanAccountRepository
    {
        private IWrapper wrapper;
        public string QUERY = @"SELECT 
	                                 ""AcctCode"",
	                                 ""U_TAX4_LIDO"", 
	                                 ""Levels"",
                                     ""U_TAX4_IdRet""
	                                 FROM OACT 
	                                 WHERE ""U_TAX4_LIDO"" = 'S'
                                     AND ""U_TAX4_IdRet"" IS NOT NULL
                                     AND ""U_TAX4_IdRet"" <> ''";
        public string idOrbitPlanoConta { get; set; }
        public string tenantId { get; set; }
        public string planoDeContaIntegrado { get; set; }
        public DBPlanAccountRepository(IWrapper wrapper)
        {
            this.wrapper = wrapper;
        }
        public bool ValidadeIfExistsIdOrbitPlanAccountSucess()
        {
            DataSet queryResult = wrapper.ExecuteQuery(@$"SELECT ""U_TAX4_IdPLNConta"", ""U_TAX4_IntegraPlano"" FROM ""@TAX4_CONFIGADDON"" WHERE ""U_TAX4_TenantId"" = '{tenantId}'");
            if (string.IsNullOrEmpty(queryResult.Tables[0].Rows[0].ItemArray[0].ToString()))
            {
                return false;
            }
            else
            {
                idOrbitPlanoConta = queryResult.Tables[0].Rows[0].ItemArray[0].ToString();
                planoDeContaIntegrado = queryResult.Tables[0].Rows[0].ItemArray[1].ToString();
                return true;
            }
        }

        public List<PlanAccount> ReturnListOfPlanAccountToAssociate()
        {
            List<PlanAccount> result = new List<PlanAccount>();
            DataSet queryResult = wrapper.ExecuteQuery(QUERY);
            string jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
            result = JsonConvert.DeserializeObject<List<PlanAccount>>(jsonConvert);
            return result;
        }

        public int UpdatePlanAccountStatusSucess()
        {
            return wrapper.ExecuteNonQuery(@$"UPDATE ""@TAX4_CONFIGADDON"" SET ""U_TAX4_IntegraPlano"" = 'S', ""U_TAX4_IdPLNConta"" = '{idOrbitPlanoConta}' WHERE ""U_TAX4_TenantId"" = '{tenantId}'");
        }
    }
}

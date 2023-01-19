using AccountService_ContasContabeis.ContasContabeis.Infrastructure.Documents.Entities;
using AccountService_ContasContabeis.ContasContabeis.repository;
using AccountService_ContasContabeis.ContasContabeis.service;
using Newtonsoft.Json;
using OrbitLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccountService_ContasContabeis.ContasContabeis.Infrastructure.repositories
{
    public class DBAccountRepository: IDBAccountRepository
    {
        private IWrapper Wrapper;
        public string QUERY = @"SELECT 
	                                 ""AcctCode"",
	                                 ""AcctName"",
	                                 ""U_TAX4_LIDO"", 
	                                 ""FatherNum"",
	                                 ""Levels"", 
	                                 CASE ""Postable""
                                     WHEN 'Y' THEN ('A')
                                     WHEN 'N' THEN ('S')
                                     END AS ""AcctType"",
                                     CASE LEFT(""AcctCode"",1)
                                     WHEN '1' THEN('01')
                                     WHEN '2' THEN('02')
                                     WHEN '3' THEN('04')
                                     WHEN '4' THEN('04')
                                     WHEN '5' THEN('04')
                                     WHEN '6' THEN('05')
                                     WHEN '7' THEN('09')
                                     WHEN '8' THEN('09')
                                     WHEN '9' THEN('09')
                                     WHEN 'A' THEN('09')
                                     END AS ""OriginCode"",
	                                 ""CreateDate"" 
	                                 FROM OACT 
	                                 WHERE ""U_TAX4_LIDO"" = 'N'";
        private string jsonConvert;
        private string idOrbit { get; set; }
        private DataSet queryResult;

        public DBAccountRepository(IWrapper wrapper)
        {
            this.Wrapper = wrapper;
        }
        public List<Account> ReturnListOfAccountToOrbit()
        {
            List<Account> result = new List<Account>();
            queryResult = Wrapper.ExecuteQuery(QUERY);
            jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
            result = JsonConvert.DeserializeObject<List<Account>>(jsonConvert);
            return result;
        }
        public string ReturnIdOrbitFatherAccountB1(string fatherAccount)
        {
            queryResult = Wrapper.ExecuteQuery($@"SELECT ""U_TAX4_IdRet"" FROM OACT WHERE ""AcctCode"" = '{fatherAccount}'");
            idOrbit = queryResult.Tables[0].Rows[0].ItemArray[0].ToString();
            if (String.IsNullOrEmpty(idOrbit))
            {
                throw new Exception("Conta Pai sem ID");
            }
            return idOrbit;
        }
        public int UpdateAccountStatusSucess(Account account, ContasContabeisOutput output)
        {
            return Wrapper.ExecuteNonQuery(@$"UPDATE OACT SET ""U_TAX4_LIDO"" = 'S', ""U_TAX4_Stat"" = 'Carga Efetuada', ""U_TAX4_IdRet"" = '{output.id}' WHERE ""AcctCode"" = '{account.AcctCode}'");
        }
        public int UpdateAccountStatusError(Account account, ContasContabeisError output)
        {
            string messageError = string.Empty;
            foreach (Error item in output.errors)
            {
                messageError += item.msg + " - " + item.param + " - ";
            }
            return Wrapper.ExecuteNonQuery(@$"UPDATE OACT SET ""U_TAX4_LIDO"" = 'N', ""U_TAX4_Stat"" = '{messageError}' WHERE ""AcctCode"" = '{account.AcctCode}'");
        }
    }
}

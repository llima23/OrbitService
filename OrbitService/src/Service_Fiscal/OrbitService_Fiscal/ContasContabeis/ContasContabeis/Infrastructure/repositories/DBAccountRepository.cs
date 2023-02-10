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
        //public string QUERY = @"SELECT 
        //                          ""AcctCode"",
        //                          ""AcctName"",
        //                          ""U_TAX4_LIDO"", 
        //                          ""FatherNum"",
        //                          ""Levels"", 
        //                          CASE ""Postable""
        //                             WHEN 'Y' THEN ('A')
        //                             WHEN 'N' THEN ('S')
        //                             END AS ""AcctType"",
        //                             COALESCE(""U_TAX4_NatGrpCnt"",'') as ""OriginCode"",
        //                          ""CreateDate"",
        //                             ""U_TAX4_IdRet""
        //                          FROM OACT 
        //                          WHERE ""U_TAX4_LIDO"" = 'N'";

        public string QUERY = @"SELECT * FROM (
                                SELECT
                                T0.""AcctCode"",
                                T0.""AcctName"",
                                T0.""U_TAX4_LIDO"",
                                T0.""FatherNum"",
                                T0.""Levels"",
                                CASE T0.""Postable""WHEN 'Y' THEN 'A'WHEN 'N' THEN 'S'END AS ""AcctType"",
                                COALESCE(T0.""U_TAX4_NatGrpCnt"",'') AS ""OriginCode"",
                                T0.""CreateDate"",
                                T0.""U_TAX4_IdRet"",
                                T0.""GroupMask""
                                FROM OACT T0
                                JOIN OACT T1 ON T0.""FatherNum"" = T1.""AcctCode"" AND T1.""U_TAX4_IdRet"" IS NOT NULL
                                WHERE COALESCE(t0.""U_TAX4_LIDO"",'N') = 'N' AND T0.""U_TAX4_IdRet"" IS NULL AND T0.""Levels"" > '1'
                                UNION
                                SELECT
                                T0.""AcctCode"",
                                T0.""AcctName"",
                                T0.""U_TAX4_LIDO"",
                                T0.""FatherNum"",
                                T0.""Levels"",
                                CASE T0.""Postable""WHEN 'Y' THEN 'A'WHEN 'N' THEN 'S'END AS ""AcctType"",
                                COALESCE(T0.""U_TAX4_NatGrpCnt"",'') AS ""OriginCode"",
                                T0.""CreateDate"",
                                T0.""U_TAX4_IdRet"",
                                T0.""GroupMask""
                                FROM OACT T0
                                WHERE COALESCE(t0.""U_TAX4_LIDO"",'N') = 'N'
                                AND T0.""U_TAX4_IdRet"" IS NULL
                                AND T0.""Levels"" = '1' ) SQ 
                                ORDER BY SQ.""GroupMask"", SQ.""Levels"", SQ.""AcctCode""";
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
        public int UpdateStatusErrorValidation(Account account, string message)
        {
            return Wrapper.ExecuteNonQuery(@$"UPDATE ""OACT"" SET ""U_TAX4_LIDO"" = 'N', ""U_TAX4_Stat"" = '{message}' WHERE ""AcctCode"" = '{account.AcctCode}'");
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
            try
            {
                foreach (Error item in output.errors)
                {
                    messageError += item.msg + " - " + item.param + " - ";
                }
            }
            catch
            {
                messageError = output.message;
            }
            return Wrapper.ExecuteNonQuery(@$"UPDATE OACT SET ""U_TAX4_LIDO"" = 'N', ""U_TAX4_Stat"" = '{messageError}' WHERE ""AcctCode"" = '{account.AcctCode}'");
        }
    }
}

using AccountService_LancamentoContabil.LancamentoContabil.infrastructure.documents.entities;
using AccountService_LancamentoContabil.LancamentoContabil.service;
using Newtonsoft.Json;
using OrbitLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.infrastructure.repositories
{
    public class DBLancamentoContabilRepository : IDBLancamentoContabilRepository
    {
        private IWrapper wrapper;
        public string queryHeaderLCM = @"SELECT TOP 10 DISTINCT
                                                T0.""TransId"",
		                                        T0.""TaxDate"" as ""PostDate"",
		                                        ""Memo"" as ""Description"",
		                                        ""ECDPosTyp"" as ""Entry_Type"",
		                                        ""U_TAX4_EstabFiscal"" as ""Establishment_id"",
                                                ""U_TAX4_EstabID"" as ""BranchId"",
                                                ""U_TAX4_IdRetLote"" as ""IdOrbitLote""
		                                        FROM OJDT T0
                                                JOIN JDT1 T1 ON T0.""TransId"" = T1.""TransId""
                                                JOIN ""@TAX4_LCONFIGADDON"" T2 ON T1.""BPLId"" = T2.""U_TAX4_Empresa""
                                                WHERE T0.""U_TAX4_CodInt"" = '0'";

        public string queryUpdateLCM = @"SELECT TOP 10 DISTINCT
                                                T0.""TransId"",
		                                        T0.""TaxDate"" as ""PostDate"",
		                                        ""Memo"" as ""Description"",
		                                        ""ECDPosTyp"" as ""Entry_Type"",
		                                        ""U_TAX4_EstabFiscal"" as ""Establishment_id"",
                                                ""U_TAX4_EstabID"" as ""BranchId"",
                                                ""U_TAX4_IdRetLote"" as ""IdOrbitLote""
		                                        FROM OJDT T0
                                                JOIN JDT1 T1 ON T0.""TransId"" = T1.""TransId""
                                                JOIN ""@TAX4_LCONFIGADDON"" T2 ON T1.""BPLId"" = T2.""U_TAX4_Empresa""
                                                WHERE T0.""U_TAX4_CodInt"" = '1' AND T0.""U_TAX4_Stat"" is null or T0.""U_TAX4_Stat"" like '' "; 
        public string queryLineLCM = @"SELECT
	                                    T0.""TransId"",
	                                    CT.""U_TAX4_IdRet"" as ""AccountId"",
	                                    T0.""Debit"" as ""Debit"",
	                                    T0.""Credit"" as ""Credit"",
	                                    T0.""LineMemo"" as ""Historic"",
	                                    COALESCE(PR.""U_TAX4_IdRet"",'') as ""CostCenterId""
	                                    FROM JDT1 T0
	                                    JOIN OACT CT ON T0.""Account"" = CT.""AcctCode""
	                                    LEFT JOIN OPRC PR ON T0.""ProfitCode"" = PR.""PrcCode""";
        private string jsonConvert { get; set; }
        private DataSet queryResult;
        public DBLancamentoContabilRepository(IWrapper wrapper)
        {
            this.wrapper = wrapper;
        }
        public List<LancamentoContabilB1> ReturnListLCMToOrbit()
        {
            List<LancamentoContabilB1> listLcm = new List<LancamentoContabilB1>();
            queryResult = wrapper.ExecuteQuery(queryHeaderLCM);
            dynamic result = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]));
            foreach (var item in result)
            {
                LancamentoContabilB1 lcm = new LancamentoContabilB1();
                lcm.header = JsonConvert.DeserializeObject<documents.entities.Header>(JsonConvert.SerializeObject(item));
                queryResult = wrapper.ExecuteQuery(queryLineLCM + $@"WHERE T0.""TransId"" = {lcm.header.TransId}");
                jsonConvert = Convert.ToString(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0])));
                lcm.lines = JsonConvert.DeserializeObject<List<Lines>>(jsonConvert);
                listLcm.Add(lcm);
            }

            return listLcm;
        }

        public List<LancamentoContabilB1> ReturnListLCMToUpdate()
        {
            List<LancamentoContabilB1> listLcm = new List<LancamentoContabilB1>();
            queryResult = wrapper.ExecuteQuery(queryUpdateLCM);
            dynamic result = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]));
            foreach (var item in result)
            {
                LancamentoContabilB1 lcm = new LancamentoContabilB1();
                lcm.header = JsonConvert.DeserializeObject<documents.entities.Header>(JsonConvert.SerializeObject(item));
                listLcm.Add(lcm);
            }
            return listLcm;
        }

        public bool VerifyIfFieldsIsNotEmpty()
        {
            return true;
        }
        public int UpdateAccountStatusSucess(string transIds, string idRet)
        {
            return wrapper.ExecuteNonQuery(@$"UPDATE OJDT SET ""U_TAX4_LIDO"" = 'S', ""U_TAX4_Stat"" = 'Carga Efetuada', ""U_TAX4_CodInt"" = '2', ""U_TAX4_IdRet"" = '{idRet}' WHERE ""TransId"" = '{transIds}'");
        }
        public int UpdateAccountStatusError(string transId, LancamentoContabilError output)
        {
            return wrapper.ExecuteNonQuery(@$"UPDATE OJDT SET ""U_TAX4_LIDO"" = 'N', ""U_TAX4_Stat"" = '{""}', ""U_TAX4_CodInt"" = '3' WHERE ""TransId"" in ({transId})");
        }

        public int UpdateLCMFalseValidation(LancamentoContabilB1 lcm, string message)
        {
            return wrapper.ExecuteNonQuery(@$"UPDATE OJDT SET ""U_TAX4_LIDO"" = 'N', ""U_TAX4_Stat"" = '{message}', ""U_TAX4_CodInt"" = '3' WHERE ""TransId"" in ({lcm.header.TransId})");
        }
        public int UpdateAccountStatusSucessIdLote(string transIds, LancamentoContabilOutput output)
        {
            return wrapper.ExecuteNonQuery(@$"UPDATE OJDT SET ""U_TAX4_LIDO"" = 'S', ""U_TAX4_Stat"" = 'Última integración: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}', ""U_TAX4_IdRetLote"" = '{""}', ""U_TAX4_CodInt"" = '1' WHERE ""TransId"" in ({transIds})");
        }

        public int UpdateAccountStatusErrorException(int lcm, string error)
        {
            return wrapper.ExecuteNonQuery(@$"UPDATE OJDT SET ""U_TAX4_LIDO"" = 'S', ""U_TAX4_Stat"" = '{error}', ""U_TAX4_IdRet"" = null, ""U_TAX4_CodInt"" = '3' WHERE ""TransId"" = '{lcm}'");
        }

        public int UpdateEstabFiscalInLConfigAddon(string estabFiscal, string branchId)
        {
            return wrapper.ExecuteNonQuery(@$"UPDATE ""@TAX4_LCONFIGADDON"" SET ""U_TAX4_EstabFiscal"" = '{estabFiscal}' WHERE ""U_TAX4_EstabID"" = '{branchId}'");
        }
    }
}

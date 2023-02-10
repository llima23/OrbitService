using B1Library.Implementations.Repositories;
using Newtonsoft.Json;
using OrbitLibrary.Data;
using OrbitService_Fiscal.Pagamentos.Pagamentos.mapper.documents.entities;
using OrbitService_Fiscal.Pagamentos.Pagamentos.repository;
using OrbitService_Fiscal.Pagamentos.Pagamentos.service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static B1Library.Implementations.Repositories.DBTableNameRepository;

namespace OrbitService_Fiscal.Pagamentos.Pagamentos.mapper.repositories
{
    public class DBPaymentsRepository : IDBPaymentsRepository
    {
        private IWrapper wrapper;
        private string jsonConvert;
        private string idOrbit { get; set; }
        private DataSet queryResult;
        private DBTableNameRepository dBTableNameRepository;

        public DBPaymentsRepository(IWrapper wrapper)
        {
            this.wrapper = wrapper;
            dBTableNameRepository = new DBTableNameRepository();
        }
        public List<PaymentsB1> ReturnListOfPaymentsToOrbit()
        {
            List<PaymentsB1> listPayment = new List<PaymentsB1>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOutboundNFe)
            {
                DataSet queryResult = wrapper.ExecuteQuery(QueryB1ListPayments(tableName.TableHeader));
                dynamic result = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(queryResult.Tables[0]));
                foreach (var item in result)
                {
                    PaymentsB1 payments = new PaymentsB1();
                    payments = JsonConvert.DeserializeObject<PaymentsB1>(JsonConvert.SerializeObject(item));
                    listPayment.Add(payments);
                }
            }
            return listPayment;
        }
        public int UpdateAccountStatusSucess(PaymentsB1 payment, PagamentosOutput output)
        {
            return wrapper.ExecuteNonQuery(@$"UPDATE ORCT SET ""U_TAX4_CodInt"" = '2', ""U_TAX4_Stat"" = 'Carga Efetuada', ""U_TAX4_IdRet"" = '{output.data._id}' WHERE ""DocEntry"" = '{payment.DocEntryORCT}'");
        }
        public int UpdateAccountStatusError(PaymentsB1 payment, PagamentosErro output)
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
            return wrapper.ExecuteNonQuery(@$"UPDATE ORCT SET ""U_TAX4_CodInt"" = '3', ""U_TAX4_Stat"" = '{messageError}' WHERE ""DocEntry"" = '{payment.DocEntryORCT}'");
        }
        private string QueryB1ListPayments(string tableNameHeader)
        {
            string QUERY = @$"SELECT
		                            T0.""DocNum"", 
		                            T0.""DocEntry"",
		                            T0.""U_TAX4_IdRet"",
                                    TC0.""DocEntry"" as ""DocEntryORCT"",
                                    T0.""Serial"",
		                            CO.""U_TAX4_EstabID"", 
		                            CASE T0.""Model""
                                    WHEN '39' THEN ('nfe')
                                    WHEN '46' THEN ('nfse')
                                    ELSE 'outro'
                                    END AS ""dfe"",
		                            TC0.""TaxDate"",
		                            TC0.""DocTotal""
                                    FROM
                                    {tableNameHeader} T0
                                    LEFT JOIN RCT2 TC1 ON T0.""DocEntry"" = TC1.""baseAbs"" AND TC1.""InvType"" = 13
                                    LEFT JOIN ORCT TC0 ON TC0.""DocEntry"" = TC1.""DocNum""
                                    LEFT JOIN ""@TAX4_LCONFIGADDON"" CO ON TC0.""BPLId"" = CO.""U_TAX4_Empresa"" 
                                    WHERE T0.""CANCELED""='N' 
                                    AND TC0.""Canceled""='N' 
                                    AND T0.""U_TAX4_IdRet"" <> '' 
                                    AND T0.""U_TAX4_IdRet"" is not null
                                    AND TC0.""U_TAX4_CodInt"" = '0'";
            return QUERY;
        }

    }
}

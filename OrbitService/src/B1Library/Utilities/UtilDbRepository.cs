using B1Library.Applications;
using B1Library.Documents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using static B1Library.Implementations.Repositories.DBTableNameRepository;

namespace B1Library.Utilities
{
    public class UtilDbRepository
    {
        public void addInvoiceEntriesToList(List<Invoice> invoices, DataSet dsResult)
        {
            if (dsResult != null)
            {
                string formattedJson = returnJsonB1Formated(dsResult);
                if (formattedJson != null)
                {
                    invoices.AddRange(JsonConvert.DeserializeObject<List<Invoice>>(formattedJson));
                }
            }
        }
        public string returnJsonB1Formated(DataSet dataSet)
        {
            if (dataSet.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            string result = dataSet.Tables[0].Rows[0].ItemArray[0].ToString();
            result = Regex.Replace(result, @"(\\"")", @"""");
            result = Regex.Replace(result, @"(""\[)", "[");
            result = Regex.Replace(result, @"(\]"")", "]");
            result = Regex.Replace(result, @"(""{)", "{");
            result = Regex.Replace(result, @"(}"",)", "},");
            return result;
        }
        public string generateUpdateDocumentCommand(DocumentStatus documentData, TableName tableName)
        {
            StringBuilder sb = new StringBuilder();
            //TODO: update command to dynamic generate table name
    
            sb.Append(@$"UPDATE {tableName.TableHeader} SET ");
            sb.AppendLine(@$"""U_TAX4_Stat"" = '{documentData.GetStatusMessage().Replace("'", "")}'");
            sb.AppendLine(@$",""U_TAX4_CodInt"" = '{(int)documentData.Status}'");
            sb.AppendLine(@$",""U_TAX4_IdRet"" = '{documentData.IdOrbit}' ");
            sb.AppendLine(@$",""U_TAX4_Chave"" = '{documentData.ChaveDeAcessoNFe}' ");
            sb.AppendLine(@$",""U_TAX4_Prot"" = '{documentData.ProtocoloNFe}' ");
            sb.AppendLine(@$",""U_TAX4_IdComu"" = '{documentData.CommunicationId}' ");
            sb.AppendLine(@$"WHERE ");
            sb.AppendLine(@$"""DocEntry"" in ({documentData.DocEntry}, {documentData.BaseEntry})");
            return sb.ToString();
        }
    }
}

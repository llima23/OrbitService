using B1Library.Documents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace B1Library.Utilities
{
    public class Util
    {
        private static CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");
        public void addInvoiceEntriesToList(List<Invoice> invoices, DataSet dsResult)
        {
            if(dsResult != null)
            {
                string formattedJson = returnJsonB1Formated(dsResult);
                if (formattedJson != null)
                {
                    invoices.AddRange(JsonConvert.DeserializeObject<List<Invoice>>(formattedJson));
                }
            }
        }

        public string getTableQueryCommandOtherDocuments(string targetname)
        {
            return $@"SELECT * FROM ""{targetname.Trim()}"" WHERE ""ModeloDocumento"" IN ('FAT','R','RPA') and ""CargaFiscal"" = 0 FOR JSON";
        }

        public string getTableQueryCommandInboundCTe(string targetname)
        {
            return $@"SELECT * FROM ""{targetname.Trim()}"" WHERE  ""ModeloDocumento"" 
                            IN ('CT-E') and ""CargaFiscal"" = 0 FOR JSON";
        }

        public string getTableQueryCommandInboundNFe(string targetname)
        {
            return $@"SELECT * FROM ""{targetname.Trim()}"" WHERE  ""ModeloDocumento"" 
                            IN ('55') and ""CargaFiscal"" = 0 FOR JSON";
        }

        public string getTableQueryCommandInboundNFSe(string targetname)
        {
            return $@"SELECT * FROM ""{targetname.Trim()}"" WHERE  ""ModeloDocumento"" 
                            IN ('NFS-e') and ""CargaFiscal"" = 0 FOR JSON";
        }

        public string getTableQueryCommandOutboundNFe(string targetname)
        {
            return $@"SELECT * FROM ""{targetname.Trim()}"" WHERE  ""ModeloDocumento"" 
                            IN ('55') and ""CargaFiscal"" <> 0 FOR JSON";
        }

        public string getTableQueryCommandOutboundNFSe(string targetname)
        {
            return $@"SELECT * FROM ""{targetname.Trim()}"" WHERE  ""ModeloDocumento"" 
                            IN ('NFS-E') and ""CargaFiscal"" <> 0 FOR JSON";
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

        public string generateUpdateDocumentCommand(DocumentStatus documentData)
        {
            StringBuilder sb = new StringBuilder();
            //TODO: update command to dynamic generate table name
            sb.Append(@$"UPDATE OINV SET ");
            sb.AppendLine(@$"""U_TAX4_Stat"" = '{documentData.GetStatusMessage()}'");
            sb.AppendLine(@$",""U_TAX4_CodInt"" = '{(int)documentData.Status}'");
            sb.AppendLine(@$",""U_TAX4_IdRet"" = '{documentData.IdOrbit}' ");
            sb.AppendLine(@$"WHERE ");
            sb.AppendLine(@$"""DocEntry"" = {documentData.DocEntry}");
            return sb.ToString();
        }
        public double GetTaxTypeB1Sum(string TipoImpostoOrbit, Invoice invoice)
        {
            double sum = 0.00;
            foreach (CabecalhoLinha item in invoice.CabecalhoLinha)
            {
                var taxes = item.ImpostoLinha.Where(tl => tl.TipoImpostoOrbit == TipoImpostoOrbit && tl.SimOuNaoDesoneracao == "N");
                sum += taxes.Sum(i => i.ValorImposto);
            }
            return sum;
        }
        public double GetTaxTypeB1SumDeson(string TipoImpostoOrbit, Invoice invoice)
        {
            double sum = 0.00;
            foreach (CabecalhoLinha item in invoice.CabecalhoLinha)
            {
                sum = 0.00;
                var taxesDeson = item.ImpostoLinha.Where(tl => tl.TipoImpostoOrbit == TipoImpostoOrbit && tl.SimOuNaoDesoneracao == "Y");
                sum += taxesDeson.Sum(i => i.ValorImposto);
            }
            return sum;
        }
        public double GetTaxTypeB1VBcSum(string TipoImpostoOrbit, Invoice invoice)
        {
            double sum = 0.00;
            foreach (CabecalhoLinha item in invoice.CabecalhoLinha)
            {
                var taxes = item.ImpostoLinha.Where(tl => tl.TipoImpostoOrbit == TipoImpostoOrbit);
                sum += taxes.Sum(i => i.ValorBaseImposto);
            }
            return sum;
        }
        public double GetVSumDespAdic(string TipoDespesa, Invoice invoice)
        {
            double sum = 0.00;
            foreach (CabecalhoLinha item in invoice.CabecalhoLinha)
            {
                var taxes = item.DespesaAdicional.Where(tl => tl.TipoDespesa == TipoDespesa);
                sum += taxes.Sum(i => i.ValorUnitarioDespesa);
            }
            return sum;
        }

        public double GetTaxTypeB1VBcSumForItem(List<ImpostoLinha> ListimpostoLinha, string TipoImposto)
        {
            double result = 0.00;
            foreach(ImpostoLinha impostoLinha in ListimpostoLinha.Where(i => i.TipoImpostoOrbit == TipoImposto))
            {
                result += impostoLinha.ValorBaseImposto;
            }
            return result;

        }
        public double GetTaxTypeB1VImpSumForItem(List<ImpostoLinha> ListimpostoLinha, string TipoImposto)
        {
            double result = 0.00;
            foreach (ImpostoLinha impostoLinha in ListimpostoLinha.Where(i => i.TipoImpostoOrbit == TipoImposto))
            {
                result += impostoLinha.ValorImposto;
            }
            return result;

        }
        public double GetVProdSum(List<CabecalhoLinha> cabecalhoLinha)
        {
            double sum = 0.00;
            foreach (var item in cabecalhoLinha)
            {
                sum += item.ValorTotalLinnha;
            }
            return sum;
        }
        public double GetValorSomaDescontoItens(List<CabecalhoLinha> cabecalhoLinha)
        {
            double sum = 0.00;
            foreach (var item in cabecalhoLinha)
            {
                sum += item.ValorTotalDescontoLinha;
            }
            return sum;
        }
        public string ConvertDateB1ToFormatOrbit(DateTime Data, string DocTime = null)
        {
            string DataOrbit;
            if (string.IsNullOrEmpty(DocTime))
            {
                DataOrbit = Data.ToString("yyyy-MM-dd");
            }
            else
            {
                DataOrbit = Data.ToString("yyyy-MM-dd") + "T" + DocTime.Substring(0, 2) + ":" + "00" + ":00" + "-03:00";
            }
            return DataOrbit;
        }

        public string ToOrbitString(double valor)
        {
            if (valor > 0)
            {
                string ValueString = string.Format(cultureInfo, "{0:0.000000}", valor);
                ValueString = ValueString.Remove(ValueString.Length - 4);
                return ValueString;
            }
            else
            {
                return "0.00";
            }
        }
    }
}

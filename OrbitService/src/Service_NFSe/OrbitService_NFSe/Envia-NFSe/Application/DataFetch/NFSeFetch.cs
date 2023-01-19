using _4TAX_Service.Common.Domain;
using _4TAX_Service.Common.Handlers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrbitLibrary.Data;
using OrbitLibrary.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace _4TAX_Service.Application
{
    public class NFSeFetch : NFSeHandlerODBC
    {

        private IWrapper dbWrapper;

        public NFSeFetch(IWrapper wrapper)
        {
            dbWrapper = wrapper;
        }

        public dynamic GetHeader()
        {
            DataSet dsResult = new DataSet();
            try
            {
                string command = @$"SELECT TOP 10 * FROM ""BUSCADADOSNFSE""";
                //string command = @$"SELECT * FROM ""BUSCADADOSNFSE""";
                dsResult = dbWrapper.ExecuteQuery(GetCommandSQLHeader());  //TODO Resources                
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dsResult.Tables[0]));
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public dynamic GetLines(string DocEntry)
        {
            DataSet dsResult = new DataSet();
            try
            {
                //  string command = @$"SELECT * FROM ""BUSCADADOSNFSELINHAS"" WHERE ""DocEntry"" = {DocEntry}";
                dsResult = dbWrapper.ExecuteQuery(GetCommandSQLLines(DocEntry));
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dsResult.Tables[0]));
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public dynamic GetEmails(string cardCode)
        {
            DataSet dsResult = new DataSet();
            try
            {
                dsResult = dbWrapper.ExecuteQuery(GetCommandListEmails(cardCode));
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dsResult.Tables[0]));
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public List<NFSeB1Object> GetListNFSe()
        {
            List<NFSeB1Object> NFSeList = new List<NFSeB1Object>();

            try
            {
                dynamic data = GetHeader();
                if (data != null)
                {
                    if (data.Count > 0)
                    {
                        Logs.InsertLog($"Count Notas: {data.Count}");
                    }
                    foreach (var item in data)
                    {
                        NFSeB1Object nFSeB1Object = new NFSeB1Object(Convert.ToInt32(item.DocEntry));
                        nFSeB1Object = JsonConvert.DeserializeObject<NFSeB1Object>(JsonConvert.SerializeObject(item));
                        string listEmails = Convert.ToString(GetEmails(nFSeB1Object.CardCode));
                        nFSeB1Object.Emails = JsonConvert.DeserializeObject<List<Emails>>(listEmails);

                        dynamic dataLines = GetLines(item.DocEntry.ToString());
                        nFSeB1Object.Linhas = new List<Linha>();
                        nFSeB1Object.LinesTax = new List<LineTax>();
                        nFSeB1Object.LinesTaxWithholding = new List<LineTaxWithholding>();
                        foreach (var line in dataLines)
                        {
                            Linha linha = new Linha();
                            linha = JsonConvert.DeserializeObject<Linha>(JsonConvert.SerializeObject(line));

                            nFSeB1Object.Linhas.Add(linha);

                            dynamic dataTax = GetLineTax(line.DocEntry.ToString(), line.LineNum.ToString());

                            foreach (var LinesTax in dataTax)
                            {
                                LineTax oLineTax = new LineTax();
                                oLineTax = JsonConvert.DeserializeObject<LineTax>(JsonConvert.SerializeObject(LinesTax));
                                nFSeB1Object.LinesTax.Add(oLineTax);
                            }

                            if (!string.IsNullOrEmpty(linha.WtLiable))
                            {
                                if (linha.WtLiable == "Y")
                                {
                                    dynamic dataTaxWithholding = GetLineTaxWithholding(line.DocEntry.ToString(), line.LineNum.ToString());

                                    foreach (var LinesTaxWithholding in dataTaxWithholding)
                                    {
                                        LineTaxWithholding oLineTax = new LineTaxWithholding();
                                        oLineTax = JsonConvert.DeserializeObject<LineTaxWithholding>(JsonConvert.SerializeObject(LinesTaxWithholding));
                                        nFSeB1Object.LinesTaxWithholding.Add(oLineTax);
                                    }
                                }
                            }

                        }

                        NFSeList.Add(nFSeB1Object);
                    }
                }


                return NFSeList;
            }
            catch (Exception ex)
            {
                Logs.InsertLog($"Erro ao Processar NFSe: {ex}");
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public dynamic GetLineTaxWithholding(string DocEntry, string LineNum)
        {
            DataSet dsResult = new DataSet();
            try
            {
                //TODO
                string command = @$"SELECT COALESCE(T0.""Rate"",0) as ""RATE"", 
                                           COALESCE(T0.""WTAmnt"",0) as ""WTAMNT"",
                                           COALESCE(T2.""U_TAX4_TpImp"",'') as ""U_TAX4_TpImp"",
                                           ""Doc1LineNo""
                                           FROM ""INV5"" T0
                                           INNER JOIN ""OWHT"" T1 on T0.""WTCode"" = T1.""WTCode""
                                           INNER JOIN ""OWTT"" T2 on T1.""WTTypeId"" = T2.""WTTypeId""
                                   WHERE T0.""AbsEntry"" = {DocEntry} and T0.""Doc1LineNo"" = {LineNum}";
                dsResult = dbWrapper.ExecuteQuery(command);
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dsResult.Tables[0]));
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public dynamic GetLineTax(string DocEntry, string LineNum)
        {
            DataSet dsResult = new DataSet();
            try
            {
                //TODO
                string command = @$"SELECT COALESCE(T0.""TaxRate"",0) as ""TaxRate"", 
                                           COALESCE(T0.""TaxSum"",0) as ""TaxSum"",
                                           T1.""U_TAX4_TpImp"",
		                                   ""CSTfPIS"",
                                           ""CSTfCOFINS"",
                                           T0.""LineNum"",
                                           T0.""TaxInPrice""
                                           FROM ""INV4"" T0
                                           INNER JOIN ""OSTT"" T1 on T0.""staType"" = T1.""AbsId"" 
                                           INNER JOIN INV1 T2 on T0.""DocEntry"" = T2.""DocEntry"" and T0.""LineNum"" = T2.""LineNum""
                                   WHERE T0.""DocEntry"" = {DocEntry} and T0.""LineNum"" = {LineNum}";
                dsResult = dbWrapper.ExecuteQuery(command);
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dsResult.Tables[0]));
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }

        private string GetCommandSQLHeader()
        {
            string result = $@"SELECT TOP 10
	 DISTINCT T0.""DocEntry"",
	 T0.""DocNum"",
	 T0.""CANCELED"",
	 T0.""DocDate"",
	 T0.""CardCode"",
	 T0.""Address"" AS EnderecoT,
	 T0.""CardName"",
	 COALESCE(T0.""DiscPrcnt"",0) AS DiscPrcnt,
	 CAST(T0.""Header"" AS VARCHAR) AS Comments,
	 T0.""GroupNum"",
	 T0.""PeyMethod"",
	 T0.""BPChCode"",
	 T1.""U_TAX4_CodServ"",
	 T0.""Serial"",
	 T0.""SeriesStr"",
	 T0.""U_TAX4_tpOperacao"",
	 T0.""U_TAX4_tpTribNfse"",
	 --T0.""U_TAX4_TpOpNFSe"",
	 T0.""U_TAX4_NatOpNFSe"",
     T0.""U_TAX4_tpOperacao"",
	 T2.""ObjectType"",
	 T2.""TaxId0"",
	 T2.""TaxId1"",
	 T2.""TaxId3"",
	 T2.""TaxId4"",
	 T2.""TaxId5"",
	 T2.""StreetS"" AS RuaT,
	 CAST (T2.""BuildingS"" AS VARCHAR) AS ComplementoT,
	 T2.""BlockS"" AS BairroT,
	 T2.""ZipCodeS"" AS CepT,
	 T2.""CountyS"" AS CodMunicipioT,
	 T2.""StateS"" AS EstadoT,
	 T2.""CityS"" AS CidadeT,
	 T2.""AddrTypeS"" AS LogradouroT,
	 T2.""StreetNoS"" AS NumeroRuaT,
	 T3.""AddrType"",
	 T3.""E_Mail"",
	 T3.""NTSWebSite"",
	 T3.""Fax"",
	 T3.""Phone1"",
	 T3.""Phone2"",
	 T3.""ShipToDef"",
	 T3.""CardFName"",
	 T7.""County"",
	 T7.""TaxIdNum"",
	 T7.""TaxIdNum3"",
	 T7.""TaxIdNum2"",
	 T7.""AddtnlId"",
	 T7.""City"" AS CidadeP,
	 T7.""Block"",
	 T7.""ZipCode"" AS CepP,
	 T7.""StreetNo"" AS RuaNumeroP,
	 T7.""Street"" AS RuaP,
	 T7.""BPLName"",
	 T7.""BPLId"",
	 T7.""State"",
	 T6.""U_TAX4_TmZn"",
	 T6.""U_TAX4_RegEspTrib"",
	 T6.""U_TAX4_tipoRPS"",
	 T6.""U_TAX4_itCultural"",
	 T6.""U_TAX4_ContAuth"",
	 T6.""U_TAX4_justf"",
	 T6.""U_TAX4_Operacao"",
	 T6.""U_TAX4_regTrib"",
	 T8.""U_TAX4_Uf"",
	 T8.""U_TAX4_Cod"",
	 T12.""CntCodNum"",
	 T13.""IbgeCode"",
	 T14.""U_TAX4_NatOper"",
	 T15.""U_TAX4_condPag"",
	 T16.""U_TAX4_FormaPagto"",
	 T0.""DocTotal"",
	 T0.""VatSum"",
	 T0.""Model"",
	 T1.""WtLiable"",
	 T0.""NfeValue"",
	 CAST(T0.""U_TAX4_Stat"" AS VARCHAR) AS U_TAX4_Stat,
	 T0.""U_TAX4_IdRet"",
	 CASE WHEN T20.""MltpBrnchs"" = 'Y' 
THEN T19.""U_TAX4_EstabID"" 
ELSE T18.""U_TAX4_EstabID"" 
END AS ""BranchId"",
COALESCE(T6.""U_TAX4_EnvEm"",'')			   AS ""EnviaEmail"",
COALESCE(T6.""U_TAX4_NumLote"",0)             AS ""NumeroLote""
FROM ""OINV"" T0 
LEFT JOIN ""INV1"" T1 ON T0.""DocEntry"" = T1.""DocEntry"" 
LEFT JOIN ""INV12"" T2 ON T0.""DocEntry"" = T2.""DocEntry"" 
INNER JOIN ""OCRD"" T3 ON T0.""CardCode"" = T3.""CardCode"" 
INNER JOIN ""OITM"" T4 ON T1.""ItemCode"" = T4.""ItemCode"" 
INNER JOIN ""NNM1"" T5 ON T0.""Series"" = T5.""Series"" 
INNER JOIN ""@TAX4_CONFIG"" T6 ON T0.""BPLId"" = T6.""U_TAX4_Filial"" 
INNER JOIN ""OBPL"" T7 ON T6.""U_TAX4_Filial"" = T7.""BPLId"" 
INNER JOIN ""@TAX4_UF"" T8 ON T7.""State"" = T8.""U_TAX4_Uf"" 
LEFT JOIN ""INV4"" T9 ON T0.""DocEntry"" = T9.""DocEntry"" 
LEFT JOIN ""INV13"" T10 ON T0.""DocEntry"" = T10.""DocEntry"" 
AND T9.""LineNum"" = T10.""LineNum"" 
and T9.""ExpnsCode"" = T10.""ExpnsCode"" 
INNER JOIN ""OSTT"" T11 ON T9.""staType"" = T11.""AbsId"" 
INNER JOIN ""OCRY"" T12 ON T2.""CountryS"" = T12.""Code"" 
LEFT JOIN ""OCNT"" T13 ON CAST(T2.""CountyS"" AS VARCHAR) = CAST(T13.""AbsId"" AS VARCHAR) 
LEFT JOIN ""OUSG"" T14 ON T1.""Usage"" = T14.""ID"" 
INNER JOIN ""OCTG"" T15 ON T3.""GroupNum"" = T15.""GroupNum"" 
LEFT JOIN ""OPYM"" T16 ON T0.""PeyMethod"" = T16.""PayMethCod"" 
INNER JOIN ""ONFM"" T17 ON T0.""Model"" = T17.""AbsEntry"" 
LEFT JOIN ""@TAX4_CONFIGADDON"" T18 ON 1 = 1 
LEFT JOIN ""@TAX4_LCONFIGADDON"" T19 ON T19.""U_TAX4_Empresa"" = T0.""BPLId"" 
LEFT JOIN ""OADM"" T20 ON 1 = 1 
WHERE T0.""CANCELED"" = 'N' 
AND T4.""ItemClass"" = 1 
AND T17.""NfmCode"" = 'NFS-e' 
AND T0.""U_TAX4_CodInt"" = '0'
AND T0.""DocDate"" >= T6.""U_TAX4_DateInt""
AND ""U_TAX4_CARGAFISCAL"" = 'N'
AND ""SeqCode"" > 0";

            return result;

        }

        private string GetCommandSQLLines(string docentry)
        {
            //            string result = $@"SELECT T0.""ItemCode"",
            //	 T0.""LineNum"",
            //	 T0.""LineTotal"",
            //	 T0.""U_TAX4_CodServ"",
            //	 T0.""DocEntry"",
            //	 T5.""BPLId"",
            //	 REPLACE(T6.""OSvcCode"",'-1',NULL) AS OSvcCode,
            //	 T6.""U_TAX4_CodAti"",
            //	 T8.""ServiceCD"",
            //	 COALESCE(T0.""U_TAX4_IBPT"",0) as U_TAX4_IBPT,
            //	 T6.""U_TAX4_LisSer"",
            //	 T0.""Quantity"",
            //	 T0.""Price"",
            //	 T6.""ItemName"",
            //	 T6.""U_TAX4_CodCNAE"",
            //	 T6.""U_TAX4_TrMun"",
            //	 T11.""IbgeCode"",
            //	 T0.""DiscPrcnt"",
            //	 T10.""WTTypeId"",
            //	 COALESCE(T9.""LineTotal"",
            //	 0) AS LineTotalDespAd,
            //	 COALESCE(T0.""Rate"",
            //	 0) AS RATE,
            //	 COALESCE(T10.""WTAmnt"",
            //	 0) AS WTAmnt,
            //	 COALESCE(T0.""CSTfPIS"",
            //	 0) AS CSTFPIS,
            //	 COALESCE(T0.""CSTfCOFINS"",
            //	 0) AS CSTFCOFINS,
            //	 COALESCE(T3.""TaxSum"",
            //	 0) AS TAXSUM,
            //	 T3.""staType"",
            //	 COALESCE(T3.""TaxRate"",
            //	 0) AS TAXRATE,
            //    T0.""WtLiable""
            //FROM ""INV1"" T0 
            //INNER JOIN ""INV12"" T2 ON T0.""DocEntry"" = T2.""DocEntry"" 
            //INNER JOIN ""INV4"" T3 ON T0.""DocEntry"" = T3.""DocEntry"" 
            //AND T0.""LineNum"" = T3.""LineNum"" 
            //LEFT JOIN ""INV13"" T4 ON T0.""DocEntry"" = T4.""DocEntry"" 
            //AND T0.""LineNum"" = T4.""LineNum"" 
            //INNER JOIN ""OINV"" T5 ON T0.""DocEntry"" = T5.""DocEntry"" 
            //INNER JOIN ""OITM"" T6 ON T0.""ItemCode"" = T6.""ItemCode"" 
            //INNER JOIN ""OBPL"" T7 ON T5.""BPLId"" = T7.""BPLId"" 
            //LEFT JOIN ""OSCD"" T8 ON T6.""OSvcCode"" = T8.""AbsEntry"" 
            //AND T7.""County"" = T8.""County"" 
            //LEFT JOIN ""INV3"" T9 ON T0.""DocEntry"" = T9.""DocEntry"" 
            //AND T0.""LineNum"" = T9.""LineNum"" 
            //LEFT JOIN ""INV5"" T10 ON T8.""AbsEntry"" = T10.""AbsEntry"" 
            //AND T0.""LineNum"" = T10.""LineNum"" 
            //LEFT JOIN ""OCNT"" T11 ON CAST(T2.""County"" AS VARCHAR) = CAST(T11.""AbsId"" AS VARCHAR)
            //WHERE T0.""DocEntry"" = {docentry}";


            string result = $@"SELECT T0.""ItemCode"",
	                         T0.""LineNum"",
	                         T0.""LineTotal"",
	                         T0.""U_TAX4_CodServ"",
	                         T0.""DocEntry"",
	                         T5.""BPLId"",
	                         REPLACE(T6.""OSvcCode"",'-1',NULL) AS OSvcCode,
	                         T6.""U_TAX4_CodAti"",
	                         T8.""ServiceCD"",
	                         COALESCE(T0.""U_TAX4_IBPT"",0) as U_TAX4_IBPT,
	                         T6.""U_TAX4_LisSer"",
	                         T0.""Quantity"",
	                         T0.""Price"",
                             T0.""PriceBefDi"",
	                         T6.""ItemName"",
	                         T6.""U_TAX4_CodCNAE"",
	                         T6.""U_TAX4_TrMun"",
	                         T11.""IbgeCode"",
	                         COALESCE(T0.""DiscPrcnt"",0) as DiscPrcnt,
	                         T0.""WtLiable""
                             FROM ""INV1"" T0 
                             INNER JOIN ""INV12"" T2 ON T0.""DocEntry"" = T2.""DocEntry"" 
                             INNER JOIN ""OINV"" T5 ON T0.""DocEntry"" = T5.""DocEntry"" 
                             INNER JOIN ""OITM"" T6 ON T0.""ItemCode"" = T6.""ItemCode"" 
                             INNER JOIN ""OBPL"" T7 ON T5.""BPLId"" = T7.""BPLId"" 
                             LEFT JOIN ""OSCD"" T8 ON T6.""OSvcCode"" = T8.""AbsEntry"" AND T7.""County"" = T8.""County"" 
                             LEFT JOIN ""OCNT"" T11 ON CAST(T2.""County"" AS VARCHAR) = CAST(T11.""AbsId"" AS VARCHAR)
                             WHERE T0.""DocEntry"" = {docentry}";
            return result;
        }

        private string GetCommandListEmails(string cardCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"SELECT
								COALESCE(T0.""E_MailL"",'') AS ""email""
								FROM ""OCPR"" T0
								WHERE T0.""CardCode"" = '{cardCode}' AND ""Active"" = 'Y' AND ""NFeRcpn"" = 'Y' ");
            return Convert.ToString(sb);
        }

    }
}

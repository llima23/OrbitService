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
                dsResult = dbWrapper.ExecuteQuery(command);  //TODO Resources                
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
                string command = @$"SELECT * FROM ""BUSCADADOSNFSELINHAS"" WHERE ""DocEntry"" = {DocEntry}";
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

        public List<NFSeB1Object> GetListNFSe()
        {
            List<NFSeB1Object> NFSeList = new List<NFSeB1Object>();

            try
            {
                dynamic data = GetHeader();
                if(data != null)
                {
                    if (data.Count > 0)
                    {
                        Logs.InsertLog($"Count Notas: {data.Count}");
                    }
                    foreach (var item in data)
                    {
                        NFSeB1Object nFSeB1Object = new NFSeB1Object(Convert.ToInt32(item.DocEntry));
                        nFSeB1Object = JsonConvert.DeserializeObject<NFSeB1Object>(JsonConvert.SerializeObject(item));
                        dynamic dataLines = GetLines(item.DocEntry.ToString());

                        foreach (var line in dataLines)
                        {
                            Linha linha = new Linha();
                            linha = JsonConvert.DeserializeObject<Linha>(JsonConvert.SerializeObject(line));
                            nFSeB1Object.Linhas = new List<Linha>();
                            nFSeB1Object.Linhas.Add(linha);

                            dynamic dataTax = GetLineTax(line.DocEntry.ToString(), line.LineNum.ToString());
                            nFSeB1Object.LinesTax = new List<LineTax>();
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
                                    nFSeB1Object.LinesTaxWithholding = new List<LineTaxWithholding>();
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
                                           COALESCE(T2.""U_TAX4_TpImp"",'') as ""U_TAX4_TpImp""  
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
                string command = @$"SELECT COALESCE(T0.""TaxRate"",0), 
                                           COALESCE(T0.""TaxSum"",0),
                                           T1.""U_TAX4_TpImp"",
		                                   ""CSTfPIS"",
                                           ""CSTfCOFINS""
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

    }
}

using _4TAX_Service_Atualiza.Common.Domain;
using _4TAX_Service_Atualiza.Common.Handlers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrbitLibrary.Data;
using OrbitLibrary.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace _4TAX_Service_Atualiza.Application
{
    public class NFSeFetch : NFSeHandlerODBC
    {

        private IWrapper dbWrapper;

        public NFSeFetch(IWrapper wrapper)
        {
            dbWrapper = wrapper;
        }

        public dynamic GetDocumments()
        {
            DataSet dsResult = new DataSet();
            try
            {
                string command = @$"SELECT ""U_TAX4_IdRet"", ""BPLId"",""DocEntry"" FROM ""OINV"" WHERE ""U_TAX4_CodInt"" = '1' and ""Model"" = '46'";
                dsResult = dbWrapper.ExecuteQuery(command);  //TODO Resources                
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
                dynamic data = GetDocumments();
                if (data.Count > 0 )
                {
                    Logs.InsertLog($"Notas a Atualizar: {data.Count}");
                }                
                foreach (var item in data)
                {

                    NFSeB1Object nFSeB1Object = new NFSeB1Object(Convert.ToInt32(item.DocEntry));
                    nFSeB1Object = JsonConvert.DeserializeObject<NFSeB1Object>(JsonConvert.SerializeObject(item));
                    NFSeList.Add(nFSeB1Object);
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

    }
}

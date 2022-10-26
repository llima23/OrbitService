using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service_Atualiza.Common.Domain
{
    public class NFSeB1Object
    {
        public NFSeB1Object(Int32 DocEntry)
        {
            this.DocEntry = DocEntry;
        }
        [JsonProperty("DocEntry")]
        public Int32 DocEntry { get; set; }

        [JsonProperty("U_TAX4_IdRet")]
        public string U_TAX4_IdRet { get; set; }

        [JsonProperty("BPLId")]
        public int BPLId { get; set; }

        [JsonProperty("U_TAX4_Stat")]
        public string U_TAX4_Stat { get; set; }
    }
}


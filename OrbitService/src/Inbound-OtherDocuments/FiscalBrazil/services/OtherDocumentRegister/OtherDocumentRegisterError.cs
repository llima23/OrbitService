using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.FiscalBrazil.services.Error
{
    public class Error
    {
        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("param")]
        public string Param { get; set; }
    }

    public class OtherDocumentRegisterError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }
    }

}

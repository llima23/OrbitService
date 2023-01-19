using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrbitService.InboundNFSe.services.NFSeDocumentRegister
{
    public class NFSeDocumentRegisterError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]

        public List<Error> Errors { get; set; }
        public class Error
        {
            [JsonProperty("msg")]
            public string Msg { get; set; }

            [JsonProperty("param")]
            public string Param { get; set; }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.InboundOtherDocuments.services.Output
{
    public class OtherDocumentRegisterOutput
    {
        public string dfe { get; set; } = "OutroDocumento";
        public Data data { get; set; }
    }
    public class Data
    {
        public string document_id { get; set; }
        public string message { get; set; }
    }
}

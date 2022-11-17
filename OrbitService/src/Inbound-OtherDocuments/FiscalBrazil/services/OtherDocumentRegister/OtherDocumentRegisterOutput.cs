using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.FiscalBrazil.services.Output
{
    public class OtherDocumentRegisterOutput
    {
        public string dfe { get; set; } = "OutroDocumento";
        public Data data { get; set; }
    }
    public class Data
    {
        public string _id { get; set; }
        public string period { get; set; }
        public string branchId { get; set; }
        public string tipoOperacao { get; set; }
        public string dfe { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string created_by_name { get; set; }
        public string created_by_email { get; set; }
        public string updated_by_name { get; set; }
        public string updated_by_email { get; set; }
        public string tenantid { get; set; }
    }
}

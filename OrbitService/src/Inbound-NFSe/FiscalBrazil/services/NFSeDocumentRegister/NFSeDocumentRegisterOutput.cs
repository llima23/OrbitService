using Newtonsoft.Json;
using System;

namespace OrbitService.FiscalBrazil.services.NFSeDocumentRegister
{
    public class NFSeDocumentRegisterOutput
    {
        public NFSeDocumentRegisterOutput()
        {
            data = new Data();
        }
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

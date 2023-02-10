using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_COL_Fiscal.LancamentoContabil.service.GET
{
    public class LcmGetOutput
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object deleted_at { get; set; }
        public string tenantid { get; set; }
        public object externalId { get; set; }
        public string integrationId { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public Errors errors { get; set; }
        public List<Success> success { get; set; }
    }
    public class Success
    {
        public string externalId { get; set; }
        public string id { get; set; }
    }

    public class Errors
    {
    }

}

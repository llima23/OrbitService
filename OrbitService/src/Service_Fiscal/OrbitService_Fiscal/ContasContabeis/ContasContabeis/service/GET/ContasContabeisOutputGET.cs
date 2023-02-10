using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.ContasContabeis.ContasContabeis.service.GET
{
    public class ContasContabeisOutputGET
    {
        public List<Datum> data { get; set; }
        public int count { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public int totalPages { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string tenantid { get; set; }
        public string date { get; set; }
        public string origin_code { get; set; }
        public string account_type { get; set; }
        public int level { get; set; }
        public string account_code { get; set; }
        public string alias_account_code { get; set; }
        public string higher_account_code { get; set; }
        public string higher_alias_account_code { get; set; }
        public string account_name { get; set; }
        public string inactivation_date { get; set; }
        public string created_at { get; set; }
        public string dre_group { get; set; }
    }
}

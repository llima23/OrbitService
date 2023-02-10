using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.ContasContabeis.ContasContabeis.service.PUT
{
    public class ContasContabeisOutputPUT
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
        public DateTime inactivation_date { get; set; }
        public DateTime created_at { get; set; }
        public string dre_group { get; set; }
    }
}

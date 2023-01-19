using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_ContasContabeis.ContasContabeis.service
{
    public class ContasContabeisOutput
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
    }
}

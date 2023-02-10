using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_ContasContabeis.ContasContabeis.service
{
    public class ContasContabeisInput
    {
        public string date { get; set; }
        public string account_type { get; set; }
        public string account_code { get; set; }
        public string account_name { get; set; }
        public string higher_account_code_id { get; set; }
        public string origin_code { get; set; }
        public int level { get; set; }
    }
}

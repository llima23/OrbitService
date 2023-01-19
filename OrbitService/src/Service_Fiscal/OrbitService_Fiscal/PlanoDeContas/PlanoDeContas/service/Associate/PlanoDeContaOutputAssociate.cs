using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_PlanoDeContas.PlanoDeContas.service.Associate
{
    public class PlanoDeContaOutputAssociate
    {
        public string id { get; set; }
        public string tenantid { get; set; }
        public string date { get; set; }
        public string origin_code { get; set; }
        public string account_type { get; set; }
        public int level { get; set; }
        public string account_code { get; set; }
        public string higher_account_code { get; set; }
        public string account_name { get; set; }
        public DateTime created_at { get; set; }
        public List<Child> children { get; set; }
    }
    public class Child
    {
        public string id { get; set; }
        public string tenantid { get; set; }
        public string date { get; set; }
        public string origin_code { get; set; }
        public string account_type { get; set; }
        public int level { get; set; }
        public string account_code { get; set; }
        public string higher_account_code { get; set; }
        public string account_name { get; set; }
        public DateTime created_at { get; set; }
        public List<Child> children { get; set; }
    }
}

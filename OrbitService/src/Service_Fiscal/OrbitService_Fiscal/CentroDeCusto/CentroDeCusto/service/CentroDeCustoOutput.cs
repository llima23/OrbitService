using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_CentroDeCusto.CentroDeCusto.service
{
    public class CentroDeCustoOutput
    {
        public string id { get; set; }
        public string tenantid { get; set; }
        public string cost_center_code { get; set; }
        public string description { get; set; }
        public string created_by_name { get; set; }
        public string created_by_email { get; set; }
        public string updated_by_name { get; set; }
        public string updated_by_email { get; set; }
        public string inactivation_date { get; set; }
        public string created_at { get; set; }
    }
}

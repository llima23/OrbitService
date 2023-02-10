using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.Pagamentos.Pagamentos.service
{
    public class PagamentosOutput
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public string _id { get; set; }
        public string documentId { get; set; }
        public string documentNumber { get; set; }
        public string branchId { get; set; }
        public string dfe { get; set; }
        public string paymentDate { get; set; }
        public double paymentValue { get; set; }
        public string documentDate { get; set; }
        public string aliasTaker { get; set; }
        public string idTaker { get; set; }
        public List<Item> items { get; set; }
        public string created_by_name { get; set; }
        public string created_by_email { get; set; }
        public string updated_by_name { get; set; }
        public string updated_by_email { get; set; }
        public string tenantid { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }


    public class Item
    {
        public string cst { get; set; }
        public string value_pis { get; set; }
        public string value_cofins { get; set; }
        public string aliquot_pis { get; set; }
        public string aliquot_cofins { get; set; }
        public string valorItem { get; set; }
        public string unidade { get; set; }
        public string descricao { get; set; }
    }
}

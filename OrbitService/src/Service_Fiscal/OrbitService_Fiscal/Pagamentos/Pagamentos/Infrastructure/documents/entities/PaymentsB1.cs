using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.Pagamentos.Pagamentos.mapper.documents.entities
{
    public class PaymentsB1
    {
        public int DocEntryORCT { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public double DocTotal { get; set; }
        public int Serial { get; set; }
        public string TaxDate { get; set; }
        public string U_TAX4_EstabID { get; set; }
        public string U_TAX4_IdRet { get; set; }
        public string dfe { get; set; }
    }
}

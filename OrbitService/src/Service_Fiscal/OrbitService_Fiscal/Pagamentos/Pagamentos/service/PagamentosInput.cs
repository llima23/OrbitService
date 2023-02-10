using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.Pagamentos.Pagamentos.service
{
    public class PagamentosInput
    {
        public string documentId { get; set; }
        public int documentNumber { get; set; }
        public string branchId { get; set; }
        public string dfe { get; set; }
        public string paymentDate { get; set; }
        public double paymentValue { get; set; }
    }
}

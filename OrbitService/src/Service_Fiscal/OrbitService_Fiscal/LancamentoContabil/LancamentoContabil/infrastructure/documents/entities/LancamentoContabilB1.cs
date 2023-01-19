using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.infrastructure.documents.entities
{
    public class LancamentoContabilB1
    {
        public LancamentoContabilB1()
        {
            header = new Header();
            lines = new List<Lines>();
        }
        public Header header { get; set; }
        public List<Lines> lines { get; set; }
    }
    public class Header
    {
        public string Description { get; set; }
        public string Entry_Type { get; set; }
        public string Establishment_id { get; set; }
        public string BranchId { get; set; }
        public DateTime PostDate { get; set; }
        public int TransId { get; set; }
    }
    public class Lines
    {
        public string AccountId { get; set; }
        public string CostCenterId { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string Historic { get; set; }
        public int TransId { get; set; }
    }
}

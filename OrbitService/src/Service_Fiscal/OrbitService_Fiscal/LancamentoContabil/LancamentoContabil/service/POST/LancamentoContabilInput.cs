using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.service
{
    public class LancamentoContabilInput
    {
        public string data { get; set; }
    }

    public class BodyLCMInput
    {
        public BodyLCMInput()
        {
            header = new Header();
            transactions = new List<Transaction>();
        }
        public Header header { get; set; }
        public List<Transaction> transactions { get; set; }
    }

        public class Header
    {
        public string erp_id { get; set; }
        public string post_date { get; set; }
        public string header_accounting_entry_number { get; set; }
        public string description { get; set; }
        public string entry_type { get; set; }
        public string establishment_id { get; set; }
    }
    public class Transaction
    {
        public string erp_id { get; set; }
        public string accountId { get; set; }
        public string nature { get; set; }
        public string historic { get; set; }
        public double value { get; set; }
        public string costCenterId { get; set; }
    }
}

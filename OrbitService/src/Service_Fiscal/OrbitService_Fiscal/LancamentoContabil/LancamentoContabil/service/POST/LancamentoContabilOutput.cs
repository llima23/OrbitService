using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.service
{
    public class LancamentoContabilOutput
    {
        public LancamentoContabilOutput()
        {
            header = new HeaderOutput();
            transactions = new List<TransactionOutput>();
        }
        [JsonProperty("header")]
        public HeaderOutput header { get; set; }

        [JsonProperty("transactions")]
        public List<TransactionOutput> transactions { get; set; }
    }

    public class TransactionOutput
    {
        public TransactionOutput()
        {
            account = new Account();
        }
        public string id { get; set; }
        public string post_date { get; set; }
        public string accountId { get; set; }
        public string costCenterId { get; set; }
        public string nature { get; set; }
        public string historic { get; set; }
        public double value { get; set; }
        public string id_header_accounting_entry { get; set; }
        public string tenantid { get; set; }
        public DateTime created_at { get; set; }
        public Account account { get; set; }
    }
    public class HeaderOutput
    {
        public HeaderOutput()
        {
            establishment = new Establishment();
            accountingEntries = new AccountingEntries();
        }
        public string id { get; set; }
        public string post_date { get; set; }
        public string header_accounting_entry_number { get; set; }
        public string description { get; set; }
        public string entry_type { get; set; }
        public double total { get; set; }
        public string tenantid { get; set; }
        public DateTime created_at { get; set; }
        public string establishment_id { get; set; }
        public Establishment establishment { get; set; }
        public AccountingEntries accountingEntries { get; set; }
    }
    public class Establishment
    {
        public string alias { get; set; }
        public string tenantid { get; set; }
        public string taxation_type { get; set; }
        public string fiscal_qualification_type { get; set; }
        public string exception_legal_type { get; set; }
        public string branchId { get; set; }
        public string profileId { get; set; }
        public string headerAccountPlanId { get; set; }
        public string id { get; set; }
        public DateTime created_at { get; set; }
    }
    public class AccountingEntries
    {
        public AccountingEntries()
        {
            account = new Account();
        }
        public string id { get; set; }
        public string post_date { get; set; }
        public string accountId { get; set; }
        public string costCenterId { get; set; }
        public string nature { get; set; }
        public string historic { get; set; }
        public double value { get; set; }
        public string id_header_accounting_entry { get; set; }
        public string tenantid { get; set; }
        public DateTime created_at { get; set; }
        public Account account { get; set; }
    }
    public class Account
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
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.service.Establishments
{
    public class EstablishmentsOutput
    {
        public string alias { get; set; }
        public string tenantid { get; set; }
        public string taxation_type { get; set; }
        public string cod_inc_trib { get; set; }
        public string single_national_option { get; set; }
        public string cprb_option { get; set; }
        public string fiscal_qualification_type { get; set; }
        public string exception_legal_type { get; set; }
        public string branchId { get; set; }
        public string profileId { get; set; }
        public string fiscal_id_number { get; set; }
        public string headerAccountPlanId { get; set; }
        public string id { get; set; }
        public string created_at { get; set; }
        public Profile profile { get; set; }
    }
    public class Profile
    {
        public string id { get; set; }
        public string tenantid { get; set; }
        public string alias { get; set; }
        public string validFrom { get; set; }
        public string validTo { get; set; }
        public string mainEstablishmentId { get; set; }
        public string created_at { get; set; }
    }
}

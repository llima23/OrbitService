using AccountService_LancamentoContabil.LancamentoContabil.infrastructure.documents.entities;
using AccountService_LancamentoContabil.LancamentoContabil.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.LancamentoContabil.LancamentoContabil
{
    public class validationLCM
    {
        private LancamentoContabilB1 lcm { get; set; }
        private IDBLancamentoContabilRepository dbRepo { get; set; }
        public validationLCM(LancamentoContabilB1 Lcm, IDBLancamentoContabilRepository DbRepo)
        {
            this.lcm = Lcm;
            this.dbRepo = DbRepo;
        }
        public bool ValidationHeaderRequiredFields()
        {
            return true;
        }
        public bool ValidationLinesRequiredFields()
        {
            bool valid = true;
      
            return valid;
        }
    }
}

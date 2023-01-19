using AccountService_LancamentoContabil.LancamentoContabil.infrastructure.documents.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.service
{
    public interface IDBLancamentoContabilRepository
    {
        public List<LancamentoContabilB1> ReturnListLCMToOrbit();
        public int UpdateAccountStatusSucess(LancamentoContabilB1 lcm, LancamentoContabilOutput output);
        public int UpdateAccountStatusError(LancamentoContabilB1 lcm, LancamentoContabilError output);
        public int UpdateEstabFiscalInLConfigAddon(string estabFiscal, string branchId);
        public int UpdateAccountStatusErrorException(int lcm, string error);
    }
}

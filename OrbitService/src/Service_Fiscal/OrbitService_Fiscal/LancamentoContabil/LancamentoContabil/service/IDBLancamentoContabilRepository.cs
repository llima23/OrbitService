using AccountService_LancamentoContabil.LancamentoContabil.infrastructure.documents.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.service
{
    public interface IDBLancamentoContabilRepository
    {
        public List<LancamentoContabilB1> ReturnListLCMToUpdate();
        public List<LancamentoContabilB1> ReturnListLCMToOrbit();
        public int UpdateAccountStatusSucess(string transIds, string idRet);
        public int UpdateAccountStatusError(string transId, LancamentoContabilError output);
        public int UpdateEstabFiscalInLConfigAddon(string estabFiscal, string branchId);
        public int UpdateAccountStatusErrorException(int lcm, string error);
        public int UpdateLCMFalseValidation(LancamentoContabilB1 lcm, string message);
        public int UpdateAccountStatusSucessIdLote(string transIds, LancamentoContabilOutput output);
    }
}

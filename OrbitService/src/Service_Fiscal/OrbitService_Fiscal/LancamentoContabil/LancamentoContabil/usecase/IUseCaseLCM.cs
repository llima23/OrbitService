using AccountService_LancamentoContabil.LancamentoContabil.infrastructure.documents.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.usecase
{
    public interface IUseCaseLCM
    {
        public string GetEstabFiscalIdFromOrbit(string branchId, LancamentoContabilB1 lcm);
    }
}

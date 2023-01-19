using AccountService_PlanoDeContas.PlanoDeContas.Infrastructure.documents.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_PlanoDeContas.PlanoDeContas.repository
{
    public interface IDBPlanAccountRepository
    {
        public string idOrbitPlanoConta { get; set; }
        public string tenantId { get; set; }
        public string planoDeContaIntegrado { get; set; }
        public int UpdatePlanAccountStatusSucess();
        public bool ValidadeIfExistsIdOrbitPlanAccountSucess();
        public List<PlanAccount> ReturnListOfPlanAccountToAssociate();
    }
}

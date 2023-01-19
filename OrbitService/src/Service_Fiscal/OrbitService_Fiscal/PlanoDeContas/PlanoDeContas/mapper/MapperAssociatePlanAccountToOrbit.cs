using AccountService_PlanoDeContas.PlanoDeContas.Infrastructure.documents.entities;
using AccountService_PlanoDeContas.PlanoDeContas.service.Associate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountService_PlanoDeContas.PlanoDeContas.mapper
{
    public class MapperAssociatePlanAccountToOrbit
    {
        public PlanoDeContaInputAssociate MapperListAccountToAssociatePlanOrbit(List<PlanAccount> listPlanAccounts,string idOrbitPlanoConta)
        {
            PlanoDeContaInputAssociate input = new PlanoDeContaInputAssociate();
            foreach (PlanAccount item in listPlanAccounts.OrderBy(c => c.Levels))
            {
                input.accounts.Add(item.U_TAX4_IdRet);
            }
            input.header_account_plan_id = idOrbitPlanoConta;
            return input;
        }
    }
}

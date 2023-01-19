using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_PlanoDeContas.PlanoDeContas.Infrastructure.documents.entities
{
    public class PlanAccount
    {
        public string AcctCode { get; set; }
        public int Levels { get; set; }
        public string U_TAX4_IdRet { get; set; }
        public string U_TAX4_LIDO { get; set; }
    }
}

using AccountService_PlanoDeContas.PlanoDeContas.Infrastructure.documents.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_PlanoDeContas.PlanoDeContas.service.Associate
{
    public class PlanoDeContaInputAssociate
    {
        public PlanoDeContaInputAssociate()
        {
            accounts = new List<string>();
        }
        public List<string> accounts { get; set; }
        public string header_account_plan_id { get; set; }
    }
}

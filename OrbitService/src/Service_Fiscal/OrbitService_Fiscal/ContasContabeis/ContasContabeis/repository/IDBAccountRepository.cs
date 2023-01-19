using AccountService_ContasContabeis.ContasContabeis.Infrastructure.Documents.Entities;
using AccountService_ContasContabeis.ContasContabeis.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_ContasContabeis.ContasContabeis.repository
{
    public interface IDBAccountRepository
    {
        public List<Account> ReturnListOfAccountToOrbit();
        public string ReturnIdOrbitFatherAccountB1(string fatherAccount);
        public int UpdateAccountStatusSucess(Account account, ContasContabeisOutput output);
        public int UpdateAccountStatusError(Account account, ContasContabeisError output);
    }
}

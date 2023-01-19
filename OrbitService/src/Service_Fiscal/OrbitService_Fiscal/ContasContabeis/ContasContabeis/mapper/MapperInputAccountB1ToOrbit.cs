using AccountService_ContasContabeis.ContasContabeis.Infrastructure.Documents.Entities;
using AccountService_ContasContabeis.ContasContabeis.repository;
using AccountService_ContasContabeis.ContasContabeis.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_ContasContabeis.ContasContabeis.mapper
{
    public class MapperInputAccountB1ToOrbit
    {
        public IDBAccountRepository db;
        public MapperInputAccountB1ToOrbit(IDBAccountRepository db)
        {
            this.db = db;
        }
        public ContasContabeisInput ToAccountServiceRegisterInput(Account account)
        {
            ContasContabeisInput input = new ContasContabeisInput();
            try
            {
                input.account_type = account.AcctType;
                input.date = account.CreateDate.ToString("yyyy-MM-dd");
                input.level = account.Levels;
                input.account_code = account.AcctCode;
                input.account_name = account.AcctName;
                input.origin_code = account.OriginCode;
                input.higher_account_code_id = !String.IsNullOrEmpty(account.FatherNum) ? db.ReturnIdOrbitFatherAccountB1(account.FatherNum) : null;
                return input;
            }
            catch
            {
                //Do Update B1
                input = null;
                return input;
            }
    
        }
    }
}

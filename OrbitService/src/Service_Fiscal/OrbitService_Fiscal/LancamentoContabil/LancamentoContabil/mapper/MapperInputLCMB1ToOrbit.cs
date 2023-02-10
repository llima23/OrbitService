using AccountService_LancamentoContabil.LancamentoContabil.infrastructure.documents.entities;
using AccountService_LancamentoContabil.LancamentoContabil.service;
using AccountService_LancamentoContabil.LancamentoContabil.usecase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.mapper
{
    public class MapperInputLCMB1ToOrbit
    {
        private IUseCaseLCM useCase;
        public MapperInputLCMB1ToOrbit(IUseCaseLCM useCase)
        {
            this.useCase = useCase;
        }
        public BodyLCMInput ToAccountServiceRegisterInput(LancamentoContabilB1 lcm)
        {
            BodyLCMInput input = new BodyLCMInput();
            input.header.erp_id = lcm.header.TransId.ToString();
            input.header.post_date = lcm.header.PostDate.ToString("yyyy-MM-dd");
            input.header.description = lcm.header.Description;
            input.header.entry_type = lcm.header.Entry_Type;
            input.header.establishment_id = String.IsNullOrEmpty(lcm.header.Establishment_id) ? useCase.GetEstabFiscalIdFromOrbit(lcm.header.BranchId,lcm) : lcm.header.Establishment_id;
            input.header.header_accounting_entry_number = lcm.header.TransId.ToString();
            List<Transaction> listTR = new List<Transaction>();
            foreach (Lines item in lcm.lines)
            {
                Transaction transaction = new Transaction();
                transaction.erp_id = lcm.header.TransId.ToString();
                transaction.accountId = item.AccountId;
                transaction.nature = item.Debit > 0 ? "D" : "C";
                transaction.historic = item.Historic;
                transaction.value = item.Debit > 0 ? item.Debit : item.Credit;
                transaction.costCenterId = !String.IsNullOrEmpty(item.CostCenterId) ? item.CostCenterId : null;
                listTR.Add(transaction);
            }
            input.transactions = listTR;
            return input;
        }
    }
}


using AccountService_ContasContabeis.ContasContabeis.Infrastructure.Documents.Entities;
using AccountService_ContasContabeis.ContasContabeis.repository;
using System;



namespace OrbitService_Fiscal.ContasContabeis.validation
{
    public class validationContasContabeis
    {
        private Account ContasContabeis { get; set; }
        private IDBAccountRepository AccountRepository;
        public validationContasContabeis(Account ContasContabeisB1, IDBAccountRepository AccountRepository)
        {
            this.ContasContabeis = ContasContabeisB1;
            this.AccountRepository = AccountRepository;
        }

        public bool ValidationRequiredFields()
        {

            if (String.IsNullOrEmpty(ContasContabeis.OriginCode))
            {
                this.AccountRepository.UpdateStatusErrorValidation(ContasContabeis, "Validação serviço 4TAX: Natureza do grupo de contas não pode ser vazio!");
                return false;
            }
            if (String.IsNullOrEmpty(ContasContabeis.AcctType))
            {
                this.AccountRepository.UpdateStatusErrorValidation(ContasContabeis, "Validação serviço 4TAX: O tipo da conta não pode ser vazio (Analitica/Sintética)");
                return false;
            }
            else if (String.IsNullOrEmpty(ContasContabeis.AcctName))
            {
                this.AccountRepository.UpdateStatusErrorValidation(ContasContabeis, "Validação serviço 4TAX: A descrição da conta contábil não pode ser vázio!");
                return false;
            }
            else if (String.IsNullOrEmpty(ContasContabeis.AcctCode))
            {
                this.AccountRepository.UpdateStatusErrorValidation(ContasContabeis, "Validação serviço 4TAX: O Código da conta contábil não pode ser vázio!");
                return false;
            }
            else if (ContasContabeis.Levels > 10)
            {
                this.AccountRepository.UpdateStatusErrorValidation(ContasContabeis, "Validação serviço 4TAX: O nível da conta deve ser um intervalo de 1 a 10");
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}

using AccountService_ContasContabeis.ContasContabeis.Infrastructure.Documents.Entities;
using AccountService_ContasContabeis.ContasContabeis.mapper;
using AccountService_ContasContabeis.ContasContabeis.repository;
using AccountService_ContasContabeis.ContasContabeis.service;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AccountService_ContasContabeis.ContasContabeis.usecase
{
    public class UseCaseContasContabeis
    {
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        private IDBAccountRepository accountsRepository;

        public UseCaseContasContabeis(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDBAccountRepository accountRepository)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            this.accountsRepository = accountRepository;
        }

        public void Execute()
        {
            MapperInputAccountB1ToOrbit mapper = new MapperInputAccountB1ToOrbit(accountsRepository);
            ContasContabeisServices outboundAccountRegister = new ContasContabeisServices(sConfig, communicationProvider);
            List<Account> listAccounts = accountsRepository.ReturnListOfAccountToOrbit();
            foreach (Account account in listAccounts.OrderBy(x => x.Levels))
            {
                ContasContabeisInput input = new ContasContabeisInput();
                input = mapper.ToAccountServiceRegisterInput(account);
                OperationResponse<ContasContabeisOutput, ContasContabeisError> response = outboundAccountRegister.Execute(input);

                if (response.isSuccessful)
                {
                    ContasContabeisOutput output = response.GetSuccessResponse();
                    accountsRepository.UpdateAccountStatusSucess(account, output);
                }

                else
                {
                    ContasContabeisError output = response.GetErrorResponse();
                    accountsRepository.UpdateAccountStatusError(account, output);
                }
            }
        }
    }
}

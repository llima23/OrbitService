using AccountService_ContasContabeis.ContasContabeis.Infrastructure.Documents.Entities;
using AccountService_ContasContabeis.ContasContabeis.mapper;
using AccountService_ContasContabeis.ContasContabeis.repository;
using AccountService_ContasContabeis.ContasContabeis.service;
using OrbitService_Fiscal.ContasContabeis.validation;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OrbitService_Fiscal.ContasContabeis.ContasContabeis.service.GET;
using OrbitService_Fiscal.ContasContabeis.ContasContabeis.service.PUT;

namespace AccountService_ContasContabeis.ContasContabeis.usecase
{
    public class UseCaseContasContabeis
    {
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        private IDBAccountRepository accountsRepository;
        private bool PostPut = true;

        public UseCaseContasContabeis(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDBAccountRepository accountRepository)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            this.accountsRepository = accountRepository;
        }

        public void Execute()
        {
            MapperInputAccountB1ToOrbit mapper = new MapperInputAccountB1ToOrbit(accountsRepository);
            
            List<Account> listAccounts = accountsRepository.ReturnListOfAccountToOrbit();
            foreach (Account account in listAccounts.OrderBy(x => x.Levels))
            {
                validationContasContabeis validationContasContabeis = new validationContasContabeis(account, this.accountsRepository);
                if (validationContasContabeis.ValidationRequiredFields())
                {
                    if (string.IsNullOrEmpty(account.U_TAX4_IdRet))
                    {
                        ExecuteGetContasContabeis(account);
                    }
                    if (!PostPut)
                    {
                        ExecutePOSTContasContabeis(account, mapper);
                    }
                    else
                    {
                        ExecutePUTContasContabeis(account, mapper);
                    }
                }
            }
        }

        public void ExecuteGetContasContabeis(Account account)
        {
            ContasContabeisServicesGET outboundAccountRegister = new ContasContabeisServicesGET(sConfig, communicationProvider);
            OperationResponse<ContasContabeisOutputGET, ContasContabeisError> response = outboundAccountRegister.Execute(account.AcctCode);
            if (response.isSuccessful)
            {
                ContasContabeisOutputGET output = response.GetSuccessResponse();
                if (output.data.Count > 0)
                {
                    account.U_TAX4_IdRet = output.data[0].id;
                    PostPut = true;
                }
                else
                {
                    PostPut = false;
                }
            }
            else
            {
                PostPut = false;
            }
        }

        public void ExecutePOSTContasContabeis(Account account, MapperInputAccountB1ToOrbit mapper)
        {
            ContasContabeisServices outboundAccountRegister = new ContasContabeisServices(sConfig, communicationProvider);
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

        public void ExecutePUTContasContabeis(Account account, MapperInputAccountB1ToOrbit mapper)
        {
            ContasContabeisServicePUT outboundAccountRegister = new ContasContabeisServicePUT(sConfig, communicationProvider);
            ContasContabeisInputPUT input = new ContasContabeisInputPUT();
            input = mapper.ToAccountServiceRegisterInputPUT(account);
            OperationResponse<ContasContabeisOutput, ContasContabeisError> response = outboundAccountRegister.Execute(input, account.U_TAX4_IdRet);

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

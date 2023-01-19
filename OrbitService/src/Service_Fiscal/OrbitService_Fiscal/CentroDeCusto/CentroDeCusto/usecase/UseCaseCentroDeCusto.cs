using AccountService_CentroDeCusto.CentroDeCusto.infrastructure.documents.entities;
using AccountService_CentroDeCusto.CentroDeCusto.mapper;
using AccountService_CentroDeCusto.CentroDeCusto.repository;
using AccountService_CentroDeCusto.CentroDeCusto.service;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_CentroDeCusto.CentroDeCusto.usecase
{
    public class UseCaseCentroDeCusto
    {
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        private IDBCentroDeCustoRepository accountsRepository;

        public UseCaseCentroDeCusto(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDBCentroDeCustoRepository accountRepository)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            this.accountsRepository = accountRepository;
        }

        public void Execute()
        {
            MapperInputCentroDeCustoB1ToOrbit mapper = new MapperInputCentroDeCustoB1ToOrbit();
            CentroDeCustoService outboundAccountRegister = new CentroDeCustoService(sConfig, communicationProvider);
            List<CentroDeCustoB1> listAccounts = accountsRepository.ReturnListOfAccountToOrbit();
        
            foreach (CentroDeCustoB1 account in listAccounts)
            {
                CentroDeCustoInput input = new CentroDeCustoInput();
                input = mapper.ToCentroDeCustoRegisterInput(account);
    
                OperationResponse<CentroDeCustoOutput, CentroDeCustoError> response = outboundAccountRegister.Execute(input);
    
                if (response.isSuccessful)
                {
                    CentroDeCustoOutput output = response.GetSuccessResponse();
                    accountsRepository.UpdateAccountStatusSucess(account, output);
                }

                else
                {
                    CentroDeCustoError output = response.GetErrorResponse();
                    accountsRepository.UpdateAccountStatusError(account, output);
                }
            }
        }
    }
}

using AccountService_PlanoDeContas.PlanoDeContas.mapper;
using AccountService_PlanoDeContas.PlanoDeContas.repository;
using AccountService_PlanoDeContas.PlanoDeContas.service;
using AccountService_PlanoDeContas.PlanoDeContas.service.Associate;
using AccountService_PlanoDeContas.PlanoDeContas.service.Create;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_PlanoDeContas.PlanoDeContas.usecase
{
    public class UseCasePlanoDeContas
    {
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        private IDBPlanAccountRepository accountsRepository;
        public UseCasePlanoDeContas(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDBPlanAccountRepository accountsRepository)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            this.accountsRepository = accountsRepository;
            accountsRepository.tenantId = this.sConfig.TenantID.ToString();
        }
        public void Execute()
        {
            if (accountsRepository.CountAccountIntegrate() == accountsRepository.CountAccountTotalIntegrate())
            {
                if (!accountsRepository.ValidadeIfExistsIdOrbitPlanAccountSucess())
                {
                    PlanoDeContasServicesCreate planAccountCreate = new PlanoDeContasServicesCreate(sConfig, communicationProvider);
                    PlanoDeContasInputCreate inputCreate = new PlanoDeContasInputCreate
                    {
                        alias = "Plano De Contas"
                    };
                    OperationResponse<PlanoDeContasOutputCreate, PlanoDeContasError> responseCreate = planAccountCreate.ExecuteCreate(inputCreate);
                    if (responseCreate.isSuccessful)
                    {
                        //Do UpdateB1
                        PlanoDeContasOutputCreate outputCreate = responseCreate.GetSuccessResponse();
                        accountsRepository.idOrbitPlanoConta = outputCreate.id;
                    }
                }
                if (accountsRepository.planoDeContaIntegrado != "S")
                {
                    MapperAssociatePlanAccountToOrbit mapper = new MapperAssociatePlanAccountToOrbit();
                    PlanoDeContasServicesAssociate outboundAccountRegister = new PlanoDeContasServicesAssociate(sConfig, communicationProvider);
                    PlanoDeContaInputAssociate input = mapper.MapperListAccountToAssociatePlanOrbit(accountsRepository.ReturnListOfPlanAccountToAssociate(), accountsRepository.idOrbitPlanoConta);
                    OperationResponse<PlanoDeContaOutputAssociate, PlanoDeContasError> response = outboundAccountRegister.ExecuteAssociate(input);
                    if (response.isSuccessful)
                    {
                        accountsRepository.UpdatePlanAccountStatusSucess();
                    }
                }
            }
        }
    }
}

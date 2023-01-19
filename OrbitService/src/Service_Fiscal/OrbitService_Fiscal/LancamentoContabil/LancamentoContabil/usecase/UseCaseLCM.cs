using AccountService_LancamentoContabil.LancamentoContabil.infrastructure.documents.entities;
using AccountService_LancamentoContabil.LancamentoContabil.mapper;
using AccountService_LancamentoContabil.LancamentoContabil.service;
using AccountService_LancamentoContabil.LancamentoContabil.service.Establishments;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;

namespace AccountService_LancamentoContabil.LancamentoContabil.usecase
{
    public class UseCaseLCM : IUseCaseLCM
    {
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        private IDBLancamentoContabilRepository accountRepository;
        private int TransId = 0;
        public UseCaseLCM(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDBLancamentoContabilRepository accountRepository)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            this.accountRepository = accountRepository;
        }
        public void Execute()
        {
            try
            {
                MapperInputLCMB1ToOrbit mapper = new MapperInputLCMB1ToOrbit(this);
                LancamentoContabilService outboundAccountRegister = new LancamentoContabilService(sConfig, communicationProvider);
                List<LancamentoContabilB1> listLCM = accountRepository.ReturnListLCMToOrbit();
                foreach (LancamentoContabilB1 lcm in listLCM)
                {
                    TransId = lcm.header.TransId;
                    LancamentoContabilInput input = mapper.ToAccountServiceRegisterInput(lcm);
                    OperationResponse<LancamentoContabilOutput, LancamentoContabilError> response = outboundAccountRegister.Execute(input);
                    if (response.isSuccessful)
                    {
                        LancamentoContabilOutput output = response.GetSuccessResponse();
                        accountRepository.UpdateAccountStatusSucess(lcm, output);
                    }

                    else
                    {
                        LancamentoContabilError output = response.GetErrorResponse();
                        accountRepository.UpdateAccountStatusError(lcm, output);
                    }
                }
            }
            catch(Exception ex)
            {
                accountRepository.UpdateAccountStatusErrorException(TransId, ex.Message);
            }
           
        }

        public string GetEstabFiscalIdFromOrbit(string branchId, LancamentoContabilB1 lcm)
        {
            string estabFiscalId = string.Empty;
            EstablishmentsService service = new EstablishmentsService(sConfig, communicationProvider);
            OperationResponse<EstablishmentsOutput, LancamentoContabilError> response = service.GetEstabId(branchId);
            if (response.isSuccessful)
            {
                EstablishmentsOutput output = response.GetSuccessResponse();
                if (string.IsNullOrEmpty(output.id))
                {
                    LancamentoContabilError outputError = new LancamentoContabilError();
                    outputError.errors[0].msg = "Estabelecimento não cadastrado para filial utilizada";
                    accountRepository.UpdateAccountStatusError(lcm, outputError);
                }
                estabFiscalId = output.id;
                accountRepository.UpdateEstabFiscalInLConfigAddon(output.id, branchId);
            }
            else
            {
                LancamentoContabilError output = response.GetErrorResponse();
                accountRepository.UpdateAccountStatusError(lcm, output);
            }
            return estabFiscalId;
        }
    }
}

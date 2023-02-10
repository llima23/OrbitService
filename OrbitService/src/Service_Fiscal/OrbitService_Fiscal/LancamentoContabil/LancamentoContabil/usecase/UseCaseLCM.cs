using AccountService_LancamentoContabil.LancamentoContabil.infrastructure.documents.entities;
using AccountService_LancamentoContabil.LancamentoContabil.mapper;
using AccountService_LancamentoContabil.LancamentoContabil.service;
using AccountService_LancamentoContabil.LancamentoContabil.service.Establishments;
using Newtonsoft.Json;
using OrbitLibrary.Common;
using OrbitService_COL_Fiscal.LancamentoContabil.service.GET;
using OrbitService_Fiscal.LancamentoContabil.LancamentoContabil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.usecase
{
    public class UseCaseLCM : IUseCaseLCM
    {
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        private IDBLancamentoContabilRepository accountRepository;
        private string TransId;
        public UseCaseLCM(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDBLancamentoContabilRepository accountRepository)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            this.accountRepository = accountRepository;
        }
        public void Execute()
        {
            LancamentoContabilInput input = new LancamentoContabilInput();
            List<BodyLCMInput> listBody = new List<BodyLCMInput>();
            MapperInputLCMB1ToOrbit mapper = new MapperInputLCMB1ToOrbit(this);
            LancamentoContabilService outboundAccountRegister = new LancamentoContabilService(sConfig, communicationProvider);
            List<LancamentoContabilB1> listLCM = accountRepository.ReturnListLCMToOrbit();
            foreach (LancamentoContabilB1 lcm in listLCM)
            {
                validationLCM validation = new validationLCM(lcm, accountRepository);
                if (validation.ValidationHeaderRequiredFields() && validation.ValidationLinesRequiredFields())
                {
                    BodyLCMInput body = mapper.ToAccountServiceRegisterInput(lcm);
                    TransId += "'" + lcm.header.TransId + "',";
                    listBody.Add(body);
                }
            }
            TransId = TransId.Remove(TransId.Length - 1);
            string json = JsonConvert.SerializeObject(listBody, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            byte[] bytes = Encoding.Default.GetBytes(json);
            input.data = Convert.ToBase64String(bytes);

            OperationResponse<LancamentoContabilOutput, LancamentoContabilError> response = outboundAccountRegister.Execute(input);
            if (response.isSuccessful)
            {
                LancamentoContabilOutput output = response.GetSuccessResponse();
                accountRepository.UpdateAccountStatusSucessIdLote(TransId, output);
            }
            else
            {
                LancamentoContabilError output = response.GetErrorResponse();
                accountRepository.UpdateAccountStatusError(TransId, output);
            }
        }

        public void ExecuteAtualizaLCM()
        {
            MapperInputLCMB1ToOrbit mapper = new MapperInputLCMB1ToOrbit(this);
            LcmGetService serviceGET = new LcmGetService(sConfig, communicationProvider);
            List<LancamentoContabilB1> listLCM = accountRepository.ReturnListLCMToUpdate();
            foreach (LancamentoContabilB1 lcm in listLCM)
            {
                OperationResponse<LcmGetOutput, LcmGetError> response = serviceGET.Execute(lcm.header.IdOrbitLote);
                if (response.isSuccessful)
                {
                    LcmGetOutput output = response.GetSuccessResponse();
                    foreach (Success item in output.data.success.Where(s => s.externalId == lcm.header.TransId.ToString()))
                    {
                        accountRepository.UpdateAccountStatusSucess(item.externalId, item.id);
                    }
                }
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
                    accountRepository.UpdateAccountStatusError(TransId, outputError);
                }
                estabFiscalId = output.id;
                accountRepository.UpdateEstabFiscalInLConfigAddon(output.id, branchId);
            }
            else
            {
                LancamentoContabilError output = response.GetErrorResponse();
                accountRepository.UpdateAccountStatusError(TransId, output);
            }
            return estabFiscalId;
        }
    }
}

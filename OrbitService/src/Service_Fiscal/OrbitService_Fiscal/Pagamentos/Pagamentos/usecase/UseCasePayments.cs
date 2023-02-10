using OrbitLibrary.Common;
using OrbitService_Fiscal.Pagamentos.Pagamentos.mapper;
using OrbitService_Fiscal.Pagamentos.Pagamentos.mapper.documents.entities;
using OrbitService_Fiscal.Pagamentos.Pagamentos.repository;
using OrbitService_Fiscal.Pagamentos.Pagamentos.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.Pagamentos.Pagamentos.usecase
{
    public class UseCasePayments
    {
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        private IDBPaymentsRepository paymentsRepository;
        public UseCasePayments(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDBPaymentsRepository paymentsRepository)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            this.paymentsRepository = paymentsRepository;
        }
        public void Execute()
        {
            MapperPaymentsB1ToOrbit mapper = new MapperPaymentsB1ToOrbit();
            PagamentosService service = new PagamentosService(sConfig, communicationProvider);
            List<PaymentsB1> list = paymentsRepository.ReturnListOfPaymentsToOrbit();

            foreach (PaymentsB1 payment in list)
            {
                PagamentosInput input = new PagamentosInput();
                input = mapper.Mapper(payment);

                OperationResponse<PagamentosOutput, PagamentosErro> response = service.Execute(input);

                if (response.isSuccessful)
                {
                    PagamentosOutput output = response.GetSuccessResponse();
                    paymentsRepository.UpdateAccountStatusSucess(payment, output);
                }

                else
                {
                    PagamentosErro output = response.GetErrorResponse();
                    paymentsRepository.UpdateAccountStatusError(payment, output);
                }
            }
        }
    }
}

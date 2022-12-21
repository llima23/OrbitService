using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.FiscalBrazil.mappers;
using OrbitService.FiscalBrazil.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.FiscalBrazil.usecases
{
    public class UseCaseInboundCce
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;

        public UseCaseInboundCce(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDocumentsRepository documentsRepository)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            MapperInboundCce mapper = new MapperInboundCce();
            InboundCceService otherDocumentRegister = new InboundCceService(sConfig, communicationProvider);
            List<Invoice> inboundOtherDocuments = documentsRepository.GetInboundOtherDocuments();
            foreach (Invoice invoice in inboundOtherDocuments)
            {
                InboundCceInput input = mapper.ToInboundCceRegisterInput(invoice);
                OperationResponse<InboundCceOutput, InboundCceError> response = otherDocumentRegister.Execute(input);

                if (response.isSuccessful)
                {
                    InboundCceOutput output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }
            }
        }
    }
}

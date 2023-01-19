
using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.InboundOtherDocuments.mappers;
using OrbitService.InboundOtherDocuments.services;
using OrbitService.InboundOtherDocuments.services.Error;
using OrbitService.InboundOtherDocuments.services.Input;
using OrbitService.InboundOtherDocuments.services.Output;
using System.Collections.Generic;

namespace OrbitService.InboundOtherDocuments.usecases
{
    public class OtherDocumentsRegisterUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;

        public OtherDocumentsRegisterUseCase(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDocumentsRepository documentsRepository)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            Mapper mapper = new Mapper();
            OtherDocumentRegister otherDocumentRegister = new OtherDocumentRegister(sConfig, communicationProvider);
            List<Invoice> inboundOtherDocuments = documentsRepository.GetInboundOtherDocuments();
            foreach (Invoice invoice in inboundOtherDocuments)
            {
                Root root = new Root();
                OtherDocumentRegisterInput input = mapper.ToOtherDocumentRegisterInput(invoice);
                root.Data = input;
                OperationResponse<OtherDocumentRegisterOutput, OtherDocumentRegisterError> response = otherDocumentRegister.Execute(root);

                if (response.isSuccessful)
                {
                    OtherDocumentRegisterOutput output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }
            }
        }
    }
}

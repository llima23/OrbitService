
using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.FiscalBrazil.mappers;
using OrbitService.FiscalBrazil.services;
using OrbitService.FiscalBrazil.services.Error;
using OrbitService.FiscalBrazil.services.Input;
using OrbitService.FiscalBrazil.services.Output;
using System.Collections.Generic;

namespace OrbitService.FiscalBrazil.usecases
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
                OtherDocumentRegisterInput input = mapper.ToOtherDocumentRegisterInput(invoice);
                OperationResponse<OtherDocumentRegisterOutput, OtherDocumentRegisterError> response = otherDocumentRegister.Execute(input);

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

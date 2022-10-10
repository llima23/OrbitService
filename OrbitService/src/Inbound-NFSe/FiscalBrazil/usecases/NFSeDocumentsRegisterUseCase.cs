using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.FiscalBrazil.mappers;
using OrbitService.FiscalBrazil.services.NFSeDocumentRegister;
using System.Collections.Generic;

namespace OrbitService.FiscalBrazil.usecases
{
    public class NFSeDocumentsRegisterUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;

        public NFSeDocumentsRegisterUseCase(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDocumentsRepository documentsRepository)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            MapperNFSeDocumentRegister mapper = new MapperNFSeDocumentRegister();
            NFSeDocumentRegisterService NFSeDocumentRegister = new NFSeDocumentRegisterService(sConfig, communicationProvider);
            List<Invoice> inboundNFSeDocuments = documentsRepository.GetInboundNFSe();
            foreach (Invoice invoice in inboundNFSeDocuments)
            {
                NFSeDocumentRegisterInput input = mapper.ToNFSeDocumentRegisterInput(invoice);
                OperationResponse<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError> response = NFSeDocumentRegister.Execute(input);

                if (response.isSuccessful)
                {
                    NFSeDocumentRegisterOutput output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }

                else
                {
                    NFSeDocumentRegisterError output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseError(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }
            }
        }
    }
}

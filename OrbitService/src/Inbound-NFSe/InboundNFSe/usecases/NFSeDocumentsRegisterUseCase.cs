using B1Library.Applications;
using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.InboundNFSe.mappers;
using OrbitService.InboundNFSe.services.NFSeDocumentRegister;
using System.Collections.Generic;

namespace OrbitService.InboundNFSe.usecases
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
                Logs.InsertLog($"{response.Content}");

                if (response.isSuccessful)
                {
                    NFSeDocumentRegisterOutput output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }

                else
                {
                    NFSeDocumentRegisterError output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseError(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }
            }
        }
    }
}

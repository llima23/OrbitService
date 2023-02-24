using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.InboundNFSe.mappers;
using OrbitService.InboundNFSe.services.NFSeDocumentRegister;
using OrbitService_Fiscal.Application;
using System;
using System.Collections.Generic;

namespace OrbitService.InboundNFSe.usecases
{
    public class NFSeDocumentsRegisterUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        private Invoice invoice;

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
            try
            {
                List<Invoice> inboundNFSeDocuments = documentsRepository.GetInboundNFSe();
                foreach (Invoice invoice in inboundNFSeDocuments)
                {
                    this.invoice = invoice;
                    NFSeDocumentRegisterInput input = mapper.ToNFSeDocumentRegisterInput(invoice);
                    LogsFiscalBR.InsertLog($"mapper preenchido - {invoice.DocEntry}");
                    OperationResponse<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError> response = NFSeDocumentRegister.Execute(input);
                    LogsFiscalBR.InsertLog($"{response.Content}");

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
            catch(Exception ex)
            {
                LogsFiscalBR.InsertLog($"{ex.Message}");
                DocumentStatus newStatusData = new DocumentStatus("", "", ex.Message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
                documentsRepository.UpdateDocumentStatus(newStatusData, invoice.ObjetoB1);
            }
        }
    }
}

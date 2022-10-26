using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.usecases
{
    public class OutboundNFeDocumentCancelUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;

        public OutboundNFeDocumentCancelUseCase(IDocumentsRepository documentsRepository, ServiceConfiguration sConfig, CommunicationProvider communicationProvider)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            MapperInputNFeCancel mapper = new MapperInputNFeCancel();
            OutboundDFeDocumentCancelServiceNFe outboundNFeRegister = new OutboundDFeDocumentCancelServiceNFe(sConfig, communicationProvider);
            List<Invoice> OutBoundNFeDocumentsCancel = documentsRepository.GetCancelOutboundNFe();
            foreach (Invoice invoice in OutBoundNFeDocumentsCancel)
            {

                OutboundDFeDocumentCancelInputNFe input = mapper.MapperInvoiceB1ToOutboundDFeDocumentCancelInputNFe(invoice);
                OperationResponse<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe> response = outboundNFeRegister.Execute(input);

                if (response.isSuccessful)
                {

                    OutboundDFeDocumentCancelOutputNFe output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }

                else
                {
                    OutboundDFeDocumentCancelOutputNFe output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseError(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }
            }
        }
    }
}

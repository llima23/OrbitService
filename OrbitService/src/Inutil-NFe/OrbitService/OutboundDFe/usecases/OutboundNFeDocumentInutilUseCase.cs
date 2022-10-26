using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.usecases
{
    public class OutboundNFeDocumentInutilUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        public OutboundNFeDocumentInutilUseCase(IDocumentsRepository documentsRepository, ServiceConfiguration sConfig, CommunicationProvider communicationProvider)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            MapperInputNFeInutil mapper = new MapperInputNFeInutil();
            OutboundDFeDocumentInutilServicesNFe outboundNFSeInutilRegister = new OutboundDFeDocumentInutilServicesNFe(sConfig, communicationProvider);
            List<Invoice> OutBoundNFeDocumentsCancel = documentsRepository.GetInutilOutboundNFe();
            foreach (Invoice invoice in OutBoundNFeDocumentsCancel)
            {
                OutboundDFeDocumentInutilInputNFe input = mapper.MapperInvoiceB1ToOrbitInput(invoice);
                OperationResponse<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe> response = outboundNFSeInutilRegister.Execute(input);
                if (response.isSuccessful)
                {
                    OutboundDFeDocumentInutilOutputNFe output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.MapperOrbitOutputToUpdateB1Sucess(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }
                else
                {
                    OutboundDFeDocumentInutilOutputNFe output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.MapperOrbitOutputToUpdateB1Error(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }
            }
        }
    }
}

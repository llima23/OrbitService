using B1Library.Applications;
using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.usecases
{
    public class OutboundNFSeDocumentInutilUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        public OutboundNFSeDocumentInutilUseCase(IDocumentsRepository documentsRepository, ServiceConfiguration sConfig, CommunicationProvider communicationProvider)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }
        public void Execute()
        {
            MapperInputNFSeInutil mapper = new MapperInputNFSeInutil();
            OutboundDFeDocumentInutilServicesNFSe outboundNFSeInutilRegister = new OutboundDFeDocumentInutilServicesNFSe(sConfig, communicationProvider);
            List<Invoice> OutBoundNFeDocumentsCancel = documentsRepository.GetInutilOutboundNFSe();
            foreach (Invoice invoice in OutBoundNFeDocumentsCancel)
            {
                Logs.InsertLog($"IdOrbit: {invoice.Identificacao.IdRetornoOrbit} - DocNum: {invoice.Identificacao.DocNum}");
                OutboundDFeDocumentInutilInputNFSe input = mapper.MapperInvoiceB1ToOrbitInput(invoice);
                OperationResponse<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe> response = outboundNFSeInutilRegister.Execute(input);
                if (response.isSuccessful)
                {
                    OutboundDFeDocumentInutilOutputNFSe output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.MapperOrbitOutputToUpdateB1Sucess(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }
                else
                {
                    OutboundDFeDocumentInutilOutputNFSe output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.MapperOrbitOutputToUpdateB1Error(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }
            }
        }
    }
}

﻿using B1Library.Documents;
using OrbitLibrary.Common;
using OrbitService.OutboundNFe.mappers;
using OrbitService.OutboundNFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundNFe.usecases
{
    public class OutboundNFeDocumentConsultaUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        public OutboundNFeDocumentConsultaUseCase(IDocumentsRepository documentsRepository, ServiceConfiguration sConfig, CommunicationProvider communicationProvider)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            MapperNFeConsulta mapper = new MapperNFeConsulta();
            OutboundDFeDocumentConsulServicesNFe outboundNFeRegister = new OutboundDFeDocumentConsulServicesNFe(sConfig, communicationProvider);
            List<Invoice> OutBoundNFeDocumentsCancel = documentsRepository.GetCancelOutboundNFSe();
            foreach (Invoice invoice in OutBoundNFeDocumentsCancel)
            {
                OperationResponse<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe> response = outboundNFeRegister.Execute(invoice);
                if (response.isSuccessful)
                {
                    OutboundDFeDocumentConsultaOutputNFe output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }
                else
                {
                    OutboundDFeDocumentConsulErroNFe output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseErro(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }
            }
        }

    }
}
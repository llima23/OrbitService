﻿using B1Library.Documents;
using Newtonsoft.Json;
using OrbitLibrary.Common;
using OrbitService_Cancel_NFSe.Application;
using OrbitService_Cancel_NFSe.OutboundDFe.mappers;
using OrbitService_Cancel_NFSe.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Cancel_NFSe.OutboundDFe.usecases
{
    public class OutboundNFSeDocumentCancelUseCase
    {

        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;

        public OutboundNFSeDocumentCancelUseCase(IDocumentsRepository documentsRepository, ServiceConfiguration sConfig, CommunicationProvider communicationProvider)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            MapperInputNFSeCancel mapper = new MapperInputNFSeCancel();
            OutboundDFeDocumentCancelServicesNFSe outboundNFeRegister = new OutboundDFeDocumentCancelServicesNFSe(sConfig, communicationProvider);
            List<Invoice> OutBoundNFeDocumentsCancel = documentsRepository.GetCancelOutboundNFSe();
            Logs.InsertLog($"{OutBoundNFeDocumentsCancel.Count}");
            foreach (Invoice invoice in OutBoundNFeDocumentsCancel)
            {
                Logs.InsertLog($"Listou uma nota");
                OutboundDFeDocumentCancelInputNFSe input = mapper.MapperInvoiceB1ToOutboundDFeDocumentCancelInputNFSe(invoice);
                OperationResponse<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe> response = outboundNFeRegister.Execute(input);

                if (response.isSuccessful)
                {
                    Logs.InsertLog($"Response OK");
                    OutboundDFeDocumentCancelOutputNFSe output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }

                else
                {
                    Logs.InsertLog($"Response Erro");
                    OutboundDFeDocumentCancelOutputNFSe output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseError(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }
            }
        }
    }
}

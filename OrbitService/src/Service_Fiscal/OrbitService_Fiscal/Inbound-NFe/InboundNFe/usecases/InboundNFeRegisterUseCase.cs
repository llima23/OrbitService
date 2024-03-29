﻿using B1Library.Documents;
using Newtonsoft.Json;
using OrbitLibrary.Common;
using OrbitService.InboundNFe.mappers;
using OrbitService.InboundNFe.services.InboundNFeRegister;
using System;
using System.Collections.Generic;
using System.Text;
using static OrbitService.InboundNFe.services.InboundNFeRegister.InboundNFeDocumentRegisterOutput;

namespace OrbitService.InboundNFe.usecases
{
    public class InboundNFeRegisterUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;

        public InboundNFeRegisterUseCase(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDocumentsRepository documentsRepository)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            MapperInboundNFe mapper = new MapperInboundNFe();
            InboundNFeRegisterService inboundNFeRegister = new InboundNFeRegisterService(sConfig, communicationProvider);
            List<Invoice> inboundNFeDocuments = documentsRepository.GetInboundNFe();
            foreach (Invoice invoice in inboundNFeDocuments)
            {
                Root root = new Root();
                root.inboundNFeDocumentRegisterInput = mapper.ToinboundNFeDocumentRegisterInput(invoice);
                OperationResponse<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError> response = inboundNFeRegister.Execute(root);
                
                if (response.isSuccessful)
                {
                    InboundNFeDocumentRegisterOutput output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }

                else
                {
                    InboundNFeDocumentRegisterError output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseError(invoice,output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }
            }
        }
    }
}

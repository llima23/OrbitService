using B1Library.Documents;
using Newtonsoft.Json;
using OrbitLibrary.Common;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services.OutboundDFeRegister;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.usecases
{
    public class OutboundNFeRegisterUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;

        public OutboundNFeRegisterUseCase(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDocumentsRepository documentsRepository)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            MapperOutboundNFe mapper = new MapperOutboundNFe();
            OutboundNFeDocumentRegisterService outboundNFeRegister = new OutboundNFeDocumentRegisterService(sConfig, communicationProvider);
            List<Invoice> OutBoundNFeDocuments = documentsRepository.GetOutboundNFe();
            foreach (Invoice invoice in OutBoundNFeDocuments)
            {
                OutboundNFeDocumentRegisterInput input = new OutboundNFeDocumentRegisterInput();
                input = mapper.ToinboundNFeDocumentRegisterInput(invoice);
                OperationResponse<OutboundNFeDocumentRegisterOutput, OutboundNFeDocumentRegisterError> response = outboundNFeRegister.Execute(input);

                if (response.isSuccessful)
                {
                    OutboundNFeDocumentRegisterOutput output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }

                else
                {
                    //É usuado como dynamic pois a variavel 'value' do Output, retorna hora como string e hora como objeto, inviabilizando a deserialização do objeto.
                    dynamic output = JsonConvert.DeserializeObject(response.Content);
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseError(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus);
                }
            }
        }

    }
}

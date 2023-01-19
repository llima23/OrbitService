using B1Library.Applications;
using B1Library.Documents;
using Newtonsoft.Json;
using OrbitLibrary.Common;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services.OutboundDFeRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrbitService.OutboundDFe.usecases
{
    public class OutboundNFeRegisterUseCase
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        private Invoice invoice;

        public OutboundNFeRegisterUseCase(ServiceConfiguration sConfig, CommunicationProvider communicationProvider, IDocumentsRepository documentsRepository)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            try
            {
                MapperOutboundNFe mapper = new MapperOutboundNFe();
                OutboundNFeDocumentRegisterService outboundNFeRegister = new OutboundNFeDocumentRegisterService(sConfig, communicationProvider);
                List<Invoice> OutBoundNFeDocuments = documentsRepository.GetOutboundNFe();
                foreach (Invoice invoice in OutBoundNFeDocuments.OrderBy(x => x.DocEntry))
                {
                    this.invoice = invoice;
                    OutboundNFeDocumentRegisterInput input = new OutboundNFeDocumentRegisterInput();
                    input = mapper.ToinboundNFeDocumentRegisterInput(invoice);
                    OperationResponse<OutboundNFeDocumentRegisterOutput, OutboundNFeDocumentRegisterError> response = outboundNFeRegister.Execute(input);
                    Logs.InsertLog($"ContentResponse: {response.Content}");
                    if (response.isSuccessful)
                    {
                        OutboundNFeDocumentRegisterOutput output = response.GetSuccessResponse();
                        DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output, response.Content);
                        documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                    }

                    else
                    {
                        //É usuado como dynamic pois a variavel 'value' do Output, retorna hora como string e hora como objeto, inviabilizando a deserialização do objeto.
                        Logs.InsertLog($"ContentRequest: {response.Request.Body}");
                        dynamic output = JsonConvert.DeserializeObject(response.Content);
                        DocumentStatus documentStatus = mapper.ToDocumentStatusResponseError(invoice, output);
                        documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                    }
                }
            }
            catch(Exception ex)
            {
                DocumentStatus newStatusData = new DocumentStatus("", "", $"{ex.Message}", invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
                documentsRepository.UpdateDocumentStatus(newStatusData, invoice.ObjetoB1);
            }
        }

    }
}

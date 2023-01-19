using B1Library.Documents;
using DownloadAutomatico;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
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
            List<Invoice> OutBoundNFeDocumentsCancel = documentsRepository.GetConsultOutboundNFe();
            foreach (Invoice invoice in OutBoundNFeDocumentsCancel)
            {
                OperationResponse<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe> response = outboundNFeRegister.Execute(invoice);
                if (response.isSuccessful)
                {
                    OutboundDFeDocumentConsultaOutputNFe output = response.GetSuccessResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseSucessful(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);

                    if (documentStatus.Status == StatusCode.Sucess && invoice.DownloadAutomatico == "1")
                    {
                        DownloadAutomaticoXMLDanfe download = new DownloadAutomaticoXMLDanfe(sConfig, communicationProvider);
                        download.nfID = invoice.IdRetornoOrbit;
                        download.chaveSefaz = output.key;
                        download.modelo = invoice.ModeloDocumento;
                        download.ano = output.identificacao.dataHoraEmissao.ToString("yyyy");
                        download.mes = output.identificacao.dataHoraEmissao.ToString("MM");
                        download.caminhoPadraoPDF = invoice.CaminhoPDF;
                        download.caminhoPadraoXML = invoice.CaminhoXML;
                        download.DownloadDanfe();
                        download.DownloadXML();
                    }


                }
                else
                {
                    OutboundDFeDocumentConsulErroNFe output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseErro(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }
            }
        }

    }
}

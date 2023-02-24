using B1Library.Documents;
using B1Library.Documents.Entities;
using B1Library.EnviaEmail;
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

                    if ((documentStatus.Status == StatusCode.Sucess && documentStatus.Status == StatusCode.CanceladaSucess) && invoice.DownloadAutomatico == "1")
                    {
                        EnviaDownloadAutomatico(invoice, output);
                    }

                    if ((documentStatus.Status == StatusCode.Sucess || documentStatus.Status == StatusCode.CanceladaSucess) && invoice.EnviaEmailAutomatico == "S")
                    {
                        EnviaEmailAutomatico(invoice, output);
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

        private void EnviaDownloadAutomatico(Invoice invoice, OutboundDFeDocumentConsultaOutputNFe output)
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
        private void EnviaEmailAutomatico(Invoice invoice, OutboundDFeDocumentConsultaOutputNFe output)
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

            ConfigEmailAutomatico configEmail = documentsRepository.GetConfigEmail();
            EnviaEmailAutomatico envia = new EnviaEmailAutomatico(configEmail.SMTP, configEmail.UsuarioSMTP, configEmail.SenhaSMTP, configEmail.AutenticacaoSMTP == "Y" ? true : false, configEmail.PortaSMTP, configEmail.CriptografiaSSL == "Y" ? true : false);
            List<string> listEmails = new List<string>();

            if (configEmail.EnviaEmailContato == "Y")
            {
                foreach (Emails item in invoice.Emails)
                {
                    listEmails.Add(item.email);
                }
            }
            if (configEmail.EnviaEmailOculto == "Y")
            {

            }
            listEmails.Add(invoice.Parceiro.EmailParceiro);
            List<string> listAtt = new List<string>();
            listAtt.Add(download.caminhoArquivoDANFE);
            listAtt.Add(download.caminhoArquivoXML);
            envia.SendEmail(listEmails, configEmail.AssuntoEmail, formatedMessageEmail(configEmail.CorpoDeEmail,output), listAtt, "");
        }
        private string formatedMessageEmail(string message, OutboundDFeDocumentConsultaOutputNFe output)
        {
            message = message.Replace("@CNPJDest", !string.IsNullOrEmpty(output.destinatario.cnpj) ? output.destinatario.cnpj : output.destinatario.cpf);
            message = message.Replace("@Chave", output.key);
            message = message.Replace("@NumeroNota", output.identificacao.numeroDocFiscal);
            message = message.Replace("@NomeDest", output.destinatario.nome);
            return message;
        }
    }
}

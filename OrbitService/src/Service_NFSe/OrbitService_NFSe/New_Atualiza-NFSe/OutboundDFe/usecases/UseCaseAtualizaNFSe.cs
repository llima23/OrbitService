using B1Library.Documents;
using B1Library.Documents.Entities;
using B1Library.DownloadXMLDanfe_NFSe;
using B1Library.EnviaEmail;
using OrbitLibrary.Common;
using OrbitService_NFSe.New_Atualiza_NFSe.OutboundDFe.mappers;
using OrbitService_NFSe.New_Atualiza_NFSe.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_NFSe.New_Atualiza_NFSe.OutboundDFe.usecases
{
    public class UseCaseAtualizaNFSe
    {
        private IDocumentsRepository documentsRepository;
        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        public UseCaseAtualizaNFSe(IDocumentsRepository documentsRepository, ServiceConfiguration sConfig, CommunicationProvider communicationProvider)
        {
            this.documentsRepository = documentsRepository;
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void Execute()
        {
            MapperOrbitToB1AtualizaNFSe mapper = new MapperOrbitToB1AtualizaNFSe();
            AtualizaNFSeService service = new AtualizaNFSeService(sConfig, communicationProvider);
            List<Invoice> listInvoiceNFSe = documentsRepository.GetConsultOutboundNFSe();
            foreach (Invoice invoice in listInvoiceNFSe)
            {
                OperationResponse<AtualizaNFSeOutput, AtualizaNFSeError> response = service.Execute(invoice);
                if (response.isSuccessful)
                {
                    AtualizaNFSeOutput output = response.GetSuccessResponse();
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
                    AtualizaNFSeError output = response.GetErrorResponse();
                    DocumentStatus documentStatus = mapper.ToDocumentStatusResponseErro(invoice, output);
                    documentsRepository.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1);
                }
            }
        }

        private void EnviaDownloadAutomatico(Invoice invoice, AtualizaNFSeOutput output)
        {
            DownloadAutomaticoXMLDanfeNFSe download = new DownloadAutomaticoXMLDanfeNFSe(sConfig, communicationProvider);

            download.nfID = invoice.IdRetornoOrbit;
            download.modelo = invoice.ModeloDocumento;
            download.ano = output.rps.identificacao.dataEmissao.ToString("yyyy");
            download.mes = output.rps.identificacao.dataEmissao.ToString("MM");
            download.caminhoPadraoPDF = invoice.CaminhoPDF;
            download.caminhoPadraoXML = invoice.CaminhoXML;
            download.NFse = output.nfse.numero;
            download.rps = output.rps.identificacao.numero;
            download.DownloadDanfe();
            download.DownloadXML();
        }
        private void EnviaEmailAutomatico(Invoice invoice, AtualizaNFSeOutput output)
        {
            DownloadAutomaticoXMLDanfeNFSe download = new DownloadAutomaticoXMLDanfeNFSe(sConfig, communicationProvider);

            download.nfID = invoice.IdRetornoOrbit;
            download.modelo = invoice.ModeloDocumento;
            download.ano = output.rps.identificacao.dataEmissao.ToString("yyyy");
            download.mes = output.rps.identificacao.dataEmissao.ToString("MM");
            download.caminhoPadraoPDF = invoice.CaminhoPDF;
            download.caminhoPadraoXML = invoice.CaminhoXML;
            download.NFse = output.nfse.numero;
            download.rps = output.rps.identificacao.numero;
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
        private string formatedMessageEmail(string message, AtualizaNFSeOutput output)
        {
            message = message.Replace("@CNPJDest", string.IsNullOrEmpty(output.tomador.cnpj) ? output.tomador.cnpj : output.tomador.cpf);
            message = message.Replace("@RPS", output.nfse.numero);
            message = message.Replace("@NumeroNota", output.rps.identificacao.numero);
            message = message.Replace("@NomeDest", output.tomador.razaoSocial);
            return message;
        }
    }
}

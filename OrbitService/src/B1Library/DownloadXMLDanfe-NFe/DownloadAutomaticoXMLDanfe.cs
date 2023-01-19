using DownloadAutomatico.service;
using Microsoft.AspNetCore.Mvc;
using OrbitLibrary.Common;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml;

namespace DownloadAutomatico
{
    public class DownloadAutomaticoXMLDanfe
    {
        public string codigoIntegracao { get; set; }
        public string modelo { get; set; }
        public string nfID { get; set; }
        public string rps { get; set; }
        public string NFse { get; set; }
        public string caminhoPadraoPDF { get; set; }
        public string caminhoPadraoXML { get; set; }
        public string ano { get; set; }
        public string mes { get; set; }
        public string caminhoArquivo { get; set; }
        public string chaveSefaz { get; set; }
        public string prot { get; set; }
        private DownloadService service;

        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        public DownloadAutomaticoXMLDanfe(ServiceConfiguration sConfig, CommunicationProvider communicationProvider)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            service = new DownloadService(sConfig, communicationProvider);
        }

        public void DownloadDanfe()
        {
            DirectoryInfo di1 = Directory.CreateDirectory($@"{caminhoPadraoPDF.Trim()}\{ano}\{mes}\");
            if (modelo == "55")
            {
                caminhoArquivo = $@"{caminhoPadraoPDF.Trim()}\{ano}\{mes}\{chaveSefaz}.pdf";
            }
            else
            {
                caminhoArquivo = $@"{caminhoPadraoPDF.Trim()}\{ano}\{mes}\{"RPS_" + rps + "NFSE_" + NFse}.pdf";
            }
            ExecutePDF(nfID);
        }
        public void ExecutePDF(string id)
        {
            try
            {
                
                OperationResponse<object, object> response = service.ExecutePDF(id);
                byte[] bytes = Convert.FromBase64String(response.Content);
                System.IO.FileStream stream =
                new FileStream(caminhoArquivo, FileMode.CreateNew);
                System.IO.BinaryWriter writer =
                new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
            }
            catch (Exception ex) 
            { 

            }
        }

        public void DownloadXML()
        {
            if (modelo == "55")
            {
                DirectoryInfo di = Directory.CreateDirectory($@"{caminhoPadraoXML.Trim()}\{ano}\{mes}\XML Autorizado\");
                caminhoArquivo = $@"{caminhoPadraoXML.Trim()}\{ano}\{mes}\XML Autorizado\{chaveSefaz}.xml";

                if(codigoIntegracao == "5")
                {
                    di = Directory.CreateDirectory($@"{caminhoPadraoXML.Trim()}\{ano}\{mes}\XML de Cancelamento\");
                    caminhoArquivo = $@"{caminhoPadraoXML.Trim()}\{ano}\{mes}\XML de Cancelamento\{prot}.xml";
                }
            }
            else
            {
                caminhoArquivo = $@"{caminhoPadraoXML.Trim()}\{ano}\{mes}\{"RPS_" + rps + "NFSE_" + NFse}.pdf";
            }

            OperationResponse<object, object> response = service.ExecuteXML(nfID);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response.Content);
            doc.PreserveWhitespace = true;
            doc.Save(caminhoArquivo);
        }
    }
}

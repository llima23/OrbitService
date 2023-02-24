using B1Library.DownloadXMLDanfe_NFSe.service;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace B1Library.DownloadXMLDanfe_NFSe
{
    public class DownloadAutomaticoXMLDanfeNFSe
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
        public string caminhoArquivoDANFE { get; set; }
        public string caminhoArquivoXML { get; set; }
        public string chaveSefaz { get; set; }
        public string prot { get; set; }
        private DownloadAutomaticoXMLDANFENFSe service;

        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        public DownloadAutomaticoXMLDanfeNFSe(ServiceConfiguration sConfig, CommunicationProvider communicationProvider)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
            service = new DownloadAutomaticoXMLDANFENFSe(sConfig, communicationProvider);
        }
        public void DownloadDanfe()
        {
            DirectoryInfo di1 = Directory.CreateDirectory($@"{caminhoPadraoPDF.Trim()}\NFS-e\{ano}\{mes}\");
            caminhoArquivoDANFE = $@"{caminhoPadraoPDF.Trim()}\NFS-e\{ano}\{mes}\{"RPS_" + rps + "NFSE_" + NFse}.pdf";
            ExecutePDF(nfID);
        }
        public void ExecutePDF(string id)
        {
            try
            {
                OperationResponse<object, object> response = service.ExecutePDF(id);
                byte[] bytes = Convert.FromBase64String(response.Content);
                System.IO.FileStream stream =
                new FileStream(caminhoArquivoDANFE, FileMode.CreateNew);
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
            DirectoryInfo di1 = Directory.CreateDirectory($@"{caminhoPadraoXML.Trim()}\NFS-e\{ano}\{mes}\");
            caminhoArquivoXML = $@"{caminhoPadraoXML.Trim()}\NFS-e\{ano}\{mes}\{"RPS_" + rps + "NFSE_" + NFse}.xml";
            OperationResponse<object, object> response = service.ExecuteXML(nfID);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response.Content);
            doc.PreserveWhitespace = true;
            doc.Save(caminhoArquivoXML);
        }
    }
}

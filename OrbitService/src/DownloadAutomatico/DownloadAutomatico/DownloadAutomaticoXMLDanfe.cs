using DownloadAutomatico.service;
using OrbitLibrary.Common;
using System;
using System.IO;

namespace DownloadAutomatico
{
    public class DownloadAutomaticoXMLDanfe
    {
        public string modelo { get; set; }
        public string nfID { get; set; }
        public string rps { get; set; }
        public string NFse { get; set; }
        public string caminhoPadrao { get; set; }
        public string ano { get; set; }
        public string mes { get; set; }
        public string caminhoArquivo { get; set; }
        public string chaveSefaz { get; set; }

        private ServiceConfiguration sConfig;
        private CommunicationProvider communicationProvider;
        public DownloadAutomaticoXMLDanfe(ServiceConfiguration sConfig, CommunicationProvider communicationProvider)
        {
            this.sConfig = sConfig;
            this.communicationProvider = communicationProvider;
        }

        public void DownloadDanfe()
        {
            if (modelo == "46")
            {
                DirectoryInfo di1 = Directory.CreateDirectory($@"{caminhoPadrao.Trim()}\{ano}\{mes}\");
                caminhoArquivo = $@"{caminhoPadrao.Trim()}\{ano}\{mes}\{"RPS_" + rps + "NFSE_" + NFse}.pdf";
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory($@"{caminhoPadrao.Trim()}\{ano}\{mes}\");
                caminhoArquivo = $@"{caminhoPadrao.Trim()}\{ano}\{mes}\{chaveSefaz}.pdf";
            }

            DownloadService downloadService = new DownloadService(sConfig, communicationProvider);
            OperationResponse<object, object> response = downloadService.Execute(nfID);
            if (response.isSuccessful)
            {
                string teste = response.Content;
            }
        }
    }
}

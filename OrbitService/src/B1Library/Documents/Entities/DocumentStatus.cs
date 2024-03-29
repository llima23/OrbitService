﻿namespace B1Library.Documents
{

    public enum StatusCode
    {
        NotasParaEmitir = 0,
        FilaDeEmissao = 1,
        Sucess = 2,
        Erro = 3,
        CancelEmProcess = 4,
        CanceladaSucess = 5,
        InutilizadaSucess = 6,
        CargaFiscal = 7
    }

    public class DocumentStatus
    {
        public const string StatusMessageNFeInseridaNaFilaDeEmissao = "NFe inserida na fila de emissão";
        public const string StatusMessageCancelEmProcess = "Cancelamento em Processo";
        public const string StatusMessageCancelSucess = "Cancelamento Efetuado com Sucesso";
        public const string StatusMessageInutilSucess = "Inutilização Efetuado com Sucesso";
        public const string StatusMessageCargaFiscalEfetuada = "Carga fiscal efetuada";
        

        public string IdOrbit { get; set; }
        public string StatusOrbit { get; set; } = "";
        public string Descricao { get; set; } = "";
        public string ModeloDocumento { get; set; }
        public string ChaveDeAcessoNFe { get; set; }
        public string ProtocoloNFe { get; set; }
        public string CommunicationId { get; set; }
        public string CodVerificadorNFSe { get; set; }
        public string NumeroRPSNFSe { get; set; }
        public string NumeroNFSe { get; set; }
        public int ObjetoB1 { get; set; }
        public int DocEntry { get; set; }
        public int BaseEntry { get; set; }
        public int CodigoStatusOrbitB1 { get; set; }
        public StatusCode Status { get; set; }

        public DocumentStatus(string OutputIdOrbit, string OutputStatusOrbit, string OutputDescricao, int InvoiceObjetoB1, int InvoiceDocEntry, StatusCode status, string chaveDeAcesso = null, string protocoloNFe = null, int baseEntry = 0, string communicationId = null, string modeloDocumento = null, string codVerificadorNFSe = null, string numeroRPSNFSe = null, string numeroNFSe = null)
        {
            CodigoStatusOrbitB1 = (int)status;
            IdOrbit = OutputIdOrbit;
            StatusOrbit = OutputStatusOrbit;
            Descricao = OutputDescricao;
            ObjetoB1 = InvoiceObjetoB1;
            DocEntry = InvoiceDocEntry;
            Status = status;
            ChaveDeAcessoNFe = chaveDeAcesso;
            ProtocoloNFe = protocoloNFe;
            CommunicationId = communicationId;
            BaseEntry = baseEntry;
            ModeloDocumento = modeloDocumento;
            CodVerificadorNFSe = codVerificadorNFSe;
            NumeroRPSNFSe = numeroRPSNFSe;
            NumeroNFSe = numeroNFSe;
        }
        public string GetStatusMessage()
        {
            return Status switch
            {
                StatusCode.FilaDeEmissao => StatusMessageNFeInseridaNaFilaDeEmissao,
                StatusCode.Sucess => Descricao,
                StatusCode.Erro => Descricao,
                StatusCode.CancelEmProcess => StatusMessageCancelEmProcess,
                StatusCode.CanceladaSucess => StatusMessageCancelSucess,
                StatusCode.InutilizadaSucess => StatusMessageInutilSucess,
                StatusCode.CargaFiscal => StatusMessageCargaFiscalEfetuada,
                _ => "",
            };
        }

    }
}
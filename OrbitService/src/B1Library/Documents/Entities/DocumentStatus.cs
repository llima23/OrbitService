namespace B1Library.Documents
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
        public string StatusOrbit { get; set; }
        public string Descricao { get; set; }
        public int ObjetoB1 { get; set; }
        public int DocEntry { get; set; }
        public int CodigoStatusOrbitB1 { get; set; }
        public StatusCode Status { get; set; }

        public DocumentStatus(string OutputIdOrbit, string OutputStatusOrbit, string OutputDescricao, int InvoiceObjetoB1, int InvoiceDocEntry, StatusCode status)
        {
            CodigoStatusOrbitB1 = (int)status;
            IdOrbit = OutputIdOrbit;
            StatusOrbit = OutputStatusOrbit;
            Descricao = OutputDescricao;
            ObjetoB1 = InvoiceObjetoB1;
            DocEntry = InvoiceDocEntry;
            Status = status;
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
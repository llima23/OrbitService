using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_NFSe.New_Atualiza_NFSe.OutboundDFe.services
{
    public class AtualizaNFSeOutput
    {
        public AtualizaNFSeOutput()
        {
            nfse = new Nfse();
        }

        public string _id;
        public List<Eventos> eventos { get; set; }
        public Status status { get; set; }
        public Nfse nfse { get; set; }
        public Rps rps { get; set; }
        public Tomador tomador { get; set; }
    }

    public static class StatusMessageOrbitList
    {
        public const string EmProcesso = "EM PROCESSO";
        public const string EnvioEmProcesso = "ENVIO EM PROCESSAMENTO";
        public const string RPSEmitida = "RPS EMITIDA";
        public const string NFSeEmitida = "NFSE EMITIDA";
        public const string Cancelada = "CANCELADA";
        public const string RPSHomologado = "RPS HOMOLOGADO";
        public const string Inutilizada = "INUTILIZADA";
        public const string CancelamentoEmProcesso = "CANCELAMENTO EM PROCESSAMENTO";
        public const string CancelamentoEmProcessoPrefeitura = "CANCELAMENTO EM PROCESSAMENTO PELA PREFEITURA";
        public const string RpsNaoEmitida = "RPS NÂO EMITIDA";
        public const string UnknowError = "ERRO NA EMISSAO";
        public const string ValidationError = "ERRO NA VALIDACAO";
        public const string UnwantedTwin = "Unwanted Twin";
    }

    public class Eventos
    {
        public string type { get; set; }
        public string protocolo { get; set; }
        public string dhEvento { get; set; }
    }
    public class Status
    {
        public string cStat { get; set; }
        public string mStat { get; set; }
    }
    public class Nfse
    {
        public string numero { get; set; }
        public string codigoVerificacao { get; set; }
    }
    public class Rps
    {
        public Identificacao identificacao { get; set; }
    }

    public class Identificacao
    {
        public string numero { get; set; }
        public DateTime dataEmissao { get; set; }
    }
    public class Tomador
    {
        public string cnpj { get; set; }
        public string cpf { get; set; }
        public string razaoSocial { get; set; }
    }
}

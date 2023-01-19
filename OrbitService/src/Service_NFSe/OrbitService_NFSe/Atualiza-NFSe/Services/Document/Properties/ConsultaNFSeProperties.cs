using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace _4TAX_Service_Atualiza.Services.Document.Properties
{
    public partial class Consulta
    {
        public struct ConsultaSuccessResponseOutput
        {
            public string _id;
            public List<Eventos> eventos { get; set; }
            public Status status { get; set; }
            public Nfse nfse { get; set; }
            public Rps rps { get; set; }
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
        
            public static string GetStringValue(Enum value)
            {
                string output = null;
                Type type = value.GetType();
                FieldInfo fi = type.GetField(value.ToString());
                StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
                if (attrs.Length > 0)
                {
                    output = attrs[0].Value;

                }
                return output;
            }
            public class StringValue : System.Attribute
            {
                private readonly string _value;

                public StringValue(string value)
                {
                    _value = value;
                }

                public string Value
                {
                    get { return _value; }
                }
            }
        }

        public struct ConsultaFailedResponseOutput
        {
            public string code;

            public string message;
        }

    }
}

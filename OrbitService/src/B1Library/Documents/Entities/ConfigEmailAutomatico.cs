using System;
using System.Collections.Generic;
using System.Text;

namespace B1Library.Documents.Entities
{
    public class ConfigEmailAutomatico
    {
        public string AssuntoEmail { get; set; }
        public string AutenticacaoSMTP { get; set; }
        public string CorpoDeEmail { get; set; }
        public string CriptografiaSSL { get; set; }
        public string EmailRemetente { get; set; }
        public string EnviaEmailAutNFSe { get; set; }
        public string EnviaEmailAutNFe { get; set; }
        public string EnviaEmailCancelNFSe { get; set; }
        public string EnviaEmailCancelNFe { get; set; }
        public string EnviaEmailContato { get; set; }
        public string EnviaEmailOculto { get; set; }
        public string NomeDeExibicao { get; set; }
        public int PortaSMTP { get; set; }
        public string SMTP { get; set; }
        public string SenhaSMTP { get; set; }
        public string UsuarioSMTP { get; set; }
    }
}

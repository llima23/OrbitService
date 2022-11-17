using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Common
{
    public class ServiceConfiguration
    {
        private Uri BaseURI;
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool Ativo { get; private set; }
        public bool IntegraDocDFe { get; private set; }
        public bool IntegraDocFiscal { get; private set; }
        public Guid TenantID { get; private set; }

        public CredentialsProvider CredentialsProvider { get; set; }

        public ServiceConfiguration(Uri baseURI, string username, string password, bool ativo, Guid tenantID, bool integraDocDFe, bool integraDocFiscal)
        {
            // TODO: Validate the parameters
            BaseURI = baseURI;
            Username = username;
            Password = password;
            TenantID = tenantID;
            Ativo = ativo;
            IntegraDocDFe = integraDocDFe;
            IntegraDocFiscal = integraDocFiscal;
        }

        public string GetBaseURI()
        {
            return BaseURI.AbsoluteUri.EndsWith("/") ? BaseURI.AbsoluteUri.TrimEnd('/') : BaseURI.AbsoluteUri;
        }
    }
}

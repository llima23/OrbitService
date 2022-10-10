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
        public Guid TenantID { get; private set; }

        public CredentialsProvider CredentialsProvider { get; set; }

        public ServiceConfiguration(Uri baseURI, string username, string password, Guid tenantID)
        {
            // TODO: Validate the parameters
            BaseURI = baseURI;
            Username = username;
            Password = password;
            TenantID = tenantID;
        }

        public string GetBaseURI()
        {
            return BaseURI.AbsoluteUri.EndsWith("/") ? BaseURI.AbsoluteUri.TrimEnd('/') : BaseURI.AbsoluteUri;
        }
    }
}

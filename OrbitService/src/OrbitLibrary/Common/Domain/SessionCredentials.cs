using System;

namespace OrbitLibrary.Common
{
    public class SessionCredentials
    {
        public string Token { get; private set; }
        public string xApiKey { get; private set; }
        public string TenantId { get; private set; }
        public SessionCredentials()
        {

        }

        public SessionCredentials(string xApiKey_, string token, string TenantId_)
        {
            xApiKey = xApiKey_;
            Token = token;
            TenantId = TenantId_;
        }

        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(Token) && !string.IsNullOrEmpty(xApiKey))
            {
                return true;
            }
            return false;
        }
    }
}

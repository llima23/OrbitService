using System;

namespace OrbitLibrary.Common
{
    public class SessionCredentials
    {
        public string Token { get; private set; }
        public string xApiKey { get; private set; }        

        public SessionCredentials()
        {

        }

        public SessionCredentials(string xApiKey_, string token)
        {
            xApiKey = xApiKey_;
            Token = token;            
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

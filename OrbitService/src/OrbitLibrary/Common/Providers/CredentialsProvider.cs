using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Common
{
    public interface CredentialsProvider
    {
        SessionCredentials ResolveCredentials(bool forceNewAuthentication = false);
    }
}

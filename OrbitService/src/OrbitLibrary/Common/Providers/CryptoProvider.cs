using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Common.Providers
{
    public interface CryptoProvider
    {
        public string Encrypt(string value, string publicKey, string secretKey);
        public string Decrypt(string encryptedValue, string publicKey, string secretKey);
    }
}

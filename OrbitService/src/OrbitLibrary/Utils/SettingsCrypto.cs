using OrbitLibrary.Common.Providers;
using OrbitLibrary.Crypto;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Utils
{
    public class SettingsCrypto
    {
        private CryptoProvider cryptoProvider;
        private const string publicKey = "SeidorB1";
        private const string secretKey = "OrbitB1C";

        public SettingsCrypto()
        {
            cryptoProvider = new CryptoBase64();
        }
        public string Encrypt(string value)
        {
            return cryptoProvider.Encrypt(value, publicKey, secretKey);
        }

        public string Decrypt(string encryptedValue)
        {
            return cryptoProvider.Decrypt(encryptedValue, publicKey, secretKey);
        }
    }
}

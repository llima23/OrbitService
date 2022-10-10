using OrbitLibrary.Crypto;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrbitLibrary_Test.Crypto
{
    public class CryptoBase64Test
    {
        private CryptoBase64 cut;
        public CryptoBase64Test()
        {
            cut = new CryptoBase64();
        }

        [Fact]
        public void ShouldEncryptAStringBasedOnKeys()
        {
            const string expectedEncryptedString = "OSECK5lFHik=";
            string encryptedString = cut.Encrypt("orbit", "Orbit022", "Orbit022");
            Assert.Equal(expectedEncryptedString, encryptedString);
        }

        [Fact]
        public void ShouldDecryptValue()
        {
            const string expectedValue = "orbit";
            string result = cut.Decrypt("OSECK5lFHik=", "Orbit022", "Orbit022");
            Assert.Equal(expectedValue, result);
        }
    }
}

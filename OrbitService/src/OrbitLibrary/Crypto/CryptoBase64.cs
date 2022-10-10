using OrbitLibrary.Common.Providers;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OrbitLibrary.Crypto
{
    public class CryptoBase64 : CryptoProvider
    {
        public string Decrypt(string encryptedValue, string publicKey, string secretKey)
        {
            string ToReturn = "";
            byte[] privatekeyByte = { };
            privatekeyByte = System.Text.ASCIIEncoding.ASCII.GetBytes(secretKey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.ASCIIEncoding.ASCII.GetBytes(publicKey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = new byte[encryptedValue.Replace(" ", "+").Length];
            inputbyteArray = Convert.FromBase64String(encryptedValue.Replace(" ", "+"));
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                ToReturn = encoding.GetString(ms.ToArray());
            }
            return ToReturn;
        }

        public string Encrypt(string value, string publicKey, string secretKey)
        {
            string textToEncrypt = value;
            string ToReturn = "";
            byte[] secretkeyByte = { };
            secretkeyByte = System.Text.ASCIIEncoding.ASCII.GetBytes(secretKey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.ASCIIEncoding.ASCII.GetBytes(publicKey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(textToEncrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                ToReturn = Convert.ToBase64String(ms.ToArray());
            }
            return ToReturn;
        }
    }
}

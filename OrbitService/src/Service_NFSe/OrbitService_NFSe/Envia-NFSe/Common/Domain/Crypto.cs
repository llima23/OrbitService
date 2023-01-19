using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service.Application
{
    public static class Crypto
    {
        /// <summary>
        /// Descriptografar string Base64Encode
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string Decrypt(string value)
        {
            try
            {
                string chaveCripto;
                Byte[] cript = Convert.FromBase64String(value);
                chaveCripto = System.Text.ASCIIEncoding.ASCII.GetString(cript);
                return chaveCripto;
            }
            catch (Exception ex)
            {
                return $@"Erro ao descriptografar: {ex.Message}";
            }

        }

        /// <summary>
        /// Descriptografar string Base64Encode
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string Encrypt(string value)
        {
            try
            {
                string chaveCripto;
                Byte[] cript = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
                chaveCripto = Convert.ToBase64String(cript);
                return chaveCripto;
            }
            catch (Exception ex)
            {
                return $@"Erro ao criptografar dados: {ex.Message}";
            }
        }
    }
}

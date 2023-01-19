using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service.Common.Domain
{
    public class Functions
    {
        /// <summary>
        /// Método dinâmico para remoção de caracteres especiais
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveCaracteresEspeciais(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                return input.Replace(".", "").Replace("-", "").Replace(" - ", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace(":", "").Replace(" ", ""); 
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Método dinâmico para remoção de acento de uma string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoverAcento(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                string withDiacritics = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
                string withoutDiacritics = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
                for (int i = 0; i < withDiacritics.Length; i++)
                {
                    text = text.Replace(withDiacritics[i].ToString(), withoutDiacritics[i].ToString());
                }

                return text;
            }

            else
                return null;                        
        }

        public static string ReadInfoFromAppSettings(string type, string key)
        {
            try
            {
                var AppSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string resultInfo = String.Empty;
                resultInfo = AppSettings.GetSection(type)[key];
                return resultInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}

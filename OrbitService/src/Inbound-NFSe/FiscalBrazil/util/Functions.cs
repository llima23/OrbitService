using System;

namespace OrbitService.FiscalBrazil.util
{
    public class Functions
    {
        public string setTipoRps(string RPS, string MunFilial)
        {
            string result = "RPS";

            if (!string.IsNullOrEmpty(RPS))
            {
                result = RPS;

                if ((MunFilial == "3283" || MunFilial == "2188") && RPS == "RPS")
                {
                    result = "1";
                }
                if ((MunFilial == "3283" || MunFilial == "2188") && RPS == "RPS-M")
                {
                    result = "2";
                }
                if ((MunFilial == "3283" || MunFilial == "2188") && RPS == "RPS-C")
                {
                    result = "3";
                }
            }

            return result;
        }

        public string RemoveCaracteresEspeciais(string input)
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

        public string RemoverAcento(string text)
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
    }
}

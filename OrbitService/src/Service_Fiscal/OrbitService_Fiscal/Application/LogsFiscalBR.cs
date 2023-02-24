using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrbitService_Fiscal.Application
{
    public class LogsFiscalBR
    {
        protected static string CaminhoLOGTXT = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\LOGFISCALBR.txt";

        /// <summary>
        /// Método dinâmico para inserção de log em documento txt definido no CaminhoLOGTXT
        /// </summary>
        /// <param name="Mensagem">Mensagem de Log</param>
        public static void InsertLog(string Mensagem)
        {
            using (StreamWriter XWriter = new StreamWriter(CaminhoLOGTXT, true))
            {
                try
                {
                    XWriter.WriteLine(DateTime.Now + " - " + Mensagem);
                    XWriter.Flush();
                    XWriter.Close();
                }
                finally
                {
                    XWriter.Dispose();
                }
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _4TAX_Service.Application
{
    public class Logs
    {
        protected static string CaminhoLOGTXT = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\LOG.txt";

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

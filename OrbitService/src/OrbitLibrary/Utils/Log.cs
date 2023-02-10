using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrbitService_COL_Fiscal.Utils
{
    public class Log
    {
        protected static string pathLog = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
        private static object locker = new object();
        public static void InsertLog(string message)
        {
            string dateStr = DateTime.Now.ToString("ddMMyyyy");
            if (!Directory.Exists(pathLog + "\\Logs"))
            {
                DirectoryInfo di = Directory.CreateDirectory(pathLog + "\\Logs");
            }

            string filePath = pathLog + "\\Logs\\ProcessLog_" + dateStr + ".txt";
            FileInfo fi = new FileInfo(filePath);

            lock (locker)
            {
                using (FileStream file = new FileStream(fi.FullName, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter streamWriter = new StreamWriter(file))
                {
                    streamWriter.WriteLine($"[{ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}] {message}");
                    streamWriter.Close();
                }
            }
        }
    }
}

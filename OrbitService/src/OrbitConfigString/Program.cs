using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionStringGenerator
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        //Fazer o Publish para gerar o arquivo.
        [STAThread]
        static void Main()
        {
            FormConnectionString form = new FormConnectionString();
            form.ShowDialog();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4TAX_Service
{
    public class Program
    {

        /*
         * COMANDOS PARA GERAR O SERVIÇO APÓS TER FEITO O PUBLISH EM ALGUMA PASTA.
         * PS: ARQUIVO appsettings.json deve estar dentro do C > Windows > System32
         * 
         * sc.exe create "{NOME DO SERVIÇO}" binpath="{CAMINHO PASTA \ NOME ARQUIVO.EXE}"
         * 
         */



        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}

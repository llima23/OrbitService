using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _4TAX_Service_Atualiza
{
    #region Notas de versão
    /* 
           * V1.1.0.1
           * Rafael Cordeiro Rodrigues
           * 19/09/2022
           * Ajuste: Tratamento de retorno do Status nulo do Orbit, colocando novamente a nota na fila de atualização de status
           * Classe: MyQuery
    */
    /* 
        * V1.1.0.2
        * Rafael Cordeiro Rodrigues
        * 27/09/2022
        * Ajuste: Ajuste Log para identificar Erro integração Orbit
        * Classe: NFSeProcess
    */
    /* 
      * V1.1.0.3
      * Rafael Cordeiro Rodrigues
      * 29/09/2022
      * Ajuste: Ajuste Log mStat | Default GetStatusOrbitToB1 = 3
      * Classe: MyQuery
  */

    #endregion
    public class Program
    {
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

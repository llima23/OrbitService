using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using _4TAX_Service_Atualiza.Application;
using OrbitLibrary.Common;
using _4TAX_Service_Atualiza.Services.Document.NFSe;
using _4TAX_Service_Atualiza.Application.Client;
using OrbitLibrary.Utils;
using OrbitLibrary.Infrastructure.Repositories;
using OrbitLibrary.Data;
using System.Collections.Generic;

namespace _4TAX_Service_Atualiza
{
    public class Worker : BackgroundService
    {
        protected static string CaminhoLOGTXT = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\LOG_Atualiza.txt";

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
        public override void Dispose()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(1000, stoppingToken);
                    List<ServiceDependencies> ListserviceDependencies = Defaults.GetListServiceDependencies();
                    foreach (ServiceDependencies serviceDependencies in ListserviceDependencies)
                    {
                        if(serviceDependencies.sConfig.Ativo && serviceDependencies.sConfig.IntegraDocDFe)
                        {
                            NFSeProcess nFSeProcess = new NFSeProcess(serviceDependencies.sConfig, serviceDependencies.DbWrapper);
                            NFSeFetch nFSeFetch = new NFSeFetch(serviceDependencies.DbWrapper);
                            nFSeProcess.IntegrateNFSe(nFSeFetch.GetListNFSe(), new Consulta(serviceDependencies.sConfig, Defaults.GetCommunicationProvider()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logs.InsertLog($"Erro Execução serviço Atualiza NFSe: {ex}");
                }
            }
        }
    }
}

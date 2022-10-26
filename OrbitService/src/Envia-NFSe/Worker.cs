using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using _4TAX_Service.Application;
using OrbitLibrary.Common;
using OrbitLibrary.Services.Session;
using _4TAX_Service.Services.Document.NFSe;
using _4TAX_Service.Common.Domain;
using static _4TAX_Service.Services.Document.Properties.Emit;
using Newtonsoft.Json;
using _4TAX_Service.Application.Client;
using OrbitLibrary;
using OrbitLibrary.Utils;
using OrbitLibrary.Repositories;
using OrbitLibrary.Infrastructure.Repositories;
using OrbitLibrary.Data;
using System.Data.Odbc;
using OrbitLibrary.Infrastructure.Data;

namespace _4TAX_Service
{
    public class Worker : BackgroundService
    {
        protected static string CaminhoLOGTXT = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\LOG.txt";

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
                        NFSeProcess nFSeProcess = new NFSeProcess(serviceDependencies.sConfig);
                        NFSeFetch nFSeFetch = new NFSeFetch(serviceDependencies.DbWrapper);
                        nFSeProcess.IntegrateNFSe(nFSeFetch.GetListNFSe(), new EmitMapper(), new Emit(serviceDependencies.sConfig, Defaults.GetCommunicationProvider()));
                    }
                }
                catch (Exception ex)
                {
                    Logs.InsertLog($"Erro Execução serviço Envia NFSe: {ex}");
                }
            }
        }

    }
}

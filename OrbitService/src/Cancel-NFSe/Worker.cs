using B1Library.Implementations.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrbitLibrary.Common;
using OrbitLibrary.Data;
using OrbitLibrary.Infrastructure.Repositories;
using OrbitLibrary.Utils;
using OrbitService_Cancel_NFSe.Application;
using OrbitService_Cancel_NFSe.OutboundDFe.usecases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrbitService_Cancel_NFSe
{
    public class Worker : BackgroundService
    {
        protected static string CaminhoLOGTXT = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()) + "\\LOG.txt";

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
                try
                {
                    IWrapper dbWrapper = Defaults.GetWrapper();
                    DBServiceConfigurationRepository repo = new DBServiceConfigurationRepository(dbWrapper);
                    ServiceConfiguration sConfig = repo.GetConfiguration();
                    OutboundNFSeDocumentCancelUseCase use = new OutboundNFSeDocumentCancelUseCase(new DBDocumentsRepository(dbWrapper, "SBO_GRINGO_PRD"), sConfig, Defaults.GetCommunicationProvider());
                    Logs.InsertLog($"Execute USE");
                    use.Execute();
                }
                catch(Exception ex) 
                {
                    Logs.InsertLog($"Erro Execução serviço Envia NFSe: {ex}");
                }
           
            }
        }
    }
}

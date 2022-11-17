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
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(1000, stoppingToken);
                    List<ServiceDependencies> ListserviceDependencies = Defaults.GetListServiceDependencies();
                    foreach (ServiceDependencies serviceDependencies in ListserviceDependencies)
                    {
                        if (serviceDependencies.sConfig.Ativo && serviceDependencies.sConfig.IntegraDocDFe)
                        {
                            try
                            {
                                OutboundNFSeDocumentCancelUseCase useCase = new OutboundNFSeDocumentCancelUseCase(new DBDocumentsRepository(serviceDependencies.DbWrapper),serviceDependencies.sConfig, serviceDependencies.communicationProvider);
                                useCase.Execute();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }
    }
}

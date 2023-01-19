using B1Library.Implementations.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrbitLibrary.Utils;
using OrbitService.OutboundDFe.usecases;
using OrbitService.OutboundNFe.usecases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrbitService_NFe
{
    public class Worker : BackgroundService
    {
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
                            //ExecuteEnviaNFe(serviceDependencies);
                            //ExecuteAtualizaNFe(serviceDependencies);
                            //ExecuteCancelaNFe(serviceDependencies);
                            //ExecuteInutilizaNFe(serviceDependencies);
                            Thread t = new Thread(() => ExecuteEnviaNFe(serviceDependencies));
                            t.Start();
                            Thread t2 = new Thread(() => ExecuteAtualizaNFe(serviceDependencies));
                            t2.Start();
                            Thread t3 = new Thread(() => ExecuteCancelaNFe(serviceDependencies));
                            t3.Start();
                            Thread t4 = new Thread(() => ExecuteInutilizaNFe(serviceDependencies));
                            t4.Start();
                            t.Join();
                            t2.Join();
                            t3.Join();
                            t4.Join();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

            }
        }
        private void ExecuteEnviaNFe(ServiceDependencies serviceDependencies)
        {
            try
            {
                OutboundNFeRegisterUseCase useCase = new OutboundNFeRegisterUseCase(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBDocumentsRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {

            }

        }
        private void ExecuteAtualizaNFe(ServiceDependencies serviceDependencies)
        {
            try
            {
                OutboundNFeDocumentConsultaUseCase useCase = new OutboundNFeDocumentConsultaUseCase(new DBDocumentsRepository(serviceDependencies.DbWrapper), serviceDependencies.sConfig, serviceDependencies.communicationProvider);
                useCase.Execute();
            }
            catch
            {

            }

        }
        private void ExecuteCancelaNFe(ServiceDependencies serviceDependencies)
        {
            try
            {
                OutboundNFeDocumentCancelUseCase useCase = new OutboundNFeDocumentCancelUseCase(new DBDocumentsRepository(serviceDependencies.DbWrapper), serviceDependencies.sConfig, serviceDependencies.communicationProvider);
                useCase.Execute();
            }
            catch
            {

            }

        }
        private void ExecuteInutilizaNFe(ServiceDependencies serviceDependencies)
        {
            try
            {
                OutboundNFeDocumentInutilUseCase useCase = new OutboundNFeDocumentInutilUseCase(new DBDocumentsRepository(serviceDependencies.DbWrapper), serviceDependencies.sConfig, serviceDependencies.communicationProvider);
                useCase.Execute();
            }
            catch
            {

            }

        }
    }
}

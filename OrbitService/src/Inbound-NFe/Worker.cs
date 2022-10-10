using B1Library.Implementations.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrbitLibrary.Utils;
using OrbitService.FiscalBrazil.usecases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrbitService
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
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                //Nossas threads
                List<ServiceDependencies> ListserviceDependencies = Defaults.GetListServiceDependencies();
                foreach (ServiceDependencies serviceDependencies in ListserviceDependencies)
                {
                    try
                    {
                       InboundNFeRegisterUseCase useCase = new InboundNFeRegisterUseCase(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBDocumentsRepository(serviceDependencies.DbWrapper,serviceDependencies.DatabaseName));
                       useCase.Execute();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
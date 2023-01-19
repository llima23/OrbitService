using _4TAX_Service.Application;
using _4TAX_Service.Application.Client;
using _4TAX_Service.Services.Document.NFSe;
using _4TAX_Service_Atualiza.Application;
using _4TAX_Service_Atualiza.Application.Client;
using _4TAX_Service_Atualiza.Services.Document.NFSe;
using B1Library.Implementations.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrbitLibrary.Utils;
using OrbitService.OutboundDFe.usecases;
using OrbitService_Cancel_NFSe.OutboundDFe.usecases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrbitService_NFSe
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
                            //ExecuteEnviaNFSe(serviceDependencies);
                            //ExecuteAtualizaNFSe(serviceDependencies);
                            //ExecuteCancelaNFSe(serviceDependencies);
                            //ExecuteInutilizaNFSe(serviceDependencies);
                            Thread t = new Thread(() => ExecuteEnviaNFSe(serviceDependencies));
                            t.Start();
                            Thread t2 = new Thread(() => ExecuteAtualizaNFSe(serviceDependencies));
                            t2.Start();
                            Thread t3 = new Thread(() => ExecuteCancelaNFSe(serviceDependencies));
                            t3.Start();
                            Thread t4 = new Thread(() => ExecuteInutilizaNFSe(serviceDependencies));
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
                    B1Library.Applications.Logs.InsertLog($"{ex.Message}");
                }

            }
        }
        private void ExecuteEnviaNFSe(ServiceDependencies serviceDependencies)
        {
            try
            {
                NFSeProcess nFSeProcess = new NFSeProcess(serviceDependencies.sConfig, serviceDependencies.DbWrapper);
                NFSeFetch nFSeFetch = new NFSeFetch(serviceDependencies.DbWrapper);
                nFSeProcess.IntegrateNFSe(nFSeFetch.GetListNFSe(), new EmitMapper(), new Emit(serviceDependencies.sConfig, Defaults.GetCommunicationProvider()));
            }
            catch
            {

            }
        }
        private void ExecuteAtualizaNFSe(ServiceDependencies serviceDependencies)
        {
            try
            {
                NFSeProcessAtualiza nFSeProcess = new NFSeProcessAtualiza(serviceDependencies.sConfig, serviceDependencies.DbWrapper);
                NFSeFetchAtualiza nFSeFetch = new NFSeFetchAtualiza(serviceDependencies.DbWrapper);
                nFSeProcess.IntegrateNFSe(nFSeFetch.GetListNFSe(), new Consulta(serviceDependencies.sConfig, Defaults.GetCommunicationProvider()));
            }
            catch
            {

            }
        }
        private void ExecuteCancelaNFSe(ServiceDependencies serviceDependencies)
        {
            try
            {
                OutboundNFSeDocumentCancelUseCase useCase = new OutboundNFSeDocumentCancelUseCase(new DBDocumentsRepository(serviceDependencies.DbWrapper), serviceDependencies.sConfig, serviceDependencies.communicationProvider);
                useCase.Execute();
            }
            catch
            {

            }

        }
        private void ExecuteInutilizaNFSe(ServiceDependencies serviceDependencies)
        {
            try
            {
                OutboundNFSeDocumentInutilUseCase useCase = new OutboundNFSeDocumentInutilUseCase(new DBDocumentsRepository(serviceDependencies.DbWrapper), serviceDependencies.sConfig, serviceDependencies.communicationProvider);
                useCase.Execute();
            }
            catch
            {
            }

        }
    }
}

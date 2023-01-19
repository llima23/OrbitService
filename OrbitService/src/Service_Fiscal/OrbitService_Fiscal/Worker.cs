using AccountService_CentroDeCusto.CentroDeCusto.infrastructure.repositories;
using AccountService_CentroDeCusto.CentroDeCusto.usecase;
using AccountService_ContasContabeis.ContasContabeis.Infrastructure.repositories;
using AccountService_ContasContabeis.ContasContabeis.usecase;
using AccountService_LancamentoContabil.LancamentoContabil.infrastructure.repositories;
using AccountService_LancamentoContabil.LancamentoContabil.usecase;
using AccountService_PlanoDeContas.PlanoDeContas.Infrastructure.repositories;
using AccountService_PlanoDeContas.PlanoDeContas.usecase;
using B1Library.Implementations.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrbitLibrary.Utils;
using OrbitService.InboundCce.usecases;
using OrbitService.InboundNFe.usecases;
using OrbitService.InboundNFSe.usecases;
using OrbitService.InboundOtherDocuments.usecases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrbitService_Fiscal
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
                        if (serviceDependencies.sConfig.Ativo && serviceDependencies.sConfig.IntegraDocFiscal)
                        {
                            //ExecuteEnviaNFe(serviceDependencies);
                            //ExecuteAtualizaNFe(serviceDependencies);
                            //ExecuteCancelaNFe(serviceDependencies);
                            //ExecuteInutilizaNFe(serviceDependencies);
                            Thread t = new Thread(() => ExecuteCentroDeCusto(serviceDependencies));
                            t.Start();
                            Thread t2 = new Thread(() => ExecuteContasContabeis(serviceDependencies));
                            t2.Start();
                            Thread t3 = new Thread(() => ExecuteInboundCce(serviceDependencies));
                            t3.Start();
                            Thread t4 = new Thread(() => ExecuteInboundNFe(serviceDependencies));
                            t4.Start();
                            Thread t5 = new Thread(() => ExecuteInboundNFSe(serviceDependencies));
                            t5.Start();
                            Thread t6 = new Thread(() => ExecuteInboundOtherDocuments(serviceDependencies));
                            t6.Start();
                            Thread t7 = new Thread(() => ExecuteLCM(serviceDependencies));
                            t7.Start();
                            Thread t8 = new Thread(() => ExecutePlanoDeContas(serviceDependencies));
                            t8.Start();
                            t.Join();
                            t2.Join();
                            t3.Join();
                            t4.Join();
                            t5.Join();
                            t6.Join();
                            t7.Join();
                            t8.Join();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void ExecutePlanoDeContas(ServiceDependencies serviceDependencies)
        {
            try
            {
                UseCasePlanoDeContas useCase = new UseCasePlanoDeContas(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBPlanAccountRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {
            }

        }

        private void ExecuteLCM(ServiceDependencies serviceDependencies)
        {
            try
            {
                UseCaseLCM useCase = new UseCaseLCM(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBLancamentoContabilRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {

            }
        
        }

        private void ExecuteInboundOtherDocuments(ServiceDependencies serviceDependencies)
        {
            try
            {
                OtherDocumentsRegisterUseCase useCase = new OtherDocumentsRegisterUseCase(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBDocumentsRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {

            }
    
        }

        private void ExecuteInboundNFSe(ServiceDependencies serviceDependencies)
        {
            try
            {
                NFSeDocumentsRegisterUseCase useCase = new NFSeDocumentsRegisterUseCase(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBDocumentsRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {

            }
         
        }

        private void ExecuteInboundNFe(ServiceDependencies serviceDependencies)
        {
            try
            {
                InboundNFeRegisterUseCase useCase = new InboundNFeRegisterUseCase(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBDocumentsRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {

            }
          
        }

        private void ExecuteInboundCce(ServiceDependencies serviceDependencies)
        {
            try
            {
                UseCaseInboundCce useCase = new UseCaseInboundCce(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBDocumentsRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {

            }
       
        }

        private void ExecuteContasContabeis(ServiceDependencies serviceDependencies)
        {
            try
            {
                UseCaseContasContabeis useCase = new UseCaseContasContabeis(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBAccountRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {

            }
        
        }

        private void ExecuteCentroDeCusto(ServiceDependencies serviceDependencies)
        {
            try
            {
                UseCaseCentroDeCusto useCase = new UseCaseCentroDeCusto(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBCentroDeCustoRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {

            }
        }
    }
}

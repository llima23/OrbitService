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
using OrbitService_Fiscal.Pagamentos.Pagamentos.mapper.repositories;
using OrbitService_Fiscal.Pagamentos.Pagamentos.usecase;
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
        private ServiceDependencies service;
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
                    List<Thread> threads = new List<Thread>();
                    await Task.Delay(1000, stoppingToken);
                    List<ServiceDependencies> ListserviceDependencies = Defaults.GetListServiceDependencies();
                    foreach (ServiceDependencies serviceDependencies in ListserviceDependencies.Where(x => x.DbWrapper.DataBaseName == "SBO_ORBITDEV"))
                    {
                       
                        this.service = serviceDependencies;
                        if (serviceDependencies.sConfig.Ativo && serviceDependencies.sConfig.IntegraDocFiscal)
                        {

                            Thread t = new Thread(() => ExecuteAll());
                            threads.Add(t);
                        }
                    }
                    foreach (Thread item in threads)
                    {
                        item.Start();
                    }
                    foreach (Thread item in threads)
                    {
                        item.Join();
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }
        private void ExecuteAll()
        {
            Thread t = new Thread(() => ExecuteCentroDeCusto(this.service));
            t.Start();
            Thread t2 = new Thread(() => ExecuteContasContabeis(this.service));
            t2.Start();
            Thread t3 = new Thread(() => ExecuteInboundCce(this.service));
            t3.Start();
            Thread t4 = new Thread(() => ExecuteInboundNFe(this.service));
            t4.Start();
            Thread t5 = new Thread(() => ExecuteInboundNFSe(this.service));
            t5.Start();
            Thread t6 = new Thread(() => ExecuteInboundOtherDocuments(this.service));
            t6.Start();
            //Thread t7 = new Thread(() => ExecuteLCM(this.service));
            //t7.Start();
            Thread t8 = new Thread(() => ExecutePlanoDeContas(this.service));
            t8.Start();
            Thread t9 = new Thread(() => ExecutePayments(this.service));
            t9.Start();

            t.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();
            t6.Join();
            //t7.Join();
            t8.Join();
            t9.Join();
        }
        private void ExecutePayments(ServiceDependencies serviceDependencies)
        {
            try
            {
                UseCasePayments useCase = new UseCasePayments(serviceDependencies.sConfig, serviceDependencies.communicationProvider, new DBPaymentsRepository(serviceDependencies.DbWrapper));
                useCase.Execute();
            }
            catch
            {
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

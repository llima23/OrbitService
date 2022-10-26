using B1Library.Documents;
using Moq;
using OrbitLibrary.Common;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.OutboundDFe.services;
using OrbitService.OutboundDFe.usecases;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace OrbitService_Test.OutboundDFe.usecases
{
    public class OutboundNFeDocumentInutilUseCaseTest
    {
        private Mock<IDocumentsRepository> mockDocumentsRepo;
        private OutboundNFeDocumentInutilUseCase cut;
        private CommonServiceTestData<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe> t;
        private DocumentStatus documentStatus;
        private OperationResponse<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe> response;
        public OutboundNFeDocumentInutilUseCaseTest()
        {
            t = new CommonServiceTestData<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe>();
            mockDocumentsRepo = new Mock<IDocumentsRepository>();
            cut = new OutboundNFeDocumentInutilUseCase(mockDocumentsRepo.Object, t.sConfig, t.mockClient.Object);
        }
        [Fact]
        public void ShouldExecuteUseCaseOutboundNFSeDocumentInutilSucess()
        {
            response = TestsBuilder.CreateOperationResponse<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe>(TestsBuilder.CreateOperationRequest(), "{\"branchId\":\"d1540357-5a30-41dd-bb6a-be3f7a363660\",\"versao\":\"4.00\",\"serie\":\"2\",\"nNfIni\":\"337\",\"nNfFin\":\"337\",\"xJust\":\"Necessariocancelamentodanota.\",\"ano\":\"2022\"}", HttpStatusCode.OK);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = new Invoice();
            invoice.Identificacao.BranchId = "21345";
            invoice.Identificacao.Versao = "4.00";
            invoice.Identificacao.SerieDocumento = "1";
            invoice.Identificacao.NumeroDocumento = "100";
            invoice.Identificacao.Justificativa = "Inutilizacao do Documento";
            invoice.Identificacao.DataEmissao = DateTime.Now;
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetInutilOutboundNFe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>(), invoice.ObjetoB1))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetInutilOutboundNFe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1), Times.Once());

        }
    }
}

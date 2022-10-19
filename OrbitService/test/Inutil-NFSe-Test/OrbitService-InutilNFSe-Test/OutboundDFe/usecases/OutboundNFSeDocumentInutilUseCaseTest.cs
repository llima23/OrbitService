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

namespace OrbitService_InutilNFSe_Test.OutboundDFe.usecases
{
    public class OutboundNFSeDocumentInutilUseCaseTest
    {
        private Mock<IDocumentsRepository> mockDocumentsRepo;
        private OutboundNFSeDocumentInutilUseCase cut;
        private CommonServiceTestData<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe> t;
        private DocumentStatus documentStatus;
        private OperationResponse<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe> response;
        public OutboundNFSeDocumentInutilUseCaseTest()
        {
            t = new CommonServiceTestData<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe>();
            mockDocumentsRepo = new Mock<IDocumentsRepository>();
            cut = new OutboundNFSeDocumentInutilUseCase(mockDocumentsRepo.Object, t.sConfig, t.mockClient.Object);
        }

        [Fact]
        public void ShouldExecuteUseCaseOutboundNFSeDocumentInutilSucess()
        {
            response = TestsBuilder.CreateOperationResponse<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe>(TestsBuilder.CreateOperationRequest(), "{\"sucess\":true,\"message\":\"INUTILIZACAO\"}", HttpStatusCode.OK);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = new Invoice();
            invoice.Identificacao.IdRetornoOrbit = "21345";
            invoice.Identificacao.BranchId = "12345";
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetInutilOutboundNFSe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>(), invoice.ObjetoB1))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetInutilOutboundNFSe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1), Times.Once());

        }

        [Fact]
        public void ShouldExecuteUseCaseOutboundNFSeDocumentInutilError()
        {
            response = TestsBuilder.CreateOperationResponse<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe>(TestsBuilder.CreateOperationRequest(), "{\"message\":\"Userisnotauthorizedtoaccessthisresourcewithanexplicitdeny\",\"customErrorMessage\":\"Requestedpath'POST-/documentservice/nfse/inutilizacao'notfound\",\"path\":{\"resource_name\":\"\",\"entity_name\":\"\",\"action_name\":\"\"}}", HttpStatusCode.Forbidden);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = new Invoice();
            invoice.Identificacao.IdRetornoOrbit = "21345";
            invoice.Identificacao.BranchId = "12345";
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetInutilOutboundNFSe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>(), invoice.ObjetoB1))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetInutilOutboundNFSe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1), Times.Once());

        }

    }
}

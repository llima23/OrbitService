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

namespace OrbitService_Tst.OutboundDFe.usecases
{
    public class OutboundNFeDocumentCancelUseCaseTest
    {
        private Mock<IDocumentsRepository> mockDocumentsRepo;
        private OutboundNFeDocumentCancelUseCase cut;
        private CommonServiceTestData<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe> t;
        private DocumentStatus documentStatus;
        private OperationResponse<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe> response;

        public OutboundNFeDocumentCancelUseCaseTest()
        {
            t = new CommonServiceTestData<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe>();
            mockDocumentsRepo = new Mock<IDocumentsRepository>();
            cut = new OutboundNFeDocumentCancelUseCase(mockDocumentsRepo.Object, t.sConfig, t.mockClient.Object);
        }

        [Fact]
        public void ShouldExecuteUseCaseOutboundNFSeDocumentCancelSucess()
        {
            response = TestsBuilder.CreateOperationResponse<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe>(TestsBuilder.CreateOperationRequest(), "{\"nfeId\":\"1234\",\"xJust\":\"Cancelamento\"}", HttpStatusCode.OK);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = new Invoice();
            invoice.Identificacao.IdRetornoOrbit = "1234";
            invoice.Identificacao.Justificativa = "Cancelamento";
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetCancelOutboundNFe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>(), invoice.ObjetoB1))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetCancelOutboundNFe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1), Times.Once());

        }

        [Fact]
        public void ShouldExecuteUseCaseOutboundNFSeDocumentCancelError()
        {
            response = TestsBuilder.CreateOperationResponse<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe>(TestsBuilder.CreateOperationRequest(), "{\"nfeId\":\"1234\",\"xJust\":\"Cancelamento\"}", HttpStatusCode.BadRequest);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = new Invoice();
            invoice.Identificacao.IdRetornoOrbit = "1234";
            invoice.Identificacao.Justificativa = "Cancelamento";
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetCancelOutboundNFe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>(), invoice.ObjetoB1))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetCancelOutboundNFe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1), Times.Once());

        }
    }
}

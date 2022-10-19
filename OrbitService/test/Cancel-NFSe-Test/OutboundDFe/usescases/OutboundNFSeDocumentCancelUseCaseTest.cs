using B1Library.Documents;
using Moq;
using OrbitLibrary.Common;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService_Cancel_NFSe.OutboundDFe.services;
using OrbitService_Cancel_NFSe.OutboundDFe.usecases;
using OrbitService_Test.TestUtils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace OrbitService_Test_Cancel_NFSe.OutboundDFe.usescases
{
    public class OutboundNFSeDocumentCancelUseCaseTest
    {
        private Mock<IDocumentsRepository> mockDocumentsRepo;
        private OutboundNFSeDocumentCancelUseCase cut;
        private CommonServiceTestData<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe> t;
        private DocumentStatus documentStatus;
        private OperationResponse<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe> response;

        public OutboundNFSeDocumentCancelUseCaseTest()
        {
            t = new CommonServiceTestData<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe>();
            mockDocumentsRepo = new Mock<IDocumentsRepository>();
            cut = new OutboundNFSeDocumentCancelUseCase(mockDocumentsRepo.Object, t.sConfig, t.mockClient.Object);
        }
        [Fact]
        public void ShouldExecuteUseCaseOutboundNFSeDocumentCancelSucess()
        {
            response = TestsBuilder.CreateOperationResponse<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe>(TestsBuilder.CreateOperationRequest(), "{\"branchId\":\"21345\",\"nfseId\":\"12345\",\"motivo\":\"NECESSARIOCANCELAMENTODESSANOTA\",\"soft_cancel\":true}", HttpStatusCode.OK);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = new Invoice();
            invoice.Identificacao.IdRetornoOrbit = "21345";
            invoice.Identificacao.BranchId = "12345";
            invoice.Identificacao.Justificativa = "NECESSARIO CANCELAMENTO DESSA NOTA";
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetCancelOutboundNFSe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>(), invoice.ObjetoB1))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetCancelOutboundNFSe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1), Times.Once());

        }

        [Fact]
        public void ShouldExecuteUseCaseOutboundNFSeDocumentCancelError()
        {
            response = TestsBuilder.CreateOperationResponse<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe>(TestsBuilder.CreateOperationRequest(), "{\"success\":false,\"async\":true,\"version\":\"string\",\"softEvent\":true,\"message\":\"string\",\"processDate\":\"string\",\"alerts\":[{\"code\":\"string\",\"description\":\"string\"}],\"errors\":[{\"code\":\"string\",\"description\":\"string\"}],\"communicationIds\":[\"string\"]}", HttpStatusCode.BadRequest);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = new Invoice();
            invoice.Identificacao.IdRetornoOrbit = "21345";
            invoice.Identificacao.BranchId = "12345";
            invoice.Identificacao.Justificativa = "NECESSARIO CANCELAMENTO DESSA NOTA";
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetCancelOutboundNFSe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>(), invoice.ObjetoB1))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetCancelOutboundNFSe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1), Times.Once());

        }
    }
}

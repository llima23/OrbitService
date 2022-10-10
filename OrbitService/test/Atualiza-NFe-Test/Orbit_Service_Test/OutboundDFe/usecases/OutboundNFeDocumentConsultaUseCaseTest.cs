using B1Library.Documents;
using Moq;
using OrbitLibrary.Common;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.OutboundNFe.services;
using OrbitService.OutboundNFe.usecases;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Orbit_Service_Test.OutboundDFe.usecases
{
    public class OutboundNFeDocumentConsultaUseCaseTest
    {
        private Mock<IDocumentsRepository> mockDocumentsRepo;
        private OutboundNFeDocumentConsultaUseCase cut;
        private CommonServiceTestData<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe> t;
        private DocumentStatus documentStatus;
        private OperationResponse<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe> response;

        public OutboundNFeDocumentConsultaUseCaseTest()
        {
            t = new CommonServiceTestData<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe>();
            mockDocumentsRepo = new Mock<IDocumentsRepository>();
            cut = new OutboundNFeDocumentConsultaUseCase(mockDocumentsRepo.Object, t.sConfig, t.mockClient.Object);
        }
        [Fact]
        public void ShouldExecuteUseCaseOutboundNFeDocumentConsultaSucess()
        {
            response = TestsBuilder.CreateOperationResponse<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe>(TestsBuilder.CreateOperationRequest(), "{\"status\":{\"cStat\":\"100\",\"mStat\":\"AUTORIAZADA\"}}", HttpStatusCode.OK);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = new Invoice();
            invoice.Identificacao.IdRetornoOrbit = "21345";
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetCancelOutboundNFSe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>()))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetCancelOutboundNFSe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus), Times.Once());
        }
    }
}

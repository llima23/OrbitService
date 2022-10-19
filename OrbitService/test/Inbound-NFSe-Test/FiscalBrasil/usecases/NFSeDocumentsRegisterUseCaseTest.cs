using B1Library.Documents;
using Moq;
using OrbitLibrary.Common;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.FiscalBrazil.services.NFSeDocumentRegister;
using OrbitService.FiscalBrazil.usecases;
using OrbitService_Test.TestUtils;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace OrbitService_Test.FiscalBrasil.usecases
{
    public class NFSeDocumentsRegisterUseCaseTest
    {
        private Mock<IDocumentsRepository> mockDocumentsRepo;
        private NFSeDocumentsRegisterUseCase cut;
        private CommonServiceTestData<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError> t;
        private DocumentStatus documentStatus;
        private OperationResponse<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError> response;

        public NFSeDocumentsRegisterUseCaseTest()
        {
            t = new CommonServiceTestData<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError>();
            mockDocumentsRepo = new Mock<IDocumentsRepository>();
            cut = new NFSeDocumentsRegisterUseCase(t.sConfig, t.mockClient.Object, mockDocumentsRepo.Object);
        }

        [Fact]
        public void ShouldRegisterAOtherDocumentWithSuccess()
        {
            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = InvoiceB1FakeGenerator.GetFakeB1NFSeDocuments();
            listInvoiceB1.Add(invoice);

            response = TestsBuilder.CreateOperationResponse<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError>(TestsBuilder.CreateOperationRequest(), "{\"data\":{\"_id\":\"633b282d67fb3c0013232450\",\"period\":\"2022/08/02\",\"branchId\":\"d1540357-5a30-41dd-bb6a-be3f7a363660\",\"tipoOperacao\":\"input\",\"dfe\":\"nfe\",\"status\":\"Sucesso\",\"description\":\"\",\"created_by_name\":\"RobsonCunha\",\"created_by_email\":\"robson.cunha@seidor.com.br\",\"updated_by_name\":\"RobsonCunha\",\"updated_by_email\":\"robson.cunha@seidor.com.br\",\"tenantid\":\"9665176c-7ab0-4439-b2ab-25e28cb34689\"}}", HttpStatusCode.OK);

            mockDocumentsRepo
                .Setup(m => m.GetInboundNFSe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>(), invoice.ObjetoB1))
                .Callback<DocumentStatus>(ds => documentStatus = ds)
                .Returns(1);
            t.mockClient
                .Setup(c => c.Send<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetInboundNFSe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1), Times.Once());
        }
    }
}

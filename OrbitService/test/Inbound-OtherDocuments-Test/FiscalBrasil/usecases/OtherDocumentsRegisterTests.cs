using B1Library.Documents;
using Moq;
using OrbitLibrary.Common;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.InboundOtherDocuments.services.Error;
using OrbitService.InboundOtherDocuments.services.Output;
using OrbitService.InboundOtherDocuments.usecases;
using OrbitService_Test.TestUtils;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace OrbitService_Test.FiscalBrazil.usecases
{
    public class OtherDocumentsRegisterTests
    {
        private Mock<IDocumentsRepository> mockDocumentsRepo;
        private OtherDocumentsRegisterUseCase cut;
        private CommonServiceTestData<OtherDocumentRegisterOutput, OtherDocumentRegisterError> t;
        private DocumentStatus documentStatus;

        public OtherDocumentsRegisterTests()
        {
            t = new CommonServiceTestData<OtherDocumentRegisterOutput, OtherDocumentRegisterError>();
            mockDocumentsRepo = new Mock<IDocumentsRepository>();
            cut = new OtherDocumentsRegisterUseCase(t.sConfig, t.mockClient.Object,mockDocumentsRepo.Object);
        }

        [Fact]
        public void ShouldRegisterAOtherDocumentWithSuccess()
        {
            OperationResponse<OtherDocumentRegisterOutput, OtherDocumentRegisterError> response = TestsBuilder.CreateOperationResponse<OtherDocumentRegisterOutput, OtherDocumentRegisterError>(TestsBuilder.CreateOperationRequest(), "{\"data\":{\"_id\":\"633b282d67fb3c0013232450\",\"period\":\"2022/08/02\",\"branchId\":\"d1540357-5a30-41dd-bb6a-be3f7a363660\",\"tipoOperacao\":\"input\",\"dfe\":\"nfe\",\"status\":\"Sucesso\",\"description\":\"\",\"created_by_name\":\"RobsonCunha\",\"created_by_email\":\"robson.cunha@seidor.com.br\",\"updated_by_name\":\"RobsonCunha\",\"updated_by_email\":\"robson.cunha@seidor.com.br\",\"tenantid\":\"9665176c-7ab0-4439-b2ab-25e28cb34689\"}}", HttpStatusCode.OK);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = InvoiceB1FakeGenerator.GetFakeInvoiceB1();
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetInboundOtherDocuments())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>(), invoice.ObjetoB1))
                .Callback<DocumentStatus>(ds => documentStatus = ds)
                .Returns(1);
            t.mockClient
                .Setup(c => c.Send<OtherDocumentRegisterOutput, OtherDocumentRegisterError>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetInboundOtherDocuments(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus, invoice.ObjetoB1), Times.Once());
        }
    }
}

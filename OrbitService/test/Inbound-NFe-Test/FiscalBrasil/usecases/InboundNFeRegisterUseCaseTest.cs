using B1Library.Documents;
using Moq;
using OrbitLibrary.Common;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.FiscalBrazil.services.InboundNFeRegister;
using OrbitService.FiscalBrazil.usecases;
using OrbitService_Test.TestUtils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace OrbitService_Test.FiscalBrasil.usecases
{
    public class InboundNFeRegisterUseCaseTest
    {
        private Mock<IDocumentsRepository> mockDocumentsRepo;
        private InboundNFeRegisterUseCase cut;
        private CommonServiceTestData<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError> t;
        private DocumentStatus documentStatus;
        private OperationResponse<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError> response;

        public InboundNFeRegisterUseCaseTest()
        {
            t = new CommonServiceTestData<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>();
            mockDocumentsRepo = new Mock<IDocumentsRepository>();
            cut = new InboundNFeRegisterUseCase(t.sConfig, t.mockClient.Object, mockDocumentsRepo.Object);
        }

        [Fact]
        public void ShouldRegisterInboundNFeRegisterWithSuccessAndStatusOrbitSucesso()
        {
            response = TestsBuilder.CreateOperationResponse<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>(TestsBuilder.CreateOperationRequest(), "{\"data\":{\"_id\":\"633b282d67fb3c0013232450\",\"period\":\"2022/08/02\",\"branchId\":\"d1540357-5a30-41dd-bb6a-be3f7a363660\",\"tipoOperacao\":\"input\",\"dfe\":\"nfe\",\"status\":\"Sucesso\",\"description\":\"\",\"created_by_name\":\"RobsonCunha\",\"created_by_email\":\"robson.cunha@seidor.com.br\",\"updated_by_name\":\"RobsonCunha\",\"updated_by_email\":\"robson.cunha@seidor.com.br\",\"tenantid\":\"9665176c-7ab0-4439-b2ab-25e28cb34689\"}}", HttpStatusCode.OK);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = InvoiceB1FakeGenerator.GetFakeInvoiceB1();
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetInboundNFe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>()))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetInboundNFe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus), Times.Once());
        }

        [Fact]
        public void ShouldRegisterInboundNFeRegisterWithSuccessAndStatusOrbitErro()
        {
            response = TestsBuilder.CreateOperationResponse<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>(TestsBuilder.CreateOperationRequest(), "{\"data\":{\"_id\":\"62e93df6e7685efe4f787aa8\",\"tenantid\":\"9665176c-7ab0-4439-b2ab-25e28cb34689\",\"period\":\"2022/08/02\",\"branchId\":\"d1540357-5a30-41dd-bb6a-be3f7a363660\",\"dfe\":\"nfe\",\"event\":\"emit\",\"tipoOperacao\":\"input\",\"status\":\"Erro\",\"description\":\"emitente.cnpj\",\"updated_by_name\":\"RobsonCunha\",\"updated_by_email\":\"robson.cunha@seidor.com.br\"}}", HttpStatusCode.OK);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = InvoiceB1FakeGenerator.GetFakeInvoiceB1();
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetInboundNFe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>()))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetInboundNFe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus), Times.Once());
        }

        [Fact]
        public void ShouldRegisterInboundNFeRegisterWithErro()
        {
            response = TestsBuilder.CreateOperationResponse<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>(TestsBuilder.CreateOperationRequest(), "{\"code\":0,\"message\":\"BadRequest\",\"errors\":[{\"msg\":\"BadRequest\",\"param\":\"Header\"}]}", HttpStatusCode.BadRequest);

            List<Invoice> listInvoiceB1 = new List<Invoice>();
            Invoice invoice = InvoiceB1FakeGenerator.GetFakeInvoiceB1();
            listInvoiceB1.Add(invoice);

            mockDocumentsRepo
                .Setup(m => m.GetInboundNFe())
                .Returns(listInvoiceB1);
            mockDocumentsRepo
                 .Setup(m => m.UpdateDocumentStatus(It.IsAny<DocumentStatus>()))
                 .Callback<DocumentStatus>(ds => documentStatus = ds)
                 .Returns(1);
            t.mockClient
               .Setup(c => c.Send<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>(It.IsAny<OperationRequest>()))
               .Callback<OperationRequest>(r => t.request = r)
               .Returns(response);

            cut.Execute();

            mockDocumentsRepo.Verify(m => m.GetInboundNFe(), Times.Once());
            mockDocumentsRepo.Verify(m => m.UpdateDocumentStatus(documentStatus), Times.Once());
        }

    }
}

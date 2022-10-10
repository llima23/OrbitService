using Moq;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services.OutboundDFeRegister;
using OrbitService_Test.TestUtils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace OrbitService_Test.OutboundDFe.services.OutboundNFeRegister
{
    public class OutboundNFeDocumentRegisterServiceTest
    {
        private CommonServiceTestData<OutboundNFeDocumentRegisterOutput, OutboundNFeDocumentRegisterError> t;
        private OutboundNFeDocumentRegisterService cut;
        private MapperOutboundNFe mapper;
        public OutboundNFeDocumentRegisterServiceTest()
        {
            t = new CommonServiceTestData<OutboundNFeDocumentRegisterOutput, OutboundNFeDocumentRegisterError>();
            t.mountCredentialsProviderMock();
            cut = new OutboundNFeDocumentRegisterService(t.sConfig, t.mockClient.Object);
            mapper = new MapperOutboundNFe();
        }

        [Fact]
        public void ShouldRegisterAValidNFeDocumentRequest()
        {
            t.mockClient
                .Setup(c => c.Send<OutboundNFeDocumentRegisterOutput, OutboundNFeDocumentRegisterError>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(TestsBuilder.CreateOperationResponse<OutboundNFeDocumentRegisterOutput, OutboundNFeDocumentRegisterError>(t.request));

            OutboundNFeDocumentRegisterInput input = new OutboundNFeDocumentRegisterInput();
            input = mapper.ToinboundNFeDocumentRegisterInput(InvoiceB1FakeGenerator.GetFakeInvoiceB1());
            OperationResponse<OutboundNFeDocumentRegisterOutput, OutboundNFeDocumentRegisterError> response = cut.Execute(input);

            Assert.NotNull(response);
            Assert.Equal(Method.POST, t.request.Method);
            Assert.EndsWith(OutboundNFeDocumentRegisterService.ENDPOINT, t.request.Uri.AbsoluteUri);
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.Token));
            Assert.Contains(new KeyValuePair<string, string>(HTTPHeaders.ContentType, HTTPContentTypes.ApplicationJson), t.request.Headers);
        }
    }
}


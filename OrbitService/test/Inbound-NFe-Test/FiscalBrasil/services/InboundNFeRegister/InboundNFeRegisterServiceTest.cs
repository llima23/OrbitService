using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.FiscalBrazil.mappers;
using OrbitService.FiscalBrazil.services.InboundNFeRegister;
using OrbitService_Test.FiscalBrasil.mappers;
using OrbitService_Test.TestUtils;
using Xunit;

namespace OrbitService_Test.FiscalBrasil.services.InboundNFeRegister
{
    public class InboundNFeRegisterServiceTest
    {
        private CommonServiceTestData<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError> t;
        private InboundNFeRegisterService cut;
        private MapperInboundNFe mapper;        
        public InboundNFeRegisterServiceTest()
        {
            t = new CommonServiceTestData<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>();
            t.mountCredentialsProviderMock();
            cut = new InboundNFeRegisterService(t.sConfig, t.mockClient.Object);
            mapper = new MapperInboundNFe();            
        }

        [Fact]
        public void ShouldRegisterAValidOtherDocumentRequest()
        {
            t.mockClient
                .Setup(c => c.Send<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(TestsBuilder.CreateOperationResponse<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError>(t.request));

            InboundNFeDocumentRegisterInput input = new InboundNFeDocumentRegisterInput();
            Root root = new Root();
            root.inboundNFeDocumentRegisterInput = mapper.ToinboundNFeDocumentRegisterInput(InvoiceB1FakeGenerator.GetFakeInvoiceB1());
            OperationResponse<InboundNFeDocumentRegisterOutput, InboundNFeDocumentRegisterError> response = cut.Execute(root);

            Assert.NotNull(response);
            Assert.Equal(Method.POST, t.request.Method);
            Assert.EndsWith(InboundNFeRegisterService.ENDPOINT, t.request.Uri.AbsoluteUri);
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.Token));
            Assert.Contains(new KeyValuePair<string, string>(HTTPHeaders.ContentType, HTTPContentTypes.ApplicationJson), t.request.Headers);
        }    

    }
}

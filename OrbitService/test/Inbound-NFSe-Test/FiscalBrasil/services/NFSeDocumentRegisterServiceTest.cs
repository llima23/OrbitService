using Moq;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.FiscalBrazil.services;
using OrbitService.FiscalBrazil.services.NFSeDocumentRegister;
using System.Collections.Generic;
using Xunit;

namespace OrbitService_Test.FiscalBrasil.services
{
    public class NFSeDocumentRegisterServiceTest
    {
        private CommonServiceTestData<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError> t;
        private NFSeDocumentRegisterService cut;
        public NFSeDocumentRegisterServiceTest()
        {
            t = new CommonServiceTestData<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError>();
            t.mountCredentialsProviderMock();
            cut = new NFSeDocumentRegisterService(t.sConfig, t.mockClient.Object);
        }

        [Fact]
        public void ShouldRegisterAValidOtherDocumentRequest()
        {
            t.mockClient
                .Setup(c => c.Send<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(TestsBuilder.CreateOperationResponse<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError>(t.request));

            NFSeDocumentRegisterInput input = new NFSeDocumentRegisterInput();
            OperationResponse<NFSeDocumentRegisterOutput, NFSeDocumentRegisterError> response = cut.Execute(input);

            Assert.NotNull(response);
            Assert.Equal(Method.POST, t.request.Method);
            Assert.EndsWith(NFSeDocumentRegisterService.ENDPOINT, t.request.Uri.AbsoluteUri);
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.Token));
            Assert.Contains(new KeyValuePair<string, string>(HTTPHeaders.ContentType, HTTPContentTypes.ApplicationJson), t.request.Headers);
        }
    }
}

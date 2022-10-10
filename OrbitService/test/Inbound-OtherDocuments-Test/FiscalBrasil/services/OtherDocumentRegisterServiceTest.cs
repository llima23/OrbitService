using Moq;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.FiscalBrazil.services;
using OrbitService.FiscalBrazil.services.Error;
using OrbitService.FiscalBrazil.services.Input;
using OrbitService.FiscalBrazil.services.Output;
using System.Collections.Generic;
using Xunit;

namespace OrbitService_Test.FiscalBrazil.services
{
    public class OtherDocumentRegisterServiceTest
    {
        private CommonServiceTestData<OtherDocumentRegisterOutput, OtherDocumentRegisterError> t;
        private OtherDocumentRegister cut;
        public OtherDocumentRegisterServiceTest()
        {
            t = new CommonServiceTestData<OtherDocumentRegisterOutput, OtherDocumentRegisterError>();
            t.mountCredentialsProviderMock();
            cut = new OtherDocumentRegister(t.sConfig, t.mockClient.Object);
        }

        [Fact]
        public void ShouldRegisterAValidOtherDocumentRequest()
        {
            t.mockClient
                .Setup(c => c.Send<OtherDocumentRegisterOutput, OtherDocumentRegisterError>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(TestsBuilder.CreateOperationResponse<OtherDocumentRegisterOutput, OtherDocumentRegisterError>(t.request));

            OtherDocumentRegisterInput input = new OtherDocumentRegisterInput();
            OperationResponse<OtherDocumentRegisterOutput, OtherDocumentRegisterError> response = cut.Execute(input);

            Assert.NotNull(response);
            Assert.Equal(Method.POST, t.request.Method);
            Assert.EndsWith(OtherDocumentRegister.ENDPOINT, t.request.Uri.AbsoluteUri);
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.Token));
            Assert.Contains(new KeyValuePair<string, string>(HTTPHeaders.ContentType, HTTPContentTypes.ApplicationJson), t.request.Headers);
        }
    }
}

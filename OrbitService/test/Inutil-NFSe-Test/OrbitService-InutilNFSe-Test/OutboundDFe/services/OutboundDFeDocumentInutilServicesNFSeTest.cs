using Moq;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrbitService_InutilNFSe_Test.OutboundDFe.services
{
    public class OutboundDFeDocumentInutilServicesNFSeTest
    {
        private CommonServiceTestData<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe> t;
        private OutboundDFeDocumentInutilServicesNFSe cut;
        public OutboundDFeDocumentInutilServicesNFSeTest()
        {
            t = new CommonServiceTestData<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe>();
            t.mountCredentialsProviderMock();
            cut = new OutboundDFeDocumentInutilServicesNFSe(t.sConfig, t.mockClient.Object);
        }

        [Fact]
        public void ShouldRegisterAValidNFeDocumentRequest()
        {
            t.mockClient
          .Setup(c => c.Send<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe>(It.IsAny<OperationRequest>()))
          .Callback<OperationRequest>(r => t.request = r)
          .Returns(TestsBuilder.CreateOperationResponse<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe>(t.request));

            OutboundDFeDocumentInutilInputNFSe input = new OutboundDFeDocumentInutilInputNFSe
            {
                branchId = "2145",
                nfseId = "213455"
            };
            OperationResponse<OutboundDFeDocumentInutilOutputNFSe, OutboundDFeDocumentInutilOutputNFSe> response = cut.Execute(input);

            Assert.NotNull(response);
            Assert.Equal(Method.POST, t.request.Method);
            Assert.EndsWith(OutboundDFeDocumentInutilServicesNFSe.ENDPOINT, t.request.Uri.AbsoluteUri);
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.Token));
            Assert.Contains(new KeyValuePair<string, string>(HTTPHeaders.ContentType, HTTPContentTypes.ApplicationJson), t.request.Headers);
        }

    }
}

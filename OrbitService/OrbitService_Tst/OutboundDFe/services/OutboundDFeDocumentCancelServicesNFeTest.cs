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

namespace OrbitService_Tst.OutboundDFe.services
{
    public class OutboundDFeDocumentCancelServicesNFeTest
    {
        private OutboundDFeDocumentCancelServiceNFe cut;
        private CommonServiceTestData<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe> t;

        public OutboundDFeDocumentCancelServicesNFeTest()
        {

            t = new CommonServiceTestData<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe>();
            t.mountCredentialsProviderMock();
            cut = new OutboundDFeDocumentCancelServiceNFe(t.sConfig, t.mockClient.Object);
        }
        [Fact]
        public void ShouldExecutRequestCancelToOrbit()
        {
            t.mockClient
              .Setup(c => c.Send<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe>(It.IsAny<OperationRequest>()))
              .Callback<OperationRequest>(r => t.request = r)
              .Returns(TestsBuilder.CreateOperationResponse<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe>(t.request));

            OperationResponse<OutboundDFeDocumentCancelOutputNFe, OutboundDFeDocumentCancelOutputNFe> response = cut.Execute(It.IsAny<OutboundDFeDocumentCancelInputNFe>());

            Assert.NotNull(response);
            Assert.Equal(Method.POST, t.request.Method);
            Assert.EndsWith(OutboundDFeDocumentCancelServiceNFe.ENDPOINT, t.request.Uri.AbsoluteUri);
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.Token));
        }
    }
}

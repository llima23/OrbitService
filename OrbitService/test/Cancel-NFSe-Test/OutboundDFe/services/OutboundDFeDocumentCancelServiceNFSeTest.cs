using Moq;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService_Cancel_NFSe.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace OrbitService_Test_Cancel_NFSe.OutboundDFe.services
{
    public class OutboundDFeDocumentCancelServiceNFSeTest
    {
        private OutboundDFeDocumentCancelServicesNFSe cut;
        private CommonServiceTestData<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe> t;

        public OutboundDFeDocumentCancelServiceNFSeTest()
        {
           
            t = new CommonServiceTestData<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe>();
            t.mountCredentialsProviderMock();
            cut = new OutboundDFeDocumentCancelServicesNFSe(t.sConfig, t.mockClient.Object);

        }
        [Fact]
        public void ShouldExecutRequestCancelToOrbit()
        {
            t.mockClient
              .Setup(c => c.Send<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe>(It.IsAny<OperationRequest>()))
              .Callback<OperationRequest>(r => t.request = r)
              .Returns(TestsBuilder.CreateOperationResponse<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe>(t.request));

            OperationResponse<OutboundDFeDocumentCancelOutputNFSe, OutboundDFeDocumentCancelOutputNFSe> response = cut.Execute(It.IsAny<OutboundDFeDocumentCancelInputNFSe>());

            Assert.NotNull(response);
            Assert.Equal(Method.POST, t.request.Method);
            Assert.EndsWith(OutboundDFeDocumentCancelServicesNFSe.ENDPOINT, t.request.Uri.AbsoluteUri);
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.Token));
        }
    }
}

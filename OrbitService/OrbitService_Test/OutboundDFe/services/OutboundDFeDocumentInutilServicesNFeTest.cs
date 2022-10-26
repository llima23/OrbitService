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

namespace OrbitService_Test.OutboundDFe.services
{
    public class OutboundDFeDocumentInutilServicesNFeTest
    {
        private CommonServiceTestData<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe> t;
        private OutboundDFeDocumentInutilServicesNFe cut;
        public OutboundDFeDocumentInutilServicesNFeTest()
        {
            t = new CommonServiceTestData<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe>();
            t.mountCredentialsProviderMock();
            cut = new OutboundDFeDocumentInutilServicesNFe(t.sConfig, t.mockClient.Object);
        }


        [Fact]
        public void ShouldRegisterAValidNFeDocumentRequest()
        {
            t.mockClient
          .Setup(c => c.Send<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe>(It.IsAny<OperationRequest>()))
          .Callback<OperationRequest>(r => t.request = r)
          .Returns(TestsBuilder.CreateOperationResponse<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe>(t.request));

            OutboundDFeDocumentInutilInputNFe input = new OutboundDFeDocumentInutilInputNFe
            {
                branchId = "2145",
                versao = "4.00",
                serie = "1",
                nNfIni = "100",
                nNfFin = "100",
                xJust = "Inutilizacao do Documento",
                ano = DateTime.Now.ToString("yyyy")
            };
            OperationResponse<OutboundDFeDocumentInutilOutputNFe, OutboundDFeDocumentInutilOutputNFe> response = cut.Execute(input);

            Assert.NotNull(response);
            Assert.Equal(Method.PUT, t.request.Method);
            Assert.EndsWith(OutboundDFeDocumentInutilServicesNFe.ENDPOINT, t.request.Uri.AbsoluteUri);
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.Token));
            Assert.Contains(new KeyValuePair<string, string>(HTTPHeaders.ContentType, HTTPContentTypes.ApplicationJson), t.request.Headers);
        }

    }
}

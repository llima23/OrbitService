using B1Library.Documents;
using Moq;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using OrbitLibrary_Test.Builders;
using OrbitLibrary_Test.TestUtils;
using OrbitService.OutboundNFe.services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orbit_Service_Test.OutboundDFe.services
{
    public class OutboundDFeDocumentConsultaOutputNFeTest
    {
        private OutboundDFeDocumentConsulServicesNFe cut;
        private CommonServiceTestData<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe> t;
        public OutboundDFeDocumentConsultaOutputNFeTest()
        {
            t = new CommonServiceTestData<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe>();
            t.mountCredentialsProviderMock();
            cut = new OutboundDFeDocumentConsulServicesNFe(t.sConfig, t.mockClient.Object);
        }
        [Fact]
        public void ShouldExecutRequestCancelToOrbit()
        {
            t.mockClient
              .Setup(c => c.Send<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe>(It.IsAny<OperationRequest>()))
              .Callback<OperationRequest>(r => t.request = r)
              .Returns(TestsBuilder.CreateOperationResponse<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe>(t.request));
            Invoice invoice = new Invoice();
            invoice.IdRetornoOrbit = "2145";
            OperationResponse<OutboundDFeDocumentConsultaOutputNFe, OutboundDFeDocumentConsulErroNFe> response = cut.Execute(invoice);

            Assert.NotNull(response);
            Assert.Equal(Method.GET, t.request.Method);
            Assert.Contains("/documentservice/api/nfe/consulta/" + invoice.IdRetornoOrbit, t.request.Uri.AbsoluteUri);
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.True(t.request.Headers.ContainsKey(HTTPHeaders.Token));
        }
    }
}

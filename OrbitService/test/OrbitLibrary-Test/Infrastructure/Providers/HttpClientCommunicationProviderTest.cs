using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using OrbitLibrary.Common;
using RestSharp;

namespace OrbitLibrary_Test.Infrastructure.Providers
{

    public class HttpClientForTest : HttpClientCommunicationProvider
    {

        private IRestClient client;
        public HttpClientForTest(IRestClient client)
        {
            this.client = client;
        }

        protected override IRestClient GetRestClient(Uri url)
        {
            return client;
        }
    }

    public class HttpClientTestData : IDisposable
    {
        public HttpClientCommunicationProvider cut;
        public Mock<IRestClient> mock;

        public HttpClientTestData()
        {
            mock = new Mock<IRestClient>();
            cut = new HttpClientForTest(mock.Object);
        }

        public void Dispose()
        {
        }
    }

    public class HttpClientCommunicationProviderTest : IClassFixture<HttpClientTestData>
    {
        private HttpClientTestData t;
        private OperationRequest request;
        private IRestRequest restRequest;
        private OperationResponse<string,string> response;

        public HttpClientCommunicationProviderTest(HttpClientTestData testData) => t = testData;

        [Fact]
        public void ShouldCall()
        {
            var restResponse = new RestResponse();
            restResponse.Headers.Add(new Parameter("name", "value", ParameterType.HttpHeader));
            t.mock
                .Setup(c => c.Execute(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => restRequest = r) //Intercepta e pega a variável de referência
                .Returns(restResponse);

            request = new OperationRequest(new Uri("http://localhost/test"), OrbitLibrary.Common.Method.POST);
            request.Headers.Add("header", "example");
            request.Headers.Add("other", "value1");
            request.PutBody("Example");

            response = t.cut.Send<string, string>(request);
            t.mock.Verify(e => e.Execute(restRequest), Times.Once());
            Assert.Equal(RestSharp.Method.POST, restRequest.Method);
            Assert.True(restRequest.Parameters.Exists(match: p => p.Type == ParameterType.HttpHeader && p.Name == "header" && p.Value.ToString() == "example"));
            Assert.True(restRequest.Parameters.Exists(match: p => p.Type == ParameterType.HttpHeader && p.Name == "other" && p.Value.ToString() == "value1"));
            Assert.True(restRequest.Parameters.Exists(match: p => p.Type == ParameterType.RequestBody && p.Value.ToString() == "Example"));

            Assert.Contains(new KeyValuePair<string, string>("name", "value"), response.Headers);
        }
    }
}

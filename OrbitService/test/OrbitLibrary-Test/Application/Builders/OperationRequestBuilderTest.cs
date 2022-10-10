using OrbitLibrary.Common;
using Moq;
using OrbitLibrary_Test.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OrbitLibrary_Test.TestUtils;
using OrbitLibrary.Utils;

namespace OrbitLibrary_Test.Application.Builders
{
    public class OperationRequestBuilderTest
    {
        private CommonServiceTestData<string,string> testData;
        private OperationRequestBuilder cut;
        private Mock<RequestBodySerializerHandler> mSerializer;

        public OperationRequestBuilderTest()
        {
            testData = new CommonServiceTestData<string, string>();
            mSerializer = new Mock<RequestBodySerializerHandler>();
            cut = new OperationRequestBuilder(testData.sConfig);
        }


        [Fact]
        public void ShouldCreateSimpleGETRequest()
        {
            testData.request = cut
                .EndpointPath(Method.GET, "/endpoint")
                                    .Build();
            Assert.Equal(Method.GET, testData.request.Method);
            Assert.EndsWith("/endpoint", testData.request.Uri.AbsoluteUri);
        }

        [Fact]
        public void ShouldPutBody()
        {
            testData.request = cut
                .EndpointPath(Method.POST, "/endpoint")
                .Body("example")
                .Build();

            Assert.Equal("example", testData.request.Body);
        }

        [Fact]
        public void ShouldNotHaveBody()
        {
            testData.request = cut
                .EndpointPath(Method.POST, "/endpoint")
                .Build();

            Assert.Null(testData.request.Body);
        }

        [Fact]
        public void ShouldNotPutBodyWithSomeMethods()
        {
            testData.request = cut
                .EndpointPath(Method.GET, "/endpoint")
                .Body("example")
                .Build();

            Assert.Null(testData.request.Body);
        }

        [Fact]
        public void ShouldCallBodySerializer()
        {
            mSerializer
                .Setup(m => m.Execute("example", It.IsAny<OperationRequest>()));

            testData.request = cut
                .EndpointPath(Method.POST, "/endpoint")
                .Body("example")
                .Serializer(mSerializer.Object)
                .Build();

            mSerializer.Verify(m => m.Execute("example", testData.request), Times.Once());
        }

        [Fact]
        public void ShouldAddParameters()
        {
            testData.request = cut
                    .EndpointPath(Method.GET, "/endpoint")
                    .AddHeader("h1", "v1")
                    .AddHeader("h2", "v2")
                    .Build();
            Assert.Equal(2, testData.request.Headers.Count);
            Assert.Contains(new KeyValuePair<string, string>("h1", "v1"), testData.request.Headers);
            Assert.Contains(new KeyValuePair<string, string>("h2", "v2"), testData.request.Headers);
        }

        [Fact]
        public void ShouldAddCredentialsHeaders()
        {
            var pk = Guid.NewGuid();
            testData.mockCredentialsProvider
                .Setup(m => m.ResolveCredentials(It.IsAny<bool>()))
                .Returns(new SessionCredentials(pk.ToString(), "token"));

            testData.request = cut
                    .EndpointPath(Method.GET, "/endpoint")
                    .CredentialsProvider(testData.mockCredentialsProvider.Object)
                    .Build();

            testData.mockCredentialsProvider.Verify(m => m.ResolveCredentials(It.IsAny<bool>()), Times.Once());
            Assert.Contains(new KeyValuePair<string, string>(HTTPHeaders.XAPIKey, pk.ToString()), testData.request.Headers);
            Assert.Contains(new KeyValuePair<string, string>(HTTPHeaders.Token, "token"), testData.request.Headers);
        }

        [Fact]
        public void ShouldNotAddCredentialsHeadersIfCredentialsIsNull()
        {
            var tenantID = Guid.NewGuid();
            testData.mockCredentialsProvider
                .Setup(m => m.ResolveCredentials(It.IsAny<bool>()))
                .Returns<SessionCredentials>(null);

            testData.request = cut
                    .EndpointPath(Method.GET, "/endpoint")
                    .CredentialsProvider(testData.mockCredentialsProvider.Object)
                    .Build();

            testData.mockCredentialsProvider.Verify(m => m.ResolveCredentials(It.IsAny<bool>()), Times.Once());
            Assert.False(testData.request.Headers.ContainsKey(HTTPHeaders.XAPIKey));
            Assert.False(testData.request.Headers.ContainsKey(HTTPHeaders.Token));
        }

        [Fact]
        public void ShouldCallCredentialsProviderWithForceNewAuthentication()
        {
            testData.mockCredentialsProvider
                .Setup(m => m.ResolveCredentials(false))
                .Returns(new SessionCredentials());

            testData.request = cut
                .EndpointPath(Method.GET, "/endpoint")
                .CredentialsProvider(testData.mockCredentialsProvider.Object)
                .Build();

            testData.mockCredentialsProvider.Verify(m => m.ResolveCredentials(false), Times.Once());

            testData.mockCredentialsProvider.Reset();

            testData.mockCredentialsProvider
                .Setup(m => m.ResolveCredentials(true))
                .Returns(new SessionCredentials());

            testData.request = cut
                .EndpointPath(Method.GET, "/endpoint")
                .CredentialsProvider(testData.mockCredentialsProvider.Object)
                .ForceNewAuthentication()
                .Build();

            testData.mockCredentialsProvider.Verify(m => m.ResolveCredentials(true), Times.Once());
        }

        [Fact]
        public void ShouldReturnsIfShouldPutgAuthentication()
        {
            Assert.False(cut.ShouldPutAuthentication());
            cut.CredentialsProvider(testData.mockCredentialsProvider.Object);
            Assert.True(cut.ShouldPutAuthentication());
        }

        [Fact]
        public void ShouldSetOnePathParamToURL()
        {
            const string idValue = "1234-1234-1234-1234";
            testData.request = cut.EndpointPath(Method.GET, "/endpoint/{id}")
                                  .SetPathParam("{id}", idValue)
                                  .Build();
            Assert.Contains("/endpoint/" + idValue, testData.request.Uri.ToString());
        }

        [Fact]
        public void ShouldSetPathParamsToURL()
        {
            const string idValue = "1234-1234-1234-1234";
            const string nameValue = "parameter";
            testData.request = cut.EndpointPath(Method.GET, "/endpoint/{id}/{name}")
                                  .SetPathParam("{id}", idValue)
                                  .SetPathParam("{name}", nameValue)
                                  .Build();
            Assert.Contains("/endpoint/" + idValue + "/" + nameValue, testData.request.Uri.ToString());
        }

        [Fact]
        public void ShouldOneAddQueryParamToUrl()
        {
            testData.request = cut.EndpointPath(Method.GET, "/endpoint")
                               .AddQueryParam("orderby", "desc")
                               .Build();
            Assert.Contains("/endpoint?orderby=desc", testData.request.Uri.ToString());
        }

        [Fact]
        public void ShouldAddQueryParamsToUrl()
        {
            testData.request = cut.EndpointPath(Method.GET, "/endpoint")
                               .AddQueryParam("orderby", "desc")
                               .AddQueryParam("sortby", "asc")
                               .Build();
            Assert.Contains("/endpoint?orderby=desc&sortby=asc", testData.request.Uri.ToString());
        }
    }
}

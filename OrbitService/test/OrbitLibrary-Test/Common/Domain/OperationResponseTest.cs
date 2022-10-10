using Moq;
using OrbitLibrary.Common;
using OrbitLibrary.Common.Interfaces;
using System;
using System.Net;
using Xunit;

namespace OrbitLibrary_Test.Common.Domain
{
    public class OperationResponseTest
    {
        private OperationResponse<string,bool> cut;
        private OperationRequest req;
        private Uri responseUri;
        private Mock<ResponseBodyDeserializerHandler> mock;

        public OperationResponseTest()
        {
            req = new OperationRequest(new Uri("http://localhost"), Method.GET);
            responseUri = new Uri("http://hostresponse.com");
            cut = new OperationResponse<string,bool>(req, responseUri , HttpStatusCode.OK, "OK", "{\"nfseId\":\"6310f66030c559845b32e0ce\",\"message\":\"NFSe inserida na fila de emissão\"}");
            mock = new Mock<ResponseBodyDeserializerHandler>();
            cut.DeserializerHandle = mock.Object;
        }

        [Fact]
        public void ShouldCreateResponseInstance()
        {
            Assert.Equal(req, cut.Request);
            Assert.Equal(responseUri, cut.ResponseUri);
            Assert.Equal(HttpStatusCode.OK, cut.StatusCode);
            Assert.Equal("OK", cut.StatusDescription);
            Assert.Equal("{\"nfseId\":\"6310f66030c559845b32e0ce\",\"message\":\"NFSe inserida na fila de emissão\"}", cut.Content);
        }

        [Fact]
        public void ShouldReturnContentTypeCamelCase()
        {
            cut.Headers.Add("Content-Type", "application/json");
            Assert.Equal("application/json", cut.ContentType);
        }

        [Fact]
        public void ShouldReturnContentTypeLowerCase()
        {
            cut.Headers.Add("content-type", "application/json");
            Assert.Equal("application/json", cut.ContentType);
        }

        [Fact]
        public void ShouldReturnsSuccessResponse()
        {
            mock
                .Setup(m => m.Execute<string, string, bool>(It.IsAny<OperationResponse<string,bool>>()))
                .Returns(cut.Content);

            Assert.Equal("{\"nfseId\":\"6310f66030c559845b32e0ce\",\"message\":\"NFSe inserida na fila de emissão\"}", cut.GetSuccessResponse());
        }

        [Fact]
        public void ShouldReturnsErrorResponse()
        {

            cut = new OperationResponse<string, bool>(req, responseUri, HttpStatusCode.BadRequest, "OK", "true");
            cut.DeserializerHandle = mock.Object;
            mock
                .Setup(m => m.Execute<bool, string, bool>(cut))
                .Returns(true);

            Assert.True(cut.GetErrorResponse());
        }

        [Fact]
        public void ShouldReturnsIsSuccessful()
        {
            cut = new OperationResponse<string, bool>(req, responseUri, HttpStatusCode.Continue, "Continue", "");
            Assert.False(cut.isSuccessful);

            cut = new OperationResponse<string, bool>(req, responseUri, HttpStatusCode.OK, "OK", "");
            Assert.True(cut.isSuccessful);

            cut = new OperationResponse<string, bool>(req, responseUri, HttpStatusCode.Created, "Created", "");
            Assert.True(cut.isSuccessful);

            cut = new OperationResponse<string, bool>(req, responseUri, HttpStatusCode.Ambiguous, "Ambiguous", "");
            Assert.False(cut.isSuccessful);
        }

        [Fact]
        public void ShouldCallDeserializer()
        {
            It.IsAnyType isAnyType = new It.IsAnyType();
            mock
                .Setup(m => m.Execute<It.IsAnyType,It.IsAnyType, It.IsAnyType>(It.IsAny<OperationResponse<It.IsAnyType, It.IsAnyType>>()))
                .Returns(isAnyType);
            cut.DeserializerHandle = mock.Object;
            var op = cut.Deserialize<It.IsAnyType>();
            mock.Verify(m => m.Execute<It.IsAnyType, It.IsAnyType, It.IsAnyType>(It.IsAny<OperationResponse<It.IsAnyType, It.IsAnyType>>()), Times.Once());
        }
    }
}

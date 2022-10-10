using OrbitLibrary.Common;
using OrbitLibrary.Common.Interfaces;
using Moq;
using OrbitLibrary_Test.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OrbitLibrary_Test.TestUtils;

namespace OrbitLibrary_Test.Common
{

    public class ForTestServiceHandler : BaseService<string,string>
    {
        public ForTestServiceHandler(ServiceConfiguration sConfig, CommunicationProvider client) : base(sConfig, client) { }

        public OperationRequestBuilder Builder => base.GetBuilder();

        public OperationResponse<string,string> DoAnOperation(OperationRequestBuilder builder, ResponseBodyDeserializerHandler handle)
        {
            return InvokeOperation(builder, handle);
        }
    }

    public class BaseServiceTest 
    {
        public CommonServiceTestData<string, string> t;
        private OperationRequestBuilder builder;
        private ForTestServiceHandler cut; //(C)ode (U)nder (T)est

        public BaseServiceTest()
        {
            t = new CommonServiceTestData<string, string>();
            cut = new ForTestServiceHandler(t.sConfig, t.mockClient.Object);
        }

        private void setupMockClient()
        {
            t.mockClient
                .Setup(c => c.Send<string, string>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(TestsBuilder.CreateOperationResponse<string, string>(t.request));
        }

        [Fact]
        public void ShouldReturnsAValidOperationRequestBuilder()
        {
            Assert.NotNull(cut.Builder);
            //Ensures that each new call will returns a new instance
            Assert.NotSame(cut.Builder, cut.Builder);
        }

        [Fact]
        public void ShouldBuildOperationRequestWithoutDeserializeHandleAndInvokeCommunicationClient()
        {
            builder = cut.Builder.EndpointPath(Method.GET, "/endpoint");

            setupMockClient();

            t.response = cut.DoAnOperation(builder, null);

            Assert.NotNull(t.request);
            t.mockClient.Verify(client => client.Send<string, string>(t.request), Times.Once());
            Assert.Null(t.response.DeserializerHandle); //Unsetted DeserializeHandle
        }

        [Fact]
        public void ShouldBuildOperationRequestWithDeserializeHandle()
        {
            builder = cut.Builder.EndpointPath(Method.GET, "/endpoint");

            setupMockClient();

            t.response = cut.DoAnOperation(builder, t.mockBodyDeserializerHandler.Object);


            Assert.NotNull(t.request);
            t.mockClient.Verify(client => client.Send<string, string>(t.request), Times.Once());
            Assert.Equal(t.mockBodyDeserializerHandler.Object, t.response.DeserializerHandle);
        }

        [Fact]
        public void ShouldRetryRequestIfAuthenticationIsRequiredAndResponseIsUnauthorized()
        {
            builder = cut.Builder
                            .EndpointPath(Method.GET, "/endpoint")
                            .CredentialsProvider(t.mockCredentialsProvider.Object);

            MockSequence sequence = new MockSequence();

            t.mockClient
                .InSequence(sequence)
                .Setup(c => c.Send<string, string>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(TestsBuilder.CreateOperationResponse<string, string>(t.request, "", System.Net.HttpStatusCode.Unauthorized));

            t.mockClient
                .InSequence(sequence)
                .Setup(c => c.Send<string, string>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(TestsBuilder.CreateOperationResponse<string, string>(t.request));

            t.response = cut.DoAnOperation(builder, t.mockBodyDeserializerHandler.Object);
            Assert.NotNull(t.request);
            t.mockClient.Verify(client => client.Send<string, string>(It.IsAny<OperationRequest>()), Times.Exactly(2));
            Assert.Equal(t.mockBodyDeserializerHandler.Object, t.response.DeserializerHandle);
        }

        [Fact]
        public void ShouldNotRetryRequestIfAuthenticationIsNotRequired()
        {
            builder = cut.Builder.EndpointPath(Method.GET, "/endpoint");

            t.mockClient
                .Setup(c => c.Send<string, string>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(TestsBuilder.CreateOperationResponse<string, string>(t.request, "", System.Net.HttpStatusCode.Unauthorized));

            t.response = cut.DoAnOperation(builder, null);
            Assert.NotNull(t.request);
            t.mockClient.Verify(client => client.Send<string, string>(It.IsAny<OperationRequest>()), Times.Once());
        }
    }
}

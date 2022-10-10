using OrbitLibrary.Common;
using Moq;
using OrbitLibrary_Test.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using OrbitLibrary.Common.Interfaces;

namespace OrbitLibrary_Test.TestUtils
{
    public class CommonServiceTestData<TSuccess, TError> : IDisposable
    {
        public Mock<CommunicationProvider> mockClient;
        public Mock<CredentialsProvider> mockCredentialsProvider;
        public Mock<ResponseBodyDeserializerHandler> mockBodyDeserializerHandler;
        public ServiceConfiguration sConfig;
        public OperationRequest request;
        public OperationResponse<TSuccess, TError> response;

        public CommonServiceTestData()
        {
            mockClient = new Mock<CommunicationProvider>();
            mockCredentialsProvider = new Mock<CredentialsProvider>();
            mockBodyDeserializerHandler = new Mock<ResponseBodyDeserializerHandler>();
            sConfig = TestsBuilder.CreateFakeConfigurationForTest();
            sConfig.CredentialsProvider = mockCredentialsProvider.Object;
        }


        public void mountCredentialsProviderMock()
        {
            SessionCredentials session = new SessionCredentials("x-api-key", "token");
            mockCredentialsProvider
                .Setup(m => m.ResolveCredentials(It.IsAny<bool>()))
                .Returns(session);
        }

        public void Dispose()
        {
            mockClient.Reset();
            mockCredentialsProvider.Reset();
            mockBodyDeserializerHandler.Reset();
        }
    }
}

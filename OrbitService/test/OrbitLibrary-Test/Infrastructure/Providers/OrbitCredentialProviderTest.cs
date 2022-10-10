using OrbitLibrary.Common;
using Moq;
using OrbitLibrary_Test.Builders;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;
using OrbitLibrary.Services;
using OrbitLibrary.Services.Session;
using OrbitLibrary_Test.TestUtils;

namespace OrbitLibrary_Test.Infrastructure.Providers
{
    public class OrbitCredentialProviderTest
    {
        private CommonServiceTestData<LoginResponseOutput, ErrorOutput> testData;
        OrbitCredentialProvider cut;
        SessionCredentials credentials;
        LoginResponseOutput authenticationOutput;

        public OrbitCredentialProviderTest()
        {
            setupTestsInstances();
        }

        private void setupTestsInstances()
        {
            testData = new CommonServiceTestData<LoginResponseOutput, ErrorOutput>();
            cut = new OrbitCredentialProvider(testData.sConfig, testData.mockClient.Object);
        }

        private void setupMockClient(object responseContentObject, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            testData.mockClient
                .Setup(m => m.Send<LoginResponseOutput, ErrorOutput>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => testData.request = r)
                .Returns(
                    TestsBuilder
                        .CreateOperationResponse<LoginResponseOutput, ErrorOutput>(
                            testData.request,
                            System.Text.Json.JsonSerializer.Serialize(responseContentObject), statusCode
                            )
                        );
        }

        private LoginResponseOutput createAuthenticationOutputWithoutTenants()
        {
            LoginResponseOutput output = new LoginResponseOutput();
            output.token = "jwt_token";
            UserData userData = new UserData();
            userData.tenants = new List<Tenant>();
            output.data = userData;
            return output;
        }

        private LoginResponseOutput createSuccessAuthenticationOutput(string tenantID = null)
        {
            var output = createAuthenticationOutputWithoutTenants();
            Tenant tenant = new Tenant();
            tenant.pk = "tenant_pk";
            tenant.tenantId = tenantID == null ? testData.sConfig.TenantID.ToString() : tenantID;
            output.data.tenants.Add(tenant);
            return output;
        }

        [Fact]
        public void ShouldCallCommunicationClientAndReturnValidSessionCredential()
        {
            authenticationOutput = createSuccessAuthenticationOutput();

            setupMockClient(authenticationOutput);

            credentials = cut.ResolveCredentials(forceNewAuthentication: true);
            testData.mockClient.Verify(m => m.Send<LoginResponseOutput, ErrorOutput>(It.IsAny<OperationRequest>()), Times.Once());
            Assert.True(credentials.IsValid());
            Assert.Equal(authenticationOutput.data.tenants[0].pk, credentials.xApiKey.ToString());
            Assert.Equal(authenticationOutput.token, credentials.Token);
        }

        [Fact]
        public void ShouldReturnUnauthorizedError()
        {
            ErrorOutput errorOutput = new ErrorOutput();
            errorOutput.statusCode = (int)HttpStatusCode.Unauthorized;
            errorOutput.message = "Unauthorized";

            setupMockClient(errorOutput, HttpStatusCode.Unauthorized);

            credentials = cut.ResolveCredentials(forceNewAuthentication: true);

            Assert.NotNull(credentials);
            Assert.False(credentials.IsValid());
        }

        [Fact]
        public void ShouldReturnAnInvalidSessionIfTheResponseBodyHasAnUnknownStructure()
        {
            var unknownBodyStructure = new
            {
                unknownField = "teste",
            };

            setupMockClient(unknownBodyStructure);
            credentials = cut.ResolveCredentials(forceNewAuthentication: true);
            Assert.False(credentials.IsValid());
        }

        [Fact]
        public void ShouldReturnInvalidSessionWhenUserDoNotHaveTenants()
        {
            authenticationOutput = createAuthenticationOutputWithoutTenants();

            setupMockClient(authenticationOutput);

            credentials = cut.ResolveCredentials(forceNewAuthentication: true);
            testData.mockClient.Verify(m => m.Send<LoginResponseOutput, ErrorOutput>(It.IsAny<OperationRequest>()), Times.Once());
            Assert.False(credentials.IsValid());
            Assert.Equal(authenticationOutput.token, credentials.Token);
        }

        [Fact]
        public void ShouldReturnInvalidSessionWhenUserDoNotHasTheSelectedTenant()
        {
            authenticationOutput = createSuccessAuthenticationOutput(tenantID: "other_tenant_guid");

            setupMockClient(authenticationOutput);

            credentials = cut.ResolveCredentials(forceNewAuthentication: true);
            testData.mockClient.Verify(m => m.Send<LoginResponseOutput, ErrorOutput>(It.IsAny<OperationRequest>()), Times.Once());
            Assert.False(credentials.IsValid());
            Assert.Equal(authenticationOutput.token, credentials.Token);
        }

        [Fact]
        public void ShouldReturnTheLastValidInstanceOfCredentialsWithoutCallCommunicationClientIfInstanceExists()
        {
            authenticationOutput = createSuccessAuthenticationOutput();
            setupMockClient(authenticationOutput);
            credentials = cut.ResolveCredentials(forceNewAuthentication: true);
            testData.mockClient.Verify(m => m.Send<LoginResponseOutput, ErrorOutput>(It.IsAny<OperationRequest>()), Times.Once());
            Assert.True(credentials.IsValid());
            Assert.Equal(authenticationOutput.data.tenants[0].pk, credentials.xApiKey.ToString());
            Assert.Equal(authenticationOutput.token, credentials.Token);

            OrbitCredentialProvider otherCut = new OrbitCredentialProvider(testData.sConfig, testData.mockClient.Object);
            SessionCredentials otherCredentials = otherCut.ResolveCredentials();
            Assert.NotSame(cut, otherCut);
            Assert.Same(credentials, otherCredentials);
            testData.mockClient.Verify(m => m.Send<LoginResponseOutput, ErrorOutput>(It.IsAny<OperationRequest>()), Times.Once());
        }

        [Fact]
        public void ShouldCallAuthenticationServiceWhenForceNewAuthenticationParameterIsTrue()
        {
            authenticationOutput = createSuccessAuthenticationOutput();
            setupMockClient(authenticationOutput);
            credentials = cut.ResolveCredentials(forceNewAuthentication: true);
            testData.mockClient.Verify(m => m.Send<LoginResponseOutput, ErrorOutput>(It.IsAny<OperationRequest>()), Times.Once());
            Assert.True(credentials.IsValid());
            Assert.Equal(authenticationOutput.data.tenants[0].pk, credentials.xApiKey.ToString());
            Assert.Equal(authenticationOutput.token, credentials.Token);

            SessionCredentials otherCredentials = cut.ResolveCredentials(forceNewAuthentication: true);
            Assert.NotSame(credentials, otherCredentials);
            testData.mockClient.Verify(m => m.Send<LoginResponseOutput, ErrorOutput>(It.IsAny<OperationRequest>()), Times.Exactly(2));
        }
    }
}

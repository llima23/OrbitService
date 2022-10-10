using OrbitLibrary.Common;
using Moq;
using OrbitLibrary_Test.Builders;
using System;
using Xunit;

namespace OrbitLibrary_Test.Common.Domain
{
    public class TestDataServiceConfiguration : IDisposable
    {
        public ServiceConfiguration cut; //(C)ode (U)nder (T)est
        public Mock<CommunicationProvider> mockClient;
        public Mock<CredentialsProvider> mockCredentialsProvider;

        public TestDataServiceConfiguration()
        {
            cut = NewServiceConfiguration("http://localhost");
        }

        public ServiceConfiguration NewServiceConfiguration(string baseURI)
        {
            mockClient = new Mock<CommunicationProvider>();
            mockCredentialsProvider = new Mock<CredentialsProvider>();
            return TestsBuilder.CreateFakeConfigurationForTest();
        }

        public void Dispose()
        {
        }
    }

    public class ServiceConfigurationTest : IClassFixture<TestDataServiceConfiguration>
    {
        public TestDataServiceConfiguration t;

        public ServiceConfigurationTest(TestDataServiceConfiguration testData) => t = testData;

        [Fact]
        public void GetBaseURIWithoutLatestSlash()
        {
            t.cut = t.NewServiceConfiguration("http://localhost");
            Assert.NotNull(t.cut);
            Assert.Equal("http://localhost", t.cut.GetBaseURI());

            t.cut = t.NewServiceConfiguration("http://localhost/");
            Assert.Equal("http://localhost", t.cut.GetBaseURI());
        }

        [Fact]
        public void ShouldSetCredentialsProvider()
        {
            Assert.Null(t.cut.CredentialsProvider);
            t.cut.CredentialsProvider = t.mockCredentialsProvider.Object;
            Assert.NotNull(t.cut.CredentialsProvider);
            Assert.Equal(t.mockCredentialsProvider.Object, t.cut.CredentialsProvider);
        }
    }
}

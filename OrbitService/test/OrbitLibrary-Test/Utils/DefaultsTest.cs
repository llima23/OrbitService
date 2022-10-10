using OrbitLibrary_Test.TestUtils;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using Xunit;

namespace OrbitLibrary_Test.Utils
{
    public class DefaultsTest
    {
        private CommonServiceTestData<string, string> testData;

        public DefaultsTest()
        {
            testData = new CommonServiceTestData<string, string>();
        }

        [Fact]
        public void ShouldReturnsDefaultCredentialProvider()
        {
            var credentialsProvider = Defaults.GetCredentialsProvider(testData.sConfig, testData.mockClient.Object);
            Assert.IsType<OrbitCredentialProvider>(credentialsProvider);
        }
    }
}

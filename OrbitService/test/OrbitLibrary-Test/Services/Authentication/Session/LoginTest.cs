using OrbitLibrary.Common;
using Moq;
using Newtonsoft.Json;
using OrbitLibrary_Test.Builders;
using Xunit;
using OrbitLibrary.Services.Session;
using OrbitLibrary_Test.TestUtils;

namespace OrbitLibrary_Test.Services.Authentication.Session
{

    public class LoginTest : IClassFixture<CommonServiceTestData<LoginResponseOutput, ErrorOutput>>
    {

        private CommonServiceTestData<LoginResponseOutput, ErrorOutput> t;
        private Login cut;

        public LoginTest(CommonServiceTestData<LoginResponseOutput, ErrorOutput> testData)
        {
            t = testData;
            cut = new Login(t.sConfig, testData.mockClient.Object);
        }

        [Fact]
        public void LoginWithouParameters()
        {
            t.mockClient
                .Setup(c => c.Send<LoginResponseOutput, ErrorOutput>(It.IsAny<OperationRequest>()))
                .Callback<OperationRequest>(r => t.request = r)
                .Returns(TestsBuilder.CreateOperationResponse<LoginResponseOutput, ErrorOutput>(t.request));

            t.response = cut.Execute();
            Assert.NotNull(t.request);
            Assert.NotNull(t.response);
            Assert.EndsWith(Login.ENDPOINT, t.request.Uri.AbsoluteUri);
            var input = new LoginRequestInput();
            input.Username = t.sConfig.Username;
            input.Password = t.sConfig.Password;
            Assert.Equal(JsonConvert.SerializeObject(input), t.request.Body);
        }
    }
}

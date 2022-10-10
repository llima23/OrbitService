using OrbitLibrary.Common;
using OrbitLibrary_Test.Builders;
using Xunit;

namespace OrbitLibrary_Test.Infrastructure.Handlers
{
    class Output
    {
        public string name { get; set; }
    }

    public class JsonResponseBodyDeserializerTest
    {
        private JsonResponseBodyDeserializer cut;
        private OperationResponse<string,string> response;

        public JsonResponseBodyDeserializerTest()
        {
            cut = new JsonResponseBodyDeserializer();
        }

        [Fact]
        public void ShouldDeserializeJsonToObject()
        {
            string content = @"{""name"":""test""}";
            response = TestsBuilder.CreateOperationResponse<string,string>(TestsBuilder.CreateOperationRequest(), content);

            Output output;

            output = cut.Execute<Output,string, string>(response);
            Assert.Equal("test", output.name);
            
        }
    }
}

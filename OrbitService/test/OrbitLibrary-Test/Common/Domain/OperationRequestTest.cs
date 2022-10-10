using OrbitLibrary.Common;
using System;
using Xunit;

namespace OrbitLibrary_Test.Common.Domain
{

    public class TestData : IDisposable
    {
        public Uri Uri;
        public OperationRequest cut; //(C)ode (U)nder (T)est

        public TestData()
        {
            Uri = new Uri("http://localhost:3000");
            cut = NewRequest();
        }

        public OperationRequest NewRequest()
        {
            return new OperationRequest(Uri, Method.GET);
        }

        public void Dispose()
        {
        }
    }

    public class OperationRequestTest : IClassFixture<TestData>
    {

        public TestData t;

        public OperationRequestTest(TestData testData) => t = testData;

        [Fact]
        public void InstanceCreation()
        {
            Assert.NotNull(t.cut);
            Assert.Equal(t.Uri, t.cut.Uri);
            Assert.Equal(Method.GET, t.cut.Method);
        }

        [Fact]
        public void PutBody()
        {
            t.cut.PutBody("Example Body");
            Assert.Equal("Example Body", t.cut.Body);
        }

        [Fact]
        public void AddingHeaders()
        {
            t.cut.Headers.Add("Content-Type", "application/json");
            Assert.Single(t.cut.Headers);
            t.cut.Headers.Add("Other-header", "ok");
            Assert.Equal(2, t.cut.Headers.Count);
        }

    }
}

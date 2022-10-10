using OrbitLibrary.Common;
using System;
using Xunit;

namespace OrbitLibrary_Test.Common.Domain
{
    public class SessionCredentialsTest
    {
        private SessionCredentials cut;

        [Fact]
        public void shouldCreateAValidInstance()
        {
            string pk = Guid.NewGuid().ToString();
            cut = new SessionCredentials(pk, "jwt");
            Assert.Equal(pk, cut.xApiKey);
            Assert.Equal("jwt", cut.Token);
            Assert.True(cut.IsValid());
        }

        [Fact]
        public void shouldCreateAInvalidInstance()
        {
            cut = new SessionCredentials();
            Assert.False(cut.IsValid());
        }
    }
}

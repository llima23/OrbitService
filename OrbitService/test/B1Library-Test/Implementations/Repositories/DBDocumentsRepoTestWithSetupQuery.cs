using B1Library.Implementations.Repositories;
using Moq;
using OrbitLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace B1Library_Tests.Implementations.Repositories
{
    public class DBDocumentsRepoTestWithSetupQuery
    {
        private DBDocumentsRepository cut;
        private Mock<IWrapper> mockWrapper;

        public DBDocumentsRepoTestWithSetupQuery()
        {
            mockWrapper = new Mock<IWrapper>();
            cut = new DBDocumentsRepository(mockWrapper.Object);
        }   

        [Fact]
        public void ShouldGetInboundOtherDocuments()
        {

        }
    }
}

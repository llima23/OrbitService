using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrbitLibrary_Test.Infrastructure.Handlers
{
    public class JsonRequestBodySerializerTest
    {
        private JsonRequestBodySerializer cut;
        private OperationRequest request;

        public JsonRequestBodySerializerTest()
        {
            cut = new JsonRequestBodySerializer();
            request = new OperationRequest(new Uri("http://localhost"), Method.POST);
        }

        [Fact]
        public void ShouldSerializeTheObjectToJson()
        {
            cut.Execute(new
            {
                username = "user"
            }, request);

            Assert.Equal("{\"username\":\"user\"}", request.Body);
            Assert.Contains(new KeyValuePair<string, string>(HTTPHeaders.ContentType, HTTPContentTypes.ApplicationJson), request.Headers);
        }

        [Fact]
        public void ShouldSerializeObjectWithNullFields()
        {
            cut.Execute(new
            {
                username = "user",
                field = (string)null
            }, request);

            Assert.Equal("{\"username\":\"user\",\"field\":null}", request.Body);
        }

        [Fact]
        public void ShouldSerializeObjectWithoutNullFields()
        {
            cut = new JsonRequestBodySerializer(removeNullFields: true);
            cut.Execute(new
            {
                username = "user",
                field = (string)null
            }, request);

            Assert.Equal("{\"username\":\"user\"}", request.Body);
        }
    }
}

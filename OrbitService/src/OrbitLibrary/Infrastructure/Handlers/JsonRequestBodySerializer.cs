using Newtonsoft.Json;
using OrbitLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Common
{
    public class JsonRequestBodySerializer : RequestBodySerializerHandler
    {
        public bool RemoveNullFields { get; set; } = false;

        public JsonRequestBodySerializer(bool removeNullFields = false)
        {
            RemoveNullFields = removeNullFields;
        }
        public void Execute(object originalBody, OperationRequest request)
        {
            JsonSerializerSettings settings = getSerializeSettings();
            request.PutBody(JsonConvert.SerializeObject(originalBody, Formatting.None, settings));
            request.Headers.Add(HTTPHeaders.ContentType, HTTPContentTypes.ApplicationJson);
        }

        private JsonSerializerSettings getSerializeSettings()
        {
            return new JsonSerializerSettings{
                NullValueHandling = RemoveNullFields ? NullValueHandling.Ignore : NullValueHandling.Include
            };
        }
    }
}

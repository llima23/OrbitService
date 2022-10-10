using OrbitLibrary.Common.Interfaces;
using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace OrbitLibrary.Common
{
    public class JsonResponseBodyDeserializer : ResponseBodyDeserializerHandler
    {
        public T Execute<T,TSuccess,TError>(OperationResponse<TSuccess, TError> response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}

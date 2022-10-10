using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Common.Interfaces
{
    public interface ResponseBodyDeserializerHandler
    {
        public T Execute<T,TSuccess,TError>(OperationResponse<TSuccess, TError> response); 
    }
}

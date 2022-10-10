using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Common
{
    public interface CommunicationProvider
    {
        OperationResponse<TSuccess,TError> Send<TSuccess, TError>(OperationRequest request);
    }
}

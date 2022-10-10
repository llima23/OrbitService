using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Common
{
    public interface RequestBodySerializerHandler
    {
        void Execute(object originalBody, OperationRequest request);
    }
}

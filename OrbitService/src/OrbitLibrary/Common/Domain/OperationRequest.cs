using System;
using System.Collections.Generic;

namespace OrbitLibrary.Common
{
    public enum Method
    {
        GET,
        HEAD,
        POST,
        PUT,
        DELETE,
        OPTIONS,
        PATCH
    }

    public class OperationRequest
    {
   
        public Uri Uri { get; private set; }
        public Method Method { get; private set; }

        // TODO: Create methods to add, remove and clear headers
        public SortedDictionary<string, string> Headers { get; private set; }
        public object Body { get; private set; }

        public OperationRequest(Uri uri, Method method)
        {
            Uri = uri;
            Method = method;
            Headers = new SortedDictionary<string, string>();
        }

        public void PutBody(object newBody)
        {
            Body = newBody;
        }
    }
}

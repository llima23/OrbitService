using OrbitLibrary.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;

namespace OrbitLibrary.Common
{
    public class OperationResponse<TSuccess,TError>
    {
        public OperationRequest Request { get; private set; }
        public Uri ResponseUri { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public string StatusDescription { get; }
        public SortedDictionary<string, string> Headers { get; private set; }
        public string Content { get; set; }
        public string ContentType { 
            get
            {
                return Headers.ContainsKey("Content-Type") ? Headers.GetValueOrDefault("Content-Type") : Headers.GetValueOrDefault("content-type");
            }
        }
        public ResponseBodyDeserializerHandler DeserializerHandle { get; set; }
        public bool isSuccessful { 
            get
            {
                return StatusCode >= HttpStatusCode.OK && StatusCode < HttpStatusCode.Ambiguous;
            }
         }

        public OperationResponse(OperationRequest request, Uri responseUri, HttpStatusCode statusCode, string statusDescription, string content)
        {
            Request = request;
            ResponseUri = responseUri;
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            Content = content;
            Headers = new SortedDictionary<string, string>();
        }

        public T Deserialize<T>()
        {
            return this.DeserializerHandle.Execute<T , TSuccess, TError>(this);
        }

        public TError GetErrorResponse()
        {
            return this.DeserializerHandle.Execute<TError, TSuccess, TError>(this);
        }

        public TSuccess GetSuccessResponse()
        {
            return this.DeserializerHandle.Execute<TSuccess, TSuccess, TError>(this);
        }
    }
}

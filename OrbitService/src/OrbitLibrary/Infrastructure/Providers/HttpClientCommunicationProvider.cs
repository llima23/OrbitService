using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitLibrary.Common
{
    public class HttpClientCommunicationProvider : CommunicationProvider
    {

        protected virtual IRestClient GetRestClient(Uri url)
        {
            return new RestClient(url);
        }

        public OperationResponse<TSuccess, TError> Send<TSuccess, TError>(OperationRequest request)
        {
            IRestClient client = GetRestClient(request.Uri);
            RestRequest r = new RestRequest(getRestResharpMethod(request.Method));
            r.AddHeaders(request.Headers);
            setBody(request, r);

            IRestResponse response = client.Execute(r);
            OperationResponse<TSuccess, TError> operationResponse = new OperationResponse<TSuccess, TError>(request, response.ResponseUri, response.StatusCode, response.StatusDescription, response.Content);

            foreach (var item in response.Headers)
            {
                if (item.Type == ParameterType.HttpHeader)
                {
                    operationResponse.Headers.Add(item.Name, item.Value.ToString());
                }
            }

            return operationResponse;
        }

        private void setBody(OperationRequest request, RestRequest r)
        {
            if (request.Method.Equals(Method.HEAD) ||
               request.Method.Equals(Method.OPTIONS) ||
               request.Method.Equals(Method.GET) ||
               request.Method.Equals(Method.DELETE)) {
                return;
            }
            r.AddJsonBody(request.Body);
        }

        private RestSharp.Method getRestResharpMethod(Method method)
        {
            return (RestSharp.Method)Enum.Parse(typeof(RestSharp.Method), method.ToString());
        }
    }
}

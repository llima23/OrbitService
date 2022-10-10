using OrbitLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OrbitLibrary.Common
{
    public class OperationRequestBuilder
    {
        private ServiceConfiguration sConfig;
        private Method method;
        private string endpointPath;
        private object body;
        private RequestBodySerializerHandler bodySerializer;
        public SortedDictionary<string, string> headers;
        public CredentialsProvider credentialsProvider;
        private bool forceNewAuthentication;
        private SortedDictionary<string, string> pathParams;
        private SortedDictionary<string, string> queryParams;

        public object HTTPUtils { get; private set; }

        public OperationRequestBuilder(ServiceConfiguration sConfig)
        {
            this.sConfig = sConfig;
            headers = new SortedDictionary<string, string>();
            pathParams = new SortedDictionary<string, string>();
            queryParams = new SortedDictionary<string, string>();
        }

        public OperationRequest Build()
        {
            Uri fullURI = generateFullURI(endpointPath);
            OperationRequest request = new OperationRequest(fullURI, method);
            setHeaders(request);
            setBody(request);
            setCredentials(request);
            return request;
        }

        public OperationRequestBuilder EndpointPath(Method method, string endpointPath)
        {
            this.method = method;
            this.endpointPath = endpointPath;
            return this;
        }

        private void setCredentials(OperationRequest request)
        {
            if (!ShouldPutAuthentication())
            {
                return;
            }
            SessionCredentials credentials = credentialsProvider.ResolveCredentials(forceNewAuthentication);
            if (credentials == null)
            {
                return;
            }
            request.Headers.Add(HTTPHeaders.XAPIKey, credentials.xApiKey);
            request.Headers.Add(HTTPHeaders.Token, credentials.Token);
        }

        private void setHeaders(OperationRequest request)
        {
            foreach (var item in headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }
        }

        private void setBody(OperationRequest request)
        {
            if (request.Method.Equals(Method.HEAD) ||
                request.Method.Equals(Method.OPTIONS) ||
                request.Method.Equals(Method.GET) ||
                request.Method.Equals(Method.DELETE))
            {
                return;
            }

            if (body == null)
            {
                return;
            }

            if (bodySerializer != null)
            {
                bodySerializer.Execute(body, request);
            }
            else
            {
                request.PutBody(body);
            }
        }

        private Uri generateFullURI(string endpointPath)
        {
            StringBuilder sb = new StringBuilder(sConfig.GetBaseURI());
            string endPointPathWithParams = mountPathParams(endpointPath);
            sb.Append(mountQueryParams(endPointPathWithParams));
            return new Uri(sb.ToString());
        }

        private string mountQueryParams(string endPointPathWithParams)
        {
            if (queryParams.Count == 0)
            {
                return endPointPathWithParams;
            }

            StringBuilder sb = new StringBuilder(endPointPathWithParams);
            sb.Append("?");
            bool shouldPutConector = false;
            foreach (var queryParam in queryParams)
            {
                sb.Append(shouldPutConector ? "&" : string.Empty);
                sb.Append(queryParam.Key);
                sb.Append("=");
                sb.Append(queryParam.Value);
                shouldPutConector = true;
            }

            return sb.ToString();
        }

        private string mountPathParams(string endpointPath)
        {
            if (pathParams.Count == 0)
            {
                return endpointPath;
            }
            Regex rx = new Regex(@"({.+?})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(endpointPath);
            string endPointPathWithParams = endpointPath;
            for (int i = 0; i < matches.Count; i++)
            {
                string paramName = matches[i].Value;
                string paramValue = pathParams.GetValueOrDefault(paramName);
                endPointPathWithParams = endPointPathWithParams.Replace(paramName, paramValue);
            }

            return endPointPathWithParams;
        }

        public OperationRequestBuilder Body(object body)
        {
            this.body = body;
            return this;
        }

        public OperationRequestBuilder Serializer(RequestBodySerializerHandler serializer)
        {
            this.bodySerializer = serializer;
            return this;
        }

        public OperationRequestBuilder AddHeader(string name, string value)
        {
            headers.Add(name, value);
            return this;
        }

        public OperationRequestBuilder CredentialsProvider(CredentialsProvider credentialsProvider)
        {
            this.credentialsProvider = credentialsProvider;
            return this;
        }

        public OperationRequestBuilder ForceNewAuthentication()
        {
            forceNewAuthentication = true;
            return this;
        }

        public bool ShouldPutAuthentication()
        {
            return credentialsProvider != null;
        }

        public OperationRequestBuilder SetPathParam(string paramName, string paramValue)
        {
            pathParams.Add(paramName, paramValue);
            return this;
        }

        public OperationRequestBuilder AddQueryParam(string queryName, string queryValue)
        {
            queryParams.Add(queryName, queryValue);
            return this;
        }
    }
}

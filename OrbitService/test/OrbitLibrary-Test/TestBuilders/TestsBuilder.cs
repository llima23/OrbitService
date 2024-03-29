﻿using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OrbitLibrary_Test.Builders
{
    public class TestsBuilder
    {
        public static ServiceConfiguration CreateFakeConfigurationForTest(string baseURI = "http://localhost")
        {
            return new ServiceConfiguration(new Uri(baseURI), "user", "password",false, Guid.NewGuid(), true, true);
        }

        public static OperationRequest CreateOperationRequest(string url = "http://localhost", Method method = Method.POST)
        {
            return new OperationRequest(new Uri(url), method);
        }

        public static OperationResponse<TSuccess, TError> CreateOperationResponse<TSuccess, TError>(OperationRequest request, string content = "", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new OperationResponse<TSuccess, TError>(request, new Uri("http://responsehost.com"), statusCode,"OK", content);
        }
    }
}

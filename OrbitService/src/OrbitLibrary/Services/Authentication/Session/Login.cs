using Newtonsoft.Json;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OrbitLibrary.Services.Session
{
    public struct LoginRequestInput
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class LoginResponseOutput
    {
        public string token { get; set; }
        public string accessToken { get; set; }
        public UserData data { get; set; }

    }

    public class UserData
    {
        public string user_id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public List<Tenant> tenants { get; set; }
    }

    public class Tenant
    {
        public string pk { get; set; }
        public string tenantId { get; set; }
        public string tenantName { get; set; }
        public object adminUser { get; set; }
        public object created_at { get; set; }
    }

    public class ErrorOutput
    {
        public int statusCode { get; set; }
        public string message { get; set; }
    }

    public class Login : BaseService<LoginResponseOutput, ErrorOutput>
    {
        public const string ENDPOINT = "/auth";


        public Login(ServiceConfiguration sConfig, CommunicationProvider client) : base(sConfig, client)
        {
        }

        public OperationResponse<LoginResponseOutput, ErrorOutput> Execute()
        {
            LoginRequestInput input = new LoginRequestInput();
            input.Username = sConfig.Username;
            input.Password = sConfig.Password;

            return InvokeOperation(
                GetBuilder().EndpointPath(Method.POST, ENDPOINT)
                            .Body(input)
                            .Serializer(new JsonRequestBodySerializer()),
                new JsonResponseBodyDeserializer());
        }
    }
}

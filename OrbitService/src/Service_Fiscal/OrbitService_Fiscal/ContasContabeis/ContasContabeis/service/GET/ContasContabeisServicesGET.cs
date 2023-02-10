using AccountService_ContasContabeis.ContasContabeis.service;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.ContasContabeis.ContasContabeis.service.GET
{
    public class ContasContabeisServicesGET : BaseService<ContasContabeisOutputGET, ContasContabeisError>
    {
        public const string ENDPOINT = "/accountservice/api/account/tenantid/getAll";
        public ContasContabeisServicesGET(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<ContasContabeisOutputGET, ContasContabeisError> Execute(string id)
        {
            return InvokeOperation(
                    GetBuilder()
                        .EndpointPath(Method.GET, ENDPOINT)
                        .CredentialsProvider(sConfig.CredentialsProvider)
                        .AddQueryParam("account_code", $@"{id}")
                        .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                    new JsonResponseBodyDeserializer());
        }
    }
}

using AccountService_ContasContabeis.ContasContabeis.service;
using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.ContasContabeis.ContasContabeis.service.PUT
{
    public class ContasContabeisServicePUT : BaseService<ContasContabeisOutput, ContasContabeisError>
    {
        public const string ENDPOINT = "/accountservice/api/account/{accountId}";
        public ContasContabeisServicePUT(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<ContasContabeisOutput, ContasContabeisError> Execute(ContasContabeisInputPUT input, string id)
        {
            return InvokeOperation(
                  GetBuilder()
                      .EndpointPath(Method.PUT, ENDPOINT)
                      .CredentialsProvider(sConfig.CredentialsProvider)
                      .Body(input)
                      .SetPathParam("{accountId}", $@"{id}")
                      .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                  new JsonResponseBodyDeserializer());
        }
    }
}

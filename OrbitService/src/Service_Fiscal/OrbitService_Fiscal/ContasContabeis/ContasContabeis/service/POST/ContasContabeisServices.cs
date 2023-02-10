using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_ContasContabeis.ContasContabeis.service
{
    public class ContasContabeisServices : BaseService<ContasContabeisOutput, ContasContabeisError>
    {
        public const string ENDPOINT = "/accountservice/api/account";
        public ContasContabeisServices(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<ContasContabeisOutput, ContasContabeisError> Execute(ContasContabeisInput input)
        {
            return InvokeOperation(
                    GetBuilder()
                        .EndpointPath(Method.POST, ENDPOINT)
                        .CredentialsProvider(sConfig.CredentialsProvider)
                        .Body(input)
                        .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                    new JsonResponseBodyDeserializer());
        }
    }
}

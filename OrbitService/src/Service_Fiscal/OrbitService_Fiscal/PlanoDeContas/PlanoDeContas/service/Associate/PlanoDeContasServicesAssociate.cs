using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_PlanoDeContas.PlanoDeContas.service.Associate
{
    public class PlanoDeContasServicesAssociate : BaseService<PlanoDeContaOutputAssociate, PlanoDeContasError>
    {
        public const string ENDPOINT = "/accountservice/api/link-account-plan";
        public PlanoDeContasServicesAssociate(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<PlanoDeContaOutputAssociate, PlanoDeContasError> ExecuteAssociate(PlanoDeContaInputAssociate input)
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

using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_PlanoDeContas.PlanoDeContas.service.Create
{
    public class PlanoDeContasServicesCreate : BaseService<PlanoDeContasOutputCreate, PlanoDeContasError>
    {
        public const string ENDPOINT = "/accountservice/api/accounts-plan";
        public PlanoDeContasServicesCreate(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<PlanoDeContasOutputCreate, PlanoDeContasError> ExecuteCreate(PlanoDeContasInputCreate input)
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

using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_CentroDeCusto.CentroDeCusto.service
{
    public class CentroDeCustoService : BaseService<CentroDeCustoOutput, CentroDeCustoError>
    {
        public const string ENDPOINT = "/accountservice/api/costCenter";
        public CentroDeCustoService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<CentroDeCustoOutput, CentroDeCustoError> Execute(CentroDeCustoInput input)
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

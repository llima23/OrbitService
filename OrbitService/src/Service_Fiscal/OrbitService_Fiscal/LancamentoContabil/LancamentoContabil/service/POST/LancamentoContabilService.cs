using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.service
{
    public class LancamentoContabilService : BaseService<LancamentoContabilOutput, LancamentoContabilError>
    {
        public const string ENDPOINT = "/supplieraccountservice/api/accounting-balances";
        public const string ENDPOINTGET = "/supplieraccountservice/api/accounting-balances/{id}";
        public LancamentoContabilService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<LancamentoContabilOutput, LancamentoContabilError> Execute(LancamentoContabilInput input)
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

using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountService_LancamentoContabil.LancamentoContabil.service
{
    public class LancamentoContabilService : BaseService<LancamentoContabilOutput, LancamentoContabilError>
    {
        public const string ENDPOINT = "/accountservice/api/accountingEntry";
        public const string ENDPOINTGET = "/taxservice/api/establishment/branch/{id}";
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

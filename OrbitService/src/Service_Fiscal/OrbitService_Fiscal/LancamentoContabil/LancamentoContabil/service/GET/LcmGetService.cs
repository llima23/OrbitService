using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_COL_Fiscal.LancamentoContabil.service.GET
{
    public class LcmGetService : BaseService<LcmGetOutput, LcmGetError>
    {
        public const string ENDPOINT = "/supplieraccountservice/api/accountingBalances/{id}";
        public LcmGetService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<LcmGetOutput, LcmGetError> Execute(string idLote)
        {
            return InvokeOperation(
                    GetBuilder()
                        .EndpointPath(Method.GET, ENDPOINT)
                        .SetPathParam("{id}", $@"{idLote}")
                        .CredentialsProvider(sConfig.CredentialsProvider)
                        .Serializer(new JsonRequestBodySerializer(true)),
                    new JsonResponseBodyDeserializer());

        }
    }
}

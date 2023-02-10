using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Fiscal.Pagamentos.Pagamentos.service
{
    public class PagamentosService : BaseService<PagamentosOutput, PagamentosErro>
    {
        public const string ENDPOINT = "/taxdocumentservice/api/Payment";
        public PagamentosService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<PagamentosOutput, PagamentosErro> Execute(PagamentosInput input)
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

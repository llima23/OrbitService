using OrbitLibrary.Common;

namespace AccountService_LancamentoContabil.LancamentoContabil.service.Establishments
{
    public class EstablishmentsService : BaseService<EstablishmentsOutput, LancamentoContabilError>
    {
        public const string ENDPOINTGET = "/taxservice/api/establishment/branch/{id}";
        public EstablishmentsService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }
        public OperationResponse<EstablishmentsOutput, LancamentoContabilError> GetEstabId(string branchId)
        {
            return InvokeOperation(
                 GetBuilder()
                     .EndpointPath(Method.GET, ENDPOINTGET)
                     .CredentialsProvider(sConfig.CredentialsProvider)
                     .SetPathParam("{id}", branchId)
                     .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                 new JsonResponseBodyDeserializer());
        }
    }
}

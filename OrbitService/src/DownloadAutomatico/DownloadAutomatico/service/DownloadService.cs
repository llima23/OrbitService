using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadAutomatico.service
{
    public class DownloadService : BaseService<object, object>
    {
        public const string ENDPOINT = "/documentservice/api/nfe/print/{id}";
        public DownloadService(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<object, object> Execute(string id)
        {
            return InvokeOperation(
                    GetBuilder()
                        .EndpointPath(Method.GET, ENDPOINT)
                        .AddHeader("Accept", "application/pdf")
                        .CredentialsProvider(sConfig.CredentialsProvider)
                        .SetPathParam("{id}", id)
                        .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                    new JsonResponseBodyDeserializer());
        }
    }
}

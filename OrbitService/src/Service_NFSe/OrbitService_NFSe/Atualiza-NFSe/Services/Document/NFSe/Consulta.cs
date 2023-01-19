using Newtonsoft.Json;
using OrbitLibrary.Common;
using OrbitLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static _4TAX_Service_Atualiza.Services.Document.Properties.Consulta;

namespace _4TAX_Service_Atualiza.Services.Document.NFSe
{

    public partial class Consulta : BaseService<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput>
    {
        public const string ENDPOINT = "/documentservice/api/nfse/consulta/{id}";

        public Consulta(ServiceConfiguration sConfig, CommunicationProvider client) : base(sConfig, client) { }

        public OperationResponse<ConsultaSuccessResponseOutput, ConsultaFailedResponseOutput> Execute(string Id, bool prod)
        {
            OperationRequestBuilder requestBuilder = GetBuilder()
                                                    .EndpointPath(Method.GET, ENDPOINT)
                                                    .CredentialsProvider(Defaults.GetCredentialsProvider(sConfig, Client))
                                                    .AddHeader("prod", prod.ToString())
                                                    .SetPathParam("{id}",Id)
                                                    .Serializer(new JsonRequestBodySerializer(removeNullFields: true));
            return InvokeOperation(requestBuilder, new JsonResponseBodyDeserializer());
        }
    }
}

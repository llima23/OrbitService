using OrbitLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace B1Library.DownloadXMLDanfe_NFSe.service
{
    public class DownloadAutomaticoXMLDANFENFSe : BaseService<object,object>
    {
        public const string ENDPOINT = "/documentservice/api/nfse/print/{id}";
        public const string ENDPOINTXML = "/documentservice/api/nfse/xml/emit/{id}";
        public DownloadAutomaticoXMLDANFENFSe(ServiceConfiguration sConfig, CommunicationProvider communicationClient) : base(sConfig, communicationClient)
        {

        }

        public OperationResponse<object, object> ExecutePDF(string id)
        {
            return InvokeOperation(
                    GetBuilder()
                        .EndpointPath(Method.GET, ENDPOINT)
                        .AddHeader("Accept", "text/plain")
                        .CredentialsProvider(sConfig.CredentialsProvider)
                        .SetPathParam("{id}", id)
                        .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                    new JsonResponseBodyDeserializer());
        }

        public OperationResponse<object, object> ExecuteXML(string id)
        {
            return InvokeOperation(
                    GetBuilder()
                        .EndpointPath(Method.GET, ENDPOINTXML)
                        .AddHeader("Accept", "application/xml")
                        .CredentialsProvider(sConfig.CredentialsProvider)
                        .SetPathParam("{id}", id)
                        .Serializer(new JsonRequestBodySerializer(removeNullFields: true)),
                    new JsonResponseBodyDeserializer());
        }
    }
}

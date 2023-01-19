using B1Library.Documents;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.mappers
{
    public class MapperInputNFeCancel
    {
        public OutboundDFeDocumentCancelInputNFe MapperInvoiceB1ToOutboundDFeDocumentCancelInputNFe(Invoice invoice)
        {
            OutboundDFeDocumentCancelInputNFe input = new OutboundDFeDocumentCancelInputNFe
            {
                nfeId = invoice.IdRetornoOrbit,
                xJust = invoice.Identificacao.Justificativa
            };
            return input;
        }

        public DocumentStatus ToDocumentStatusResponseSucessful(Invoice invoice, OutboundDFeDocumentCancelOutputNFe output)
        {
            DocumentStatus documentStatus = new DocumentStatus(invoice.IdRetornoOrbit, "", output.retEnvEvento.xMotivo, invoice.ObjetoB1, invoice.DocEntry, StatusCode.CanceladaSucess, output.retEnvEvento.retEvento[0].infEvento.chNFe, output.retEnvEvento.retEvento[0].infEvento.nProt, invoice.BaseEntry, output.communicationIds[0]);
            return documentStatus;
        }

        public DocumentStatus ToDocumentStatusResponseError(Invoice invoice, OutboundDFeDocumentCancelOutputNFe output)
        {
            DocumentStatus newStatusData = new DocumentStatus(Convert.ToString(invoice.IdRetornoOrbit), "", output.message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro, invoice.ChaveDeAcessoNFe, invoice.ProtocoloNFe, invoice.BaseEntry);
            return newStatusData;
        }
    }
}

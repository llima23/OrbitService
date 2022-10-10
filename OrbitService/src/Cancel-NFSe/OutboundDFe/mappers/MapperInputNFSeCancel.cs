using B1Library.Documents;
using Newtonsoft.Json;
using OrbitService_Cancel_NFSe.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OrbitService_Cancel_NFSe.OutboundDFe.mappers
{
    public class MapperInputNFSeCancel
    {

        public OutboundDFeDocumentCancelInputNFSe MapperInvoiceB1ToOutboundDFeDocumentCancelInputNFSe(Invoice invoice)
        {
            OutboundDFeDocumentCancelInputNFSe input = new OutboundDFeDocumentCancelInputNFSe
            {
                branchId = invoice.Identificacao.BranchId,
                nfseId = invoice.Identificacao.IdRetornoOrbit,
                motivo = invoice.Identificacao.Justificativa,
                soft_cancel = true
            };
            return input;
        }

        public DocumentStatus ToDocumentStatusResponseSucessful(Invoice invoice, OutboundDFeDocumentCancelOutputNFSe output)
        {
            foreach (var item in output.alerts)
            {
                output.message += item.description + " - " + "\r";
            }
            DocumentStatus documentStatus = new DocumentStatus(invoice.Identificacao.IdRetornoOrbit, Convert.ToString(output.success), output.message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.CanceladaSucess);
            return documentStatus;
        }

        public DocumentStatus ToDocumentStatusResponseError(Invoice invoice, OutboundDFeDocumentCancelOutputNFSe output)
        {
            string DescricaoErro = string.Empty;
            foreach (var item in output.errors)
            {
                DescricaoErro += item.description + " - " + "\r";
            }
            foreach (var item in output.alerts)
            {
                DescricaoErro += item.description + " - " + "\r";
            }
            DocumentStatus newStatusData = new DocumentStatus(Convert.ToString(invoice.Identificacao.IdRetornoOrbit), Convert.ToString(output.success), DescricaoErro, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
            return newStatusData;
        }
    }
}

using B1Library.Documents;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.mappers
{
    public class MapperInputNFeInutil
    {
        public OutboundDFeDocumentInutilInputNFe MapperInvoiceB1ToOrbitInput(Invoice invoice)
        {
            OutboundDFeDocumentInutilInputNFe input = new OutboundDFeDocumentInutilInputNFe
            {
                branchId = invoice.BranchId,
                versao = invoice.Identificacao.Versao,
                serie = invoice.Identificacao.SerieDocumento,
                nNfIni = invoice.Identificacao.NumeroDocumento,
                nNfFin = invoice.Identificacao.NumeroDocumento,
                xJust = invoice.Identificacao.Justificativa,
                ano = invoice.Identificacao.DataEmissao.ToString("yyyy")
            };
            return input;
        }

        public DocumentStatus MapperOrbitOutputToUpdateB1Sucess(Invoice invoice, OutboundDFeDocumentInutilOutputNFe output)
        {
            string communicationId = string.Empty;
            try
            {
                communicationId = output.communicationIds[0];
            }
            catch
            {
                communicationId = "";
            }
            return new DocumentStatus(invoice.IdRetornoOrbit, "", output.retInutNFe.infInut.xMotivo, invoice.ObjetoB1, invoice.DocEntry, StatusCode.InutilizadaSucess, invoice.ChaveDeAcessoNFe, output.retInutNFe.infInut.nProt, invoice.BaseEntry, communicationId);
        }

        public DocumentStatus MapperOrbitOutputToUpdateB1Error(Invoice invoice, OutboundDFeDocumentInutilOutputNFe output, string content)
        {
            return new DocumentStatus(invoice.IdRetornoOrbit, "", content.Replace("'",""), invoice.ObjetoB1, invoice.DocEntry, StatusCode.CancelEmProcess, invoice.ChaveDeAcessoNFe, invoice.ProtocoloNFe, invoice.BaseEntry);
        }
    }
}

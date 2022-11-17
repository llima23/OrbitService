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
                branchId = invoice.Identificacao.BranchId,
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
            return new DocumentStatus(invoice.IdRetornoOrbit, "", output.retInutNFe.infInut.xMotivo, invoice.ObjetoB1, invoice.DocEntry, StatusCode.InutilizadaSucess, invoice.ChaveDeAcessoNFe, invoice.ProtocoloNFe, invoice.BaseEntry, output.communicationIds[0]);
        }

        public DocumentStatus MapperOrbitOutputToUpdateB1Error(Invoice invoice, OutboundDFeDocumentInutilOutputNFe output)
        {
            return new DocumentStatus(invoice.IdRetornoOrbit, "", output.message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro, invoice.ChaveDeAcessoNFe, invoice.ProtocoloNFe, invoice.BaseEntry);
        }
    }
}

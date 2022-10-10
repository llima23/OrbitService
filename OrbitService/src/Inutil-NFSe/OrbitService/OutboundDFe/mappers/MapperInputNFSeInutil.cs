using B1Library.Documents;
using OrbitService.OutboundDFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundDFe.mappers
{
    public class MapperInputNFSeInutil
    {

        public OutboundDFeDocumentInutilInputNFSe MapperInvoiceB1ToOrbitInput(Invoice invoice)
        {
            OutboundDFeDocumentInutilInputNFSe input = new OutboundDFeDocumentInutilInputNFSe();
            input.branchId = invoice.Identificacao.BranchId;
            input.nfseId = invoice.Identificacao.IdRetornoOrbit;
            return input;
        }

        public DocumentStatus MapperOrbitOutputToUpdateB1Sucess(Invoice invoice, OutboundDFeDocumentInutilOutputNFSe output)
        {
            return new DocumentStatus(invoice.Identificacao.IdRetornoOrbit,Convert.ToString(output.success), output.message, invoice.ObjetoB1,invoice.DocEntry,StatusCode.InutilizadaSucess);
        }

        public DocumentStatus MapperOrbitOutputToUpdateB1Error(Invoice invoice, OutboundDFeDocumentInutilOutputNFSe output)
        {
            return new DocumentStatus(invoice.Identificacao.IdRetornoOrbit, Convert.ToString(output.success), output.message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
        }
    }
}

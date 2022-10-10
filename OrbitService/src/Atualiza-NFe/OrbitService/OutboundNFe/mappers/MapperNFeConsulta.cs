using B1Library.Documents;
using OrbitService.OutboundNFe.services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.OutboundNFe.mappers
{
    public class MapperNFeConsulta
    {
        public DocumentStatus ToDocumentStatusResponseSucessful(Invoice invoice, OutboundDFeDocumentConsultaOutputNFe output)
        {
            if(output.status.cStat == "100")
            {
                return new DocumentStatus(invoice.Identificacao.IdRetornoOrbit, output.status.cStat, output.status.mStat, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Sucess);
            }
            else
            {
                return new DocumentStatus(invoice.Identificacao.IdRetornoOrbit, output.status.cStat, output.status.mStat, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
            }
        }
        public DocumentStatus ToDocumentStatusResponseErro(Invoice invoice, OutboundDFeDocumentConsulErroNFe output)
        {
            foreach (var item in output.errors)
            {
                output.message += item.msg + " - " + "\r";
            }
            return new DocumentStatus(invoice.Identificacao.IdRetornoOrbit, "", output.message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
        }
    }
}

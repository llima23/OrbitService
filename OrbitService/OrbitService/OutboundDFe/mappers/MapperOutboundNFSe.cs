using B1Library.Documents;
using B1Library.Utilities;
using OrbitService.OutboundDFe.services.OutboundNFSeRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitService.OutboundDFe.mappers
{
    public class MapperOutboundNFSe
    {
        private Util util;
        public MapperOutboundNFSe()
        {
            util = new Util();
        }
        public OutboundNFSeDocumentRegisterInput ToinboundNFSeDocumentRegisterInput(Invoice invoice)
        {
            OutboundNFSeDocumentRegisterInput input = new OutboundNFSeDocumentRegisterInput();
            input.BranchId = invoice.Identificacao.BranchId;
            input.numeroLote = Convert.ToString(invoice.DocEntry);
            #region IDENTIFICACAO
            input.Rps.identificacao.cNf = Convert.ToString(invoice.DocEntry + 100);
            input.Rps.identificacao.serie = invoice.Identificacao.SerieDocumento;
            input.Rps.identificacao.numero = invoice.Identificacao.NumeroDocumento;
            input.Rps.identificacao.dataEmissao = util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataEmissao);
            input.Rps.identificacao.competencia = util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataEmissao);
            input.Rps.identificacao.indPres = 0;
            input.Rps.identificacao.naturezaOperacao = "1";
            input.Rps.identificacao.tipoRps = (invoice.Filial.CodigoIBGEMunicipioFilial == "3283" || invoice.Filial.CodigoIBGEMunicipioFilial == "2188") && invoice.Identificacao.TipoRps == "RPS" ? invoice.Identificacao.TipoRps : "1";
            input.Rps.identificacao.regimeEspecialTributacao = !String.IsNullOrEmpty(invoice.Identificacao.TipoTributacaoNFSe) ? invoice.Identificacao.TipoTributacaoNFSe.Split("-")[0].ToString().Trim() : null;
            input.Rps.identificacao.optanteSimplesNacional = invoice.Identificacao.RegEspTrib == "6" ? "S" : "N";



            #endregion IDENTIFICACAO
            return input;
        }
    }
}

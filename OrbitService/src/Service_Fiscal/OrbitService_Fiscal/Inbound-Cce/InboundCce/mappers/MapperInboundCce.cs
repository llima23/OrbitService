using B1Library.Documents;
using B1Library.Utilities;
using OrbitService.InboundCce.services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OrbitService.InboundCce.mappers
{
    public class MapperInboundCce
    {
        private Util util;
        public MapperInboundCce()
        {
            util = new Util();
        }
        public InboundCceInput ToInboundCceRegisterInput(Invoice invoice)
        {
            InboundCceInput input = new InboundCceInput();
            input.data.branchId = invoice.Identificacao.BranchId;
            #region IDENTIFICACAO
            input.data.identificacao.modeloDoc = invoice.ModeloDocumento;
            input.data.identificacao.serie = invoice.Identificacao.SerieDocumento;
            input.data.identificacao.numeroDoc = invoice.Identificacao.NumeroDocumento;
            input.data.identificacao.dataEmissao = invoice.Identificacao.DataEmissao.ToString("yyyy-MM-dd");
            input.data.identificacao.chvAcesso = Convert.ToInt64(invoice.ChaveDeAcessoNFe);
            #endregion IDENTIFICACAO
            #region EMITENTE
            input.data.emitente.razaoSocial = invoice.Parceiro.RazaoSocialParceiro;
            input.data.emitente.cnpj = !String.IsNullOrEmpty(invoice.Parceiro.CnpjParceiro) ? Regex.Replace(invoice.Parceiro.CnpjParceiro, @"\.|\/|-", "") : string.Empty;
            input.data.emitente.cpf = !String.IsNullOrEmpty(invoice.Parceiro.CpfParceiro) ? Regex.Replace(invoice.Parceiro.CpfParceiro, @"\.|\/|-", "") : string.Empty;
            input.data.emitente.inscricaoEstadual = !String.IsNullOrEmpty(invoice.Parceiro.InscIeParceiro) ? Regex.Replace(invoice.Parceiro.InscIeParceiro, @"\.|\/|-", "") : string.Empty;
            input.data.emitente.inscricaoMunicipal = !String.IsNullOrEmpty(invoice.Parceiro.InscMunParceiro) ? Regex.Replace(invoice.Parceiro.InscMunParceiro, @"\.|\/|-", "") : string.Empty;
            input.data.emitente.indIeEmitente = invoice.Parceiro.IndicadorIEParceiro;

            input.data.emitente.endereco.logradouro = invoice.Parceiro.LogradouroParceiro;
            input.data.emitente.endereco.numero = Convert.ToInt16(invoice.Parceiro.NumeroLogradouroParceiro);
            input.data.emitente.endereco.complemento = invoice.Parceiro.ComplementoParceiro;
            input.data.emitente.endereco.bairro = invoice.Parceiro.BairroParceiro;
            input.data.emitente.endereco.complemento = invoice.Parceiro.BairroParceiro;
            input.data.emitente.endereco.codigoMunicipio = Convert.ToInt32(invoice.Parceiro.CodigoIBGEMunicipioParceiro);
            input.data.emitente.endereco.municipio = invoice.Parceiro.MunicipioParceiro;
            input.data.emitente.endereco.uf = invoice.Parceiro.UFParceiro;
            input.data.emitente.endereco.cep = !String.IsNullOrEmpty(invoice.Parceiro.CEPParceiro) ? Regex.Replace(invoice.Parceiro.CEPParceiro, @"\.|\/|-", "") : string.Empty;
            input.data.emitente.endereco.codigoPais = Convert.ToInt16(invoice.Parceiro.CodigoPaisParceiro);
            input.data.emitente.endereco.pais = invoice.Parceiro.NomePaisParceiro;
            input.data.emitente.endereco.fone = !String.IsNullOrEmpty(invoice.Parceiro.FoneParceiro) ? Convert.ToInt64(Regex.Replace(invoice.Parceiro.FoneParceiro, @"\.|\/|-", "")) : 0;
            input.data.emitente.endereco.email = invoice.Parceiro.EmailParceiro;
            #endregion EMITENTE
            #region DESTINATARIO
            input.data.destinatario.razaoSocial = invoice.Filial.RazaoSocialFilial;
            input.data.destinatario.cnpj = !String.IsNullOrEmpty(invoice.Filial.CNPJFilial) ? Regex.Replace(invoice.Filial.CNPJFilial, @"\.|\/|-", "") : string.Empty;
            input.data.destinatario.cpf = !String.IsNullOrEmpty(invoice.Filial.CPFFilial) ? Regex.Replace(invoice.Filial.CPFFilial, @"\.|\/|-", "") : string.Empty;
            input.data.destinatario.inscricaoEstadual = !String.IsNullOrEmpty(invoice.Filial.InscIeFilial) ? Regex.Replace(invoice.Filial.InscIeFilial, @"\.|\/|-", "") : string.Empty;
            input.data.destinatario.indIeDestinatario = invoice.Filial.IndicadorIEFilial;

            input.data.destinatario.endereco.logradouro = invoice.Filial.LogradouroFilial;
            input.data.destinatario.endereco.numero = Convert.ToInt16(invoice.Filial.NumeroLogradouroFilial);
            input.data.destinatario.endereco.complemento = invoice.Filial.ComplementoFilial;
            input.data.destinatario.endereco.bairro = invoice.Filial.BairroFilial;
            input.data.destinatario.endereco.complemento = invoice.Filial.BairroFilial;
            input.data.destinatario.endereco.codigoMunicipio = Convert.ToInt32(invoice.Filial.CodigoIBGEMunicipioFilial);
            input.data.destinatario.endereco.municipio = invoice.Filial.MunicipioFilial;
            input.data.destinatario.endereco.uf = invoice.Filial.UFFilial;
            input.data.destinatario.endereco.cep = !String.IsNullOrEmpty(invoice.Filial.CEPFilial) ? Regex.Replace(invoice.Filial.CEPFilial, @"\.|\/|-", "") : string.Empty;
            input.data.destinatario.endereco.codigoPais = Convert.ToInt16(invoice.Filial.CodigoPaisFilial);
            input.data.destinatario.endereco.pais = invoice.Filial.NomePaisFilial;


            #endregion DESTINATARIO
            #region ITEM
            List<Item> listItemOrbit = new List<Item>();
            foreach (CabecalhoLinha itemB1 in invoice.CabecalhoLinha)
            {
                Item itemOrbit = new Item();
                itemOrbit.codigoItem = itemB1.CodigoItem;
                itemOrbit.descricaoItem = itemB1.DescricaoItemLinhaDocumento;
                itemOrbit.CFOP = itemB1.CodigoCFOP;
                itemOrbit.quantidade = itemB1.QuantidadeLinha;
                itemOrbit.valorItem = itemB1.ValorTotalLinnha;
                #region IMPOSTO
                foreach (ImpostoLinha imposto in itemB1.ImpostoLinha)
                {
                    switch (imposto.TipoImpostoOrbit)
                    {
                        case "-6":
                            itemOrbit.impostos.cstICMS = itemB1.CSTICMSLinha;
                            itemOrbit.impostos.vbcICMS = util.GetTaxTypeB1VBcSumForItem(itemB1.ImpostoLinha, imposto.TipoImpostoOrbit);
                            itemOrbit.impostos.vRedBcICMS = imposto.pRedBc > 0 ? itemOrbit.impostos.vbcICMS * imposto.pRedBc : 0;
                            itemOrbit.impostos.aliqICMS = imposto.PorcentagemImposto;
                            itemOrbit.impostos.valorICMS = util.GetTaxTypeB1VImpSumForItem(itemB1.ImpostoLinha, imposto.TipoImpostoOrbit);
                            break;
                        case "-8":
                            itemOrbit.impostos.cstPIS = itemB1.CSTPisLinha;
                            itemOrbit.impostos.vbcPIS = util.GetTaxTypeB1VBcSumForItem(itemB1.ImpostoLinha, imposto.TipoImpostoOrbit);
                            itemOrbit.impostos.aliqPIS = imposto.PorcentagemImposto;
                            itemOrbit.impostos.valorPIS = util.GetTaxTypeB1VImpSumForItem(itemB1.ImpostoLinha, imposto.TipoImpostoOrbit);
                            break;
                        case "-10":
                            itemOrbit.impostos.cstCOFINS = itemB1.CSTCofinsLinha;
                            itemOrbit.impostos.vbcCOFINS = util.GetTaxTypeB1VBcSumForItem(itemB1.ImpostoLinha, imposto.TipoImpostoOrbit);
                            itemOrbit.impostos.aliqCOFINS = imposto.PorcentagemImposto;
                            itemOrbit.impostos.valorCOFINS = util.GetTaxTypeB1VImpSumForItem(itemB1.ImpostoLinha, imposto.TipoImpostoOrbit);
                            break;
                    }
                }
                #endregion IMPOSTO
                listItemOrbit.Add(itemOrbit);
            }
            input.data.item = listItemOrbit;

            #endregion ITEM
            #region VALORES
            input.data.valores.valorDocumento = invoice.Identificacao.ValorTotalNF;
            input.data.valores.vbcICMS = util.GetTaxTypeB1VBcSum("-6", invoice);
            input.data.valores.valorICMS = util.GetTaxTypeB1Sum("-6", invoice);
            input.data.valores.vBcPIS = util.GetTaxTypeB1VBcSum("-8", invoice);
            input.data.valores.valorPIS = util.GetTaxTypeB1Sum("-8", invoice);
            input.data.valores.vbcCOFINS = util.GetTaxTypeB1VBcSum("-10", invoice);
            input.data.valores.valorCOFINS = util.GetTaxTypeB1Sum("-10", invoice);
            #endregion VALORES
            #region STATUS
            input.data.statusDoc.cStat = 0;
            input.data.statusDoc.mStat = "CCE EMITIDA";
            input.data.statusDoc.dataLancamento = invoice.Identificacao.DataLancamento.ToString("yyyy-MM-dd");
            #endregion STATUS

            return input;
        }

        internal DocumentStatus ToDocumentStatusResponseError(Invoice invoice, InboundCceError output)
        {
            DocumentStatus newStatusData = new DocumentStatus("", "", output.message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
            return newStatusData;
        }

        public DocumentStatus ToDocumentStatusResponseSucessful(Invoice invoice, InboundCceOutput output)
        {
            StatusCode status = StatusCode.CargaFiscal;
            DocumentStatus newStatusData = new DocumentStatus(output.data.document_id, "", "", invoice.ObjetoB1, invoice.DocEntry, status);
            return newStatusData;
        }


    }
}

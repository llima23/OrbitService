using B1Library.Documents;
using B1Library.Utilities;
using OrbitService.InboundOtherDocuments.services;
using OrbitService.InboundOtherDocuments.services.Error;
using OrbitService.InboundOtherDocuments.services.Input;
using OrbitService.InboundOtherDocuments.services.Output;
using System;
using System.Collections.Generic;

namespace OrbitService.InboundOtherDocuments.mappers
{
    public class Mapper
    {
        private Util util;
        public Mapper()
        {
            util = new Util();
        }

        public OtherDocumentRegisterInput ToOtherDocumentRegisterInput(Invoice invoice)
        {
            //TODO: Necessita realizar mapeamento dos campos comentados na classe
            //TODO: Campos que possuem conversão, devem ser tratados na lib 
            OtherDocumentRegisterInput input = Factory.CreateOtherDocumentRegisterInputInstance();

            #region DATA
            input.BranchId = invoice.Identificacao.BranchId;
            input.CreatedAt = invoice.Identificacao.DataEmissao;
            input.UpdatedAt = invoice.Identificacao.DataLancamento;
            
            #endregion DATA
            #region IDENTIFICACAO
            input.Identificacao.DataEmissao = invoice.Identificacao.DataEmissao.ToString("yyyy-MM-dd");
            input.Identificacao.DataLancamento = invoice.Identificacao.DataLancamento.ToString("yyyy-MM-dd");
            input.Identificacao.NumeroDocFiscal = invoice.Identificacao.NumeroDocumento.ToString();
            input.Identificacao.Modelo = invoice.ModeloDocumento;
            input.Identificacao.Serie = invoice.Identificacao.SerieDocumento;
            input.Identificacao.Finalidade = invoice.Identificacao.FinalideDocumento;
            
            #endregion IDENTIFICACAO
            #region EMITENTE
            input.Emitente.RazaoSocial = invoice.Filial.RazaoSocialFilial;
            input.Emitente.Cnpj = invoice.Filial.CNPJFilial;
            input.Emitente.InscricaoEstadual = invoice.Filial.InscIeFilial;
            input.Emitente.InscricaoMunicipal = invoice.Filial.MunicipioFilial;
            input.Emitente.Endereco.Logradouro = invoice.Filial.LogradouroFilial;
            input.Emitente.Endereco.Numero = invoice.Filial.NumeroLogradouroFilial;
            input.Emitente.Endereco.Bairro = invoice.Filial.BairroFilial;
            input.Emitente.Endereco.Uf = invoice.Filial.UFFilial;
            input.Emitente.Endereco.Cep = invoice.Filial.CEPFilial;

            #endregion EMITENTE
            #region DESTINATARIO
            input.Destinatario.RazaoSocial = invoice.Parceiro.RazaoSocialParceiro;
            input.Destinatario.Cnpj = invoice.Parceiro.CnpjParceiro;
            input.Destinatario.Cpf = invoice.Parceiro.CpfParceiro;
            input.Destinatario.InscricaoEstadual = invoice.Parceiro.InscIeParceiro;
            input.Destinatario.InscricaoMunicipal = invoice.Parceiro.InscMunParceiro;
            input.Destinatario.Endereco.Logradouro = invoice.Parceiro.LogradouroParceiro;
            input.Destinatario.Endereco.Numero = invoice.Parceiro.NumeroLogradouroParceiro;
            input.Destinatario.Endereco.Complemento = invoice.Parceiro.ComplementoParceiro;
            input.Destinatario.Endereco.Bairro = invoice.Parceiro.BairroParceiro;
            input.Destinatario.Endereco.CodigoMunicipio = invoice.Parceiro.MunicipioParceiro;
            input.Destinatario.Endereco.Uf = invoice.Parceiro.UFParceiro;
            input.Destinatario.Endereco.Cep = invoice.Parceiro.CEPParceiro;

            #endregion DESTINATARIO
            #region VALORES
            input.Valores.Iss = util.GetTaxTypeB1Sum("-7",invoice);
            input.Valores.Pis = util.GetTaxTypeB1Sum("-8",invoice);
            input.Valores.Cofins = util.GetTaxTypeB1Sum("-10",invoice);
            #region VALORES IMPOSTOS RETIDOS
            input.Valores.Csll = util.GetTaxTypeB1Sum("CSLL", invoice);
            input.Valores.Inss = util.GetTaxTypeB1Sum("INSS", invoice);
            input.Valores.Ir = util.GetTaxTypeB1Sum("IR", invoice);
            input.Valores.OutrasRetencoes = util.GetTaxTypeB1Sum("OUTRASRETENCOES", invoice);
            #endregion VALORES IMPOSTOS RETIDOS
            input.Valores.ValorLiquido = util.GetVProdSum(invoice.CabecalhoLinha);
            input.Valores.ValorBruto = util.GetVProdSum(invoice.CabecalhoLinha);
            #endregion VALORES

            #region ITEM
            input.Itens = ReturnListItemInboundOtherDocuments(invoice);
            #endregion ITEM

            #region STATUS
            input.Status.CStat = "100"; //TODO: MAPEAR
            input.Status.MStat = "AUTORIZADA"; //TODO: MAPEAR 
            #endregion STATUS


            return input;
        }

        private List<Iten> ReturnListItemInboundOtherDocuments(Invoice invoice)
        {
            List<Iten> lstIten = new List<Iten>();
            foreach (var item in invoice.CabecalhoLinha)
            {
                Iten iten = new Iten();
                iten.CodigoItem = item.CodigoItem;
                iten.DescricaoItem = item.DescricaoItemLinhaDocumento;
                iten.Cfop = item.CodigoCFOP;
                iten.UnidadeMedida = item.UnidadeComercial;
                iten.Quantidade = util.ToOrbitString(item.QuantidadeLinha);
                iten.ValorItem = util.ToOrbitString(item.ValorUnitarioLinha);
                iten.Impostos = ReturnListImposto(item);
                lstIten.Add(iten);

            }
            return lstIten;
        }

        public Impostos ReturnListImposto(CabecalhoLinha cabecalhoLinha)
        {
            Impostos imposto = new Impostos();

            foreach (var item in cabecalhoLinha.ImpostoLinha)
            {
                if (!String.IsNullOrEmpty(item.TipoImpostoOrbit))
                {
                    switch (item.TipoImpostoOrbit)
                    {
                        case "-10":
                            imposto.AliqCOFINS = util.ToOrbitString(item.PorcentagemImposto);
                            imposto.ValorCOFINS = util.ToOrbitString(item.ValorImposto);
                            imposto.VBcCOFINS = util.ToOrbitString(item.ValorBaseImposto);
                            imposto.CstCOFINS = cabecalhoLinha.CSTCofinsLinha;
                            break;
                        case "-8":
                            imposto.AliqPIS = util.ToOrbitString(item.PorcentagemImposto);
                            imposto.ValorPIS = util.ToOrbitString(item.ValorImposto);
                            imposto.VBcPIS = util.ToOrbitString(item.ValorBaseImposto);
                            imposto.CstPIS = cabecalhoLinha.CSTPisLinha;
                            break;

                    }
                }
            }

            return imposto;
        }

        internal DocumentStatus ToDocumentStatusResponseError(Invoice invoice, string message)
        {
            DocumentStatus newStatusData = new DocumentStatus("", "", message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
            return newStatusData;
        }

        public DocumentStatus ToDocumentStatusResponseSucessful(Invoice invoice, OtherDocumentRegisterOutput output)
        {
            StatusCode status = StatusCode.CargaFiscal;
            DocumentStatus newStatusData = new DocumentStatus(output.data.document_id, "", "", invoice.ObjetoB1, invoice.DocEntry, status);
            return newStatusData;
        }
    }
}

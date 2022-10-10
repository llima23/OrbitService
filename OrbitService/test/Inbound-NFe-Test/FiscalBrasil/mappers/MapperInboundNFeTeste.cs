using B1Library.Documents;
using B1Library.Utilities;
using Newtonsoft.Json;
using OrbitService.FiscalBrazil.mappers;
using OrbitService.FiscalBrazil.services.InboundNFeRegister;
using OrbitService_Test.TestUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OrbitService_Test.FiscalBrasil.mappers
{
    public class MapperInboundNFeTeste
    {
        private MapperInboundNFe cut;
        private Invoice invoice;
        private Util util;
        public MapperInboundNFeTeste()
        {
            cut = new MapperInboundNFe();
            invoice = InvoiceB1FakeGenerator.GetFakeInvoiceB1();
            util = new Util();
        }

        [Fact]
        public void ShouldConvertInvoiceB1ToInboundNFeInput()
        {
            InboundNFeDocumentRegisterInput input = new InboundNFeDocumentRegisterInput();
            input = cut.ToinboundNFeDocumentRegisterInput(invoice);
            var jsonInput = JsonConvert.SerializeObject(input, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});

            #region HEADER
            Assert.Equal(invoice.Identificacao.BranchId, input.BranchId);
            Assert.Equal(invoice.TipoDocumento, input.tipoOperacao);
            Assert.Equal(invoice.Identificacao.Versao, input.Versao);
            Assert.Equal(util.ToOrbitString(invoice.Identificacao.DadosCobranca), input.dadosCobranca);
            Assert.Equal(invoice.TipoDocumento, input.tipoOperacao);
            Assert.Equal(invoice.Identificacao.Key, input.Key);
            #endregion HEADER
            #region IDENTIFICACAO 
            Assert.Equal(invoice.Filial.CodigoUFFilial, input.identificacao.CodigoUf);
            Assert.Equal(invoice.Identificacao.NaturezaOperacaoDocumento, input.identificacao.NaturezaOperacao);
            Assert.Equal(invoice.Identificacao.SerieDocumento, input.identificacao.Serie);
            Assert.Equal(invoice.Identificacao.NumeroDocumento, input.identificacao.NumeroDocFiscal);
            Assert.Equal(util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataEmissao, invoice.Identificacao.DocTime), input.identificacao.DataHoraEmissao);
            Assert.Equal(invoice.Filial.CodigoIBGEMunicipioFilial.ToString(), input.identificacao.CodigoMunicipioFg);
            Assert.Equal(invoice.Identificacao.OperacaoNFe, input.identificacao.FormatoNfe);
            Assert.Equal(invoice.Identificacao.FinalideDocumento, input.identificacao.Finalidade);
            Assert.Equal(invoice.Identificacao.ConsumidorFinal, input.identificacao.IndFinal);
            Assert.Equal(invoice.Identificacao.IndicadorPresenca, input.identificacao.IndPres);
            Assert.Equal(invoice.Identificacao.IndicadorIntermediario, input.identificacao.IndIntermed);
            Assert.Equal(invoice.TipoNF, input.identificacao.tpNf.ToString());
            #endregion IDENTIFICACAO 
            #region Parceiro
            Assert.Equal(invoice.Filial.CNPJFilial, input.Destinatario.Cnpj);
            Assert.Equal(invoice.Filial.RazaoSocialFilial, input.Destinatario.Nome);
            Assert.Equal(invoice.Filial.LogradouroFilial, input.Destinatario.Endereco.Logradouro);
            Assert.Equal(invoice.Filial.NumeroLogradouroFilial, input.Destinatario.Endereco.Numero);
            Assert.Equal(invoice.Filial.BairroFilial, input.Destinatario.Endereco.Bairro);
            Assert.Equal(invoice.Filial.CodigoIBGEMunicipioFilial, input.Destinatario.Endereco.CodigoMunicipio);
            Assert.Equal(invoice.Filial.UFFilial, input.Destinatario.Endereco.Uf);
            Assert.Equal(invoice.Filial.CEPFilial, input.Destinatario.Endereco.Cep);
            Assert.Equal(invoice.Filial.CodigoPaisFilial, input.Destinatario.Endereco.CodigoPais);
            Assert.Equal(invoice.Filial.InscIeFilial, input.Destinatario.Ie);
            #endregion Parceiro
            #region TOTAL
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VBcSum("-6",invoice)), input.total.IcmsTot.VBc);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-6",invoice)), input.total.IcmsTot.VIcms);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-12",invoice)), input.total.IcmsTot.VIcmsDeson);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-3",invoice)), input.total.IcmsTot.VFcp);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VBcSum("-5",invoice)), input.total.IcmsTot.VBcSt);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-5",invoice)), input.total.IcmsTot.VSt);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-14",invoice)), input.total.IcmsTot.VFcpSt);
            Assert.Equal(util.ToOrbitString(util.GetVProdSum(invoice.CabecalhoLinha)), input.total.IcmsTot.VProd);
            Assert.Equal(util.ToOrbitString(util.GetVSumDespAdic("1",invoice)), input.total.IcmsTot.VFrete);
            Assert.Equal(util.ToOrbitString(util.GetVSumDespAdic("2",invoice)), input.total.IcmsTot.VSeg);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-11",invoice)), input.total.IcmsTot.VIi);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-4",invoice)), input.total.IcmsTot.VIpi);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-8",invoice)), input.total.IcmsTot.VPis);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-10",invoice)), input.total.IcmsTot.VCofins);
            Assert.Equal(util.ToOrbitString(util.GetVSumDespAdic("3",invoice)), input.total.IcmsTot.VOutro);
            Assert.Equal(util.ToOrbitString(invoice.Identificacao.ValorTotalNF), input.total.IcmsTot.VNf);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-9",invoice)), input.total.IcmsTot.VIcmsUfDest);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-13",invoice)), input.total.IcmsTot.VIcmsUfRemet);
            #endregion TOTAL
            #region DET
            input.det = cut.ReturnListDetInboundNFe(invoice);
            Assert.NotNull(input.det);
            Assert.True(input.det.Count > 0);
            #endregion DET
            #region EMAIL
            Assert.Equal(invoice.Parceiro.EmailParceiro,input.Emails[0]);
            #endregion EMAIL
            #region TRANSP
            Assert.Equal(invoice.Parceiro.ModalidadeFrete, input.transp.ModFrete);
            #endregion TRANSP
            //TODO: COBR
            #region PAG
            Assert.Equal(invoice.Identificacao.CondicaoDePagamentoDocumento, input.pag.DetPag[0].IndPag);
            Assert.Equal(invoice.Identificacao.FormaDePagamentoDocumento, input.pag.DetPag[0].TPag);
            Assert.Equal(util.ToOrbitString(invoice.Identificacao.ValorTotalNF), input.pag.DetPag[0].VPag);
            #endregion PAG
            #region STATUS
            Assert.Equal("100",input.status.CStat);
            Assert.Equal("AUTORIZADA", input.status.MStat);
            #endregion STATUS
            #region EVENTOS
            Assert.Equal("EMISSÃO", input.eventos[0].Type);
            Assert.Equal(util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataEmissao, invoice.Identificacao.DocTime), input.eventos[0].DhEvento);
            #endregion EVENTOS
            #region Filial
            Assert.Equal(invoice.Parceiro.CnpjParceiro, input.Emitente.Cnpj);
            Assert.Equal(invoice.Parceiro.RazaoSocialParceiro, input.Emitente.Name);
            Assert.Equal(invoice.Parceiro.LogradouroParceiro, input.Emitente.Endereco.Logradouro);
            Assert.Equal(invoice.Parceiro.NumeroLogradouroParceiro, input.Emitente.Endereco.Numero);
            Assert.Equal(invoice.Parceiro.BairroParceiro, input.Emitente.Endereco.Bairro);
            Assert.Equal(invoice.Parceiro.CodigoIBGEMunicipioParceiro, input.Emitente.Endereco.CodigoMunicipio);
            Assert.Equal(invoice.Parceiro.UFParceiro, input.Emitente.Endereco.Uf);
            Assert.Equal(invoice.Parceiro.CEPParceiro, input.Emitente.Endereco.Cep);
            Assert.Equal(invoice.Parceiro.CodigoPaisParceiro, input.Emitente.Endereco.CodigoPais);            
            #endregion Filial


            Assert.NotNull(input);
        }

        [Fact]
        public void ShouldReturnListDetInboundNFe()
        {
            List<Det> lstDet = cut.ReturnListDetInboundNFe(invoice);
            foreach (var item in lstDet)
            {
                foreach (var linha in invoice.CabecalhoLinha.Where(x => x.NItem.ToString() == item.NItem))
                {
                    Assert.Equal(linha.NItem.ToString(), item.NItem);
                    Assert.Equal(linha.CodigoItem, item.prod.Codigo);
                    if (String.IsNullOrEmpty(linha.CodigoDeBarras))
                    {
                        Assert.Equal("SEM GTIN", item.prod.Cean);
                        Assert.Equal("SEM GTIN", item.prod.CeanTrib);
                    }
                    else
                    {
                        Assert.Equal(linha.CodigoDeBarras, item.prod.Cean);
                        Assert.Equal(linha.CodigoDeBarras, item.prod.CeanTrib);
                    }

                    Assert.Equal(linha.DescricaoItemLinhaDocumento, item.prod.Descricao);
                    Assert.Equal(linha.CodigoNCM, item.prod.Ncm);
                    Assert.Equal(linha.CodigoCFOP, item.prod.CodigoFiscalOperacoes);
                    Assert.Equal(linha.UnidadeComercial, item.prod.UnidadeComercial);
                    Assert.Equal(util.ToOrbitString(linha.QuantidadeLinha), item.prod.QuantidadeComercial);
                    Assert.Equal(util.ToOrbitString(linha.ValorUnitarioLinha), item.prod.ValorUnitarioComercializacao);
                    Assert.Equal(util.ToOrbitString(linha.ValorTotalLinnha), item.prod.ValorTotalBruto);
                    Assert.Equal(linha.UnidadeComercial, item.prod.UnidadeTributavel);
                    Assert.Equal(util.ToOrbitString(linha.QuantidadeLinha), item.prod.QuantidadeTributavel);
                    Assert.Equal(cut.ReturnValorUnitarioDespesaAdicional(linha), item.prod.ValorFrete);
                    item.Imposto = cut.ReturnListImposto(linha);
                    Assert.NotNull(item.Imposto);                                  
                }
            }

        }

    }
}

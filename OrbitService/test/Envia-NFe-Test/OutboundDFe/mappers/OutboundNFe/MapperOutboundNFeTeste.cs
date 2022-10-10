using B1Library.Documents;
using B1Library.Utilities;
using Newtonsoft.Json;
using OrbitService.OutboundDFe.mappers;
using OrbitService.OutboundDFe.services.OutboundDFeRegister;
using OrbitService_Test.TestUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OrbitService_Test.OutboundDFe.mappers.OutboundNFe
{
    public class MapperOutboundNFeTeste
    {
        private MapperOutboundNFe cut;
        private Invoice invoice;
        private Util util;

        public MapperOutboundNFeTeste()
        {
            cut = new MapperOutboundNFe();
            invoice = InvoiceB1FakeGenerator.GetFakeInvoiceB1();
            util = new Util();
        }

        [Fact]
        public void ShouldConvertInvoiceB1ToInboundNFeInput()
        {
            OutboundNFeDocumentRegisterInput input = new OutboundNFeDocumentRegisterInput();
            input = cut.ToinboundNFeDocumentRegisterInput(invoice);
            var jsonInput = JsonConvert.SerializeObject(input, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

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
            Assert.Equal(invoice.Parceiro.CnpjParceiro, input.Destinatario.Cnpj);
            Assert.Equal(invoice.Parceiro.RazaoSocialParceiro, input.Destinatario.Nome);
            Assert.Equal(invoice.Parceiro.LogradouroParceiro, input.Destinatario.Endereco.Logradouro);
            Assert.Equal(invoice.Parceiro.NumeroLogradouroParceiro, input.Destinatario.Endereco.Numero);
            Assert.Equal(invoice.Parceiro.BairroParceiro, input.Destinatario.Endereco.Bairro);
            Assert.Equal(invoice.Parceiro.CodigoIBGEMunicipioParceiro, input.Destinatario.Endereco.CodigoMunicipio);
            Assert.Equal(invoice.Parceiro.UFParceiro, input.Destinatario.Endereco.Uf);
            Assert.Equal(invoice.Parceiro.CEPParceiro, input.Destinatario.Endereco.Cep);
            Assert.Equal(invoice.Parceiro.CodigoPaisParceiro, input.Destinatario.Endereco.CodigoPais);
            Assert.Equal(invoice.Parceiro.InscIeParceiro, input.Destinatario.Ie);
            #endregion Parceiro
            #region DET
            input.det = cut.ReturnListDetInboundNFe(invoice);
            Assert.NotNull(input.det);
            Assert.True(input.det.Count > 0);
            #endregion DET
            #region EMAIL
            Assert.Equal(invoice.Parceiro.EmailParceiro, input.Emails[0]);
            #endregion EMAIL
            #region TRANSP
            Assert.Equal(invoice.Parceiro.ModalidadeFrete, input.transp.ModFrete);
            #endregion TRANSP            
            #region COBR
            #region FATURA
            Assert.Equal(invoice.Identificacao.NumeroDocumento, input.cobr.Fatura.Numero);
            Assert.Equal(util.ToOrbitString(invoice.Identificacao.ValorTotalNF), input.cobr.Fatura.ValorOriginal);
            Assert.Equal(util.ToOrbitString(util.GetValorSomaDescontoItens(invoice.CabecalhoLinha)), input.cobr.Fatura.ValorDesconto);
            Assert.Equal(util.ToOrbitString(invoice.Identificacao.ValorTotalNF - util.GetValorSomaDescontoItens(invoice.CabecalhoLinha)), input.cobr.Fatura.ValorLiquido);
            #endregion FATURA
            #region DUPLICATA
            foreach (var duplicata in invoice.Duplicata)
            {
                Assert.Equal(Convert.ToString(duplicata.NumeroDuplicata).PadLeft(3, '0'), input.cobr.Duplicata[0].Numero);
                Assert.Equal(util.ConvertDateB1ToFormatOrbit(duplicata.DataVencimento), input.cobr.Duplicata[0].DataVencimento);
                Assert.Equal(util.ToOrbitString(duplicata.ValorDuplicata), input.cobr.Duplicata[0].Valor);
            }
            #endregion DUPLICATA
            #endregion COBR
            #region PAG
            Assert.Equal(invoice.Identificacao.CondicaoDePagamentoDocumento, input.pag.DetPag[0].IndPag);
            Assert.Equal(invoice.Identificacao.FormaDePagamentoDocumento, input.pag.DetPag[0].TPag);
            Assert.Equal(util.ToOrbitString(invoice.Identificacao.ValorTotalNF), input.pag.DetPag[0].VPag);
            #endregion PAG      
            #region TOTAL
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VBcSum("-6", invoice)), input.total.IcmsTot.VBc);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-6", invoice)), input.total.IcmsTot.VIcms);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1SumDeson("-6", invoice)), input.total.IcmsTot.VIcmsDeson);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VBcSum("-5", invoice)), input.total.IcmsTot.VBcSt);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-5", invoice)), input.total.IcmsTot.VSt);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-14", invoice)), input.total.IcmsTot.VFcpSt);
            Assert.Equal(util.ToOrbitString(util.GetVProdSum(invoice.CabecalhoLinha)), input.total.IcmsTot.VProd);
            Assert.Equal(util.ToOrbitString(util.GetVSumDespAdic("1", invoice)), input.total.IcmsTot.VFrete);
            Assert.Equal(util.ToOrbitString(util.GetVSumDespAdic("2", invoice)), input.total.IcmsTot.VSeg);
            Assert.Equal(util.ToOrbitString(util.GetValorSomaDescontoItens(invoice.CabecalhoLinha)), input.total.IcmsTot.VDesc);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-11", invoice)), input.total.IcmsTot.VIi);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-4", invoice)), input.total.IcmsTot.VIpi);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-8", invoice)), input.total.IcmsTot.VPis);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-10", invoice)), input.total.IcmsTot.VCofins);
            Assert.Equal(util.ToOrbitString(util.GetVSumDespAdic("3", invoice)), input.total.IcmsTot.VOutro);
            Assert.Equal(util.ToOrbitString(invoice.Identificacao.ValorTotalNF), input.total.IcmsTot.VNf);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-9", invoice)), input.total.IcmsTot.VIcmsUfDest);
            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-13", invoice)), input.total.IcmsTot.VIcmsUfRemet);
            foreach (Det det in input.det)
            {
                if (string.IsNullOrEmpty(det.Imposto.IcmsUfDest.VFcpUfDest))
                {
                    Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-3", invoice)), input.total.IcmsTot.VFcp);
                    Assert.Equal("0.00", input.total.IcmsTot.VFcpUfDest);
                }
                else
                {
                    Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1Sum("-3", invoice)), input.total.IcmsTot.VFcpUfDest);
                    Assert.Equal("0.00", input.total.IcmsTot.VFcp);
                }
            }
            #endregion TOTAL
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
                    Assert.Equal(!String.IsNullOrEmpty(cut.ReturnValorUnitarioDespesaAdicional(linha, "1")) ? cut.ReturnValorUnitarioDespesaAdicional(linha, "1") : "0.00", item.prod.ValorFrete);
                    Assert.Equal(!String.IsNullOrEmpty(cut.ReturnValorUnitarioDespesaAdicional(linha, "2")) ? cut.ReturnValorUnitarioDespesaAdicional(linha, "2") : null, item.prod.ValorSeguro);
                    Assert.Equal(!String.IsNullOrEmpty(cut.ReturnValorUnitarioDespesaAdicional(linha, "3")) ? cut.ReturnValorUnitarioDespesaAdicional(linha, "3") : null, item.prod.VOutro);
                    item.Imposto = cut.ReturnListImposto(linha);
                    foreach (var impostoLinha in linha.ImpostoLinha)
                    {
                        if (impostoLinha.TipoImpostoOrbit == "-6")
                        {
                            if (impostoLinha.SimOuNaoDesoneracao == "Y")
                            {
                                Assert.Equal(linha.MotivoDesoneracao, item.Imposto.Icms.MotDeson);
                                Assert.Equal(util.ToOrbitString(impostoLinha.ValorImposto), item.Imposto.Icms.VDeson);
                            }
                        }

                        if (impostoLinha.TipoImpostoOrbit == "-5")
                        {
                            Assert.Equal("4", item.Imposto.Icms.ModBcSt);
                            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(linha.ImpostoLinha, impostoLinha.TipoImpostoOrbit)), item.Imposto.Icms.VBcSt);
                            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(linha.ImpostoLinha, impostoLinha.TipoImpostoOrbit)), item.Imposto.Icms.VImpSt);
                            Assert.Equal(util.ToOrbitString(impostoLinha.PorcentagemImposto), item.Imposto.Icms.PImpSt);
                            Assert.Equal(util.ToOrbitString(impostoLinha.MVast), item.Imposto.Icms.PMvast);
                            if (string.IsNullOrEmpty(item.Imposto.Icms.VDeson))
                            {
                                item.Imposto.Icms.ModBc = "3";
                            }

                        }
                        if (impostoLinha.TipoImpostoOrbit == "-9")
                        {

                            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(linha.ImpostoLinha, impostoLinha.TipoImpostoOrbit)), item.Imposto.IcmsUfDest.VBcUfDest);
                            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(linha.ImpostoLinha, impostoLinha.TipoImpostoOrbit)), item.Imposto.IcmsUfDest.VBcFcpUfDest);
                            Assert.Equal(util.ToOrbitString(impostoLinha.AliquotaIntDestino), item.Imposto.IcmsUfDest.PIcmsUfDest);
                            Assert.Equal(util.ToOrbitString(impostoLinha.PorcentagemImposto), item.Imposto.IcmsUfDest.PIcmsInter);
                            Assert.Equal(util.ToOrbitString(impostoLinha.PartilhaInterestadual), item.Imposto.IcmsUfDest.PIcmsInterPart);
                            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(linha.ImpostoLinha, impostoLinha.TipoImpostoOrbit)), item.Imposto.IcmsUfDest.VIcmsUfDest);

                        }
                        if (impostoLinha.TipoImpostoOrbit == "-3")
                        {
                            Assert.Equal(util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(linha.ImpostoLinha, impostoLinha.TipoImpostoOrbit)), item.Imposto.IcmsUfDest.VFcpUfDest);
                            Assert.Equal(util.ToOrbitString(impostoLinha.PorcentagemImposto), item.Imposto.IcmsUfDest.PFcpUfDest);

                        }
                        if (impostoLinha.TipoImpostoOrbit == "-13")
                        {
                            Assert.Equal(util.ToOrbitString(impostoLinha.PorcentagemImposto), item.Imposto.IcmsUfDest.VIcmsUfRemet);
                        }
                    }
                    Assert.NotNull(item.Imposto);
                }
            }

        }


        [Fact]
        public void ShouldToDocumentStatusResponseWithAListError()
        {
            string DescricaoErro = string.Empty;
            OutboundNFeDocumentRegisterError output = new OutboundNFeDocumentRegisterError();
            List<Error> listaErro = new List<Error>();
            Error erro = new Error();
            erro.value = "0";
            erro.msg = "Erro";
            erro.param = "identificacao.finalidade";
            erro.location = "body";
            listaErro.Add(erro);

            erro = new Error();
            erro.msg = "Erro2";
            erro.param = "transp.modFrete";
            erro.location = "body";
            listaErro.Add(erro);

            output.errors = listaErro;
            output.message = "badRequest";

            output.nfeId = "633f0cfc5f3810098c73b663";
            foreach (Error item in output.errors)
            {
                DescricaoErro += item.param + " - " + item.msg + "\r";
            }

            DocumentStatus newStatusData = cut.ToDocumentStatusResponseError(invoice, output);
            Assert.Equal(output.nfeId, newStatusData.IdOrbit);
            Assert.Equal(output.message, newStatusData.StatusOrbit);
            Assert.Equal(DescricaoErro, newStatusData.Descricao);
            Assert.Equal(invoice.ObjetoB1, newStatusData.ObjetoB1);
            Assert.Equal(invoice.DocEntry, newStatusData.DocEntry);
            Assert.Equal(StatusCode.Erro, newStatusData.Status);

        }

        [Fact]
        public void ShouldToDocumentStatusResponseSucessful()
        {
            OutboundNFeDocumentRegisterOutput output = new OutboundNFeDocumentRegisterOutput();
            output.message = "NFe inserida na fila de emissão";
            output.nfeId = "633f0cfc5f3810098c73b663";
            DocumentStatus newStatusData = cut.ToDocumentStatusResponseSucessful(invoice, output);
            Assert.Equal(output.nfeId, newStatusData.IdOrbit);
            Assert.Equal(newStatusData.GetStatusMessage(), output.message);
            Assert.Equal(invoice.ObjetoB1, newStatusData.ObjetoB1);
            Assert.Equal(invoice.DocEntry, newStatusData.DocEntry);
            Assert.Equal(StatusCode.FilaDeEmissao, newStatusData.Status);
        }
    }
}

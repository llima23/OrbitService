using B1Library.Documents;
using B1Library.Utilities;
using OrbitService.OutboundDFe.services.OutboundDFeRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OrbitService.OutboundDFe.mappers
{
    public class MapperOutboundNFe
    {
        private Util util;
        public MapperOutboundNFe()
        {
            util = new Util();
        }
        public OutboundNFeDocumentRegisterInput ToinboundNFeDocumentRegisterInput(Invoice invoice)
        {

            // Categoria do imposto    Orbit - Tipo de Imposto
            //ICMS2                     -6            
            //ICMS Dif                  -6                        
            //ICMS - ST                 -5
            //IPI                       -4                         
            //PIS                       -8                        
            //COFINS                    -10
            //II                        -11
            //ISS                       -7
            //FCP                       -3
            //ICMSDest                  -9
            //ICMSRemet                 -13
            //ICMSDeson                 -12
            //FCP - ST                  -14
            //FRETE                     1

            OutboundNFeDocumentRegisterInput input = FactoryOutboundNFeRegister.CreateOutboundNFeDocumentRegisterInputInstance();
            #region HEADER           
            input.BranchId = invoice.Identificacao.BranchId;
            input.tipoOperacao = invoice.TipoDocumento;
            input.Versao = invoice.Identificacao.Versao;
            input.dadosCobranca = util.ToOrbitString(invoice.Identificacao.DadosCobranca);
            input.Key = !string.IsNullOrEmpty(invoice.Identificacao.Key) ? invoice.Identificacao.Key : null;
            #endregion HEADER
            #region IDENTIFICACAO
            input.identificacao.CodigoUf = invoice.Filial.CodigoUFFilial;
            input.identificacao.NaturezaOperacao = invoice.Identificacao.NaturezaOperacaoDocumento;
            input.identificacao.Serie = invoice.Identificacao.SerieDocumento;
            input.identificacao.NumeroDocFiscal = invoice.Identificacao.NumeroDocumento;
            input.identificacao.DataHoraEmissao = util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataEmissao, invoice.Identificacao.DocTime);
            if(invoice.Identificacao.EnviaDataHora == "1")
            {
                input.identificacao.DataHoraSaidaOuEntrada = !string.IsNullOrEmpty(invoice.Identificacao.HoraDeEnvio) ? invoice.Identificacao.DataDeEnvio.ToString("yyyy-MM-dd") + "T" + invoice.Identificacao.HoraDeEnvio + DateTime.Now.ToString(":ss") + "-03:00" : input.identificacao.DataHoraEmissao;
            }
            input.identificacao.CodigoMunicipioFg = invoice.Filial.CodigoIBGEMunicipioFilial;
            input.identificacao.FormatoNfe = invoice.Identificacao.OperacaoNFe;
            if(invoice.Identificacao.OperacaoNFe == "6")
            {
                input.identificacao.DhCont = input.identificacao.DataHoraEmissao;
                input.identificacao.XJust = invoice.Identificacao.JustContigencia;
            }
            input.identificacao.Finalidade = invoice.Identificacao.FinalideDocumento;
            input.identificacao.IndFinal = invoice.Identificacao.ConsumidorFinal;
            input.identificacao.IndPres = invoice.Identificacao.IndicadorPresenca;
            input.identificacao.IndIntermed = invoice.Identificacao.IndicadorIntermediario;
            input.identificacao.CodigoNf = Convert.ToString(invoice.DocEntry);
            input.identificacao.tpNf = invoice.TipoNF;
            List<NFref> listNFref = new List<NFref>();
            foreach (var item in invoice.DocRef)
            {
                NFref nFref = new NFref
                {
                    RefNFe = item.ChaveDocRef
                };
                listNFref.Add(nFref);
            }
            input.identificacao.NFref = listNFref;


            #endregion #region IDENTIFICACAO
            #region DESTINATARIO
            input.Destinatario.Cnpj = !String.IsNullOrEmpty(invoice.Parceiro.CnpjParceiro) ? Regex.Replace(invoice.Parceiro.CnpjParceiro, @"\.|\/|-", "") : null;
            input.Destinatario.Cpf = !String.IsNullOrEmpty(invoice.Parceiro.CpfParceiro) ? Regex.Replace(invoice.Parceiro.CpfParceiro, @"\.|\/|-", "") : null;
            input.Destinatario.Isuf = !String.IsNullOrEmpty(invoice.Parceiro.InscSuframa) ? Regex.Replace(invoice.Parceiro.InscSuframa, @"\.|\/|-", "") : null;
            input.Destinatario.IdEstrangeiro = !String.IsNullOrEmpty(invoice.Parceiro.IdEstrangeiro) ? Regex.Replace(invoice.Parceiro.IdEstrangeiro, @"\.|\/|-", "") : null;
            input.Destinatario.Nome = invoice.Parceiro.RazaoSocialParceiro;
            input.Destinatario.Endereco.Logradouro = invoice.Parceiro.LogradouroParceiro;
            input.Destinatario.Endereco.Numero = invoice.Parceiro.NumeroLogradouroParceiro;
            input.Destinatario.Endereco.Bairro = invoice.Parceiro.BairroParceiro;
            input.Destinatario.Endereco.CodigoMunicipio = invoice.Parceiro.CodigoIBGEMunicipioParceiro;
            input.Destinatario.Endereco.Uf = invoice.Parceiro.UFParceiro;
            input.Destinatario.Endereco.Cep = !String.IsNullOrEmpty(invoice.Parceiro.CEPParceiro) ? Regex.Replace(invoice.Parceiro.CEPParceiro, @"(\.)|-", "") : null;
            input.Destinatario.Endereco.CodigoPais = invoice.Parceiro.CodigoPaisParceiro;
            input.Destinatario.Endereco.Ie = !String.IsNullOrEmpty(invoice.Parceiro.InscIeParceiro) ? Regex.Replace(invoice.Parceiro.InscIeParceiro, @"(\.)|-", "") : null;
            input.Destinatario.Ie = !String.IsNullOrEmpty(invoice.Parceiro.InscIeParceiro) ? Regex.Replace(invoice.Parceiro.InscIeParceiro, @"(\.)|-", "") : null;
            input.Destinatario.IndIeDestinatario = invoice.Parceiro.IndicadorIEParceiro;
            #endregion DESTINATARIO            

            #region DET
            input.det = ReturnListDetInboundNFe(invoice);
            #endregion DET
            #region EMAIL
            List<string> lstEmail = new List<string>();
            lstEmail.Add(invoice.Parceiro.EmailParceiro);
            input.Emails = lstEmail;
            #endregion EMAIL
            #region TRANSP
            input.transp.ModFrete = invoice.Parceiro.ModalidadeFrete;
            #endregion TRANSP
            #region COBR
            #region FATURA
            input.cobr.Fatura.Numero = invoice.Identificacao.NumeroDocumento;
            input.cobr.Fatura.ValorOriginal = util.ToOrbitString(invoice.Identificacao.ValorTotalNF);
            input.cobr.Fatura.ValorDesconto = "0.00";
            input.cobr.Fatura.ValorLiquido = util.ToOrbitString(invoice.Identificacao.ValorTotalNF);
            #endregion FATURA
            #region DUPLICATA
            List <Duplicatum> lstDuplicata = new List<Duplicatum>();
            foreach (var item in invoice.Duplicata)
            {
                if(item.ValorDuplicata > 0)
                {
                    Duplicatum duplicata = new Duplicatum();
                    duplicata.Numero = Convert.ToString(item.NumeroDuplicata).PadLeft(3, '0');
                    duplicata.DataVencimento = util.ConvertDateB1ToFormatOrbit(item.DataVencimento);
                    duplicata.Valor = util.ToOrbitString(item.ValorDuplicata);
                    lstDuplicata.Add(duplicata);
                }
            }
            input.cobr.Duplicata = lstDuplicata;
            #endregion DUPLICATA
            #endregion COBR
            #region PAG                
            List<DetPag> lstDetPag = new List<DetPag>();
            DetPag detPag = new DetPag();
            detPag.IndPag = !string.IsNullOrEmpty(invoice.Identificacao.CondicaoDePagamentoDocumento) ? invoice.Identificacao.CondicaoDePagamentoDocumento : null;
            detPag.TPag = invoice.Identificacao.FormaDePagamentoDocumento;
            detPag.VPag = invoice.Identificacao.FormaDePagamentoDocumento != "90" ? util.ToOrbitString(invoice.Identificacao.ValorTotalNF) : "0.00";
            lstDetPag.Add(detPag);
            input.pag.DetPag = lstDetPag;
            #endregion PAG           
            #region TOTAL
            input.total.IcmsTot.VBc = util.ToOrbitString(util.GetTaxTypeB1VBcSum("-6", invoice));
            input.total.IcmsTot.VIcms = util.ToOrbitString(util.GetTaxTypeB1Sum("-6", invoice));
            input.total.IcmsTot.VIcmsDeson = util.ToOrbitString(util.GetTaxTypeB1SumDeson("-6", invoice));
            input.total.IcmsTot.VBcSt = util.ToOrbitString(util.GetTaxTypeB1VBcSum("-5", invoice));
            input.total.IcmsTot.VSt = util.ToOrbitString(util.GetTaxTypeB1Sum("-5", invoice));
            input.total.IcmsTot.VFcpSt = util.ToOrbitString(util.GetTaxTypeB1Sum("-14", invoice));
            input.total.IcmsTot.VFcpStRet = "0.00"; //TODO
            input.total.IcmsTot.VProd = invoice.CabecalhoLinha[0].SoImposto != "Y" ? util.ToOrbitString(util.GetVProdSum(invoice.CabecalhoLinha)) : "0.00";
            input.total.IcmsTot.VFrete = util.ToOrbitString(util.GetVSumDespAdic("1", invoice));
            input.total.IcmsTot.VSeg = util.ToOrbitString(util.GetVSumDespAdic("2", invoice));
            input.total.IcmsTot.VDesc = util.ToOrbitString(util.GetValorSomaDescontoItens(invoice.CabecalhoLinha));
            input.total.IcmsTot.VIi = util.ToOrbitString(util.GetTaxTypeB1Sum("-11", invoice));
            input.total.IcmsTot.VIpi = util.ToOrbitString(util.GetTaxTypeB1Sum("-4", invoice));
            input.total.IcmsTot.VIpiDevol = "0.00"; //TODO
            input.total.IcmsTot.VPis = util.ToOrbitString(util.GetTaxTypeB1Sum("-8", invoice));
            input.total.IcmsTot.VCofins = util.ToOrbitString(util.GetTaxTypeB1Sum("-10", invoice));
            input.total.IcmsTot.VOutro = util.ToOrbitString(util.GetVSumDespAdic("3", invoice));
            input.total.IcmsTot.VIcmsUfDest = util.ToOrbitString(util.GetTaxTypeB1Sum("-9", invoice));
            input.total.IcmsTot.VIcmsUfRemet = util.ToOrbitString(util.GetTaxTypeB1Sum("-13", invoice));

            input.total.IcmsTot.VNf = util.ToOrbitString(invoice.Identificacao.ValorTotalNF);




            foreach (Det det in input.det)
            {
                if (string.IsNullOrEmpty(det.Imposto.IcmsUfDest.VFcpUfDest)) 
                {
                    input.total.IcmsTot.VFcp = util.ToOrbitString(util.GetTaxTypeB1Sum("-3", invoice));
                    input.total.IcmsTot.VFcpUfDest = "0.00";
                }
                else
                {
                    input.total.IcmsTot.VFcpUfDest = util.ToOrbitString(util.GetTaxTypeB1Sum("-3", invoice));
                    input.total.IcmsTot.VFcp = "0.00";
                }
            }



            #endregion TOTAL                              

            input.exporta.UfSaidaPais = !String.IsNullOrEmpty(invoice.Identificacao.UFDeExportacao) ? invoice.Identificacao.UFDeExportacao : null;
            input.exporta.XLocExporta = !String.IsNullOrEmpty(invoice.Identificacao.LocalDeExportacao) ? invoice.Identificacao.UFDeExportacao : null;
            input.infAdic.InfAdFisco = !string.IsNullOrEmpty(invoice.Identificacao.InfAdFisco) ? invoice.Identificacao.InfAdFisco : null;
            input.identificacao.IdLocalDestino = invoice.CabecalhoLinha[0].IdLocalDestino;
            input.Emitente.InscricaoEstadual = !String.IsNullOrEmpty(invoice.Filial.InscIeFilial) ? Regex.Replace(invoice.Filial.InscIeFilial, @"(\.)|-", "") : null;

            return input;
        }

        public DocumentStatus ToDocumentStatusResponseError(Invoice invoice, dynamic output)
        {
            string DescricaoErro = string.Empty;
            try
            {
                foreach (var item in output.errors)
                {
                    DescricaoErro += item.param + " - " + item.msg + "\r";
                }
            }
            catch
            {
                DescricaoErro = output.message;
            }
            DocumentStatus newStatusData = new DocumentStatus(Convert.ToString(output.nfeId), Convert.ToString(output.message).Replace("'",""), DescricaoErro.Replace("'", ""), invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
            return newStatusData;
        }

        public DocumentStatus ToDocumentStatusResponseSucessful(Invoice invoice, OutboundNFeDocumentRegisterOutput output)
        {
            DocumentStatus newStatusData = new DocumentStatus(output.nfeId, "", output.message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.FilaDeEmissao);
            return newStatusData;
        }

        public List<Det> ReturnListDetInboundNFe(Invoice invoice)
        {
            List<Det> lstDet = new List<Det>();
            foreach (var item in invoice.CabecalhoLinha)
            {
                Det det = new Det();
                det.NItem = item.NItem;
                det.prod.Codigo = item.CodigoItem;
                if (String.IsNullOrEmpty(item.CodigoDeBarras))
                {
                    det.prod.Cean = "SEM GTIN";
                    det.prod.CeanTrib = "SEM GTIN";
                }
                else
                {
                    det.prod.Cean = item.CodigoDeBarras;
                    det.prod.CeanTrib = item.CodigoDeBarras;
                }
                det.prod.Descricao = item.DescricaoItemLinhaDocumento;
                det.prod.Ncm = Regex.Replace(item.CodigoNCM, @"(\.)", "");
                det.prod.CodigoFiscalOperacoes = item.CodigoCFOP;
                det.prod.Cest = !String.IsNullOrEmpty(item.CodigoCEST) ? item.CodigoCEST : null;
                det.prod.UnidadeComercial = item.UnidadeComercial;
                det.prod.QuantidadeComercial = util.ToOrbitString(item.QuantidadeLinha);
                det.prod.ValorUnitarioComercializacao = util.ToOrbitString(item.ValorUnitarioLinha);
                det.prod.ValorTotalBruto = item.SoImposto != "Y" ? util.ToOrbitString(item.ValorTotalLinnha) : "0.00";
                det.prod.UnidadeTributavel = item.UnidadeComercial.ToString();
                det.prod.QuantidadeTributavel = util.ToOrbitString(item.QuantidadeLinha);
                det.prod.ValorUnitarioTributacao = util.ToOrbitString(item.ValorUnitarioLinha);
                det.prod.ValorFrete = !String.IsNullOrEmpty(ReturnValorUnitarioDespesaAdicional(item, "1")) ? ReturnValorUnitarioDespesaAdicional(item, "1") : "0.00";
                det.prod.ValorSeguro = !String.IsNullOrEmpty(ReturnValorUnitarioDespesaAdicional(item, "2")) ? ReturnValorUnitarioDespesaAdicional(item, "2") : null;
                det.prod.VOutro = !String.IsNullOrEmpty(ReturnValorUnitarioDespesaAdicional(item, "3")) ? ReturnValorUnitarioDespesaAdicional(item, "3") : null;
                det.prod.IndTot = "1"; //TODO
                det.prod.ValorDesconto = util.ToOrbitString(item.ValorTotalDescontoLinha);
                det.prod.XPed = !String.IsNullOrEmpty(item.NumeroPedido) ? item.NumeroPedido : null;
                det.prod.NItemPed = !String.IsNullOrEmpty(item.NumeroItemPedido) ? item.NumeroItemPedido : null;
                det.Imposto = ReturnListImposto(item);
                det.prod.Di = ReturnListDi(item,invoice);
                lstDet.Add(det);

            }
            return lstDet;

        }

        public List<Di> ReturnListDi(CabecalhoLinha cabecalhoLinha, Invoice invoice)
        {
            List<Di> listDi = new List<Di>();
            List<Adi> listAdi = new List<Adi>();
            Adi adi = new Adi();
            Di di = new Di();
            foreach (var item in cabecalhoLinha.DadosDI)
            {
                adi.NAdicao = item.NumeroAdicao;
                adi.NSeqAdic = item.NumeroSequenciaAdicao;
                adi.CFabricante = invoice.Parceiro.CodigoParceiro;
                adi.VDescDI = util.ToOrbitString(item.ValorDescontoDI);
                listAdi.Add(adi);

                di.NumeroDocumentoImportacao = item.NumeroDocImportacao;
                di.Ddi = util.ConvertDateB1ToFormatOrbit(item.Ddi);
                di.LocalDesembaracoAduaneiro = item.LocalDesembaracoAduaneiro;
                di.UfDesembaracoAduaneiro = item.UFDesembaracoAduaneiro;
                di.DataDesembaracoAduaneiro = util.ConvertDateB1ToFormatOrbit(item.DataDesembaracoAduaneiro);
                di.ViaTransporteInternacional = item.ViaTransporteInternacional.Substring(0, 1);
                di.Vafrmm = !String.IsNullOrEmpty(item.ValorFrmm) ? item.ValorFrmm : null;
                di.TpIntermedio = item.TpIntermedio.Substring(0, 1);
                di.CnpjAdiquirente = !String.IsNullOrEmpty(item.CNPJAdiquirente) ? Regex.Replace(item.CNPJAdiquirente, @"\.|\/|-", "") : null;
                di.UfAdiquirente = item.UFAdiquirente;
                di.CodigoExportador = invoice.Parceiro.CodigoParceiro;
                di.Adi = listAdi; 
                listDi.Add(di);
            }

            return listDi;
        }

        public Imposto ReturnListImposto(CabecalhoLinha cabecalhoLinha)
        {
            Imposto imposto = new Imposto();

            foreach (var item in cabecalhoLinha.ImpostoLinha)
            {
                if (!String.IsNullOrEmpty(item.TipoImpostoOrbit))
                {
                    switch (item.TipoImpostoOrbit)
                    {
                        case "-14":
                            imposto.Icms.PFcpSt = util.ToOrbitString(item.PorcentagemImposto);
                            imposto.Icms.VFcpSt = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Icms.VBcFcpSt = util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            break;
                        case "-10":
                            imposto.Cofins.PImp = util.ToOrbitString(item.PorcentagemImposto);
                            imposto.Cofins.VImp = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Cofins.VBc = util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Cofins.Cst = cabecalhoLinha.CSTCofinsLinha;
                            break;
                        case "-8":
                            imposto.Pis.PPis = util.ToOrbitString(item.PorcentagemImposto);
                            imposto.Pis.VPis = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Pis.VBc = util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Pis.Cst = cabecalhoLinha.CSTPisLinha;
                            break;
                        case "-6":
                            imposto.Icms.Orig = cabecalhoLinha.OrigICMS;
                            imposto.Icms.Cst = cabecalhoLinha.CSTICMSLinha;
                            imposto.Icms.ModBc = "3"; //MAPEAR
                            imposto.Icms.PRedBc = item.pRedBc > 0 ? util.ToOrbitString(item.pRedBc) : null;
                            imposto.Icms.VBc = util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Icms.PImp = util.ToOrbitString(item.PorcentagemImposto); //TODO:
                            imposto.Icms.VImp = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            if (item.SimOuNaoDesoneracao == "Y")
                            {
                                imposto.Icms.VDeson = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                                imposto.Icms.MotDeson = cabecalhoLinha.MotivoDesoneracao;
                                imposto.Icms.VBc = null;
                                imposto.Icms.PImp = null;
                                imposto.Icms.VImp = null;
                            }
                            if(item.VDif > 0)
                            {
                                imposto.Icms.VIcmsDif = util.ToOrbitString(item.VDif);
                                imposto.Icms.PDif = util.ToOrbitString(item.PDif);
                                imposto.Icms.VIcmsOp = util.ToOrbitString(item.VICMSOp);
                            }
                            break;
                        case "-4":
                            imposto.Ipi.CEnq = cabecalhoLinha.cEnq;
                            imposto.Ipi.Cst = cabecalhoLinha.CSTIPILinha;
                            imposto.Ipi.VBc = util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Ipi.PImp = util.ToOrbitString(item.PorcentagemImposto); //TODO:
                            imposto.Ipi.VImp = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            break;
                        case "-5":                  
                            imposto.Icms.ModBcSt = "4"; //MAPEAR
                            imposto.Icms.VBcSt = util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Icms.PImpSt = util.ToOrbitString(item.PorcentagemImposto); //TODO:
                            imposto.Icms.VImpSt = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Icms.PMvast = util.ToOrbitString(item.MVast);
                            if (string.IsNullOrEmpty(imposto.Icms.VDeson))
                            {
                                imposto.Icms.ModBc = "3";
                            }
                            break;
                        case "-9":
                            imposto.IcmsUfDest.VBcUfDest = util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.IcmsUfDest.VBcFcpUfDest = util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.IcmsUfDest.PIcmsUfDest = util.ToOrbitString(item.AliquotaIntDestino);
                            imposto.IcmsUfDest.PIcmsInter = util.ToOrbitString(item.PorcentagemImposto);
                            imposto.IcmsUfDest.PIcmsInterPart = util.ToOrbitString(item.PartilhaInterestadual);
                            imposto.IcmsUfDest.VIcmsUfDest = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            break;
                        case "-3":
                            imposto.IcmsUfDest.PFcpUfDest = util.ToOrbitString(item.PorcentagemImposto);
                            imposto.IcmsUfDest.VFcpUfDest = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            break;
                        case "-13":
                            imposto.IcmsUfDest.VIcmsUfRemet = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            break;
                        case "-11":
                            imposto.Ii.VBc = util.ToOrbitString(util.GetTaxTypeB1VBcSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Ii.VDespAdu = "0.00";
                            imposto.Ii.VIi = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                            imposto.Ii.VIof = "0.00";
                            break;

                    }
                }
            }

            return imposto;
        }

        public string ReturnValorUnitarioDespesaAdicional(CabecalhoLinha cabecalhoLinha, string TipoDespesa)
        {
            double valorUnitarioDespesa = 0.00;
            foreach (var DespAdc in cabecalhoLinha.DespesaAdicional.Where(i => i.TipoDespesa == TipoDespesa))
            {
                switch (DespAdc.TipoDespesa)
                {
                    case "1":
                        valorUnitarioDespesa += DespAdc.ValorUnitarioDespesa;
                        break;
                    case "2":
                        valorUnitarioDespesa += DespAdc.ValorUnitarioDespesa;
                        break;
                    case "3":
                        valorUnitarioDespesa += DespAdc.ValorUnitarioDespesa;
                        break;
                }
            }

            return valorUnitarioDespesa > 0 ? util.ToOrbitString(valorUnitarioDespesa) : null;
        }
    }
}

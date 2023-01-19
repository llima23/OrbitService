using B1Library.Documents;
using B1Library.Utilities;
using OrbitService.InboundNFe.services;
using OrbitService.InboundNFe.services.InboundNFeRegister;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static OrbitService.InboundNFe.services.InboundNFeRegister.InboundNFeDocumentRegisterOutput;

namespace OrbitService.InboundNFe.mappers
{
    public class MapperInboundNFe
    {
        private Util util;
        public MapperInboundNFe()
        {
            util = new Util();
        }
        public InboundNFeDocumentRegisterInput ToinboundNFeDocumentRegisterInput(Invoice invoice)
        {
            
            // Categoria do imposto    Orbit - Tipo de Imposto
            //ICMS2                      -6            
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

            InboundNFeDocumentRegisterInput input = FactoryInboundNFeRegister.CreateInboundNFeDocumentRegisterInputInstance();
            #region HEADER           
            input.BranchId = invoice.Identificacao.BranchId;
            input.tipoOperacao = invoice.TipoDocumento;
            input.Versao = invoice.Identificacao.Versao;
            input.dadosCobranca = util.ToOrbitString(invoice.Identificacao.DadosCobranca);
            input.Key = invoice.Identificacao.Key;
            #endregion HEADER
            #region IDENTIFICACAO
            input.identificacao.CodigoUf = invoice.Filial.CodigoUFFilial;
            input.identificacao.NaturezaOperacao = invoice.Identificacao.NaturezaOperacaoDocumento;
            input.identificacao.Serie = invoice.Identificacao.SerieDocumento;
            input.identificacao.NumeroDocFiscal = invoice.Identificacao.NumeroDocumento;
            input.identificacao.DataHoraEmissao = util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataEmissao, invoice.Identificacao.DocTime);
            input.identificacao.DataHoraSaidaOuEntrada = util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataEmissao, invoice.Identificacao.DocTime);
            input.identificacao.CodigoMunicipioFg = invoice.Filial.CodigoIBGEMunicipioFilial;
            input.identificacao.FormatoNfe = invoice.Identificacao.OperacaoNFe;
            input.identificacao.Finalidade = invoice.Identificacao.FinalideDocumento;
            input.identificacao.IndFinal = invoice.Identificacao.ConsumidorFinal;
            input.identificacao.IndPres = invoice.Identificacao.IndicadorPresenca;
            input.identificacao.IndIntermed = invoice.Identificacao.IndicadorIntermediario;
            input.identificacao.CodigoNf = invoice.Identificacao.NumeroDocumento;
            input.identificacao.tpNf = invoice.TipoNF;

            #endregion #region IDENTIFICACAO
            #region Parceiro
            input.Destinatario.Cnpj = !String.IsNullOrEmpty(invoice.Filial.CNPJFilial) ? Regex.Replace(invoice.Filial.CNPJFilial, @"\.|\/|-", "") : string.Empty;
            input.Destinatario.Nome = invoice.Filial.RazaoSocialFilial;
            input.Destinatario.Endereco.Logradouro = invoice.Filial.LogradouroFilial;
            input.Destinatario.Endereco.Numero = invoice.Filial.NumeroLogradouroFilial;
            input.Destinatario.Endereco.Bairro = invoice.Filial.BairroFilial;
            input.Destinatario.Endereco.CodigoMunicipio = invoice.Filial.CodigoIBGEMunicipioFilial;
            input.Destinatario.Endereco.Uf = invoice.Filial.UFFilial;
            input.Destinatario.Endereco.Cep = !String.IsNullOrEmpty(invoice.Filial.CEPFilial) ? Regex.Replace(invoice.Filial.CEPFilial, @"(\.)|-", "") : string.Empty;
            input.Destinatario.Endereco.CodigoPais = invoice.Filial.CodigoPaisFilial;
            input.Destinatario.Ie = invoice.Filial.InscIeFilial;
            input.Destinatario.IndIeDestinatario = invoice.Filial.IndicadorIEFilial;
            #endregion Parceiro
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
            input.cobr.Fatura.Numero = invoice.Identificacao.NumeroDocumento;
            input.cobr.Fatura.ValorOriginal = util.ToOrbitString(invoice.Identificacao.ValorTotalNF);
            input.cobr.Fatura.ValorLiquido = util.ToOrbitString(invoice.Identificacao.ValorTotalNF);
            #endregion COBR
            #region PAG                
            List <DetPag> lstDetPag = new List<DetPag>();
            DetPag detPag = new DetPag();
            detPag.IndPag = invoice.Identificacao.CondicaoDePagamentoDocumento;
            detPag.TPag = invoice.Identificacao.FormaDePagamentoDocumento;
            detPag.VPag = util.ToOrbitString(invoice.Identificacao.ValorTotalNF);
            lstDetPag.Add(detPag);
            input.pag.DetPag = lstDetPag;
            #endregion PAG
            #region STATUS
            input.status.CStat = "100"; //TODO: MAPEAR
            input.status.MStat = "AUTORIZADA"; //TODO: MAPEAR 
            #endregion STATUS
            #region EVENTOS
            List<Evento> lstEventos = new List<Evento>();
            Evento evento = new Evento();
            evento.Type = "EMISSÃO"; //TODO: MAPEAR
            evento.Protocolo = invoice.Identificacao.NumeroProtocolo;
            evento.DhEvento = util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataEmissao, invoice.Identificacao.DocTime);
            lstEventos.Add(evento);
            input.eventos = lstEventos;
            #endregion EVENTOS
            #region Filial
            input.Emitente.Endereco.Logradouro = invoice.Parceiro.LogradouroParceiro;
            input.Emitente.Endereco.Numero = invoice.Parceiro.NumeroLogradouroParceiro;
            input.Emitente.Endereco.Bairro = invoice.Parceiro.BairroParceiro;
            input.Emitente.Endereco.CodigoMunicipio = invoice.Parceiro.CodigoIBGEMunicipioParceiro;
            input.Emitente.Endereco.Municipio = "SÃO PAULO";
            input.Emitente.Endereco.Uf = invoice.Parceiro.UFParceiro;
            input.Emitente.Endereco.Cep = !String.IsNullOrEmpty(invoice.Parceiro.CEPParceiro) ? Regex.Replace(invoice.Parceiro.CEPParceiro, @"(\.)|-", "") : string.Empty;
            input.Emitente.Endereco.CodigoPais = invoice.Parceiro.CodigoPaisParceiro;
            input.Emitente.Endereco.Fone = invoice.Parceiro.FoneParceiro;
            input.Emitente.Name = invoice.Parceiro.RazaoSocialParceiro;
            input.Emitente.NomeFantasia = invoice.Parceiro.RazaoSocialParceiro;
            input.Emitente.InscricaoEstadual = invoice.Parceiro.InscIeParceiro;
            input.Emitente.Cnpj = !String.IsNullOrEmpty(invoice.Parceiro.CnpjParceiro) ? Regex.Replace(invoice.Parceiro.CnpjParceiro, @"\.|\/|-", "") : string.Empty;
            #endregion Filial
            input.identificacao.IdLocalDestino = invoice.CabecalhoLinha[0].IdLocalDestino;
            input.Emitente.CodigoRegimeTributario = "1";

            return input;
        }

        internal DocumentStatus ToDocumentStatusResponseError(Invoice invoice, InboundNFeDocumentRegisterError output)
        {
            DocumentStatus newStatusData = new DocumentStatus("", "", output.Message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
            return newStatusData;
        }

        public DocumentStatus ToDocumentStatusResponseSucessful(Invoice invoice, InboundNFeDocumentRegisterOutput output)
        {
            StatusCode status = StatusCode.CargaFiscal;
            if(output.data.status == "Erro")
            {
               status = StatusCode.Erro;
            }
            DocumentStatus newStatusData = new DocumentStatus(output.data._id, output.data.status,output.data.description,invoice.ObjetoB1,invoice.DocEntry, status, invoice.Identificacao.Key);
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
                det.prod.UnidadeComercial = item.UnidadeComercial;
                det.prod.QuantidadeComercial = util.ToOrbitString(item.QuantidadeLinha);
                det.prod.ValorUnitarioComercializacao = util.ToOrbitString(item.ValorUnitarioLinha);
                det.prod.ValorTotalBruto = util.ToOrbitString(item.ValorTotalLinnha);
                det.prod.UnidadeTributavel = item.UnidadeComercial.ToString();
                det.prod.QuantidadeTributavel = util.ToOrbitString(item.QuantidadeLinha);
                det.prod.ValorUnitarioTributacao = util.ToOrbitString(item.ValorUnitarioLinha);
                det.prod.ValorFrete = ReturnValorUnitarioDespesaAdicional(item);
                det.prod.IndTot = "1";
                det.Imposto = ReturnListImposto(item);
                lstDet.Add(det);

            }
            return lstDet;

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
                            if (item.SimOuNaoDesoneracao == "Y" || item.ValorIcmsDesonerado > 0)
                            {
                                imposto.Icms.VDeson = util.ToOrbitString(util.GetTaxTypeB1VImpSumForItem(cabecalhoLinha.ImpostoLinha, item.TipoImpostoOrbit));
                                imposto.Icms.MotDeson = cabecalhoLinha.MotivoDesoneracao;
                                imposto.Icms.VBc = null;
                                imposto.Icms.PImp = null;
                                imposto.Icms.VImp = null;
                            }
                            if (item.VDif > 0)
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

        public string ReturnValorUnitarioDespesaAdicional(CabecalhoLinha cabecalhoLinha)
        {
            string valorUnitarioDespesa = "0.00";
            foreach (var DespAdc in cabecalhoLinha.DespesaAdicional)
            {
                switch (DespAdc.TipoDespesa)
                {
                    case "1":
                        valorUnitarioDespesa = util.ToOrbitString(DespAdc.ValorUnitarioDespesa);
                        break;
                }
            }

            return valorUnitarioDespesa;
        }

    }
}

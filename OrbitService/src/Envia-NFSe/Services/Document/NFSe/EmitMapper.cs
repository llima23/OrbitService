using _4TAX_Service.Application;
using _4TAX_Service.Common.Domain;
using _4TAX_Service.Common.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using static _4TAX_Service.Services.Document.Properties.Emit;

namespace _4TAX_Service.Services.Document.NFSe
{
    public class EmitMapper
    {
        public EmitRequestInput ConvertToOrbitObject(NFSeB1Object b1Document)
        {
            try
            {

                EmitRequestInput requestInput = new EmitRequestInput();

                requestInput.branchId = b1Document.BranchId;
                requestInput.numeroLote = b1Document.DocEntry.ToString();
                requestInput.erpId = b1Document.DocEntry.ToString();
                #region IDENTIFICAÇÃO

                requestInput.rps.identificacao.cNf = (b1Document.DocEntry + 100).ToString();
                requestInput.rps.identificacao.serie = b1Document.SeriesStr;
                requestInput.rps.identificacao.numero = b1Document.Serial;
                requestInput.rps.identificacao.tpOperacao = b1Document.U_TAX4_TpOpNFSe != "0" ? b1Document.U_TAX4_TpOpNFSe : null;
                requestInput.rps.identificacao.dataEmissao = Convert.ToDateTime(b1Document.DocDate);
                requestInput.rps.identificacao.competencia = Convert.ToDateTime(b1Document.DocDate);
                requestInput.rps.identificacao.indPres = 0; // TODO: Implementação Addon 
                //requestInput.rps.identificacao.naturezaOperacao = b1Document.U_TAX4_NatOpNFSe != "0" ? b1Document.U_TAX4_NatOpNFSe :null;
                requestInput.rps.identificacao.tipoRps = DocumentServiceFunctions.setTipoRps(b1Document.U_TAX4_tipoRPS, b1Document.County);

                requestInput.rps.identificacao.regimeEspecialTributacao = String.IsNullOrEmpty(b1Document.U_TAX4_tpTribNfse) ? null : b1Document.U_TAX4_tpTribNfse.Substring(0, 1);

                if (b1Document.U_TAX4_RegEspTrib == "6")
                {
                    requestInput.rps.identificacao.optanteSimplesNacional = "1";
                }
                else
                {
                    requestInput.rps.identificacao.optanteSimplesNacional = "false";
                }

                if (!String.IsNullOrEmpty(b1Document.U_TAX4_itCultural))
                {
                    if (b1Document.U_TAX4_itCultural == "1")
                        requestInput.rps.identificacao.incentivadorCultural = "1";
                    else
                        requestInput.rps.identificacao.incentivadorCultural = "false";
                }
                else
                {
                    requestInput.rps.identificacao.incentivadorCultural = "false";
                }

                #endregion

                #region TOMADOR
                if (!String.IsNullOrEmpty(b1Document.TaxId0)) //CNPJ
                {
                    requestInput.rps.tomador.cnpj = Functions.RemoveCaracteresEspeciais(b1Document.TaxId0);
                }
                requestInput.rps.tomador.cpf = !String.IsNullOrEmpty(b1Document.TaxId4) ? Functions.RemoveCaracteresEspeciais(b1Document.TaxId4) : null;

                if (!String.IsNullOrEmpty(b1Document.TaxId1))
                {
                    if (b1Document.TaxId1 == "Isento")
                        b1Document.TaxId1 = "";
                    else
                        requestInput.rps.tomador.inscricaoEstadual = Functions.RemoveCaracteresEspeciais(b1Document.TaxId1);
                }
                else
                requestInput.rps.tomador.inscricaoEstadual = null;

                if (!String.IsNullOrEmpty(b1Document.TaxId5))
                {
                    requestInput.rps.tomador.docEstrangeiro = b1Document.TaxId5;
                    requestInput.rps.tomador.tipo = "5";
                }
                if (!String.IsNullOrEmpty(b1Document.CardName))
                {
                    if (b1Document.CardName.Length > 75)
                        requestInput.rps.tomador.razaoSocial = Functions.RemoverAcento(b1Document.CardName.Substring(0, 75));
                    else
                        requestInput.rps.tomador.razaoSocial = Functions.RemoverAcento(b1Document.CardName);

                    requestInput.rps.tomador.nomeFantasia = requestInput.rps.tomador.razaoSocial;
                }

                requestInput.rps.tomador.inscricaoMunicipal = !String.IsNullOrEmpty(b1Document.TaxId3) ? Functions.RemoverAcento(b1Document.TaxId3) : null;

                #region TOMADOR - CONTATO
                requestInput.rps.tomador.contato.email = b1Document.E_Mail;
                requestInput.rps.tomador.contato.telefone = !String.IsNullOrEmpty(b1Document.Phone1) ? Functions.RemoveCaracteresEspeciais(b1Document.Phone1) : "0";
                requestInput.rps.tomador.contato.ddd = !String.IsNullOrEmpty(b1Document.Phone2) ? Functions.RemoveCaracteresEspeciais(b1Document.Phone2) : "0";
                requestInput.rps.tomador.contato.fax = !String.IsNullOrEmpty(b1Document.Fax) ? Functions.RemoveCaracteresEspeciais(b1Document.Fax) : null;
                requestInput.rps.tomador.contato.site = !String.IsNullOrEmpty(b1Document.NTSWebSite) ? b1Document.NTSWebSite : null;

                if (!String.IsNullOrEmpty(b1Document.E_Mail))
                {
                    List<string> lstEmail = new List<string>();
                    lstEmail.Add(b1Document.E_Mail);
                    foreach (var item in b1Document.Emails)
                    {
                        lstEmail.Add(item.email);
                    }

                    requestInput.emails = lstEmail;
                }
                else
                {
                    requestInput.emails = null;
                }

                #endregion

                #region TOMADOR - ENDEREÇO
                if (!String.IsNullOrEmpty(b1Document.LOGRADOUROT))
                    if (b1Document.LOGRADOUROT.Length <= 3)
                        requestInput.rps.tomador.endereco.tipoLogradouro = Functions.RemoverAcento(b1Document.LOGRADOUROT);
                    else
                        requestInput.rps.tomador.endereco.tipoLogradouro = Functions.RemoverAcento(b1Document.LOGRADOUROT).Substring(0, 3);

                requestInput.rps.tomador.endereco.logradouro = Functions.RemoverAcento(b1Document.RUAT);
                requestInput.rps.tomador.endereco.numero = b1Document.NUMERORUAT;
                requestInput.rps.tomador.endereco.complemento = !String.IsNullOrEmpty(b1Document.COMPLEMENTOT) ? b1Document.COMPLEMENTOT : null;
                requestInput.rps.tomador.endereco.bairro = !String.IsNullOrEmpty(b1Document.BAIRROT) ? Functions.RemoverAcento(b1Document.BAIRROT) : null;
                requestInput.rps.tomador.endereco.nomePais = "Brasil"; //TODO: GET COUNTRY
                requestInput.rps.tomador.endereco.tpBairro = !String.IsNullOrEmpty(b1Document.BAIRROT) ? Functions.RemoverAcento(b1Document.BAIRROT) : null;
                requestInput.rps.tomador.endereco.codigoMunicipio = b1Document.IbgeCode;
                requestInput.rps.tomador.endereco.nomeMunicipio = b1Document.CIDADET;
                requestInput.rps.tomador.endereco.uf = b1Document.ESTADOT;
                requestInput.rps.tomador.endereco.cUf = !String.IsNullOrEmpty(b1Document.IbgeCode) ? b1Document.IbgeCode.Substring(0, 2) : "0";
                requestInput.rps.tomador.endereco.codigoPais = "1058"; //TODO: GET COUNTRY ID
                requestInput.rps.tomador.endereco.cep = Functions.RemoveCaracteresEspeciais(b1Document.CEPT);
                #endregion

                #endregion

                #region INTERMEDIARIO
                if (!String.IsNullOrEmpty(b1Document.BPChCode))
                {
                    requestInput.rps.intermediario.cpf = !String.IsNullOrEmpty(b1Document.TaxId4) ? Functions.RemoveCaracteresEspeciais(b1Document.TaxId4) : null;
                    requestInput.rps.intermediario.cnpj = !String.IsNullOrEmpty(b1Document.TaxId0) ? Functions.RemoveCaracteresEspeciais(b1Document.TaxId0) : null;
                    requestInput.rps.intermediario.razaoSocial = requestInput.rps.tomador.razaoSocial;
                    requestInput.rps.intermediario.inscricaoMunicipal = !String.IsNullOrEmpty(b1Document.TaxId3) ? Functions.RemoveCaracteresEspeciais(b1Document.TaxId3) : "0";
                    requestInput.rps.intermediario.contato.telefone = !String.IsNullOrEmpty(b1Document.Phone1) ? Functions.RemoveCaracteresEspeciais(b1Document.Phone1) : "0";
                    requestInput.rps.intermediario.contato.ddd = !String.IsNullOrEmpty(b1Document.Phone2) ? Functions.RemoveCaracteresEspeciais(b1Document.Phone2) : "0";
                    requestInput.rps.intermediario.contato.fax = !String.IsNullOrEmpty(b1Document.Fax) ? Functions.RemoveCaracteresEspeciais(b1Document.Fax) : null;
                    requestInput.rps.intermediario.endereco.codigoPais = "1058";
                }
                #endregion

                #region SERVIÇO
                foreach (var Linhas in b1Document.Linhas)
                {
                    #region SERVIÇO - CABEÇALHO

                    if (!String.IsNullOrEmpty(Linhas.U_TAX4_CodServ))
                        requestInput.rps.servico.codigoServico = Functions.RemoveCaracteresEspeciais(Linhas.U_TAX4_CodServ);
                    else
                        requestInput.rps.servico.codigoServico = Functions.RemoveCaracteresEspeciais(Linhas.OSvcCode);
                    if (!String.IsNullOrEmpty(Linhas.ServiceCD))
                        requestInput.rps.servico.codigoServico = Functions.RemoveCaracteresEspeciais(Linhas.ServiceCD);
                    if (!String.IsNullOrEmpty(Linhas.U_TAX4_CodAti))
                        requestInput.rps.servico.codigoAtividade = Functions.RemoveCaracteresEspeciais(Linhas.U_TAX4_CodAti);



                    requestInput.rps.servico.quantidade = Linhas.Quantity;
                    requestInput.rps.servico.valorUnitario = Linhas.Price;
                    List<string> descricao = new List<string>();
                    descricao.Add("Código: " + Linhas.ItemCode);
                    descricao.Add(Linhas.ItemName);
                    descricao.Add("Quantidade: " + Linhas.Quantity);
                    descricao.Add("Valor Unitário: R$ " + Linhas.Price);
                    descricao.Add("Valor Total do Item: R$ " + Linhas.LineTotal);
                    if (!string.IsNullOrEmpty(b1Document.Comments))
                    {
                        string[] subs = b1Document.Comments.Split('\r');
                        foreach (var item in subs)
                        {
                            descricao.Add(item);
                        }
                    }
                    if (Linhas.U_TAX4_IBPT > 0)
                    {

                        double valor = ((Convert.ToDouble(Linhas.U_TAX4_IBPT) * Linhas.LineTotal) / 100);
                        descricao.Add($@"Conforme Lei 12.741/2012, o percentual total de impostos incidentes neste serviço prestado é de aproximadamente – {Linhas.U_TAX4_IBPT}% - R${valor}(Fonte IBPT)" + Linhas.RECEBEIBPT);
                    }
                    string[] intList = descricao.ToArray();
                    requestInput.rps.servico.discriminacao = intList;
                    requestInput.rps.servico.itemListaServico = Linhas.U_TAX4_LisSer;

                    if (String.IsNullOrEmpty(Linhas.U_TAX4_LisSer))
                        requestInput.rps.servico.itemListaServico = "false";
                    else
                        requestInput.rps.servico.itemListaServico = Linhas.U_TAX4_LisSer;

                    if (String.IsNullOrEmpty(Linhas.U_TAX4_CodCNAE))
                        requestInput.rps.servico.cnae = "0";
                    else
                        requestInput.rps.servico.cnae = Functions.RemoveCaracteresEspeciais(Linhas.U_TAX4_CodCNAE);

                    requestInput.rps.servico.codigoTributacaoMunicipio = !String.IsNullOrEmpty(Linhas.U_TAX4_TrMun) ? Linhas.U_TAX4_TrMun : null;


                    requestInput.rps.servico.codigoMunicipioIncidencia = requestInput.rps.identificacao.regimeEspecialTributacao == "T" ? "3550308" : b1Document.IbgeCode;


                    #endregion
                    #region SERVIÇO - VALORES
                    requestInput.rps.servico.valores.totalServicos = b1Document.NfeValue;
                    requestInput.rps.servico.valores.totalDeducoes = 0; //TODO
                    requestInput.rps.servico.valores.descontoCondicionado = 0;//TODO
                    requestInput.rps.servico.valores.descontoIncondicionado = Linhas.DiscPrcnt * Linhas.LineTotal;


                    #region SERVIÇO - VALORES - IMPOSTO RETIDO
                    foreach (var LineTaxWithholding in b1Document.LinesTaxWithholding)
                    {
                        if (!String.IsNullOrEmpty(LineTaxWithholding.U_TAX4_TpImp) && Linhas.WtLiable == "Y")
                        {
                            switch (LineTaxWithholding.U_TAX4_TpImp)
                            {
                                case "1":
                                    requestInput.rps.servico.valores.pis.aliquota = LineTaxWithholding.RATE;
                                    requestInput.rps.servico.valores.pis.valor += LineTaxWithholding.WTAMNT;
                                    requestInput.rps.servico.valores.pis.baseCalculo = Linhas.LineTotal;
                                    break;
                                case "2":
                                    requestInput.rps.servico.valores.cofins.aliquota = LineTaxWithholding.RATE;
                                    requestInput.rps.servico.valores.cofins.valor += LineTaxWithholding.WTAMNT;
                                    requestInput.rps.servico.valores.cofins.baseCalculo = Linhas.LineTotal;
                                    break;
                                case "3":
                                    requestInput.rps.servico.valores.ir.aliquota = LineTaxWithholding.RATE;
                                    requestInput.rps.servico.valores.ir.valor = LineTaxWithholding.WTAMNT;
                                    requestInput.rps.servico.valores.ir.baseCalculo = Linhas.LineTotal.ToString();
                                    break;
                                case "4":
                                    requestInput.rps.servico.valores.csll.aliquota = LineTaxWithholding.RATE;
                                    requestInput.rps.servico.valores.csll.valor = LineTaxWithholding.WTAMNT;
                                    requestInput.rps.servico.valores.csll.baseCalculo = Linhas.LineTotal.ToString();
                                    break;
                                case "5":
                                    requestInput.rps.servico.valores.inss.aliquota = LineTaxWithholding.RATE;
                                    requestInput.rps.servico.valores.inss.valor = LineTaxWithholding.WTAMNT;
                                    requestInput.rps.servico.valores.inss.baseCalculo = Linhas.LineTotal.ToString();
                                    break;
                                case "6":
                                    requestInput.rps.servico.valores.iss.exigibilidadeIss = b1Document.U_TAX4_NatOpNFSe != "0" ? b1Document.U_TAX4_NatOpNFSe : null;
                                    requestInput.rps.servico.valores.iss.baseCalculo = Linhas.LineTotal;
                                    requestInput.rps.servico.valores.iss.retido = true;
                                    requestInput.rps.servico.valores.iss.aliquota = LineTaxWithholding.RATE;
                                    requestInput.rps.servico.valores.iss.valorRetido += Linhas.WTAMNT;
                                    break;
                            }
                            requestInput.rps.servico.valores.outrasRetencoes = 0.00;
                        }
                    }
                    #endregion
                    #region SERVIÇO - VALORES - IMPOSTO NÃO RETIDO
                    foreach (var LineTax in b1Document.LinesTax)
                    {
                        if (!String.IsNullOrEmpty(LineTax.U_TAX4_TpImp) && LineTax.TaxSum > 0 && Linhas.WtLiable != "Y")
                        {
                            switch (LineTax.U_TAX4_TpImp)
                            {
                                case "-10":
                                    requestInput.rps.servico.valores.cofins.aliquota = LineTax.TaxRate;
                                    requestInput.rps.servico.valores.cofins.valor += LineTax.TaxSum;
                                    requestInput.rps.servico.valores.cofins.baseCalculo = Linhas.LineTotal;
                                    requestInput.rps.servico.valores.cofins.cst = LineTax.CSTfCOFINS;
                                    break;
                                case "-7":
                                    requestInput.rps.servico.valores.iss.retido = false;
                                    requestInput.rps.servico.valores.iss.exigibilidadeIss = b1Document.U_TAX4_NatOpNFSe != "0" ? b1Document.U_TAX4_NatOpNFSe : null;
                                    requestInput.rps.servico.valores.iss.baseCalculo = Linhas.LineTotal;
                                    requestInput.rps.servico.valores.iss.aliquota = LineTax.TaxRate;
                                    requestInput.rps.servico.valores.iss.valor = LineTax.TaxSum;
                                    requestInput.rps.servico.valores.iss.valorRetido = 0.00;
                                    break;
                                case "-8":
                                    requestInput.rps.servico.valores.pis.aliquota = LineTax.TaxRate;
                                    requestInput.rps.servico.valores.pis.valor += LineTax.TaxSum;
                                    requestInput.rps.servico.valores.pis.baseCalculo = Linhas.LineTotal;
                                    requestInput.rps.servico.valores.pis.cst = LineTax.CSTfPIS;
                                    break;

                            }
                        }
                    }
                    #endregion




                    requestInput.rps.servico.valores.iss.valorRetido = requestInput.rps.servico.valores.iss.valorRetido;
                    requestInput.rps.servico.valores.iss.valor = requestInput.rps.servico.valores.iss.valor;
                    requestInput.rps.servico.valores.csll.valor = requestInput.rps.servico.valores.csll.valor;
                    requestInput.rps.servico.valores.ir.valor = requestInput.rps.servico.valores.ir.valor;
                    requestInput.rps.servico.valores.inss.valor = requestInput.rps.servico.valores.inss.valor;
                    requestInput.rps.servico.valores.cofins.valor = requestInput.rps.servico.valores.cofins.valor;
                    requestInput.rps.servico.valores.pis.valor = requestInput.rps.servico.valores.pis.valor;

                    #endregion
                }
                #endregion

                #region PAG
                DetPag detPag = new DetPag();
                detPag.tPag = b1Document.U_TAX4_FormaPagto;
                detPag.indPag = b1Document.U_TAX4_CondPag;
                detPag.vPag = b1Document.NfeValue.ToString();
                List<DetPag> lstDetPag = new List<DetPag>();
                lstDetPag.Add(detPag);
                requestInput.rps.pag.detPag = lstDetPag;
                #endregion

                if (b1Document.IbgeCode == "9999999")
                {
                    requestInput.rps.tomador.endereco = null;
                }

                return requestInput;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}


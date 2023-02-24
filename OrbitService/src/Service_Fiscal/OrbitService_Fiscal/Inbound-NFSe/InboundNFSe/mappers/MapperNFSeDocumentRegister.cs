using B1Library.Documents;
using B1Library.Utilities;
using OrbitService.InboundNFSe.services.NFSeDocumentRegister;
using OrbitService.InboundNFSe.util;
using System;
using System.Collections.Generic;
using static OrbitService.InboundNFSe.services.NFSeDocumentRegister.NFServico;

namespace OrbitService.InboundNFSe.mappers
{
    public class MapperNFSeDocumentRegister
    {
        private Util util;
        public MapperNFSeDocumentRegister()
        {
            util = new Util();
        }
        public NFSeDocumentRegisterInput ToNFSeDocumentRegisterInput(Invoice invoice)
        {
            try
            {
                Functions functions = new Functions();

                NFSeDocumentRegisterInput input = FactoryNFSeDocumentRegisterInput.CreateNFSeDocumentRegisterInputInstance();

                #region DATA
                input.Event = invoice.CANCELED == "Y" ? "cancel" : "emit";
                input.NFServico.BranchId = invoice.Identificacao.BranchId;
                input.NFServico.numeroLote = invoice.Identificacao.DocEntry.ToString();
                

                string[] recebeEmail = { invoice.Parceiro.EmailParceiro };
                input.NFServico.emails = recebeEmail;

                input.NFServico.dataLancamento =  util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataLancamento, invoice.Identificacao.DocTime);
                #endregion DATA

                #region IDENTIFICAÇÃO
                input.NFServico.Rps.Identificacao.cNf = invoice.Identificacao.DocEntry.ToString();
                input.NFServico.Rps.Identificacao.indPres = Convert.ToInt32(invoice.Identificacao.IndicadorPresenca);
                input.NFServico.Rps.Identificacao.Numero = invoice.Identificacao.NumeroDocumento;
                input.NFServico.Rps.Identificacao.TipoRps = invoice.Identificacao.DocEntry.ToString();
                input.NFServico.Rps.Identificacao.DataEmissao = util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataLancamento, invoice.Identificacao.DocTime);
                input.NFServico.Rps.Identificacao.Competencia = util.ConvertDateB1ToFormatOrbit(invoice.Identificacao.DataLancamento, invoice.Identificacao.DocTime);
                input.NFServico.Rps.Identificacao.NaturezaOperacao = invoice.Identificacao.NaturezaOperacaoDocumento;
                input.NFServico.Rps.Identificacao.RegimeEspecialTributacao = !String.IsNullOrEmpty(invoice.Identificacao.TipoTributacaoNFSe) ? invoice.Identificacao.TipoTributacaoNFSe.Split("-")[0].ToString().Trim() : null;
                input.NFServico.Rps.Identificacao.IncentivadorCultural = invoice.Identificacao.IncentivadorCultural;
                #endregion IDENTIFICAÇÃO  

                #region TOMADOR 
                input.NFServico.Rps.Tomador.Cnpj = functions.RemoveCaracteresEspeciais(invoice.Filial.CNPJFilial);
                input.NFServico.Rps.Tomador.RazaoSocial = invoice.Filial.RazaoSocialFilial;
              
                input.NFServico.Rps.Tomador.InscricaoMunicipal = invoice.Filial.InscIeFilial;

                #region TOMADOR ENDEREÇO
                input.NFServico.Rps.Tomador.Endereco.TipoLogradouro = invoice.Filial.AdressTypeFilial;
                input.NFServico.Rps.Tomador.Endereco.Logradouro = invoice.Filial.LogradouroFilial;
                input.NFServico.Rps.Tomador.Endereco.Numero = invoice.Filial.NumeroLogradouroFilial;
                input.NFServico.Rps.Tomador.Endereco.Bairro = invoice.Filial.BairroFilial;
                input.NFServico.Rps.Tomador.Endereco.CodigoMunicipio = invoice.Filial.CodigoIBGEMunicipioFilial;
                input.NFServico.Rps.Tomador.Endereco.Uf = invoice.Filial.UFFilial;
                input.NFServico.Rps.Tomador.Endereco.cUf = invoice.Filial.CodigoUFFilial;
                input.NFServico.Rps.Tomador.Endereco.Cep = invoice.Filial.CEPFilial;
                input.NFServico.Rps.Tomador.Endereco.CodigoPais = invoice.Filial.CodigoPaisFilial;
                input.NFServico.Rps.Tomador.Endereco.nomePais = invoice.Filial.NomePaisFilial;
                input.NFServico.Rps.Tomador.Endereco.nomeMunicipio = invoice.Filial.MunicipioFilial;
                input.NFServico.Rps.Tomador.Endereco.tpBairro = invoice.Filial.BairroFilial;
                #endregion TOMADOR ENDEREÇO

                #region TOMADOR CONTATO
                input.NFServico.Rps.Tomador.Contato.Ddd = null;
                input.NFServico.Rps.Tomador.Contato.Telefone = null;
                input.NFServico.Rps.Tomador.Contato.Fax = null;
                input.NFServico.Rps.Tomador.Contato.Site = null;
                input.NFServico.Rps.Tomador.Contato.Email = null;
                #endregion TOMADOR CONTATO

                #endregion

                #region PRESTADOR 
                input.NFServico.Rps.Prestador.cnpj = functions.RemoveCaracteresEspeciais(invoice.Parceiro.CnpjParceiro);
                input.NFServico.Rps.Prestador.razaoSocial = invoice.Parceiro.RazaoSocialParceiro;
                input.NFServico.Rps.Prestador.inscricaoMunicipal = invoice.Parceiro.InscIeParceiro;

                #region Prestador ENDEREÇO
                input.NFServico.Rps.Prestador.Endereco.tipoLogradouro = invoice.Parceiro.TipoLogradouroParceiro;
                input.NFServico.Rps.Prestador.Endereco.logradouro = invoice.Parceiro.LogradouroParceiro;
                input.NFServico.Rps.Prestador.Endereco.numero = invoice.Parceiro.NumeroLogradouroParceiro;
                input.NFServico.Rps.Prestador.Endereco.bairro = invoice.Parceiro.BairroParceiro;
                input.NFServico.Rps.Prestador.Endereco.codigoMunicipio = invoice.Parceiro.CodigoIBGEMunicipioParceiro;
                input.NFServico.Rps.Prestador.Endereco.uf = invoice.Parceiro.UFParceiro;
                input.NFServico.Rps.Prestador.Endereco.cUf = invoice.Parceiro.CodigoUFParceiro;
                input.NFServico.Rps.Prestador.Endereco.cep = invoice.Parceiro.CEPParceiro;
                input.NFServico.Rps.Prestador.Endereco.codigoPais = invoice.Parceiro.CodigoPaisParceiro;
                input.NFServico.Rps.Prestador.Endereco.tpBairro = invoice.Parceiro.BairroParceiro;
                #endregion Prestador ENDEREÇO

                #endregion

                #region SERVIÇO
                foreach (var Linhas in invoice.CabecalhoLinha)
                {
                    input.NFServico.Rps.Servico.CodigoServico = !String.IsNullOrEmpty(Linhas.CodigoTributacaoMuncipio) ? Linhas.CodigoTributacaoMuncipio : Linhas.CodigoServicoEntrada;
                    input.NFServico.Rps.Servico.Quantidade = Convert.ToInt32(Linhas.QuantidadeLinha);
                    input.NFServico.Rps.Servico.ValorUnitario = Linhas.ValorUnitarioLinha;

                    #region discriminacao
                    List<string> discriminacao = new List<string>();
                    discriminacao.Add("Código: " + Linhas.CodigoItem);
                    discriminacao.Add(Linhas.DescricaoItemLinhaDocumento);
                    discriminacao.Add("Quantidade: " + Linhas.QuantidadeLinha);
                    discriminacao.Add("Valor Unitário: R$ " + Linhas.ValorUnitarioLinha);
                    discriminacao.Add("Valor Total do Item: R$ " + Linhas.ValorTotalLinnha);

                    if (Linhas.PorcentagemIBPTLinha > 0)
                    {
                        if (Convert.ToDouble(Linhas.PorcentagemIBPTLinha) > 0)
                        {
                            string recebeIBPT = ((Convert.ToDouble(Linhas.PorcentagemIBPTLinha) * Linhas.ValorTotalLinnha) / 100).ToString();
                            discriminacao.Add("Valor Total dos Tributos: R$ " + recebeIBPT);
                        }
                    }

                    string[] discriminacaoList = discriminacao.ToArray();
                    input.NFServico.Rps.Servico.Discriminacao = discriminacaoList;
                    #endregion discriminacao

                    input.NFServico.Rps.Servico.ItemListaServico = Linhas.ItemListaServico;
                    input.NFServico.Rps.Servico.CodigoTributacaoMunicipio = Linhas.CodigoTributacaoMuncipio;
                    input.NFServico.Rps.Servico.CodigoMunicipioIncidencia = invoice.Parceiro.CodigoIBGEMunicipioParceiro;
                    input.NFServico.Rps.Servico.CodigoAtividade = Linhas.CodigoAtividade;

                    #region SERVIÇO - VALORES
                    input.NFServico.Rps.Servico.Valores.TotalServicos = invoice.Identificacao.ValorTotalNF;
                    input.NFServico.Rps.Servico.Valores.TotalDeducoes = 0.0;
                    input.NFServico.Rps.Servico.Valores.DescontoCondicionado = 0.0;
                    input.NFServico.Rps.Servico.Valores.DescontoIncondicionado = Linhas.ValorTotalDescontoLinha * Linhas.ValorTotalLinnha;
                    input.NFServico.Rps.Servico.Valores.OutrasRetencoes = 0.0;

                    #region SERVIÇO - VALORES - IMPOSTO RETIDO
                    foreach (var LineTaxWithholding in Linhas.ImpostoRetidoLinha)
                    {
                        if (!String.IsNullOrEmpty(LineTaxWithholding.TipoImpostoOrbit))
                        {
                            switch (LineTaxWithholding.TipoImpostoOrbit)
                            {
                                case "1":
                                    input.NFServico.Rps.Servico.Valores.Pis.Aliquota = LineTaxWithholding.PorcentagemImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Pis.Valor += LineTaxWithholding.ValorImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Pis.baseCalculo = util.ToOrbitString(LineTaxWithholding.TaxbleAmnt);
                                    input.NFServico.Rps.Servico.Valores.Pis.codigo_receita = LineTaxWithholding.CodOfiReceita;
                                    break;
                                case "2":
                                    input.NFServico.Rps.Servico.Valores.Cofins.Aliquota = LineTaxWithholding.PorcentagemImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Cofins.Valor += LineTaxWithholding.ValorImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Cofins.baseCalculo = util.ToOrbitString(LineTaxWithholding.TaxbleAmnt);
                                    input.NFServico.Rps.Servico.Valores.Cofins.codigo_receita = LineTaxWithholding.CodOfiReceita;
                                    break;
                                case "3":
                                    input.NFServico.Rps.Servico.Valores.Ir.Aliquota = LineTaxWithholding.PorcentagemImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Ir.Valor = LineTaxWithholding.ValorImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Ir.baseCalculo = util.ToOrbitString(LineTaxWithholding.TaxbleAmnt);
                                    input.NFServico.Rps.Servico.Valores.Ir.codigo_receita = LineTaxWithholding.CodOfiReceita;
                                    break;
                                case "4":
                                    input.NFServico.Rps.Servico.Valores.Csll.Aliquota = LineTaxWithholding.PorcentagemImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Csll.Valor = LineTaxWithholding.ValorImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Csll.baseCalculo = util.ToOrbitString(LineTaxWithholding.TaxbleAmnt);
                                    input.NFServico.Rps.Servico.Valores.Csll.codigo_receita = LineTaxWithholding.CodOfiReceita;
                                    break;
                                case "5":
                                    input.NFServico.Rps.Servico.Valores.Inss.Aliquota = LineTaxWithholding.PorcentagemImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Inss.Valor = LineTaxWithholding.ValorImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Inss.baseCalculo = util.ToOrbitString(LineTaxWithholding.TaxbleAmnt);
                                    input.NFServico.Rps.Servico.Valores.Inss.codigo_receita = LineTaxWithholding.CodOfiReceita;
                                    break;
                                case "6":
                                    input.NFServico.Rps.Servico.Valores.Iss.ExigibilidadeIss = "2";
                                    input.NFServico.Rps.Servico.Valores.Iss.BaseCalculo = LineTaxWithholding.TaxbleAmnt;
                                    input.NFServico.Rps.Servico.Valores.Iss.Retido = true;
                                    input.NFServico.Rps.Servico.Valores.Iss.Aliquota = LineTaxWithholding.PorcentagemImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Iss.ValorRetido += LineTaxWithholding.ValorImpostoRetido;
                                    input.NFServico.Rps.Servico.Valores.Iss.codigo_receita = LineTaxWithholding.CodOfiReceita;
                                    break;
                            }
                            input.NFServico.Rps.Servico.Valores.OutrasRetencoes = 0.00;
                        }


                        if (LineTaxWithholding.TipoImpostoOWHT == "7")
                        {
                            input.NFServico.Rps.Servico.Valores.Pis.Aliquota = 0.65;
                            input.NFServico.Rps.Servico.Valores.Pis.Valor += Convert.ToDouble(util.ToOrbitString(LineTaxWithholding.TaxbleAmnt * (0.65 / 100)));
                            input.NFServico.Rps.Servico.Valores.Pis.baseCalculo = util.ToOrbitString(LineTaxWithholding.TaxbleAmnt);
                            input.NFServico.Rps.Servico.Valores.Pis.codigo_receita = LineTaxWithholding.CodOfiReceita;

                            input.NFServico.Rps.Servico.Valores.Cofins.Aliquota = 3.00;
                            input.NFServico.Rps.Servico.Valores.Cofins.Valor += Convert.ToDouble(util.ToOrbitString(LineTaxWithholding.TaxbleAmnt * (3.00 / 100)));
                            input.NFServico.Rps.Servico.Valores.Cofins.baseCalculo = util.ToOrbitString(LineTaxWithholding.TaxbleAmnt);
                            input.NFServico.Rps.Servico.Valores.Cofins.codigo_receita = LineTaxWithholding.CodOfiReceita;

                            input.NFServico.Rps.Servico.Valores.Csll.Aliquota = 1.00;
                            input.NFServico.Rps.Servico.Valores.Csll.Valor = Convert.ToDouble(util.ToOrbitString(LineTaxWithholding.TaxbleAmnt * (1.00 / 100)));
                            input.NFServico.Rps.Servico.Valores.Csll.baseCalculo = util.ToOrbitString(LineTaxWithholding.TaxbleAmnt);
                            input.NFServico.Rps.Servico.Valores.Csll.codigo_receita = LineTaxWithholding.CodOfiReceita;
                        }
                    }
                    #endregion SERVIÇO - VALORES - IMPOSTO RETIDO

                    #region SERVIÇO - VALORES - IMPOSTO NÃO RETIDO
                    foreach (var LineTax in Linhas.ImpostoLinha)
                    {
                        if (!String.IsNullOrEmpty(LineTax.TipoImpostoOrbit) && LineTax.ValorImposto > 0)
                        {
                            switch (LineTax.TipoImpostoOrbit)
                            {
                                case "-10":
                                    input.NFServico.Rps.Servico.Valores.Cofins.Aliquota = LineTax.PorcentagemImposto;
                                    input.NFServico.Rps.Servico.Valores.Cofins.Valor += LineTax.ValorImposto;
                                    input.NFServico.Rps.Servico.Valores.Cofins.baseCalculo = util.ToOrbitString(Linhas.ValorTotalLinnha);
                                    
                                    break;
                                case "-7":
                                    input.NFServico.Rps.Servico.Valores.Iss.Retido = false;
                                    input.NFServico.Rps.Servico.Valores.Iss.ExigibilidadeIss = "2";
                                    input.NFServico.Rps.Servico.Valores.Iss.BaseCalculo = Linhas.ValorTotalLinnha;
                                    input.NFServico.Rps.Servico.Valores.Iss.Aliquota = LineTax.PorcentagemImposto;
                                    input.NFServico.Rps.Servico.Valores.Iss.Valor = LineTax.ValorImposto;
                                    input.NFServico.Rps.Servico.Valores.Iss.ValorRetido = 0.00;
                                    break;
                                case "-8":
                                    input.NFServico.Rps.Servico.Valores.Pis.Aliquota = LineTax.PorcentagemImposto;
                                    input.NFServico.Rps.Servico.Valores.Pis.Valor += LineTax.ValorImposto;
                                    input.NFServico.Rps.Servico.Valores.Pis.baseCalculo = util.ToOrbitString(Linhas.ValorTotalLinnha);
                                    
                                    break;
                            }
                        }
                    }
                    #endregion SERVIÇO - VALORES - IMPOSTO NÃO RETIDO

                    input.NFServico.Rps.Servico.Valores.Cofins.Cst = Linhas.CSTCofinsLinha;
                    input.NFServico.Rps.Servico.Valores.Pis.Cst = Linhas.CSTPisLinha;

                    input.NFServico.Rps.Servico.Valores.Iss.ValorRetido = input.NFServico.Rps.Servico.Valores.Iss.ValorRetido;
                    input.NFServico.Rps.Servico.Valores.Iss.Valor = input.NFServico.Rps.Servico.Valores.Iss.Valor;
                    input.NFServico.Rps.Servico.Valores.Csll.Valor = input.NFServico.Rps.Servico.Valores.Csll.Valor;
                    input.NFServico.Rps.Servico.Valores.Ir.Valor = input.NFServico.Rps.Servico.Valores.Ir.Valor;
                    input.NFServico.Rps.Servico.Valores.Inss.Valor = input.NFServico.Rps.Servico.Valores.Inss.Valor;
                    input.NFServico.Rps.Servico.Valores.Cofins.Valor = input.NFServico.Rps.Servico.Valores.Cofins.Valor;
                    input.NFServico.Rps.Servico.Valores.Pis.Valor = input.NFServico.Rps.Servico.Valores.Pis.Valor;
                    #endregion
                }
                #endregion SERVIÇO

                #region PAG
                DetPag detPag = new DetPag();
                detPag.tPag = invoice.Identificacao.FormaDePagamentoDocumento;
                detPag.indPag = invoice.Identificacao.CondicaoDePagamentoDocumento;
                detPag.vPag = util.ToOrbitString(invoice.Identificacao.ValorTotalNF);
                List<DetPag> lstDetPag = new List<DetPag>();
                lstDetPag.Add(detPag);
                input.NFServico.Rps.Pag.DetPag = lstDetPag;
                #endregion

                #region NFSE
                input.NFServico.nfse.numero = input.NFServico.Rps.Identificacao.Numero;
                input.NFServico.nfse.dataEmissao = input.NFServico.Rps.Identificacao.DataEmissao;
                input.NFServico.nfse.codigoMunicipioGerador = input.NFServico.Rps.Tomador.Endereco.CodigoMunicipio;
                #endregion NFSE
                #region STATUS
                input.NFServico.status.cStat = "0";
                input.NFServico.status.mStat = "NFSE EMITIDA";

                List<Eventos> lstEventos = new List<Eventos>();
                Eventos eventos = new Eventos();
                eventos.type = "EMISSÃO NFSE";
                lstEventos.Add(eventos);

                input.NFServico.eventos = lstEventos;

                #endregion STATUS

                return input;
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

        internal DocumentStatus ToDocumentStatusResponseError(Invoice invoice, NFSeDocumentRegisterError output)
        {
            DocumentStatus newStatusData = new DocumentStatus("", "", output.Message, invoice.ObjetoB1, invoice.DocEntry, StatusCode.Erro);
            return newStatusData;
        }

        public DocumentStatus ToDocumentStatusResponseSucessful(Invoice invoice, NFSeDocumentRegisterOutput output)
        {
            StatusCode status = StatusCode.CargaFiscal;
            DocumentStatus newStatusData = new DocumentStatus(output.data.document_id, "", "", invoice.ObjetoB1, invoice.DocEntry, status);
            return newStatusData;
        }
    }
}

using B1Library.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Test.TestUtils
{
    public class InvoiceB1Fake_ICMSST_ICMSDeson_DescontoLinha_Frete
    {
        public static Invoice GetFakeInvoiceB1()
        {
            Invoice invoice = new Invoice();
            Identificacao identificacao = new Identificacao();
            Filial Filial = new Filial();
            CabecalhoLinha line = new CabecalhoLinha();
            Parceiro Parceiro = new Parceiro();

            invoice.DocEntry = 327;
            invoice.ObjetoB1 = 13;
            invoice.ModeloDocumento = "39";
            invoice.TipoNF = "output";
            invoice.CargaFiscal = 1;
            invoice.CodInt = "0";


            #region HEADER
            identificacao.Versao = "4.00";
            identificacao.BranchId = "12812e2d-fcc5-406a-b4e9-5e8827e04470";
            identificacao.DadosCobranca = 481.22;
            identificacao.Key = "31220733026064000110550010000012151610036030";
            invoice.TipoDocumento = "input";
            #endregion

            #region IDENTIFICACAO
            Parceiro.CodigoUFParceiro = "35";
            identificacao.NaturezaOperacaoDocumento = "Venda Adquirida Terc";
            identificacao.SerieDocumento = "2";
            identificacao.NumeroDocumento = "25";
            identificacao.DataEmissao = new DateTime(2022, 7, 11, 00, 00, 00);
            identificacao.DocTime = "1130";
            line.IdLocalDestino = "1";
            Parceiro.CodigoIBGEMunicipioParceiro = "3171204";
            identificacao.OperacaoNFe = "1";
            identificacao.FinalideDocumento = "1";
            identificacao.ConsumidorFinal = "1";
            identificacao.IndicadorPresenca = "9";
            identificacao.IndicadorIntermediario = 0;
            identificacao.DocEntry = 817;
            invoice.TipoNF = "0";
            identificacao.ValorTotalNF = 481.22;
            #endregion

            #region EMAIL 
            Parceiro.EmailParceiro = "mce.vidia@gmail.com";
            #endregion EMAIL

            #region DET

            #region ITEM 1        

            #region HEADER ITEM
            line.NItem = "1";
            line.CodigoItem = "P13";
            line.CodigoDeBarras = String.Empty;
            line.DescricaoItemLinhaDocumento = "P13";
            line.CodigoNCM = "27112100";
            line.CodigoCFOP = "6120";
            line.UnidadeComercial = "KG";
            line.QuantidadeLinha = 7;
            line.ValorUnitarioLinha = 45.00;
            line.ValorTotalLinnha = 302.40;
            line.ValorTotalDescontoLinha = 4.00;
            #endregion HEADER ITEM

            #region Despesa
            List<DespesaAdicional> listDespesaAdicional = new List<DespesaAdicional>();
            DespesaAdicional despesaAdicional = new DespesaAdicional();
            despesaAdicional.TipoDespesa = "1";
            despesaAdicional.ValorUnitarioDespesa = 90.00;
            listDespesaAdicional.Add(despesaAdicional);
            line.DespesaAdicional = listDespesaAdicional;
            #endregion Despesa


            #region IMPOSTO ITEM
            List<ImpostoLinha> listImpostoLinha = new List<ImpostoLinha>();
            ImpostoLinha impostoLinha;

            #region ICMS 
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "ICMS";
            impostoLinha.TipoImpostoOrbit = "-6";
            line.OrigICMS = "0";
            line.CSTICMSLinha = "30";
            impostoLinha.SimOuNaoDesoneracao = "Y";
            line.MotivoDesoneracao = "7";
            impostoLinha.ValorImposto = 12.60;
            listImpostoLinha.Add(impostoLinha);
            #endregion ICMS




            #region ICMSST
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "ICMSST";
            impostoLinha.TipoImpostoOrbit = "-5";
            impostoLinha.MVast = 101.10;
            impostoLinha.ValorImposto = 101.42;
            impostoLinha.ValorBaseImposto = 633.47;
            impostoLinha.PorcentagemImposto = 18;

            listImpostoLinha.Add(impostoLinha);
            #endregion ICMSST

            #region PIS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "PIS";
            impostoLinha.TipoImpostoOrbit = "-8";
            line.CSTPisLinha = "06";
            listImpostoLinha.Add(impostoLinha);
            #endregion PIS

            #region COFINS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "COFINS";
            impostoLinha.TipoImpostoOrbit = "-10";
            line.CSTCofinsLinha = "06";
            listImpostoLinha.Add(impostoLinha);
            #endregion COFINS

            #region IPI
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "IPI";
            impostoLinha.TipoImpostoOrbit = "-4";
            line.CSTIPILinha = "55";
            line.cEnq = "102";
            listImpostoLinha.Add(impostoLinha);
            #endregion IPI

            line.ImpostoLinha = listImpostoLinha;

            #endregion IMPOSTO ITEM

            invoice.AddItemLine(line);

            #endregion ITEM 1

            #endregion DET

            #region TRANSP
            Parceiro.ModalidadeFrete = "0";
            #endregion TRANSP

            #region COBR
            #region FATURA
            identificacao.NumeroDocumento = "62";
            #endregion FATURA

            #region DUPLICATA
            List<Duplicata> lstDuplicata = new List<Duplicata>();
            Duplicata duplicata = new Duplicata();
            duplicata.NumeroDuplicata = 001;
            duplicata.DataVencimento = new DateTime(2022, 7, 11, 00, 00, 00);
            duplicata.ValorDuplicata = 481.22;
            lstDuplicata.Add(duplicata);
            invoice.Duplicata = lstDuplicata;
            #endregion DUPLICATA
            #endregion COBR

            #region PAG            
            identificacao.CondicaoDePagamentoDocumento = "0";
            identificacao.FormaDePagamentoDocumento = "90";

            #endregion PAG

            #region DESTINATARIO

            #region HEADER DESTINATARIO
            Parceiro.CnpjParceiro = "33026064000110";
            Parceiro.RazaoSocialParceiro = "MCE USINAGEM EIRELI";
            Parceiro.InscIeParceiro = "33990830023";
            #endregion HEADER DESTINATARIO

            #region ENDERECO DESTINATARIO
            Parceiro.TipoLogradouroParceiro = "Rua";
            Parceiro.LogradouroParceiro = "VICENTINO JOSE HERCULANO";
            Parceiro.NumeroLogradouroParceiro = "67";
            Parceiro.BairroParceiro = "INDUSTRIAL";
            Parceiro.CodigoIBGEMunicipioParceiro = "3171204";
            Parceiro.UFParceiro = "MG";
            Parceiro.CEPParceiro = "33200414";
            Parceiro.CodigoPaisParceiro = "1058";
            Parceiro.FoneParceiro = "31997450905";
            #endregion ENDERECO DESTINATARIO

            #endregion DESTINATARIO


            Filial.InscIeFilial = "117077509119";
            Filial.CodigoIBGEMunicipioFilial = "1302603";
            invoice.Identificacao = identificacao;
            invoice.Filial = Filial;
            invoice.Parceiro = Parceiro;


            return invoice;
        }
    }
}

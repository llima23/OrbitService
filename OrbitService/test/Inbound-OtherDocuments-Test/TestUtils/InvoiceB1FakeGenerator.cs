using B1Library.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService_Test.TestUtils
{
    public class InvoiceB1FakeGenerator
    {
        public static Invoice GetFakeInvoiceB1()
        {
            Invoice invoice = new Invoice();
            Identificacao identificacao = new Identificacao();
            Filial Filial = new Filial();
            CabecalhoLinha line = new CabecalhoLinha();
            Parceiro Parceiro = new Parceiro();

            invoice.DocEntry = 817;
            invoice.ObjetoB1 = 13;
            invoice.ModeloDocumento = "55";
            invoice.TipoNF = "input";
            invoice.CargaFiscal = -3;
            invoice.CodInt = "0";


            #region HEADER
            identificacao.Versao = "4.00";
            identificacao.BranchId = "12812e2d-fcc5-406a-b4e9-5e8827e04470";
            identificacao.DadosCobranca = 5184.00;
            identificacao.Key = "31220733026064000110550010000012151610036030";
            invoice.TipoDocumento = "input";
            #endregion

            #region IDENTIFICACAO
            Filial.CodigoUFFilial = "31";
            identificacao.NaturezaOperacaoDocumento = "Compra Prod MP";
            identificacao.SerieDocumento = "1";
            identificacao.NumeroDocumento = "1215";
            identificacao.DataEmissao = new DateTime(2022, 7, 11, 00, 00, 00);
            identificacao.DocTime = "1130";
            line.IdLocalDestino = "1";
            Filial.CodigoIBGEMunicipioFilial = "3171204";
            identificacao.OperacaoNFe = "1";
            identificacao.FinalideDocumento = "1";
            identificacao.ConsumidorFinal = "1";
            identificacao.IndicadorPresenca = "9";
            identificacao.IndicadorIntermediario = 0;
            identificacao.DocEntry = 817;
            invoice.TipoNF = "0";
            identificacao.ValorTotalNF = 5101.20;
            #endregion

            #region HEADER DESTINATARIO
            Filial.CNPJFilial = "10829962000101";
            Filial.RazaoSocialFilial = "POLIWAY INDUSTRIA E COMERCIO DE MÁQUINAS E EQUIPAMENTOS IND";
            Filial.InscIeFilial = "33990830023";
            #endregion

            #region ENDERECO DESTINATARIO
            Filial.AdressTypeFilial = "Rua";
            Filial.LogradouroFilial = "João Pinheiro";
            Filial.NumeroLogradouroFilial = "338";
            Filial.BairroFilial = "Santo Antônio";
            Filial.CodigoIBGEMunicipioFilial = "3171204";
            Filial.UFFilial = "MG";
            Filial.CEPFilial = "33200414";
            Filial.CodigoPaisFilial = "1058";
            #endregion

            #region EMAIL 
            Parceiro.EmailParceiro = "mce.vidia@gmail.com";
            #endregion EMAIL

            #region DET

            #region ITEM 1        

            #region HEADER ITEM
            line.NItem = "1";
            line.CodigoItem = "1RF-MAW-ST-1636-AL";
            line.CodigoDeBarras = String.Empty;
            line.DescricaoItemLinhaDocumento = "MANCAL PARA ROLETE FLEXIVEL 1636\" INDUSTRIALIZADO";
            line.CodigoNCM = "00000000";
            line.CodigoCFOP = "1124";
            line.UnidadeComercial = "0";
            line.QuantidadeLinha = 738.0000;
            line.ValorUnitarioLinha = 6.2000;
            line.ValorTotalLinnha = 4575.60;
            #endregion HEADER ITEM

            #region IMPOSTO ITEM
            List<ImpostoLinha> listImpostoLinha = new List<ImpostoLinha>();
            ImpostoLinha impostoLinha;

            #region ICMS 
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "ICMS";
            impostoLinha.TipoImpostoOrbit = "-6";
            line.OrigICMS = "0";
            line.CSTICMSLinha = "10";
            listImpostoLinha.Add(impostoLinha);
            #endregion ICMS

            #region PIS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "PIS";
            impostoLinha.TipoImpostoOrbit = "-8";
            line.CSTPisLinha = "07";
            listImpostoLinha.Add(impostoLinha);
            #endregion PIS

            #region IPI
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "IPI";
            line.cEnq = "302";
            impostoLinha.TipoImpostoOrbit = "-4";
            line.CSTIPILinha = "02";
            listImpostoLinha.Add(impostoLinha);
            #endregion IPI

            #region COFINS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "COFINS";
            impostoLinha.TipoImpostoOrbit = "-10";
            line.CSTCofinsLinha = "07";
            listImpostoLinha.Add(impostoLinha);
            #endregion COFINS

            line.ImpostoLinha = listImpostoLinha;

            #endregion IMPOSTO ITEM

            invoice.AddItemLine(line);

            #endregion ITEM 1

            #region ITEM 2        

            #region HEADER ITEM
            line = new CabecalhoLinha();
            line.NItem = "2";
            line.CodigoItem = "1RF-MAW-ST-1636-AL";
            line.CodigoDeBarras = String.Empty;
            line.DescricaoItemLinhaDocumento = "MANCAL PARA ROLETE FLEXIVEL 1636\" INDUSTRIALIZADO";
            line.CodigoNCM = "00000000";
            line.CodigoCFOP = "1124";
            line.UnidadeComercial = "0";
            line.QuantidadeLinha = 73.0000;
            line.ValorUnitarioLinha = 7.2000;
            line.ValorTotalLinnha = 525.60;
            #endregion HEADER ITEM

            #region IMPOSTO ITEM
            listImpostoLinha = new List<ImpostoLinha>();

            #region ICMS 
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "ICMS";
            impostoLinha.TipoImpostoOrbit = "-6";
            line.OrigICMS = "0";
            line.CSTICMSLinha = "10";
            listImpostoLinha.Add(impostoLinha);
            #endregion ICMS

            #region PIS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "PIS";
            impostoLinha.TipoImpostoOrbit = "-8";
            line.CSTPisLinha = "07";
            listImpostoLinha.Add(impostoLinha);
            #endregion PIS

            #region IPI
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "IPI";
            line.cEnq = "302";
            impostoLinha.TipoImpostoOrbit = "-4";
            line.CSTIPILinha = "02";
            listImpostoLinha.Add(impostoLinha);
            #endregion IPI

            #region COFINS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "COFINS";
            impostoLinha.TipoImpostoOrbit = "-10";
            line.CSTCofinsLinha = "07";
            listImpostoLinha.Add(impostoLinha);
            #endregion COFINS

            line.ImpostoLinha = listImpostoLinha;

            #endregion IMPOSTO ITEM

            invoice.AddItemLine(line);

            #endregion ITEM 2

            #endregion DET

            #region TRANSP
            Parceiro.ModalidadeFrete = "0";
            #endregion TRANSP

            #region COBR
            #region FATURA
            identificacao.NumeroDocumento = "1215";
            #endregion FATURA
            #endregion COBR

            #region PAG            
            identificacao.CondicaoDePagamentoDocumento = "0";
            identificacao.FormaDePagamentoDocumento = "90";
            #endregion PAG

            #region EMITENTE

            #region HEADER EMITENTE
            Parceiro.CnpjParceiro = "33026064000110";
            Parceiro.RazaoSocialParceiro = "MCE USINAGEM EIRELI";
            Parceiro.InscIeParceiro = "33990830023";
            #endregion HEADER EMITENTE

            #region ENDERECO EMITENTE
            Parceiro.TipoLogradouroParceiro = "Rua";
            Parceiro.LogradouroParceiro = "VICENTINO JOSE HERCULANO";
            Parceiro.NumeroLogradouroParceiro = "67";
            Parceiro.BairroParceiro = "INDUSTRIAL";
            Parceiro.CodigoIBGEMunicipioParceiro = "3171204";
            Parceiro.UFParceiro = "MG";
            Parceiro.CEPParceiro = "33200414";
            Parceiro.CodigoPaisParceiro = "1058";
            Parceiro.FoneParceiro = "31997450905";
            #endregion ENDERECO EMITENTE

            #endregion EMITENTE
            invoice.Identificacao = identificacao;
            invoice.Filial = Filial;
            invoice.Parceiro = Parceiro;

            return invoice;
        }
    }
}

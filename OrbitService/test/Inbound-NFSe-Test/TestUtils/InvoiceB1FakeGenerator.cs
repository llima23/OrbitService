using B1Library.Documents;
using System;
using System.Collections.Generic;

namespace OrbitService_Test.TestUtils
{
    public class InvoiceB1FakeGenerator
    {
        public static Invoice GetFakeB1NFSeDocuments()
        {
            Invoice invoice = new Invoice();
            invoice.CodInt = "0";
            invoice.TipoDocumento = "NFSe";

            //Identificacao
            Identificacao identificacao = new Identificacao();
            identificacao.BranchId = "31d3ff0c-72e6-4a39-8445-3cf0c4d3abad";
            identificacao.DataEmissao = new DateTime(2022, 7, 11, 00, 00, 00);
            identificacao.DataLancamento = new DateTime(2022, 7, 11, 00, 00, 00);
            identificacao.NumeroDocumento = "23";
            identificacao.DocEntry = 1;
            identificacao.IdRetornoOrbit = "6304e4d83b17846cc1519597";
            identificacao.DocumentoCancelado = "N";
            identificacao.FormaDePagamentoDocumento = "00";
            identificacao.CondicaoDePagamentoDocumento = "00";
            identificacao.NumeroDocumento = "3010";
            identificacao.StatusOrbit = "0";
            identificacao.SerieDocumento = "1";
            identificacao.TipoTributacaoNFSe = "T - Tributado no Municipio";
            identificacao.NaturezaOperacaoDocumento = "Serviço";

            //Filial
            Filial Filial = new Filial();
            Filial.AdressTypeFilial = "RUA";
            Filial.CidadeFilial = "SAO PAULO";
            Filial.CNPJFilial = "03.030.482/0001-10";
            Filial.MunicipioFilial = "Sao Caetano do Sul";
            Filial.BairroFilial = "Santa Paula";
            Filial.InscIeFilial = "928191";
            Filial.RazaoSocialFilial = "RAZAO SOCIAL - Filial";
            Filial.UFFilial = "SP";
            Filial.LogradouroFilial = "MARECHAL DEODRO";
            Filial.NumeroLogradouroFilial = "580";
            Filial.CEPFilial = "09541-300";

            //Parceiro
            Parceiro Parceiro = new Parceiro();
            Parceiro.TipoLogradouroParceiro = "AV";
            Parceiro.CidadeParceiro = "RIO DE JANEIRO";
            Parceiro.CnpjParceiro = "480.291.209/0001-23";
            Parceiro.CodigoParceiro = "C000001";
            Parceiro.MunicipioParceiro = "SANTO ANDRE";
            Parceiro.CpfParceiro = "466.844.828-14";
            Parceiro.BairroParceiro = "JD COTIANA";
            Parceiro.InscIeParceiro = "2138291";
            Parceiro.MunicipioParceiro = "129321";
            Parceiro.RazaoSocialParceiro = "RAZAO SOCIAL - Parceiro";
            Parceiro.UFParceiro = "RJ";
            Parceiro.LogradouroParceiro = "ADOLFO DE MENEZES";
            Parceiro.NumeroLogradouroParceiro = "27";

            //Item
            CabecalhoLinha line = new CabecalhoLinha();
            line.PorcentagemIBPTLinha = 15.53;
            line.CodigoItem = "0001";
            line.CSTCofinsLinha = "01";
            line.CSTPisLinha = "01";
            line.ValorTotalDescontoLinha = 0;
            line.SimOuNaoRetidoLinha = "Y";
            line.ValorUnitarioLinha = 50.00;
            line.QuantidadeLinha = 1;
            line.ItemLinhaDocumento = 0;
            line.ValorTotalLinnha = 50;

            //Impostos Não Retidos
            List<ImpostoLinha> listImpostoLinha = new List<ImpostoLinha>();
            //PIS
            ImpostoLinha impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "PIS";
            impostoLinha.PorcentagemImposto = 0.65;
            impostoLinha.TipoImpostoOrbit = "-8";
            impostoLinha.ValorImposto = 0.33;
            listImpostoLinha.Add(impostoLinha);
            //COFINS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "COFINS";
            impostoLinha.PorcentagemImposto = 3;
            impostoLinha.TipoImpostoOrbit = "-10";
            impostoLinha.ValorImposto = 1.50;
            listImpostoLinha.Add(impostoLinha);
            //ISS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "ISS";
            impostoLinha.PorcentagemImposto = 5;
            impostoLinha.TipoImpostoOrbit = "-7";
            impostoLinha.ValorImposto = 2.50;
            listImpostoLinha.Add(impostoLinha);

            line.ImpostoLinha = listImpostoLinha;

            //Impostos Retidos
            List<ImpostoRetidoLinha> listImpostoRetidoLinha = new List<ImpostoRetidoLinha>();

            //IRRF
            ImpostoRetidoLinha impostoRetidoLinha = new ImpostoRetidoLinha();
            impostoRetidoLinha.PorcentagemImpostoRetido = 1.50;
            impostoRetidoLinha.TipoImpostoOrbit = "3";
            impostoRetidoLinha.ValorImpostoRetido = 0.75;
            listImpostoRetidoLinha.Add(impostoRetidoLinha);

            //CSLL
            impostoRetidoLinha = new ImpostoRetidoLinha();
            impostoRetidoLinha.PorcentagemImpostoRetido = 1.00;
            impostoRetidoLinha.TipoImpostoOrbit = "4";
            impostoRetidoLinha.ValorImpostoRetido = 0.50;
            listImpostoRetidoLinha.Add(impostoRetidoLinha);
            line.ImpostoRetidoLinha = listImpostoRetidoLinha;

            invoice.AddItemLine(line);
            line = new CabecalhoLinha();
            line.PorcentagemIBPTLinha = 15.53;
            line.CodigoItem = "0001";
            line.CSTCofinsLinha = "01";
            line.CSTPisLinha = "01";
            line.ValorTotalDescontoLinha = 0;
            line.SimOuNaoRetidoLinha = "Y";
            line.ValorUnitarioLinha = 50.00;
            line.QuantidadeLinha = 1;
            line.ItemLinhaDocumento = 0;
            line.ValorTotalLinnha = 50;

            //Impostos Não Retidos
            listImpostoLinha = new List<ImpostoLinha>();
            //PIS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "PIS";
            impostoLinha.PorcentagemImposto = 0.65;
            impostoLinha.TipoImpostoOrbit = "-8";
            impostoLinha.ValorImposto = 0.33;
            listImpostoLinha.Add(impostoLinha);
            //COFINS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "COFINS";
            impostoLinha.PorcentagemImposto = 3;
            impostoLinha.TipoImpostoOrbit = "-10";
            impostoLinha.ValorImposto = 1.50;
            listImpostoLinha.Add(impostoLinha);
            //ISS
            impostoLinha = new ImpostoLinha();
            impostoLinha.NomeImposto = "ISS";
            impostoLinha.PorcentagemImposto = 5;
            impostoLinha.TipoImpostoOrbit = "-7";
            impostoLinha.ValorImposto = 2.50;
            listImpostoLinha.Add(impostoLinha);

            line.ImpostoLinha = listImpostoLinha;

            //Impostos Retidos
            listImpostoRetidoLinha = new List<ImpostoRetidoLinha>();
            //IRRF
            impostoRetidoLinha = new ImpostoRetidoLinha();
            impostoRetidoLinha.PorcentagemImpostoRetido = 1.50;
            impostoRetidoLinha.TipoImpostoOrbit = "3";
            impostoRetidoLinha.ValorImpostoRetido = 0.75;
            listImpostoRetidoLinha.Add(impostoRetidoLinha);
            //CSLL
            impostoRetidoLinha = new ImpostoRetidoLinha();
            impostoRetidoLinha.PorcentagemImpostoRetido = 1.00;
            impostoRetidoLinha.TipoImpostoOrbit = "4";
            impostoRetidoLinha.ValorImpostoRetido = 0.50;
            listImpostoRetidoLinha.Add(impostoRetidoLinha);
            line.ImpostoRetidoLinha = listImpostoRetidoLinha;

            invoice.AddItemLine(line);

            return invoice;
        }
    }
}

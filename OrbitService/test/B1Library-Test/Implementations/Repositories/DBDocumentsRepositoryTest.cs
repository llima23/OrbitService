using B1Library.Documents;
using B1Library.Documents.Repositories;
using B1Library.Implementations.Repositories;
using Moq;
using OrbitLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace B1Library_Tests.Repository
{
    public class DBDocumentsRepositoryTest
    {
        private const string oinv_viewname = "ORBIT_INVOICE_OUT_VW";
        private const string odln_viewname = "ORBIT_DELIVERY_OUT_VW";
        private const string opch_viewname = "ORBIT_PURCHASEINVOICE_OUT_VW";
        private const string opdn_viewname = "ORBIT_PURCHASEDELIVERYNOTES_OUT_VW";

        private DataSet oinvInvoices;
        private DataSet odlnInvoices;
        private DataSet opchInvoices;
        private DataSet opdnInvoices;

        private DataSet VerifyoinvInvoices;
        private DataSet VerifyodlnInvoices;
        private DataSet VerifyopchInvoices;
        private DataSet VerifyopdnInvoices;

        private DBDocumentsRepository cut;
        private Mock<IWrapper> mockWrapper;

        private QueryViewsB1 queryViewsB1;
        public DBDocumentsRepositoryTest()
        {
            mockWrapper = new Mock<IWrapper>();
            cut = new DBDocumentsRepository(mockWrapper.Object,"");
            queryViewsB1 = new QueryViewsB1();
            oinvInvoices = createFakeInvoicesDataSet(oinv_viewname);
            odlnInvoices = createFakeInvoicesDataSet(odln_viewname);
            opchInvoices = createFakeInvoicesDataSet(opch_viewname);
            opdnInvoices = createFakeInvoicesDataSet(opdn_viewname);

            VerifyoinvInvoices = createFakeVerifyDataSet(oinv_viewname);
            VerifyodlnInvoices = createFakeVerifyDataSet(odln_viewname);
            VerifyopchInvoices = createFakeVerifyDataSet(opch_viewname);
            VerifyopdnInvoices = createFakeVerifyDataSet(opdn_viewname);


        }

        #region SETUP
        private void setupWrappperQueries()
        {
            mockWrapper
                .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(oinv_viewname)))
                .Returns(oinvInvoices);
            mockWrapper
                .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(odln_viewname)))
                .Returns(odlnInvoices);
            mockWrapper
                .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opch_viewname)))
                .Returns(opchInvoices);
            mockWrapper
                .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opdn_viewname)))
                .Returns(opdnInvoices);

            queryViewsB1.invoiceCANCELED = "N";
            queryViewsB1.codigoIntegracaoOrbit = (int)StatusCode.NotasParaEmitir;
            mockWrapper
              .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)))
              .Returns(oinvInvoices);



            string commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{oinv_viewname}'";
            mockWrapper
                .Setup(m => m.ExecuteQuery(commandQueryExpected))
                .Returns(VerifyoinvInvoices);

            commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{odln_viewname}'";
            mockWrapper
               .Setup(m => m.ExecuteQuery(commandQueryExpected))
               .Returns(VerifyodlnInvoices);

            commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{opch_viewname}'";
            mockWrapper
               .Setup(m => m.ExecuteQuery(commandQueryExpected))
               .Returns(VerifyopchInvoices);

            commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{opdn_viewname}'";
            mockWrapper
               .Setup(m => m.ExecuteQuery(commandQueryExpected))
               .Returns(VerifyopdnInvoices);

        }

        private void setupWrapperQueriesToCancel()
        {
            queryViewsB1.invoiceCANCELED = "Y";
            queryViewsB1.codigoIntegracaoOrbit = (int)StatusCode.Sucess;
            mockWrapper
           .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)))
           .Returns(oinvInvoices);

            queryViewsB1.invoiceCANCELED = "Y";
            queryViewsB1.codigoIntegracaoOrbit = (int)StatusCode.Sucess;
            mockWrapper
           .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFSe(oinv_viewname)))
           .Returns(oinvInvoices);


            string commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{oinv_viewname}'";
            mockWrapper
                .Setup(m => m.ExecuteQuery(commandQueryExpected))
                .Returns(VerifyoinvInvoices);

        }


        private DataRow ReturnFakeDataRow(DataTable table)
        {
            DataRow dataRow = table.NewRow();
            dataRow["JSONRESULT"] = ReturnJsonResultB1();
            return dataRow;
        }

        private string ReturnJsonResultB1()
        {
            string result = @"
[{""CabecalhoLinha"":""[{\""CSTCofinsLinha\"":\""50\"",\""CSTICMSLinha\"":\""00\"",\""CSTIPILinha\"":\""00\"",\""CSTPisLinha\"":\""50\"",\""CodigoCFOP\"":\""3101\"",\""CodigoDeBarras\"":\""\"",\""CodigoItem\"":\""P45\"",\""CodigoNCM\"":\""2711.21.00\"",\""CodigoServicoLinha\"":\""\"",\""DescricaoItemLinhaDocumento\"":\""P45\"",\""DespesaAdicional\"":\""[{\""TipoDespesa\"":\""1\"",\""ValorUnitarioDespesa\"":100.000000},{\""TipoDespesa\"":\""3\"",\""ValorUnitarioDespesa\"":100.000000}]\"",\""IdLocalDestino\"":\""3\"",\""ImpostoLinha\"":\""[{\""NomeImposto\"":\""COFINS-IMP\"",\""PorcentagemImposto\"":7.600000,\""TipoImpostoOrbit\"":\""-10\"",\""ValorBaseImposto\"":1388.710000,\""ValorImposto\"":105.540000},{\""NomeImposto\"":\""PIS-IMP\"",\""PorcentagemImposto\"":0.000000,\""TipoImpostoOrbit\"":\""-8\"",\""ValorBaseImposto\"":0.000000,\""ValorImposto\"":0.000000},{\""NomeImposto\"":\""IPI-IMP\"",\""PorcentagemImposto\"":0.000000,\""TipoImpostoOrbit\"":\""-4\"",\""ValorBaseImposto\"":0.000000,\""ValorImposto\"":0.000000},{\""NomeImposto\"":\""II\"",\""PorcentagemImposto\"":0.000000,\""TipoImpostoOrbit\"":\""-11\"",\""ValorBaseImposto\"":100.000000,\""ValorImposto\"":0.000000},{\""NomeImposto\"":\""ICMS-IMP\"",\""PorcentagemImposto\"":18.000000,\""TipoImpostoOrbit\"":\""-6\"",\""ValorBaseImposto\"":121.950000,\""ValorImposto\"":21.950000},{\""NomeImposto\"":\""COFINS-IMP\"",\""PorcentagemImposto\"":0.000000,\""TipoImpostoOrbit\"":\""-10\"",\""ValorBaseImposto\"":0.000000,\""ValorImposto\"":0.000000},{\""NomeImposto\"":\""PIS-IMP\"",\""PorcentagemImposto\"":1.650000,\""TipoImpostoOrbit\"":\""-8\"",\""ValorBaseImposto\"":138.870000,\""ValorImposto\"":2.290000},{\""NomeImposto\"":\""IPI-IMP\"",\""PorcentagemImposto\"":4.000000,\""TipoImpostoOrbit\"":\""-4\"",\""ValorBaseImposto\"":114.000000,\""ValorImposto\"":4.560000},{\""NomeImposto\"":\""II\"",\""PorcentagemImposto\"":14.000000,\""TipoImpostoOrbit\"":\""-11\"",\""ValorBaseImposto\"":100.000000,\""ValorImposto\"":14.000000},{\""NomeImposto\"":\""ICMS-IMP\"",\""PorcentagemImposto\"":18.000000,\""TipoImpostoOrbit\"":\""-6\"",\""ValorBaseImposto\"":159.570000,\""ValorImposto\"":28.720000},{\""NomeImposto\"":\""COFINS-IMP\"",\""PorcentagemImposto\"":7.600000,\""TipoImpostoOrbit\"":\""-10\"",\""ValorBaseImposto\"":138.870000,\""ValorImposto\"":10.550000},{\""NomeImposto\"":\""PIS-IMP\"",\""PorcentagemImposto\"":1.650000,\""TipoImpostoOrbit\"":\""-8\"",\""ValorBaseImposto\"":1388.710000,\""ValorImposto\"":22.910000},{\""NomeImposto\"":\""IPI-IMP\"",\""PorcentagemImposto\"":4.000000,\""TipoImpostoOrbit\"":\""-4\"",\""ValorBaseImposto\"":1140.000000,\""ValorImposto\"":45.600000},{\""NomeImposto\"":\""II\"",\""PorcentagemImposto\"":14.000000,\""TipoImpostoOrbit\"":\""-11\"",\""ValorBaseImposto\"":1000.000000,\""ValorImposto\"":140.000000},{\""NomeImposto\"":\""ICMS-IMP\"",\""PorcentagemImposto\"":18.000000,\""TipoImpostoOrbit\"":\""-6\"",\""ValorBaseImposto\"":1595.680000,\""ValorImposto\"":287.220000}]\"",\""ItemLinhaDocumento\"":0,\""NItem\"":\""1\"",\""OrigICMS\"":\""0\"",\""PorcentagemDiscontoLinha\"":0.000000,\""PorcentagemIBPTLinha\"":0.000000,\""QuantidadeLinha\"":1.000000,\""SimOuNaoRetidoLinha\"":\""N\"",\""UnidadeComercial\"":\""KG\"",\""ValorTotalLinnha\"":1000.000000,\""ValorUnitarioLinha\"":1000.000000,\""cEnq\"":\""999\""}]"",""CargaFiscal"":0,""CodInt"":""0"",""DocEntry"":137,""Duplicata"":""[{\""DataVencimento\"":\""2022-08-03 00:00:00.0000000\"",\""NumeroDuplicata\"":1,\""ValorDuplicata\"":1588.050000}]"",""Filial"":""{\""AdressTypeFilial\"":\""RUA\"",\""BairroFilial\"":\""Pinheiros\"",\""CEPFilial\"":\""05422-030\"",\""CNPJFilial\"":\""07.792.897/0001-82\"",\""CPFFilial\"":\""\"",\""CidadeFilial\"":\""São Paulo\"",\""CodigoIBGEMunicipioFilial\"":\""3550308\"",\""CodigoPaisFilial\"":\""1058\"",\""CodigoUFFilial\"":\""35\"",\""ComplementoFilial\"":\""\"",\""IndicadorIEFilial\"":\""1\"",\""InscIeFilial\"":\""129672815112\"",\""LogradouroFilial\"":\""CLAUDIO SOARES\"",\""MunicipioFilial\"":\""5344\"",\""NomePaisFilial\"":\""Brasil\"",\""NumeroLogradouroFilial\"":\""72\"",\""RazaoSocialFilial\"":\""FELLIPELLI INSTRUMENTOS DE DIAGNOSTICO\"",\""RegimeTributacaoFilial\"":-1,\""UFFilial\"":\""SP\""}"",""Identificacao"":""{\""BranchId\"":\""d1540357-5a30-41dd-bb6a-be3f7a363660\"",\""CondicaoDePagamentoDocumento\"":\""0\"",\""ConsumidorFinal\"":\""0\"",\""DadosCobranca\"":1588.050000,\""DataEmissao\"":\""2022-08-03 00:00:00.0000000\"",\""DataLancamento\"":\""2022-08-03 00:00:00.0000000\"",\""DocEntry\"":137,\""DocNum\"":31,\""DocTime\"":1055,\""DocumentoCancelado\"":\""N\"",\""FinalideDocumento\"":\""1\"",\""FormaDePagamentoDocumento\"":\""15\"",\""IdRetornoOrbit\"":\""\"",\""IndicadorIntermediario\"":0,\""IndicadorPresenca\"":\""9\"",\""Key\"":\""\"",\""NaturezaOperacaoDocumento\"":\""Compra Consumo\"",\""NumeroDocumento\"":\""34521\"",\""OperacaoNFe\"":\""1\"",\""SerieDocumento\"":\""1\"",\""StatusOrbit\"":\""Object reference not set to an instance of an object.\"",\""TipoOperacaoDocumento\"":\""\"",\""TipoTributacaoNFSe\"":\""\"",\""ValorTotalNF\"":1588.050000,\""Versao\"":\""4.00\""}"",""ModeloDocumento"":""55"",""TipoDocumento"":""input"",""TipoNF"":""1""},{""CabecalhoLinha"":""[{\""CSTCofinsLinha\"":\""50\"",\""CSTICMSLinha\"":\""00\"",\""CSTIPILinha\"":\""00\"",\""CSTPisLinha\"":\""50\"",\""CodigoCFOP\"":\""3101\"",\""CodigoDeBarras\"":\""\"",\""CodigoItem\"":\""P45\"",\""CodigoNCM\"":\""2711.21.00\"",\""CodigoServicoLinha\"":\""\"",\""DescricaoItemLinhaDocumento\"":\""P45\"",\""DespesaAdicional\"":\""[{\""TipoDespesa\"":\""1\"",\""ValorUnitarioDespesa\"":3177.780000},{\""TipoDespesa\"":\""3\"",\""ValorUnitarioDespesa\"":1480.790000}]\"",\""IdLocalDestino\"":\""3\"",\""ImpostoLinha\"":\""[{\""NomeImposto\"":\""PIS-IMP\"",\""PorcentagemImposto\"":0.000000,\""TipoImpostoOrbit\"":\""-8\"",\""ValorBaseImposto\"":0.000000,\""ValorImposto\"":0.000000},{\""NomeImposto\"":\""COFINS-IMP\"",\""PorcentagemImposto\"":7.600000,\""TipoImpostoOrbit\"":\""-10\"",\""ValorBaseImposto\"":15954.720000,\""ValorImposto\"":1212.560000},{\""NomeImposto\"":\""ICMS-IMP\"",\""PorcentagemImposto\"":18.000000,\""TipoImpostoOrbit\"":\""-6\"",\""ValorBaseImposto\"":18332.560000,\""ValorImposto\"":3299.860000},{\""NomeImposto\"":\""II\"",\""PorcentagemImposto\"":14.000000,\""TipoImpostoOrbit\"":\""-11\"",\""ValorBaseImposto\"":11488.890000,\""ValorImposto\"":1608.440000},{\""NomeImposto\"":\""IPI-IMP\"",\""PorcentagemImposto\"":4.000000,\""TipoImpostoOrbit\"":\""-4\"",\""ValorBaseImposto\"":13097.330000,\""ValorImposto\"":523.890000},{\""NomeImposto\"":\""PIS-IMP\"",\""PorcentagemImposto\"":1.650000,\""TipoImpostoOrbit\"":\""-8\"",\""ValorBaseImposto\"":15954.720000,\""ValorImposto\"":263.250000},{\""NomeImposto\"":\""COFINS-IMP\"",\""PorcentagemImposto\"":7.600000,\""TipoImpostoOrbit\"":\""-10\"",\""ValorBaseImposto\"":4413.010000,\""ValorImposto\"":335.390000},{\""NomeImposto\"":\""ICMS-IMP\"",\""PorcentagemImposto\"":18.000000,\""TipoImpostoOrbit\"":\""-6\"",\""ValorBaseImposto\"":5070.710000,\""ValorImposto\"":912.730000},{\""NomeImposto\"":\""II\"",\""PorcentagemImposto\"":14.000000,\""TipoImpostoOrbit\"":\""-11\"",\""ValorBaseImposto\"":3177.780000,\""ValorImposto\"":444.890000},{\""NomeImposto\"":\""IPI-IMP\"",\""PorcentagemImposto\"":4.000000,\""TipoImpostoOrbit\"":\""-4\"",\""ValorBaseImposto\"":3622.670000,\""ValorImposto\"":144.910000},{\""NomeImposto\"":\""PIS-IMP\"",\""PorcentagemImposto\"":1.650000,\""TipoImpostoOrbit\"":\""-8\"",\""ValorBaseImposto\"":4413.010000,\""ValorImposto\"":72.810000},{\""NomeImposto\"":\""COFINS-IMP\"",\""PorcentagemImposto\"":0.000000,\""TipoImpostoOrbit\"":\""-10\"",\""ValorBaseImposto\"":0.000000,\""ValorImposto\"":0.000000},{\""NomeImposto\"":\""ICMS-IMP\"",\""PorcentagemImposto\"":18.000000,\""TipoImpostoOrbit\"":\""-6\"",\""ValorBaseImposto\"":1805.840000,\""ValorImposto\"":325.050000},{\""NomeImposto\"":\""II\"",\""PorcentagemImposto\"":0.000000,\""TipoImpostoOrbit\"":\""-11\"",\""ValorBaseImposto\"":1480.790000,\""ValorImposto\"":0.000000},{\""NomeImposto\"":\""IPI-IMP\"",\""PorcentagemImposto\"":0.000000,\""TipoImpostoOrbit\"":\""-4\"",\""ValorBaseImposto\"":0.000000,\""ValorImposto\"":0.000000}]\"",\""ItemLinhaDocumento\"":0,\""NItem\"":\""1\"",\""OrigICMS\"":\""0\"",\""PorcentagemDiscontoLinha\"":0.000000,\""PorcentagemIBPTLinha\"":0.000000,\""QuantidadeLinha\"":6.000000,\""SimOuNaoRetidoLinha\"":\""N\"",\""UnidadeComercial\"":\""KG\"",\""ValorTotalLinnha\"":11488.890000,\""ValorUnitarioLinha\"":352.500000,\""cEnq\"":\""999\""}]"",""CargaFiscal"":0,""CodInt"":""0"",""DocEntry"":131,""Duplicata"":""[{\""DataVencimento\"":\""2022-08-02 00:00:00.0000000\"",\""NumeroDuplicata\"":1,\""ValorDuplicata\"":17141.310000}]"",""Filial"":""{\""AdressTypeFilial\"":\""RUA\"",\""BairroFilial\"":\""Pinheiros\"",\""CEPFilial\"":\""05422-030\"",\""CNPJFilial\"":\""07.792.897/0001-82\"",\""CPFFilial\"":\""\"",\""CidadeFilial\"":\""São Paulo\"",\""CodigoIBGEMunicipioFilial\"":\""3550308\"",\""CodigoPaisFilial\"":\""1058\"",\""CodigoUFFilial\"":\""35\"",\""ComplementoFilial\"":\""\"",\""IndicadorIEFilial\"":\""1\"",\""InscIeFilial\"":\""129672815112\"",\""LogradouroFilial\"":\""CLAUDIO SOARES\"",\""MunicipioFilial\"":\""5344\"",\""NomePaisFilial\"":\""Brasil\"",\""NumeroLogradouroFilial\"":\""72\"",\""RazaoSocialFilial\"":\""FELLIPELLI INSTRUMENTOS DE DIAGNOSTICO\"",\""RegimeTributacaoFilial\"":-1,\""UFFilial\"":\""SP\""}"",""Identificacao"":""{\""BranchId\"":\""d1540357-5a30-41dd-bb6a-be3f7a363660\"",\""CondicaoDePagamentoDocumento\"":\""0\"",\""ConsumidorFinal\"":\""0\"",\""DadosCobranca\"":17141.310000,\""DataEmissao\"":\""2022-08-02 00:00:00.0000000\"",\""DataLancamento\"":\""2022-08-02 00:00:00.0000000\"",\""DocEntry\"":131,\""DocNum\"":30,\""DocTime\"":1206,\""DocumentoCancelado\"":\""N\"",\""FinalideDocumento\"":\""1\"",\""FormaDePagamentoDocumento\"":\""15\"",\""IdRetornoOrbit\"":\""62e93df6e7685efe4f787aa8\"",\""IndicadorIntermediario\"":0,\""IndicadorPresenca\"":\""9\"",\""Key\"":\""35220807792897000182550010012443131000001318\"",\""NaturezaOperacaoDocumento\"":\""Compra Consumo\"",\""NumeroDocumento\"":\""1244313\"",\""OperacaoNFe\"":\""1\"",\""SerieDocumento\"":\""1\"",\""StatusOrbit\"":\""AUTORIZADA\"",\""TipoOperacaoDocumento\"":\""\"",\""TipoTributacaoNFSe\"":\""\"",\""ValorTotalNF\"":17141.310000,\""Versao\"":\""4.00\""}"",""ModeloDocumento"":""55"",""TipoDocumento"":""input"",""TipoNF"":""1""},{""CabecalhoLinha"":""[{\""CSTCofinsLinha\"":\""50\"",\""CSTICMSLinha\"":\""00\"",\""CSTIPILinha\"":\""00\"",\""CSTPisLinha\"":\""50\"",\""CodigoCFOP\"":\""1101\"",\""CodigoDeBarras\"":\""\"",\""CodigoItem\"":\""0001\"",\""CodigoNCM\"":\""4911.10.90\"",\""CodigoServicoLinha\"":\""\"",\""DescricaoItemLinhaDocumento\"":\""Baralho EQ-I\"",\""IdLocalDestino\"":\""1\"",\""ImpostoLinha\"":\""[{\""NomeImposto\"":\""COFINS\"",\""PorcentagemImposto\"":7.600000,\""TipoImpostoOrbit\"":\""-10\"",\""ValorBaseImposto\"":100.000000,\""ValorImposto\"":7.600000},{\""NomeImposto\"":\""PIS\"",\""PorcentagemImposto\"":1.650000,\""TipoImpostoOrbit\"":\""-8\"",\""ValorBaseImposto\"":100.000000,\""ValorImposto\"":1.650000},{\""NomeImposto\"":\""IPI\"",\""PorcentagemImposto\"":10.000000,\""TipoImpostoOrbit\"":\""-4\"",\""ValorBaseImposto\"":100.000000,\""ValorImposto\"":10.000000},{\""NomeImposto\"":\""ICMS\"",\""PorcentagemImposto\"":18.000000,\""TipoImpostoOrbit\"":\""-6\"",\""ValorBaseImposto\"":100.000000,\""ValorImposto\"":18.000000}]\"",\""ItemLinhaDocumento\"":0,\""NItem\"":\""1\"",\""OrigICMS\"":\""0\"",\""PorcentagemDiscontoLinha\"":0.000000,\""PorcentagemIBPTLinha\"":0.000000,\""QuantidadeLinha\"":1.000000,\""SimOuNaoRetidoLinha\"":\""N\"",\""UnidadeComercial\"":\""UN\"",\""ValorTotalLinnha\"":100.000000,\""ValorUnitarioLinha\"":100.000000,\""cEnq\"":\""999\""}]"",""CargaFiscal"":0,""CodInt"":""0"",""DocEntry"":34,""Duplicata"":""[{\""DataVencimento\"":\""2021-07-19 00:00:00.0000000\"",\""NumeroDuplicata\"":1,\""ValorDuplicata\"":110.000000}]"",""Filial"":""{\""AdressTypeFilial\"":\""RUA\"",\""BairroFilial\"":\""Pinheiros\"",\""CEPFilial\"":\""05422-030\"",\""CNPJFilial\"":\""07.792.897/0001-82\"",\""CPFFilial\"":\""\"",\""CidadeFilial\"":\""São Paulo\"",\""CodigoIBGEMunicipioFilial\"":\""3550308\"",\""CodigoPaisFilial\"":\""1058\"",\""CodigoUFFilial\"":\""35\"",\""ComplementoFilial\"":\""\"",\""IndicadorIEFilial\"":\""1\"",\""InscIeFilial\"":\""129672815112\"",\""LogradouroFilial\"":\""CLAUDIO SOARES\"",\""MunicipioFilial\"":\""5344\"",\""NomePaisFilial\"":\""Brasil\"",\""NumeroLogradouroFilial\"":\""72\"",\""RazaoSocialFilial\"":\""FELLIPELLI INSTRUMENTOS DE DIAGNOSTICO\"",\""RegimeTributacaoFilial\"":-1,\""UFFilial\"":\""SP\""}"",""Identificacao"":""{\""BranchId\"":\""d1540357-5a30-41dd-bb6a-be3f7a363660\"",\""CondicaoDePagamentoDocumento\"":\""0\"",\""ConsumidorFinal\"":\""0\"",\""DadosCobranca\"":110.000000,\""DataEmissao\"":\""2021-07-19 00:00:00.0000000\"",\""DataLancamento\"":\""2021-07-19 00:00:00.0000000\"",\""DocEntry\"":34,\""DocNum\"":9,\""DocTime\"":1108,\""DocumentoCancelado\"":\""N\"",\""FinalideDocumento\"":\""0\"",\""FormaDePagamentoDocumento\"":\""\"",\""IdRetornoOrbit\"":\""\"",\""IndicadorIntermediario\"":0,\""IndicadorPresenca\"":\""9\"",\""Key\"":\""\"",\""NaturezaOperacaoDocumento\"":\""\"",\""NumeroDocumento\"":\""123123123\"",\""OperacaoNFe\"":\""1\"",\""SerieDocumento\"":\""1\"",\""StatusOrbit\"":\""Data Retroativa das Configurações do 4Tax, inferior a data do Documento\"",\""TipoOperacaoDocumento\"":\""\"",\""TipoTributacaoNFSe\"":\""\"",\""ValorTotalNF\"":110.000000,\""Versao\"":\""4.00\""}"",""ModeloDocumento"":""55"",""TipoDocumento"":""input"",""TipoNF"":""1""}]";
            return result;
        }
        #endregion
        private DataSet createFakeVerifyDataSet(string ViewName)
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable("SYS.VIEWS");
            DataColumn column = new DataColumn();

            column.ColumnName = "SCHEMA_NAME";
            table.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "VIEW_NAME";
            table.Columns.Add(column);

            dataSet.Tables.Add(table);


            DataRow config = dataSet.Tables[0].NewRow();
            config["SCHEMA_NAME"] = cut.DataBaseName;
            config = dataSet.Tables[0].NewRow();
            config["VIEW_NAME"] = ViewName;

            dataSet.Tables[0].Rows.Add(config);

            return dataSet;
        }
        private DataSet createFakeInvoicesDataSet(string viewname)
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable(viewname);
            DataColumn column = new DataColumn();

            column.ColumnName = "JSONRESULT";
            column.DataType = Type.GetType("System.Object");
            table.Columns.Add(column);

            dataSet.Tables.Add(table);

            return dataSet;
        }

        private void addInvoiceToDataSet(DataSet dataset, DataRow dataRow)
        {
            dataset.Tables[0].Rows.Add(dataRow);
        }

        [Fact]
        public void ShouldGetInvoicesListWithoutDBEntries()
        {
            cut.DataBaseName = "NOMEBASE";
            setupWrappperQueries();

            List<Invoice> invoices = cut.GetInboundOtherDocuments();
            List<Invoice> invoicesOutboundNFe = cut.GetOutboundNFe();

            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(oinv_viewname)), Times.Once());
            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(odln_viewname)), Times.Once());
            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opch_viewname)), Times.Once());
            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opdn_viewname)), Times.Once());

            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)), Times.Once());
        }

        [Fact]
        public void ShouldGetInvoicesListFromAllTables()
        {
            cut.DataBaseName = "NOMEBASE";
            addInvoiceToDataSet(oinvInvoices, ReturnFakeDataRow(oinvInvoices.Tables[0]));
            addInvoiceToDataSet(odlnInvoices, ReturnFakeDataRow(odlnInvoices.Tables[0]));
            addInvoiceToDataSet(opchInvoices, ReturnFakeDataRow(opchInvoices.Tables[0]));
            addInvoiceToDataSet(opdnInvoices, ReturnFakeDataRow(opdnInvoices.Tables[0]));
            setupWrappperQueries();

            List<Invoice> invoices = cut.GetInboundOtherDocuments();
            Assert.True(invoices.Count > 3);

            List<Invoice> invoicesOutboundNFe = cut.GetOutboundNFe();
            Assert.True(invoicesOutboundNFe.Count > 0);


            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(oinv_viewname)), Times.Once());
            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(odln_viewname)), Times.Once());
            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opch_viewname)), Times.Once());
            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opdn_viewname)), Times.Once());
            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)), Times.Once());
        }



        [Fact]
        public void ShouldGetInvoicesToCancelListFromAllTables()
        {
            cut.DataBaseName = "NOMEBASE";
            addInvoiceToDataSet(oinvInvoices, ReturnFakeDataRow(oinvInvoices.Tables[0]));
            setupWrapperQueriesToCancel();

            List<Invoice> invoicesOutboundNFeCancel = cut.GetCancelOutboundNFe();
            Assert.True(invoicesOutboundNFeCancel.Count > 0);

            List<Invoice> invoicesOutboundNFSeCancel = cut.GetCancelOutboundNFSe();
            Assert.True(invoicesOutboundNFSeCancel.Count > 0);


            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)), Times.Once());
            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFSe(oinv_viewname)), Times.Once());
        }

        [Fact]
        public void ShouldUpdateB1DocumentWithSuccessStatus()
        {
            string IdOrbit = "";
            string DescricaoErro = "";
            string StatusOrbit = "";
            int ObjetoB1 = 0;
            int docEntry = 12345678;

            DocumentStatus documentData = new DocumentStatus(IdOrbit, StatusOrbit, DescricaoErro, ObjetoB1, docEntry, StatusCode.CargaFiscal);

            StringBuilder sb = new StringBuilder();
            //TODO: update command to dynamic generate table name
            sb.Append(@$"UPDATE OINV SET ");
            sb.AppendLine(@$"""U_TAX4_Stat"" = '{DocumentStatus.StatusMessageCargaFiscalEfetuada}'");
            sb.AppendLine(@$",""U_TAX4_CodInt"" = '{(int)StatusCode.CargaFiscal}'");
            sb.AppendLine(@$",""U_TAX4_IdRet"" = '{IdOrbit}' ");
            sb.AppendLine(@$"WHERE ");
            sb.AppendLine(@$"""DocEntry"" = {documentData.DocEntry}");
            string updateCommand = sb.ToString();

            mockWrapper
                .Setup(m => m.ExecuteNonQuery(updateCommand))
                .Returns(1);

            int updateResult = cut.UpdateDocumentStatus(documentData);
            Assert.Equal(1, updateResult);

            mockWrapper
                .Verify(m => m.ExecuteNonQuery(updateCommand), Times.Once());
        }

        [Fact]
        public void ShouldVerifyIfViewsExistTrueHANA()
        {
            cut.DataBaseName = "PROD_QUIMICA_CMV";
            string ViewName = "ORBIT_PURCHASEINVOICE_OUT_VW";
            //Controlled result expected
            DataSet dsResult = new DataSet();
            DataTable table = new DataTable("SYS.VIEWS");

            DataColumn column = new DataColumn();
            column.ColumnName = "SCHEMA_NAME";
            table.Columns.Add(column);
            column = new DataColumn();
            column.ColumnName = "VIEW_NAME";
            table.Columns.Add(column);

            dsResult.Tables.Add(table);

            DataRow config = dsResult.Tables[0].NewRow();
            config["SCHEMA_NAME"] = cut.DataBaseName;
            config = dsResult.Tables[0].NewRow();
            config["VIEW_NAME"] = ViewName;

            dsResult.Tables[0].Rows.Add(config);

            //Returns the result expected
            string commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{ViewName}'";            

            mockWrapper
                .Setup(m => m.ExecuteQuery(commandQueryExpected))
                .Returns(dsResult);

            bool result = cut.VerifyIfViewsExistHANA(ViewName);
            Assert.True(result);
        }
        [Fact]
        public void ShouldVerifyIfViewsExistFalseHANA()
        {
            cut.DataBaseName = "PROD_QUIMICA_CMV";
            string ViewName = "ORBIT_PURCHASEINVOICE_OUT_VW";
            //Controlled result expected
            DataSet dsResult = new DataSet();
            DataTable table = new DataTable("SYS.VIEWS");

            DataColumn column = new DataColumn();
            column.ColumnName = "SCHEMA_NAME";
            table.Columns.Add(column);
            column = new DataColumn();
            column.ColumnName = "VIEW_NAME";
            table.Columns.Add(column);

            dsResult.Tables.Add(table);

            //Returns the result expected
            string commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{ViewName}'";
            mockWrapper
                .Setup(m => m.ExecuteQuery(commandQueryExpected))
                .Returns(dsResult);

            bool result = cut.VerifyIfViewsExistHANA(ViewName);
            Assert.False(result);
        }

    }
}

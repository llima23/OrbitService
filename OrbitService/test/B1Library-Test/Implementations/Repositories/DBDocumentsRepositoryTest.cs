//using B1Library.Documents;
//using B1Library.Documents.Repositories;
//using B1Library.Implementations.Repositories;
//using Moq;
//using OrbitLibrary.Data;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Text;
//using Xunit;

//namespace B1Library_Tests.Repository
//{
//    public class DBDocumentsRepositoryTest
//    {
//        private const string oinv_viewname = "ORBIT_INVOICE_OUT_VW";
//        private const string odln_viewname = "ORBIT_DELIVERY_OUT_VW";
//        private const string opch_viewname = "ORBIT_PURCHASEINVOICE_OUT_VW";
//        private const string opdn_viewname = "ORBIT_PURCHASEDELIVERYNOTES_OUT_VW";

//        private DataSet oinvInvoices;
//        private DataSet odlnInvoices;
//        private DataSet opchInvoices;
//        private DataSet opdnInvoices;

//        private DataSet VerifyoinvInvoices;
//        private DataSet VerifyodlnInvoices;
//        private DataSet VerifyopchInvoices;
//        private DataSet VerifyopdnInvoices;

//        private DBDocumentsRepository cut;
//        private Mock<IWrapper> mockWrapper;

//        private QueryViewsB1 queryViewsB1;
//        public DBDocumentsRepositoryTest()
//        {
//            mockWrapper = new Mock<IWrapper>();
//            cut = new DBDocumentsRepository(mockWrapper.Object);
//            queryViewsB1 = new QueryViewsB1();
//            oinvInvoices = createFakeInvoicesDataSet(oinv_viewname);
//            odlnInvoices = createFakeInvoicesDataSet(odln_viewname);
//            opchInvoices = createFakeInvoicesDataSet(opch_viewname);
//            opdnInvoices = createFakeInvoicesDataSet(opdn_viewname);

//            VerifyoinvInvoices = createFakeVerifyDataSet(oinv_viewname);
//            VerifyodlnInvoices = createFakeVerifyDataSet(odln_viewname);
//            VerifyopchInvoices = createFakeVerifyDataSet(opch_viewname);
//            VerifyopdnInvoices = createFakeVerifyDataSet(opdn_viewname);


//        }

//        #region SETUP
//        private void setupWrappperQueries()
//        {
//            mockWrapper
//                .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(oinv_viewname)))
//                .Returns(oinvInvoices);
//            mockWrapper
//                .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(odln_viewname)))
//                .Returns(odlnInvoices);
//            mockWrapper
//                .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opch_viewname)))
//                .Returns(opchInvoices);
//            mockWrapper
//                .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opdn_viewname)))
//                .Returns(opdnInvoices);

//            queryViewsB1.invoiceCANCELED = "N";
//            queryViewsB1.codigoIntegracaoOrbit = (int)StatusCode.NotasParaEmitir;
//            mockWrapper
//              .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)))
//              .Returns(oinvInvoices);



//            string commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{oinv_viewname}'";
//            mockWrapper
//                .Setup(m => m.ExecuteQuery(commandQueryExpected))
//                .Returns(VerifyoinvInvoices);

//            commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{odln_viewname}'";
//            mockWrapper
//               .Setup(m => m.ExecuteQuery(commandQueryExpected))
//               .Returns(VerifyodlnInvoices);

//            commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{opch_viewname}'";
//            mockWrapper
//               .Setup(m => m.ExecuteQuery(commandQueryExpected))
//               .Returns(VerifyopchInvoices);

//            commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{opdn_viewname}'";
//            mockWrapper
//               .Setup(m => m.ExecuteQuery(commandQueryExpected))
//               .Returns(VerifyopdnInvoices);

//        }

//        private void setupWrapperQueriesToCancel()
//        {
//            queryViewsB1.invoiceCANCELED = "Y";
//            queryViewsB1.codigoIntegracaoOrbit = (int)StatusCode.Sucess;
//            mockWrapper
//           .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)))
//           .Returns(oinvInvoices);

//            queryViewsB1.invoiceCANCELED = "Y";
//            queryViewsB1.codigoIntegracaoOrbit = (int)StatusCode.Sucess;
//            mockWrapper
//           .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFSe(oinv_viewname)))
//           .Returns(oinvInvoices);


//            queryViewsB1.invoiceCANCELED = "Y";
//            mockWrapper
//           .Setup(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFSeToInutil(oinv_viewname)))
//           .Returns(oinvInvoices);


//            string commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{oinv_viewname}'";
//            mockWrapper
//                .Setup(m => m.ExecuteQuery(commandQueryExpected))
//                .Returns(VerifyoinvInvoices);

//        }

//        private DataRow ReturnFakeDataRow(DataTable table)
//        {
//            DataRow dataRow = table.NewRow();
//            dataRow["JSONRESULT"] = ReturnJsonResultB1();
//            return dataRow;
//        }

//        private string ReturnJsonResultB1()
//        {
//            string result = @"
//                           [{""CANCELED"":""Y"",""CabecalhoLinha"":""[{\""CSTCofinsLinha\"":\""01\"",\""CSTICMSLinha\"":\""\"",\""CSTIPILinha\"":\""\"",\""CSTPisLinha\"":\""01\"",\""CodigoCFOP\"":\""5933\"",\""CodigoDeBarras\"":\""\"",\""CodigoItem\"":\""SV0000000002\"",\""CodigoNCM\"":\""\"",\""CodigoServicoLinha\"":\""\"",\""CodigoTributacaoMuncipio\"":\""\"",\""DescricaoItemLinhaDocumento\"":\""REPASSES\"",\""IdLocalDestino\"":\""1\"",\""ImpostoLinha\"":\""[{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""COFINS\"",\""PartilhaInterestadual\"":0.000000,\""PorcentagemImposto\"":0.000000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""\"",\""ValorBaseImposto\"":0.000000,\""ValorImposto\"":0.000000},{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""ISS\"",\""PartilhaInterestadual\"":0.000000,\""PorcentagemImposto\"":0.000000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""-7\"",\""ValorBaseImposto\"":0.000000,\""ValorImposto\"":0.000000},{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""PIS\"",\""PartilhaInterestadual\"":0.000000,\""PorcentagemImposto\"":0.000000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""\"",\""ValorBaseImposto\"":0.000000,\""ValorImposto\"":0.000000},{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""COFINS\"",\""PartilhaInterestadual\"":0.000000,\""PorcentagemImposto\"":3.000000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""\"",\""ValorBaseImposto\"":-26.190000,\""ValorImposto\"":0.000000},{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""ISS\"",\""PartilhaInterestadual\"":0.000000,\""PorcentagemImposto\"":2.900000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""-7\"",\""ValorBaseImposto\"":-26.190000,\""ValorImposto\"":0.000000},{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""PIS\"",\""PartilhaInterestadual\"":0.000000,\""PorcentagemImposto\"":0.650000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""\"",\""ValorBaseImposto\"":-26.190000,\""ValorImposto\"":0.000000}]\"",\""ItemLinhaDocumento\"":1,\""ItemListaServico\"":\""\"",\""MotivoDesoneracao\"":\""\"",\""NItem\"":\""2\"",\""OrigICMS\"":\""\"",\""PorcentagemIBPTLinha\"":0.000000,\""QuantidadeLinha\"":1.000000,\""SimOuNaoRetidoLinha\"":\""N\"",\""UnidadeComercial\"":\""\"",\""ValorTotalDescontoLinha\"":0,\""ValorTotalLinnha\"":2791.46,\""ValorUnitarioLinha\"":2791.460000,\""cEnq\"":\""999\""},{\""CSTCofinsLinha\"":\""01\"",\""CSTICMSLinha\"":\""\"",\""CSTIPILinha\"":\""\"",\""CSTPisLinha\"":\""01\"",\""CodigoCFOP\"":\""5933\"",\""CodigoDeBarras\"":\""\"",\""CodigoItem\"":\""SV0000000001\"",\""CodigoNCM\"":\""\"",\""CodigoServicoLinha\"":\""\"",\""CodigoTributacaoMuncipio\"":\""02800\"",\""DescricaoItemLinhaDocumento\"":\""SERVICO DE LICENCIAMENTO DE SOFTWARE ATRAVES DE APLICATIVO\"",\""IdLocalDestino\"":\""1\"",\""ImpostoLinha\"":\""[{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""COFINS\"",\""PartilhaInterestadual\"":0.000000,\""PorcentagemImposto\"":3.000000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""\"",\""ValorBaseImposto\"":283.820000,\""ValorImposto\"":8.510000},{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""ISS\"",\""PartilhaInterestadual\"":0.000000,\""PorcentagemImposto\"":2.900000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""-7\"",\""ValorBaseImposto\"":283.820000,\""ValorImposto\"":8.230000},{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""PIS\"",\""PartilhaInterestadual\"":0.000000,\""PorcentagemImposto\"":0.650000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""\"",\""ValorBaseImposto\"":283.820000,\""ValorImposto\"":1.840000}]\"",\""ItemLinhaDocumento\"":0,\""ItemListaServico\"":\""02800\"",\""MotivoDesoneracao\"":\""\"",\""NItem\"":\""1\"",\""OrigICMS\"":\""\"",\""PorcentagemIBPTLinha\"":0.000000,\""QuantidadeLinha\"":1.000000,\""SimOuNaoRetidoLinha\"":\""N\"",\""UnidadeComercial\"":\""\"",\""ValorTotalDescontoLinha\"":0,\""ValorTotalLinnha\"":283.82,\""ValorUnitarioLinha\"":283.820000,\""cEnq\"":\""999\""}]"",""CargaFiscal"":28,""CodInt"":""0"",""DocEntry"":1805320,""Duplicata"":""[{\""DataVencimento\"":\""2022-09-30 00:00:00.0000000\"",\""NumeroDuplicata\"":1,\""ValorDuplicata\"":2791.460000}]"",""Filial"":""{\""AdressTypeFilial\"":\""R\"",\""BairroFilial\"":\""Vila Olimpia\"",\""CEPFilial\"":\""04547-130\"",\""CNPJFilial\"":\""34.697.707/0001-10\"",\""CPFFilial\"":\""\"",\""CidadeFilial\"":\""SAO PAULO\"",\""CodigoIBGEMunicipioFilial\"":\""3550308\"",\""CodigoPaisFilial\"":\""1058\"",\""CodigoUFFilial\"":\""35\"",\""ComplementoFilial\"":\""\"",\""IndicadorIEFilial\"":\""1\"",\""InscIeFilial\"":\""Isento\"",\""LogradouroFilial\"":\""Al Vicente Pinzon\"",\""MunicipioFilial\"":\""5344\"",\""NomePaisFilial\"":\""Brasil\"",\""NumeroLogradouroFilial\"":\""54\"",\""RazaoSocialFilial\"":\""GRINGO O MELHOR AMIGO DO MOTORISTA LTDA\"",\""RegimeTributacaoFilial\"":2,\""UFFilial\"":\""SP\""}"",""Identificacao"":""{\""BranchId\"":\""31d3ff0c-72e6-4a39-8445-3cf0c4d3abad\"",\""CondicaoDePagamentoDocumento\"":\""0\"",\""ConsumidorFinal\"":\""0\"",\""DadosCobranca\"":2791.460000,\""DataEmissao\"":\""2022-09-30 00:00:00.0000000\"",\""DataLancamento\"":\""2022-09-30 00:00:00.0000000\"",\""DocEntry\"":1805320,\""DocNum\"":1095923,\""DocTime\"":1257,\""DocumentoCancelado\"":\""Y\"",\""FinalideDocumento\"":\""0\"",\""FormaDePagamentoDocumento\"":\""\"",\""IdRetornoOrbit\"":\""633b0661a7682a5ce02736ae\"",\""IncentivadorCultural\"":\""2\"",\""IndicadorIntermediario\"":0,\""IndicadorPresenca\"":\""9\"",\""Key\"":\""\"",\""NaturezaOperacaoDocumento\"":\""\"",\""NumeroDocumento\"":\""486327\"",\""NumeroProtocolo\"":\""\"",\""OperacaoNFe\"":\""1\"",\""SerieDocumento\"":\""2\"",\""StatusOrbit\"":\""rps.servico.codigoServico: Este campo é obrigatório\"",\""TipoOperacaoDocumento\"":\""\"",\""TipoTributacaoNFSe\"":\""T - Tributado no município\"",\""ValorTotalNF\"":3075.280000,\""Versao\"":\""4.00\""}"",""Justificativa"":"""",""ModeloDocumento"":""NFS-e"",""ObjetoB1"":13,""Parceiro"":""{\""BairroParceiro\"":\""Vila Olímpia\"",\""CEPParceiro\"":\""4547130\"",\""CidadeParceiro\"":\""São Paulo\"",\""CnpjParceiro\"":\""\"",\""CodigoIBGEMunicipioParceiro\"":\""3550308\"",\""CodigoPaisParceiro\"":\""1058\"",\""CodigoParceiro\"":\""M0000000338\"",\""CodigoUFParceiro\"":\""35\"",\""ComplementoParceiro\"":\""\"",\""CpfParceiro\"":\""169.564.248-19\"",\""EmailParceiro\"":\""gringo@gringo.com.vc\"",\""EnderecoParceiro\"":\""ALVicente Pinzon,54\r\r4547130-São Paulo-SP\rBRASIL\"",\""FoneParceiro\"":\""11960152743\"",\""IndicadorIEParceiro\"":\""\"",\""InscIeParceiro\"":\""\"",\""InscMunParceiro\"":\""\"",\""LogradouroParceiro\"":\""Vicente Pinzon\"",\""ModalidadeFrete\"":\""\"",\""MunicipioParceiro\"":\""5344\"",\""NomePaisParceiro\"":\""Brasil\"",\""NumeroLogradouroParceiro\"":\""54\"",\""RazaoSocialParceiro\"":\""Nelson Almeida neto\"",\""TipoLogradouroParceiro\"":\""AL\"",\""UFParceiro\"":\""SP\""},{\""BairroParceiro\"":\""Vila Olímpia\"",\""CEPParceiro\"":\""4547130\"",\""CidadeParceiro\"":\""São Paulo\"",\""CnpjParceiro\"":\""\"",\""CodigoIBGEMunicipioParceiro\"":\""3550308\"",\""CodigoPaisParceiro\"":\""1058\"",\""CodigoParceiro\"":\""M0000000338\"",\""CodigoUFParceiro\"":\""35\"",\""ComplementoParceiro\"":\""\"",\""CpfParceiro\"":\""169.564.248-19\"",\""EmailParceiro\"":\""gringo@gringo.com.vc\"",\""EnderecoParceiro\"":\""ALVicente Pinzon,54\r\r4547130-São Paulo-SP\rBRASIL\"",\""FoneParceiro\"":\""11960152743\"",\""IndicadorIEParceiro\"":\""\"",\""InscIeParceiro\"":\""\"",\""InscMunParceiro\"":\""\"",\""LogradouroParceiro\"":\""Vicente Pinzon\"",\""ModalidadeFrete\"":\""\"",\""MunicipioParceiro\"":\""5344\"",\""NomePaisParceiro\"":\""Brasil\"",\""NumeroLogradouroParceiro\"":\""54\"",\""RazaoSocialParceiro\"":\""Nelson Almeida neto\"",\""TipoLogradouroParceiro\"":\""AL\"",\""UFParceiro\"":\""SP\""}"",""TipoDocumento"":""output"",""TipoNF"":""1"",""U_TAX4_Cancelado"":""N""}]
//                            ";
//            return result;
//        }
//        #endregion
//        private DataSet createFakeVerifyDataSet(string ViewName)
//        {
//            DataSet dataSet = new DataSet();
//            DataTable table = new DataTable("SYS.VIEWS");
//            DataColumn column = new DataColumn();

//            column.ColumnName = "SCHEMA_NAME";
//            table.Columns.Add(column);

//            column = new DataColumn();
//            column.ColumnName = "VIEW_NAME";
//            table.Columns.Add(column);

//            dataSet.Tables.Add(table);


//            DataRow config = dataSet.Tables[0].NewRow();
//            config["SCHEMA_NAME"] = cut.DataBaseName;
//            config = dataSet.Tables[0].NewRow();
//            config["VIEW_NAME"] = ViewName;

//            dataSet.Tables[0].Rows.Add(config);

//            return dataSet;
//        }
//        private DataSet createFakeInvoicesDataSet(string viewname)
//        {
//            DataSet dataSet = new DataSet();
//            DataTable table = new DataTable(viewname);
//            DataColumn column = new DataColumn();

//            column.ColumnName = "JSONRESULT";
//            column.DataType = Type.GetType("System.Object");
//            table.Columns.Add(column);

//            dataSet.Tables.Add(table);

//            return dataSet;
//        }

//        private void addInvoiceToDataSet(DataSet dataset, DataRow dataRow)
//        {
//            dataset.Tables[0].Rows.Add(dataRow);
//        }

//        [Fact]
//        public void ShouldGetInvoicesListWithoutDBEntries()
//        {
//            cut.DataBaseName = "NOMEBASE";
//            setupWrappperQueries();

//            List<Invoice> invoices = cut.GetInboundOtherDocuments();
//            List<Invoice> invoicesOutboundNFe = cut.GetOutboundNFe();

//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(oinv_viewname)), Times.Once());
//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(odln_viewname)), Times.Once());
//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opch_viewname)), Times.Once());
//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opdn_viewname)), Times.Once());

//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)), Times.Once());
//        }

//        [Fact]
//        public void ShouldGetInvoicesListFromAllTables()
//        {
//            cut.DataBaseName = "NOMEBASE";
//            addInvoiceToDataSet(oinvInvoices, ReturnFakeDataRow(oinvInvoices.Tables[0]));
//            addInvoiceToDataSet(odlnInvoices, ReturnFakeDataRow(odlnInvoices.Tables[0]));
//            addInvoiceToDataSet(opchInvoices, ReturnFakeDataRow(opchInvoices.Tables[0]));
//            addInvoiceToDataSet(opdnInvoices, ReturnFakeDataRow(opdnInvoices.Tables[0]));
//            setupWrappperQueries();

//            List<Invoice> invoices = cut.GetInboundOtherDocuments();
//            Assert.True(invoices.Count > 3);

//            List<Invoice> invoicesOutboundNFe = cut.GetOutboundNFe();
//            Assert.True(invoicesOutboundNFe.Count > 0);


//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(oinv_viewname)), Times.Once());
//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(odln_viewname)), Times.Once());
//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opch_viewname)), Times.Once());
//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1InboundOtherDocuments(opdn_viewname)), Times.Once());
//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)), Times.Once());
//        }



//        [Fact]
//        public void ShouldGetInvoicesToCancelListFromAllTables()
//        {
//            cut.DataBaseName = "NOMEBASE";
//            addInvoiceToDataSet(oinvInvoices, ReturnFakeDataRow(oinvInvoices.Tables[0]));
//            setupWrapperQueriesToCancel();

//            List<Invoice> invoicesOutboundNFeCancel = cut.GetCancelOutboundNFe();
//            Assert.True(invoicesOutboundNFeCancel.Count > 0);

//            List<Invoice> invoicesOutboundNFSeCancel = cut.GetCancelOutboundNFSe();
//            Assert.True(invoicesOutboundNFSeCancel.Count > 0);


//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFe(oinv_viewname)), Times.Once());
//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFSe(oinv_viewname)), Times.Once());
//        }

//        [Fact]
//        public void ShouldGetInvoicesToInutilListFromAllTables()
//        {
//            cut.DataBaseName = "NOMEBASE";
//            addInvoiceToDataSet(oinvInvoices, ReturnFakeDataRow(oinvInvoices.Tables[0]));
//            setupWrapperQueriesToCancel();

//            List<Invoice> invoicesOutboundNFeCancel = cut.GetInutilOutboundNFSe();
//            Assert.True(invoicesOutboundNFeCancel.Count > 0);

//            mockWrapper.Verify(m => m.ExecuteQuery(queryViewsB1.ReturnQueryB1OutboundNFSeToInutil(oinv_viewname)), Times.Once());
//        }

//        [Fact]
//        public void ShouldUpdateB1DocumentWithSuccessStatus()
//        {
//            string IdOrbit = "";
//            string DescricaoErro = "";
//            string StatusOrbit = "";
//            int ObjetoB1 = 0;
//            int docEntry = 12345678;

//            DocumentStatus documentData = new DocumentStatus(IdOrbit, StatusOrbit, DescricaoErro, ObjetoB1, docEntry, StatusCode.CargaFiscal);

//            StringBuilder sb = new StringBuilder();
//            //TODO: update command to dynamic generate table name
//            sb.Append(@$"UPDATE OINV SET ");
//            sb.AppendLine(@$"""U_TAX4_Stat"" = '{DocumentStatus.StatusMessageCargaFiscalEfetuada}'");
//            sb.AppendLine(@$",""U_TAX4_CodInt"" = '{(int)StatusCode.CargaFiscal}'");
//            sb.AppendLine(@$",""U_TAX4_IdRet"" = '{IdOrbit}' ");
//            sb.AppendLine(@$"WHERE ");
//            sb.AppendLine(@$"""DocEntry"" = {documentData.DocEntry}");
//            string updateCommand = sb.ToString();

//            mockWrapper
//                .Setup(m => m.ExecuteNonQuery(updateCommand))
//                .Returns(1);

//            int updateResult = cut.UpdateDocumentStatus(documentData);
//            Assert.Equal(1, updateResult);

//            mockWrapper
//                .Verify(m => m.ExecuteNonQuery(updateCommand), Times.Once());
//        }

//        [Fact]
//        public void ShouldVerifyIfViewsExistTrueHANA()
//        {
//            cut.DataBaseName = "PROD_QUIMICA_CMV";
//            string ViewName = "ORBIT_PURCHASEINVOICE_OUT_VW";
//            //Controlled result expected
//            DataSet dsResult = new DataSet();
//            DataTable table = new DataTable("SYS.VIEWS");

//            DataColumn column = new DataColumn();
//            column.ColumnName = "SCHEMA_NAME";
//            table.Columns.Add(column);
//            column = new DataColumn();
//            column.ColumnName = "VIEW_NAME";
//            table.Columns.Add(column);

//            dsResult.Tables.Add(table);

//            DataRow config = dsResult.Tables[0].NewRow();
//            config["SCHEMA_NAME"] = cut.DataBaseName;
//            config = dsResult.Tables[0].NewRow();
//            config["VIEW_NAME"] = ViewName;

//            dsResult.Tables[0].Rows.Add(config);

//            //Returns the result expected
//            string commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{ViewName}'";            

//            mockWrapper
//                .Setup(m => m.ExecuteQuery(commandQueryExpected))
//                .Returns(dsResult);

//            bool result = cut.VerifyIfViewsExistHANA(ViewName);
//            Assert.True(result);
//        }
//        [Fact]
//        public void ShouldVerifyIfViewsExistFalseHANA()
//        {
//            cut.DataBaseName = "PROD_QUIMICA_CMV";
//            string ViewName = "ORBIT_PURCHASEINVOICE_OUT_VW";
//            //Controlled result expected
//            DataSet dsResult = new DataSet();
//            DataTable table = new DataTable("SYS.VIEWS");

//            DataColumn column = new DataColumn();
//            column.ColumnName = "SCHEMA_NAME";
//            table.Columns.Add(column);
//            column = new DataColumn();
//            column.ColumnName = "VIEW_NAME";
//            table.Columns.Add(column);

//            dsResult.Tables.Add(table);

//            //Returns the result expected
//            string commandQueryExpected = @$"select * from ""SYS"".""VIEWS"" where ""SCHEMA_NAME"" = '{cut.DataBaseName}' and ""VIEW_NAME"" = '{ViewName}'";
//            mockWrapper
//                .Setup(m => m.ExecuteQuery(commandQueryExpected))
//                .Returns(dsResult);

//            bool result = cut.VerifyIfViewsExistHANA(ViewName);
//            Assert.False(result);
//        }

//    }
//}

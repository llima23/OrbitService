using B1Library.Documents;
using B1Library.Documents.Entities;
using B1Library.Implementations.Repositories;
using B1Library.usecase;
using Moq;
using OrbitLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;
using static B1Library.Implementations.Repositories.DBTableNameRepository;

namespace B1Library_Tests.Implementations.Repositories
{
    public class DBDocumentsRepoTestWithSetupQuery
    {
        private DBDocumentsRepository cut;
        private Mock<IWrapper> mockWrapper;
        private DBTableNameRepository dBTableNameRepository;

        public DBDocumentsRepoTestWithSetupQuery()
        {
            dBTableNameRepository = new DBTableNameRepository();
            mockWrapper = new Mock<IWrapper>();
            cut = new DBDocumentsRepository(mockWrapper.Object);
        }
        [Fact]
        public void ShouldGetInboundOtherDocuments()
        {
            List<Invoice> listInvoice = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOtherDocuments)
            {
                DataSet invoices = createFakeInvoicesDataSet(tableName.TableHeader);
                addInvoiceToDataSet(invoices, ReturnFakeDataRow(invoices.Tables[0]));
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(cut, tableName, new UseCasesB1Library(UseCase.OtherDocuments));
                SetupVerifyFields(setupQueryB1);
                string teste = setupQueryB1.SetupQueryB1SendDocumentToOrbit();
                mockWrapper.Setup(m => m.ExecuteQuery(teste)).Returns(invoices);
                listInvoice = cut.GetInboundOtherDocuments();
            }
            Assert.True(listInvoice.Count > 3);
        }

        [Fact]
        public void ShouldGetInboundOtherDocumentsWithoutDbEntries()
        {
            List<Invoice> listInvoice = new List<Invoice>();
            foreach (TableName tableName in dBTableNameRepository.tableNamesOtherDocuments)
            {
                DataSet invoices = createFakeInvoicesDataSet(tableName.TableHeader);
                SetupQueryB1 setupQueryB1 = new SetupQueryB1(cut, tableName, new UseCasesB1Library(UseCase.OtherDocuments));
                SetupVerifyFields(setupQueryB1);
                string query = setupQueryB1.SetupQueryB1SendDocumentToOrbit();
                mockWrapper.Setup(m => m.ExecuteQuery(query)).Returns(invoices);
                listInvoice = cut.GetInboundOtherDocuments();
            }
        }


        private void addInvoiceToDataSet(DataSet dataset, DataRow dataRow)
        {
            dataset.Tables[0].Rows.Add(dataRow);
        }

        private DataSet createFakeInvoicesDataSet(string tableName)
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable(tableName);
            DataColumn column = new DataColumn();

            column.ColumnName = "JSONRESULT";
            column.DataType = System.Type.GetType("System.Object");
            table.Columns.Add(column);

            dataSet.Tables.Add(table);

            return dataSet;
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
                             [{""CANCELED"":""N"",""CabecalhoLinha"":""[{\""CSTCofinsLinha\"":\""01\"",\""CSTICMSLinha\"":\""51\"",\""CSTIPILinha\"":\""\"",\""CSTPisLinha\"":\""01\"",\""CodigoCFOP\"":\""5949\"",\""CodigoDeBarras\"":\""\"",\""CodigoItem\"":\""P13\"",\""CodigoNCM\"":\""2711.21.00\"",\""CodigoServicoLinha\"":\""\"",\""CodigoTributacaoMuncipio\"":\""\"",\""DescricaoItemLinhaDocumento\"":\""P13\"",\""IdLocalDestino\"":\""1\"",\""ImpostoLinha\"":\""[{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""COFINS\"",\""PorcentagemImposto\"":7.600000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""-10\"",\""ValorBaseImposto\"":100.000000,\""ValorImposto\"":7.600000},{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""ICMS - DIF\"",\""PorcentagemImposto\"":17.000000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""\"",\""ValorBaseImposto\"":100.000000,\""ValorImposto\"":11.330000},{\""AliquotaIntDestino\"":0.000000,\""MVast\"":0.000000,\""NomeImposto\"":\""PIS\"",\""PorcentagemImposto\"":1.650000,\""SimOuNaoDesoneracao\"":\""N\"",\""TipoImpostoOrbit\"":\""-8\"",\""ValorBaseImposto\"":100.000000,\""ValorImposto\"":1.650000}]\"",\""ItemLinhaDocumento\"":0,\""ItemListaServico\"":\""\"",\""MotivoDesoneracao\"":\""\"",\""NItem\"":\""1\"",\""OrigICMS\"":\""0\"",\""PorcentagemIBPTLinha\"":15.530000,\""QuantidadeLinha\"":1.000000,\""SimOuNaoRetidoLinha\"":\""N\"",\""UnidadeComercial\"":\""KG\"",\""ValorTotalDescontoLinha\"":0,\""ValorTotalLinnha\"":100,\""ValorUnitarioLinha\"":100.000000,\""cEnq\"":\""999\""}]"",""CargaFiscal"":29,""CodInt"":""0"",""DocEntry"":1016,""Duplicata"":""[{\""DataVencimento\"":\""2022-10-14 00:00:00.0000000\"",\""NumeroDuplicata\"":1,\""ValorDuplicata\"":100.000000}]"",""Filial"":""{\""AdressTypeFilial\"":\""RUA\"",\""BairroFilial\"":\""Pinheiros\"",\""CEPFilial\"":\""05422-030\"",\""CNPJFilial\"":\""07.792.897/0001-82\"",\""CPFFilial\"":\""\"",\""CidadeFilial\"":\""São Paulo\"",\""CodigoIBGEMunicipioFilial\"":\""3550308\"",\""CodigoPaisFilial\"":\""1058\"",\""CodigoUFFilial\"":\""35\"",\""ComplementoFilial\"":\""\"",\""IndicadorIEFilial\"":\""1\"",\""InscIeFilial\"":\""129672815112\"",\""LogradouroFilial\"":\""CLAUDIO SOARES\"",\""MunicipioFilial\"":\""5344\"",\""NomePaisFilial\"":\""Brasil\"",\""NumeroLogradouroFilial\"":\""72\"",\""RazaoSocialFilial\"":\""FELLIPELLI INSTRUMENTOS DE DIAGNOSTICO\"",\""RegimeTributacaoFilial\"":-1,\""UFFilial\"":\""SP\""}"",""Identificacao"":""{\""BranchId\"":\""d1540357-5a30-41dd-bb6a-be3f7a363660\"",\""CondicaoDePagamentoDocumento\"":\""0\"",\""ConsumidorFinal\"":\""0\"",\""DadosCobranca\"":100.000000,\""DataEmissao\"":\""2022-10-14 00:00:00.0000000\"",\""DataLancamento\"":\""2022-10-14 00:00:00.0000000\"",\""DocEntry\"":1016,\""DocNum\"":466,\""DocTime\"":1328,\""DocumentoCancelado\"":\""N\"",\""FinalideDocumento\"":\""0\"",\""FormaDePagamentoDocumento\"":\""01\"",\""IdRetornoOrbit\"":\""\"",\""IncentivadorCultural\"":\""2\"",\""IndicadorIntermediario\"":0,\""IndicadorPresenca\"":\""9\"",\""Key\"":\""\"",\""NaturezaOperacaoDocumento\"":\""Venda Adquirida Terc\"",\""NumeroDocumento\"":\""306\"",\""NumeroProtocolo\"":\""\"",\""OperacaoNFe\"":\""1\"",\""SerieDocumento\"":\""2\"",\""StatusOrbit\"":\""\"",\""TipoOperacaoDocumento\"":\""\"",\""TipoTributacaoNFSe\"":\""\"",\""ValorTotalNF\"":100.000000,\""Versao\"":\""4.00\""}"",""Justificativa"":"""",""ModeloDocumento"":""55"",""ObjetoB1"":13,""TipoDocumento"":""output"",""TipoNF"":""1"",""U_TAX4_Cancelado"":""N""}]
                             ";
            return result;
        }

        private void SetupVerifyFields(SetupQueryB1 setupQueryB1)
        {
            string query = @$"SELECT ""AliasId"" FROM CUFD WHERE ""AliasId"" = '{setupQueryB1.MVast}'";
            mockWrapper.Setup(m => m.ExecuteQuery(query)).Returns(ReturnDataSetWithRowForFields(setupQueryB1.MVast));
            query = @$"SELECT ""AliasId"" FROM CUFD WHERE ""AliasId"" = '{setupQueryB1.AliquotaIntDestino}'";
            mockWrapper.Setup(m => m.ExecuteQuery(query)).Returns(ReturnDataSetWithRowForFields(setupQueryB1.AliquotaIntDestino));

            query = @$"SELECT ""AliasId"" FROM CUFD WHERE ""AliasId"" = '{setupQueryB1.PartilhaInterestadual}'";
            mockWrapper.Setup(m => m.ExecuteQuery(query)).Returns(ReturnDataSetWithoutRowForFields(setupQueryB1.PartilhaInterestadual));
        }

        public DataSet ReturnDataSetWithoutRowForFields(string Fields)
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable("CUFD");
            DataColumn column = new DataColumn();
            column.ColumnName = "AliasId";
            table.Columns.Add(column);
            dataSet.Tables.Add(table);
            return dataSet;
        }

        public DataSet ReturnDataSetWithRowForFields(string Fields)
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable("CUFD");
            DataColumn column = new DataColumn();
            column.ColumnName = "AliasId";
            table.Columns.Add(column);
            dataSet.Tables.Add(table);
            DataRow config = dataSet.Tables[0].NewRow();
            config["AliasId"] = Fields;
            dataSet.Tables[0].Rows.Add(config);

            return dataSet;
        }
    }
}

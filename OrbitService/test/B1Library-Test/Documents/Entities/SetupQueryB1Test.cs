using B1Library.Documents.Entities;
using B1Library.Implementations.Repositories;
using B1Library.usecase;
using Moq;
using OrbitLibrary.Data;
using System;
using System.Data;
using System.Text;
using Xunit;
using static B1Library.Implementations.Repositories.DBTableNameRepository;

namespace B1Library_Tests.Documents.Entities
{
    public class SetupQueryB1Test
    {
		private Mock<IWrapper> mockWrapper;
		private SetupQueryB1 cut;
		private UseCasesB1Library useCasesB1;
		private UseCase useCase;
		private TableName tableName;
		private  DBTableNameRepository tableNameRepository;
		public SetupQueryB1Test()
        {
			mockWrapper = new Mock<IWrapper>();
			useCasesB1 = new UseCasesB1Library(useCase);
			tableName = new TableName();
			tableNameRepository = new DBTableNameRepository();
			cut = new SetupQueryB1(new DBDocumentsRepository(mockWrapper.Object), tableName, useCasesB1);
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

		[Fact]
		public void ShouldSetupQueryB1ConsultDocumentInOrbit()
		{
			useCase = UseCase.ConsultaNFe;
			useCasesB1 = new UseCasesB1Library(useCase);
			foreach (TableName item in tableNameRepository.tableNamesOutboundNFe)
			{
                cut = new SetupQueryB1(new DBDocumentsRepository(mockWrapper.Object), item, useCasesB1);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($@"SELECT
	                         COALESCE(T0.""DocEntry"",0)             AS ""DocEntry"",
							 COALESCE(T1.""BaseEntry"", 0)           AS ""BaseEntry"",
							 COALESCE(T0.""U_TAX4_CodInt"", '')      AS ""CodInt"",
							 COALESCE(T0.""U_TAX4_IdRet"", '')       AS ""IdRetornoOrbit"",
							 COALESCE(OM.""NfmCode"", '')            AS ""ModeloDocumento"",
							 COALESCE(T0.""ObjType"", 0)             AS ""ObjetoB1""
							 FROM {cut.B1TableName} T0
							 JOIN ONFM OM ON T0.""Model"" = OM.""AbsEntry""
							 JOIN {cut.B1TableNameChild}1 T1 ON T0.""DocEntry"" = T1.""DocEntry""
							 LEFT JOIN NFN1 NF ON T0.""SeqCode"" = NF.""SeqCode""");
                sb.AppendLine(useCasesB1.GetCommandUseCase());
                sb.AppendLine("FOR JSON");
                string query = Convert.ToString(sb);
                string queryProd = cut.SetupQueryB1ConsultDocumentInOrbit();
                Assert.Equal(query, queryProd);
            }
			
		}

		[Fact]
		public void ShouldSetupQueryB1CancelDocumentInOrbit()
        {
            useCase = UseCase.ConsultaNFe;
            useCasesB1 = new UseCasesB1Library(useCase);
			foreach (TableName item in tableNameRepository.tableNamesOutboundNFe)
			{
                cut = new SetupQueryB1(new DBDocumentsRepository(mockWrapper.Object), item, useCasesB1);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($@"SELECT
	                         COALESCE(T0.""DocEntry"",0)			 AS ""DocEntry"",
							 COALESCE(T1.""BaseEntry"", 0)           AS ""BaseEntry"",
							 COALESCE(T0.""U_TAX4_CodInt"", '')      AS ""CodInt"",
							 COALESCE(T0.""U_TAX4_IdRet"", '')       AS ""IdRetornoOrbit"",
							 COALESCE(OM.""NfmCode"", '')            AS ""ModeloDocumento"",
							 COALESCE(NF.""SeqCode"", 0)             AS ""CargaFiscal"",
							 COALESCE(T0.""ObjType"", 0)             AS ""ObjetoB1"",
							 COALESCE(T0.""U_TAX4_Justi"", '')       AS ""Justificativa"",
							 COALESCE(T0.""CANCELED"", '')           AS ""CANCELED"",
							 COALESCE(T0.""U_TAX4_Cancelado"", '')   AS ""U_TAX4_Cancelado"",
							 REPLACE(REPLACE((
									 SELECT
									 COALESCE(OI.""TaxDate"", '')                  AS ""DataEmissao"",
									 COALESCE(OI.""DocDate"", '')                  AS ""DataLancamento"",
									 COALESCE(CAST(OI.""Serial"" AS VARCHAR), '')  AS ""NumeroDocumento"",
									 COALESCE(OI.""SeriesStr"", '')                AS ""SerieDocumento"",
COALESCE(T0.""U_TAX4_Justi"", '')             AS ""Justificativa"",
 COALESCE(CG.""U_TAX4_EstabID"", '')           AS ""BranchId"",
									 COALESCE(C2.""U_TAX4_versao"", '')            AS ""Versao""
									 FROM {cut.B1TableName} OI
								     JOIN ""@TAX4_CONFIG"" C2 ON OI.""BPLId"" = C2.""U_TAX4_Filial""
									 WHERE T0.""DocEntry"" = OI.""DocEntry"" FOR JSON),'[',''),']','') 	
									 AS ""Identificacao""
							 FROM {cut.B1TableName} T0
							 JOIN ONFM OM ON T0.""Model"" = OM.""AbsEntry""
							 JOIN {cut.B1TableNameChild}1 T1 ON T0.""DocEntry"" = T1.""DocEntry""
 JOIN ""@TAX4_LCONFIGADDON"" CG ON OI.""BPLId"" = CG.""U_TAX4_Empresa""
							 LEFT JOIN NFN1 NF ON T0.""SeqCode"" = NF.""SeqCode""
							 WHERE
							 and NF.""SeqCode"" <> 0
							 and T0.""U_TAX4_CARGAFISCAL"" = 'N'
							 and T0.""CANCELED"" = 'C'
							 and T0.""DocStatus"" = 'C'");
                sb.AppendLine(useCasesB1.GetCommandUseCase());
                sb.AppendLine("FOR JSON");


                string query = Convert.ToString(sb);
                string queryProd = cut.SetupQueryB1CancelDocumentInOrbit();
                Assert.Equal(query, queryProd);
            }
           
		}
		[Fact]
        public void ShouldSetupQueryB1SendDocumentToOrbitOutbound()
        {
            useCase = UseCase.OutboundNFe;
            useCasesB1 = new UseCasesB1Library(useCase);
			DBTableNameRepository db = new DBTableNameRepository();
			foreach (TableName item in db.tableNamesOutboundNFe)
			{
                cut = new SetupQueryB1(new DBDocumentsRepository(mockWrapper.Object), item , useCasesB1);

                string query = @$"SELECT ""AliasId"" FROM CUFD WHERE ""AliasId"" = '{cut.MVast}'";
                mockWrapper.Setup(m => m.ExecuteQuery(query)).Returns(ReturnDataSetWithRowForFields(cut.MVast));

                query = @$"SELECT ""AliasId"" FROM CUFD WHERE ""AliasId"" = '{cut.AliquotaIntDestino}'";
                mockWrapper.Setup(m => m.ExecuteQuery(query)).Returns(ReturnDataSetWithRowForFields(cut.AliquotaIntDestino));

                query = @$"SELECT ""AliasId"" FROM CUFD WHERE ""AliasId"" = '{cut.PartilhaInterestadual}'";
                mockWrapper.Setup(m => m.ExecuteQuery(query)).Returns(ReturnDataSetWithoutRowForFields(cut.PartilhaInterestadual));
                StringBuilder sb = new StringBuilder();
                #region HEADER QUERY
                sb.Append(@$"SELECT
	                         COALESCE(T0.""DocEntry"",0)            AS ""DocEntry"",
	                         COALESCE(T0.""U_TAX4_CodInt"",'')      AS ""CodInt"",
	                         COALESCE(OM.""NfmCode"",'')            AS ""ModeloDocumento"",
	                         COALESCE(NF.""SeqCode"",0)				AS ""CargaFiscal"",
	                         COALESCE(T0.""ObjType"",0)				AS ""ObjetoB1"",
	                         COALESCE(T0.""U_TAX4_Justi"",'')       AS ""Justificativa"",
	                         COALESCE(T0.""CANCELED"",'')           AS ""CANCELED"",
	                         COALESCE(T0.""U_TAX4_Cancelado"",'')   AS ""U_TAX4_Cancelado"",
	                         CASE T0.""ObjType""
	 		                        WHEN '13' THEN ('output')
	 		                        WHEN '15' THEN ('output')
	 		                        WHEN '18' THEN ('input')
	 		                        WHEN '20' THEN ('input')
	 		                        END AS ""TipoDocumento"",
	                         CASE T0.""ObjType""
	 		                        WHEN '13' THEN ('1')
	 		                        WHEN '15' THEN ('1')
	 		                        WHEN '18' THEN ('0')
	 		                        WHEN '20' THEN ('0')
	 		                        END AS ""TipoNF""
	                         ,REPLACE(REPLACE(( ");
                #endregion HEADER QUERY
                #region IDENTIFICACAO
                sb.AppendLine($@"SELECT
	 	  							 COALESCE(OI.""DocEntry"",0) 	  			AS ""DocEntry"",
									 COALESCE(OI.""DocNum"", 0)                    AS ""DocNum"",
									 COALESCE(OI.""DocTime"", 0)                   AS ""DocTime"",
									 COALESCE(OI.""TaxDate"", '')                  AS ""DataEmissao"",
									 COALESCE(OI.""DocDate"", '')                  AS ""DataLancamento"",
									 COALESCE(CAST(OI.""Serial"" AS VARCHAR), '')  AS ""NumeroDocumento"",
									 COALESCE(OI.""SeriesStr"", '')                AS ""SerieDocumento"",
									 COALESCE(OI.""U_TAX4_tpTribNfse"", '')        AS ""TipoTributacaoNFSe"",
									 COALESCE(OI.""U_TAX4_tpOperacao"", '')        AS ""TipoOperacaoDocumento"",
									 COALESCE(OI.""NfeValue"", 0)                  AS ""ValorTotalNF"",
									 COALESCE(OI.""CANCELED"", '')                 AS ""DocumentoCancelado"",
									 COALESCE(OI.""U_TAX4_Stat"", '')              AS ""StatusOrbit"",
									 COALESCE(OI.""U_TAX4_IdRet"", '')             AS ""IdRetornoOrbit"",
									 COALESCE(OI.""U_TAX4_FinNfe"", '')            AS ""FinalideDocumento"",
									 COALESCE(OU.""U_TAX4_NatOper"", '')           As ""NaturezaOperacaoDocumento"",
									 COALESCE(OC.""U_TAX4_condPag"", '')           AS ""CondicaoDePagamentoDocumento"",
									 COALESCE(OP.""U_TAX4_FormaPagto"", '')        AS ""FormaDePagamentoDocumento"",
									 COALESCE(CG.""U_TAX4_EstabID"", '')           AS ""BranchId"",
									 COALESCE(C2.""U_TAX4_versao"", '')            AS ""Versao"",
									 COALESCE(OI.""U_TAX4_Chave"", '')             AS ""Key"",
									 COALESCE(OI.""DocTotal"", 0)                  AS ""DadosCobranca"",
									 COALESCE(C2.""U_TAX4_Operacao"", '')          AS ""OperacaoNFe"",
									 COALESCE(OI.""U_TAX4_IndFinal"", '')          AS ""ConsumidorFinal"",
									 COALESCE(OI.""U_TAX4_IndPres"", '')           AS ""IndicadorPresenca"",
									 COALESCE(CAST(OI.""U_TAX4_indInter"" AS INT), 0) AS ""IndicadorIntermediario"",
									 COALESCE(OI.""U_TAX4_Prot"", '')              AS ""NumeroProtocolo"",
									 COALESCE(C2.""U_TAX4_itCultural"", '')        AS ""IncentivadorCultural""

									 FROM {cut.B1TableName} OI 
								     JOIN {cut.B1TableNameChild}12 PT ON PT.""DocEntry"" = OI.""DocEntry""
									 LEFT JOIN OUSG OU ON PT.""MainUsage"" = OU.""ID""
									 LEFT JOIN OCTG OC ON OI.""GroupNum"" = OC.""GroupNum""
									 LEFT JOIN OPYM OP ON OI.""PeyMethod"" = OP.""PayMethCod""
									 JOIN ""@TAX4_LCONFIGADDON"" CG ON OI.""BPLId"" = CG.""U_TAX4_Empresa""
									 JOIN ""@TAX4_CONFIG"" C2 ON OI.""BPLId"" = C2.""U_TAX4_Filial""
									 WHERE T0.""DocEntry"" = OI.""DocEntry"" FOR JSON),'[',''),']','') 
	
									 AS ""Identificacao""");
                #endregion IDENTIFICACAO
                #region PARCEIRO
                sb.AppendLine(cut.RetunCommandParceiro());
                #endregion PARCEIRO
                #region FILIAL
                sb.AppendLine(@$",REPLACE(REPLACE(( 
 							SELECT
							 COALESCE(OB.""BPLName"",'') 				AS ""RazaoSocialFilial"",
							 COALESCE(OB.""TaxIdNum"", '')             AS ""CNPJFilial"",
							 COALESCE(OB.""TaxIdNum2"", '')            AS ""InscIeFilial"",
							 COALESCE(OB.""AddrType"", '')             AS ""AdressTypeFilial"",
							 COALESCE(OB.""Street"", '')               AS ""LogradouroFilial"",
							 COALESCE(OB.""StreetNo"", '')             AS ""NumeroLogradouroFilial"",
							 COALESCE(OB.""Block"", '')                AS ""BairroFilial"",
							 COALESCE(OB.""ZipCode"", '')              AS ""CEPFilial"",
							 COALESCE(OB.""County"", '')               AS ""MunicipioFilial"",
							 COALESCE(OB.""State"", '')                AS ""UFFilial"",
							 COALESCE(OB.""City"", '')                 AS ""CidadeFilial"",
							 COALESCE(OB.""Building"", '')             AS ""ComplementoFilial"",
							 COALESCE(OB.""ProfTax"", 0)               AS ""RegimeTributacaoFilial"",
							 COALESCE(OY.""CntCodNum"", '')            AS ""CodigoPaisFilial"",
							 COALESCE(OT.""IbgeCode"", '')             AS ""CodigoIBGEMunicipioFilial"",
							 COALESCE(OB.""RevOffice"", '')            AS ""CPFFilial"",
							 COALESCE(OY.""Name"", '')                 AS ""NomePaisFilial"",
							 COALESCE(UF.""U_TAX4_Cod"", '')           AS ""CodigoUFFilial"",
							 '1'									   AS ""IndicadorIEFilial""
							 FROM OBPL OB
							 LEFT JOIN OCRY OY ON OB.""Country"" = OY.""Code""
							 LEFT JOIN OCNT OT ON OB.""County"" = OT.""AbsId""
							 JOIN ""@TAX4_UF"" UF ON OB.""State"" = UF.""U_TAX4_Uf""
							 WHERE OB.""BPLId"" = T0.""BPLId""
							 FOR JSON),'[',''),']','') 
							 AS ""Filial""");
                #endregion FILIAL
                #region LINHAS
                #region HEADER LINHAS
                sb.AppendLine(@$",( 
 							SELECT
 								 COALESCE(CAST(TL.""VisOrder"" + 1 AS VARCHAR),'')									AS ""NItem"",
								 COALESCE(TL.""LineNum"", 0)														AS ""ItemLinhaDocumento"",
								 COALESCE(TL.""ItemCode"", '')														AS ""CodigoItem"",
								 COALESCE(TL.""PriceBefDi"" * TL.""Quantity"", 0)									AS ""ValorTotalLinnha"",
								 COALESCE(TL.""U_TAX4_CodServ"", '')												AS ""CodigoServicoLinha"",
								 COALESCE(TL.""U_TAX4_IBPT"", 0)													AS ""PorcentagemIBPTLinha"",
								 COALESCE(TL.""Quantity"", 0)														AS ""QuantidadeLinha"",
								 COALESCE(TL.""PriceBefDi"", 0)														AS ""ValorUnitarioLinha"",
								 COALESCE((TL.""PriceBefDi"" * TL.""Quantity"") * (TL.""DiscPrcnt"" / 100),0) 		AS ""ValorTotalDescontoLinha"",
								 COALESCE(TL.""WtLiable"", '')														AS ""SimOuNaoRetidoLinha"",
								 COALESCE(SUBSTRING(TL.""CSTCode"", 1, 1), '')										AS ""OrigICMS"",
								 COALESCE(SUBSTRING(TL.""CSTCode"", 3, 2), '')										AS ""CSTICMSLinha"",
								 COALESCE(CAST(TL.""CSTfIPI"" AS VARCHAR), '')										AS ""CSTIPILinha"",
								 COALESCE(CAST(TL.""CSTfPIS"" AS VARCHAR), '')										AS ""CSTPisLinha"",
								 COALESCE(CAST(TL.""CSTfCOFINS"" AS VARCHAR), '')									AS ""CSTCofinsLinha"",
								 COALESCE(TL.""Dscription"", '')													AS ""DescricaoItemLinhaDocumento"",
								 COALESCE(OT.""CodeBars"", '')														AS ""CodigoDeBarras"",
								 COALESCE(OT.""SalUnitMsr"", '')													AS ""UnidadeComercial"",
								 COALESCE(CM.""NcmCode"", '')														AS ""CodigoNCM"",
								 COALESCE(TL.""CFOPCode"", '')														AS ""CodigoCFOP"",
								 COALESCE(OT.""U_TAX4_LisSer"", '')													AS ""ItemListaServico"",
								 COALESCE(OD.""ServiceCD"", '')														AS ""CodigoTributacaoMuncipio"",
								 COALESCE(TL.""U_TAX4_MotDes"", '')													AS ""MotivoDesoneracao"",		
								 CASE SUBSTRING(TL.""CFOPCode"",0,1) 
								 WHEN '1' THEN('1')
								 WHEN '5' THEN('1')
								 WHEN '2' THEN('2')
								 WHEN '6' THEN('2')
								 WHEN '3' THEN('3')
								 WHEN '7' THEN('7')
								 END AS ""IdLocalDestino"",
								 CASE(TL.""CSTfIPI"")
								 WHEN '02' THEN('302')
								 WHEN '52' THEN('302')
								 WHEN '04' THEN('002')
								 WHEN '54' THEN('002')
								 WHEN '05' THEN('102')
								 WHEN '55' THEN('102')
								 ELSE '999'
								 END AS ""cEnq""");
                #endregion HEADER LINHAS

                #region IMPOSTOS LINHA
                sb.AppendLine(@$",(
							SELECT
								 COALESCE(TX.""TaxSum"",0) 			   AS ""ValorImposto"",
								 COALESCE(TX.""TaxRate"", 0)           AS ""PorcentagemImposto"",
								 COALESCE(TT.""U_TAX4_TpImp"", '')     AS ""TipoImpostoOrbit"",
								 COALESCE(TT.""Name"", '')             AS ""NomeImposto"",
								 COALESCE(TX.""BaseSum"", 0)           AS ""ValorBaseImposto"",
								 COALESCE(TX.""Unencumbrd"", '')       AS ""SimOuNaoDesoneracao"" ");
                if (cut.VerifyIfFieldExists(cut.MVast))
                {
                    sb.AppendLine(@$"	 ,COALESCE(TX.""U_Lucro"",0)			   AS ""MVast""");
                }
                if (cut.VerifyIfFieldExists(cut.AliquotaIntDestino))
                {
                    sb.AppendLine(@$"	 ,COALESCE(TX.""U_AliqDest"",0)		   AS ""AliquotaIntDestino""");
                }
                if (cut.VerifyIfFieldExists(cut.PartilhaInterestadual))
                {
                    sb.AppendLine(@$"	 ,COALESCE(TX.""U_IntPart"",0)		   AS ""PartilhaInterestadual""");
                }
                sb.AppendLine(@$"	FROM {cut.B1TableNameChild}4 TX 
								JOIN OSTT TT ON TX.""staType"" = TT.""AbsId"" 
								WHERE TL.""DocEntry"" = TX.""DocEntry""
								AND TL.""LineNum"" = TX.""LineNum""
								FOR JSON)AS ""ImpostoLinha""");
                #endregion IMPOSTOS LINHA
                #region RETENCOES LINHA
                sb.AppendLine(@$",(
								SELECT
									 COALESCE(WT.""Rate"",0) 				AS ""PorcentagemImpostoRetido"",
									 COALESCE(WT.""WTAmnt"", 0)            AS ""ValorImpostoRetido"",
									 COALESCE(OW.""U_TAX4_TpImp"", '')     AS ""TipoImpostoOrbit""
									 FROM {cut.B1TableNameChild}5 WT
									 INNER JOIN OWHT OH on WT.""WTCode"" = OH.""WTCode""
									 INNER JOIN OWTT OW on OH.""WTTypeId"" = OW.""WTTypeId""
									 WHERE WT.""AbsEntry"" = T0.""DocEntry""
									 and WT.""Doc1LineNo"" = TL.""LineNum"" FOR JSON)AS ""ImpostoRetidoLinha""");
                #endregion RETENCOES LINHA
                #region DespesaAdicional
                sb.AppendLine(@$",(
								SELECT
								COALESCE(OX.""ExpnsType"",'') 			AS ""TipoDespesa"",
								COALESCE(TD.""LineTotal"", 0)			AS ""ValorUnitarioDespesa""
								FROM {cut.B1TableNameChild}13 TD
								JOIN OEXD OX ON TD.""ExpnsCode"" = OX.""ExpnsCode""
								WHERE TD.""DocEntry"" = T0.""DocEntry"" AND TD.""LineNum"" = TL.""LineNum""
								FOR JSON) AS ""DespesaAdicional"" ");
                #endregion DespesaAdicional
                sb.AppendLine(@$"FROM {cut.B1TableNameChild}1 TL 
							 JOIN OITM OT ON TL.""ItemCode"" = OT.""ItemCode""
							 LEFT JOIN ONCM CM ON OT.""NCMCode"" = CM.""AbsEntry""
							 LEFT JOIN ""OSCD"" OD ON OT.""OSvcCode"" = OD.""AbsEntry""
							 WHERE TL.""DocEntry"" = T0.""DocEntry""
							 FOR JSON) AS ""CabecalhoLinha""");
                #endregion LINHAS
                #region DUPLICATA
                sb.AppendLine($@",(
							SELECT
							COALESCE(DP.""InstlmntID"",0) 		AS ""NumeroDuplicata"",
							COALESCE(DP.""DueDate"", '')           AS ""DataVencimento"",
							COALESCE(DP.""InsTotal"", 0)           AS ""ValorDuplicata""
							FROM {cut.B1TableNameChild}6 DP
							WHERE DP.""DocEntry"" = T0.""DocEntry""
							FOR JSON) 
							AS ""Duplicata"" ");
                #endregion DUPLICATA

                sb.AppendLine($@"FROM {cut.B1TableName} T0  
							JOIN ONFM OM ON T0.""Model"" = OM.""AbsEntry""
							LEFT JOIN NFN1 NF ON T0.""SeqCode"" = NF.""SeqCode""");
                sb.AppendLine(useCasesB1.GetCommandUseCase());
                sb.AppendLine("FOR JSON");
                query = Convert.ToString(sb);
                string queryProd = cut.SetupQueryB1SendDocumentToOrbit();
                Assert.Equal(query, queryProd);
            }
           

           
		}
        [Fact]
		public void VerifyIfFieldExists()
        {
			string query = @$"SELECT ""AliasID"" FROM CUFD WHERE ""AliasID"" = '{cut.MVast}'";
			mockWrapper.Setup(m => m.ExecuteQuery(query)).Returns(ReturnDataSetWithRowForFields(cut.MVast));
			Assert.True(cut.VerifyIfFieldExists(cut.MVast));

		}
	}
}

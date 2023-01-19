using B1Library.Applications;
using B1Library.Implementations.Repositories;
using B1Library.usecase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static B1Library.Implementations.Repositories.DBTableNameRepository;

namespace B1Library.Documents.Entities
{
    public class SetupQueryB1
    {
        private DBDocumentsRepository dbRepo;
        private UseCasesB1Library useCasesB1;
        private StringBuilder sb;
        public Invoice invoice;
        public DBTableNameRepository.Type B1TableType;

        public string MVast = "Lucro";
        public string AliquotaIntDestino = "AliqDest";
        public string PartilhaInterestadual = "IntPart";
        public string VICMSOpL = "VICMSOpL";
        public string VDifL = "VDifL";
        public string PDif = "PDif";
        public string ReduICMS = "ReduICMS";
        public string B1TableName = string.Empty;
        public string B1TableNameChild = string.Empty;

        public SetupQueryB1(DBDocumentsRepository dbRepo, TableName tableName, UseCasesB1Library useCasesB1)
        {
            this.dbRepo = dbRepo;
            this.useCasesB1 = useCasesB1;
            B1TableName = tableName.TableHeader;
            B1TableType = tableName.Type;
            B1TableNameChild = tableName.TableChild;
            sb = new StringBuilder();
        }

        #region SETUP QUERY HANA 2.0
        public string SetupQueryB1ConsultDocumentInOrbit()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"SELECT
	                         COALESCE(T0.""DocEntry"",0)            AS ""DocEntry"",
							 COALESCE(T1.""BaseEntry"", 0)           AS ""BaseEntry"",
							 COALESCE(T0.""U_TAX4_CodInt"", '')      AS ""CodInt"",
							 COALESCE(T0.""U_TAX4_IdRet"", '')       AS ""IdRetornoOrbit"",
							 COALESCE(OM.""NfmCode"", '')            AS ""ModeloDocumento"",
							 COALESCE(T0.""ObjType"", 0)             AS ""ObjetoB1""
							 FROM {B1TableName} T0
							 JOIN ONFM OM ON T0.""Model"" = OM.""AbsEntry""
							 JOIN {B1TableNameChild}1 T1 ON T0.""DocEntry"" = T1.""DocEntry""
							 LEFT JOIN NFN1 NF ON T0.""SeqCode"" = NF.""SeqCode""");
            sb.AppendLine(useCasesB1.GetCommandUseCase());
            sb.AppendLine("FOR JSON");
            return Convert.ToString(sb);
        }
        public string SetupQueryB1CancelDocumentInOrbit()
        {
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
								     COALESCE(T0.""U_TAX4_Chave"", '')              AS ""ChaveDeAcessoNFe"",
									 COALESCE(T0.""U_TAX4_Prot"", '')             AS ""ProtocoloNFe"",
								     COALESCE(CG.""U_TAX4_EstabID"", '')           AS ""BranchId"",
									 COALESCE(C2.""U_TAX4_versao"", '')            AS ""Versao""
									 FROM {B1TableName} OI
								     JOIN ""@TAX4_CONFIG"" C2 ON OI.""BPLId"" = C2.""U_TAX4_Filial""
									 JOIN ""@TAX4_LCONFIGADDON"" CG ON OI.""BPLId"" = CG.""U_TAX4_Empresa""
									 WHERE T0.""DocEntry"" = OI.""DocEntry"" FOR JSON),'[',''),']','') 	
									 AS ""Identificacao""
							 FROM {B1TableName} T0
							 JOIN ONFM OM ON T0.""Model"" = OM.""AbsEntry""
							 JOIN {B1TableNameChild}1 T1 ON T0.""DocEntry"" = T1.""DocEntry""
							 LEFT JOIN NFN1 NF ON T0.""SeqCode"" = NF.""SeqCode""
							 WHERE
							 NF.""SeqCode"" <> 0
							 and T0.""U_TAX4_CARGAFISCAL"" = 'N'
							 and T0.""CANCELED"" = 'C'
							 and T0.""DocStatus"" = 'C'
								");
            sb.AppendLine(useCasesB1.GetCommandUseCase());
            sb.AppendLine("FOR JSON");
            return Convert.ToString(sb);
        }
        public string SetupQueryB1SendDocumentToOrbit()
        {
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
									WHEN '16' THEN ('0')
									WHEN '14' THEN ('0')
	 		                        WHEN '15' THEN ('1')
	 		                        WHEN '18' THEN ('0')
	 		                        WHEN '20' THEN ('0')
	 		                        END AS ""TipoNF""
	                         ,REPLACE(REPLACE(( ");
            #endregion HEADER QUERY
            #region IDENTIFICACAO
            sb.AppendLine($@"SELECT
	 	  							 COALESCE(OI.""DocEntry"",0) 	  			   AS ""DocEntry"",
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
									 COALESCE(C2.""U_TAX4_itCultural"", '')        AS ""IncentivadorCultural"",
									 COALESCE(C2.""U_TAX4_tipoRPS"",'')			   AS ""TipoRps"",
									 COALESCE(C2.""U_TAX4_RegEspTrib"",'')		   AS ""RegEspTrib"",
									 COALESCE(OI.""U_TAX4_InfAd"",'')			   AS ""InfAdFisco"",
								     COALESCE(OI.""U_TAX4_DataHora"",'')		   AS ""EnviaDataHora"",
									 COALESCE(OI.""U_TAX4_DataEnt"",null)		   AS ""DataDeEnvio"",
									 COALESCE(OI.""U_TAX4_HoraEnt"",'')			   AS ""HoraDeEnvio"",
									 COALESCE(OI.""U_TAX4_Local_Exp"",'')		   AS ""LocalDeExportacao"",
									 COALESCE(OI.""U_TAX4_UF_Exp"",'')			   AS ""UFDeExportacao"",
									 COALESCE(C2.""U_TAX4_justf"",'')			   AS ""JustContigencia""
									 

									 FROM {B1TableName} OI 
								     JOIN {B1TableNameChild}12 PT ON PT.""DocEntry"" = OI.""DocEntry""
									 LEFT JOIN OUSG OU ON PT.""MainUsage"" = OU.""ID""
									 LEFT JOIN OCTG OC ON OI.""GroupNum"" = OC.""GroupNum""
									 LEFT JOIN OPYM OP ON OI.""PeyMethod"" = OP.""PayMethCod""
									 JOIN ""@TAX4_LCONFIGADDON"" CG ON OI.""BPLId"" = CG.""U_TAX4_Empresa""
									 JOIN ""@TAX4_CONFIG"" C2 ON OI.""BPLId"" = C2.""U_TAX4_Filial""
									 WHERE T0.""DocEntry"" = OI.""DocEntry"" FOR JSON),'[',''),']','') 
	
									 AS ""Identificacao""");
            #endregion IDENTIFICACAO
            #region PARCEIRO
            sb.AppendLine(RetunCommandParceiroHANA2());
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
								 COALESCE(TL.""TaxOnly"",'')                                                        AS ""SoImposto"",
								 COALESCE(OT.""U_TAX4_cest"",'')                                                    AS ""CodigoCEST"",
								 COALESCE(TL.""Dscription"", '')													AS ""DescricaoItemLinhaDocumento"",
								 COALESCE(OT.""CodeBars"", '')														AS ""CodigoDeBarras"",
								 COALESCE(OT.""SalUnitMsr"", '')													AS ""UnidadeComercial"",
								 COALESCE(CM.""NcmCode"", '')														AS ""CodigoNCM"",
								 COALESCE(TL.""CFOPCode"", '')														AS ""CodigoCFOP"",
								 COALESCE(OT.""U_TAX4_LisSer"", '')													AS ""ItemListaServico"",
								 COALESCE(OD.""ServiceCD"", '')														AS ""CodigoTributacaoMuncipio"",
								 COALESCE(TL.""U_TAX4_MotDes"", '')													AS ""MotivoDesoneracao"",
								 COALESCE(TL.""U_TAX4_nPedido"",'')													AS ""NumeroPedido"",
								 COALESCE(TL.""U_TAX4_nItemPedido"",'')											    AS ""NumeroItemPedido"",
								 CASE SUBSTRING(TL.""CFOPCode"",0,1) 
								 WHEN '1' THEN('1')
								 WHEN '5' THEN('1')
								 WHEN '2' THEN('2')
								 WHEN '6' THEN('2')
								 WHEN '3' THEN('3')
								 WHEN '7' THEN('3')
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
            if (VerifyIfFieldExists(VDifL))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_VDifL"",0)			   AS ""VDif""");
            }
            if (VerifyIfFieldExists(PDif))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_PDif"",0)			   AS ""PDif""");
            }
            if (VerifyIfFieldExists(ReduICMS))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_ReduICMS"",0)			   AS ""pRedBc""");
            }
            if (VerifyIfFieldExists(VICMSOpL))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_VICMSOpL"",0)			   AS ""VICMSOp""");
            }
            if (VerifyIfFieldExists(MVast))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_Lucro"",0)			   AS ""MVast""");
            }
            if (VerifyIfFieldExists(AliquotaIntDestino))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_AliqDest"",0)		   AS ""AliquotaIntDestino""");
            }
            if (VerifyIfFieldExists(PartilhaInterestadual))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_IntPart"",0)		   AS ""PartilhaInterestadual""");
            }
            sb.AppendLine(@$"	FROM {B1TableNameChild}4 TX 
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
									 FROM {B1TableNameChild}5 WT
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
								FROM {B1TableNameChild}13 TD
								JOIN OEXD OX ON TD.""ExpnsCode"" = OX.""ExpnsCode""
								WHERE TD.""DocEntry"" = T0.""DocEntry"" AND TD.""LineNum"" = TL.""LineNum""
								FOR JSON) AS ""DespesaAdicional"" ");
            #endregion DespesaAdicional
            #region DADOSDI
            sb.AppendLine($@",(SELECT 
							 COALESCE(""U_TAX4_CNPJAD"",'')				AS ""CNPJAdiquirente"",
							 COALESCE(""U_TAX4_DDA"",'')					AS ""DataDesembaracoAduaneiro"",
							 COALESCE(""U_TAX4_DDI"",'')					AS ""Ddi"",
							 COALESCE(""U_TAX4_FI"",'')					AS ""TpIntermedio"",
							 COALESCE(""U_TAX4_LDA"",'')					AS ""LocalDesembaracoAduaneiro"",
							 COALESCE(""U_TAX4_NAdicao"",'')				AS ""NumeroAdicao"",
							 COALESCE(""U_TAX4_NDI"",'')					AS ""NumeroDocImportacao"",
							 COALESCE(""U_TAX4_NRDRW"",'')					AS ""NumeroDrawBack"",
							 COALESCE(""U_TAX4_NSeqAdicao"",'')			AS ""NumeroSequenciaAdicao"",
							 COALESCE(""U_TAX4_UFAD"",'')					AS ""UFAdiquirente"",
							 COALESCE(""U_TAX4_UFD"",'')					AS ""UFDesembaracoAduaneiro"",
							 COALESCE(""U_TAX4_VAFRMM"",'')				AS ""ValorFrmm"",
							 COALESCE(""U_TAX4_VDesc"",0)					AS ""ValorDescontoDI"",
							 COALESCE(""U_TAX4_VT"",'')					AS ""ViaTransporteInternacional"" 
							 FROM ""@TAX4_DADOSDI"" DI
							 LEFT JOIN ""@TAX4_ADICOES"" AD ON DI.""U_TAX4_DocEntry"" = AD.""U_TAX4_DocEntry"" and DI.""U_TAX4_LineNum"" = AD.""U_TAX4_LineNum""
							 WHERE DI.""U_TAX4_DocEntry"" = T0.""DocEntry""
							 AND DI.""U_TAX4_LineNum"" = TL.""LineNum""
							 FOR JSON)
							 AS ""DadosDI""");
            #endregion DADOSDI
            sb.AppendLine(@$"FROM {B1TableNameChild}1 TL 
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
							FROM {B1TableNameChild}6 DP
							WHERE DP.""DocEntry"" = T0.""DocEntry""
							FOR JSON) 
							AS ""Duplicata"" ");
            #endregion DUPLICATA
            #region DOCREF
            sb.AppendLine($@",(
							SELECT
							COALESCE(DC.""U_TAX4_DtDoc"",'')				AS ""DataDocRef"",
							COALESCE(DC.""U_TAX4_Cnpj"",'')					AS ""CnpjDocRef"",
							COALESCE(DC.""U_TAX4_Chave"",'')				AS ""ChaveDocRef"",
							COALESCE(DC.""U_TAX4_Serie"",'')				AS ""SerieDocRef"",
							COALESCE(DC.""U_TAX4_NumNf"",'')				AS ""NumNfDocRef"",
							COALESCE(DC.""U_TAX4_CUf"",'')					AS ""CUfDocRef"",
							COALESCE(DC.""U_TAX4_Mod"",'')					AS ""ModDocRef""
							FROM ""@TAX4_DOCREF"" DC
							WHERE DC.""U_TAX4_DocEntry"" = T0.""DocEntry""
							FOR JSON)
							AS ""DocRef"" ");
            #endregion DOCREF

            sb.AppendLine($@"FROM {B1TableName} T0  
							JOIN ONFM OM ON T0.""Model"" = OM.""AbsEntry""
							JOIN ""@TAX4_CONFIG"" CO ON T0.""BPLId"" = CO.""U_TAX4_Filial""
							LEFT JOIN NFN1 NF ON T0.""SeqCode"" = NF.""SeqCode""");
            sb.AppendLine(useCasesB1.GetCommandUseCase());
            sb.AppendLine(@"AND ADD_DAYS (TO_DATE (CURRENT_DATE, 'YYYY-MM-DD'), -(CO.""U_TAX4_DtRetro"")) <= T0.""DocDate""");
            sb.AppendLine("FOR JSON");
            return Convert.ToString(sb);
        }
        public string RetunCommandParceiroHANA2()
        {
            string command;
            if (B1TableType == DBTableNameRepository.Type.Saida)
            {
                command = $@",REPLACE(REPLACE((
 							SELECT
								COALESCE(T0.""CardCode"",'') 					   AS ""CodigoParceiro"",
	 							COALESCE(T0.""CardName"", '')                      AS ""RazaoSocialParceiro"",
	 							COALESCE(T0.""Address"", '')                       AS ""EnderecoParceiro"",
	 							COALESCE(PT.""TaxId0"", '')                        AS ""CnpjParceiro"",
								COALESCE(PT.""TaxId8"",'')						   AS ""InscSuframa"",
	 							CASE PT.""TaxId1""
	 							WHEN 'Isento' THEN('')
	 							ELSE COALESCE(PT.""TaxId1"", '')
	 							END AS ""InscIeParceiro"",
	 							COALESCE(PT.""TaxId3"", '')                        AS ""InscMunParceiro"",
	 							COALESCE(PT.""TaxId4"", '')                        AS ""CpfParceiro"",
	 							COALESCE(PT.""StreetS"", '')                       AS ""LogradouroParceiro"",
	 							COALESCE(PT.""BuildingS"", '')                     AS ""ComplementoParceiro"",
	 							COALESCE(PT.""BlockS"", '')                        AS ""BairroParceiro"",
	 							COALESCE(PT.""ZipCodeS"", '')                      AS ""CEPParceiro"",
	 							COALESCE(PT.""CountyS"", '')                       AS ""MunicipioParceiro"",
								COALESCE(PT.""StateS"", '')                        AS ""UFParceiro"",
	 							COALESCE(PT.""CityS"", '')                         AS ""CidadeParceiro"",
	 							COALESCE(PT.""AddrTypeS"", '')                     AS ""TipoLogradouroParceiro"",
	 							COALESCE(PT.""StreetNoS"", '')                     AS ""NumeroLogradouroParceiro"",
	 							COALESCE(OY.""CntCodNum"", '')                     AS ""CodigoPaisParceiro"",
		 						COALESCE(OT.""IbgeCode"", '')                      AS ""CodigoIBGEMunicipioParceiro"",
		 						COALESCE(CONCAT(OD.""Phone2"", OD.""Phone1""), '') AS ""FoneParceiro"",
		 						COALESCE(OD.""E_Mail"", '')                        AS ""EmailParceiro"",
		 						COALESCE(OY.""Name"", '')                          AS ""NomePaisParceiro"",
		 						COALESCE(UF.""U_TAX4_Cod"", '')                    AS ""CodigoUFParceiro"",
		 						COALESCE(D1.""U_TAX4_indIEDest"", '')              AS ""IndicadorIEParceiro"",
		 						COALESCE(PT.""Incoterms"", '')                     AS ""ModalidadeFrete"",
								COALESCE(PT.""TaxId5"",'')						   AS ""IdEstrangeiro""
								FROM {B1TableNameChild}12 PT
								LEFT JOIN OCRY OY ON PT.""CountryS"" = OY.""Code""
								LEFT JOIN OCNT OT ON PT.""CountyS"" = OT.""AbsId""
								LEFT JOIN OCRD OD ON T0.""CardCode"" = OD.""CardCode""
								LEFT JOIN ""@TAX4_UF"" UF ON PT.""StateS"" = UF.""U_TAX4_Uf""
								LEFT JOIN CRD1 D1 ON OD.""CardCode"" = D1.""CardCode"" and T0.""ShipToCode"" = D1.""Address""
								WHERE PT.""DocEntry"" = T0.""DocEntry"" and D1.""AdresType"" = 'S'
								FOR JSON),'[',''),']','') 
								AS ""Parceiro""";
                return command;
            }
            else
            {
                command = $@",REPLACE(REPLACE((
 							SELECT
								COALESCE(T0.""CardCode"",'') 					   AS ""CodigoParceiro"",
	 							COALESCE(T0.""CardName"", '')                      AS ""RazaoSocialParceiro"",
	 							COALESCE(T0.""Address"", '')                       AS ""EnderecoParceiro"",
	 							COALESCE(PT.""TaxId0"", '')                        AS ""CnpjParceiro"",
	 							CASE PT.""TaxId1""
	 							WHEN 'Isento' THEN('')
	 							ELSE COALESCE(PT.""TaxId1"", '')
	 							END AS ""InscIeParceiro"",
	 							COALESCE(PT.""TaxId3"", '')                        AS ""InscMunParceiro"",
	 							COALESCE(PT.""TaxId4"", '')                        AS ""CpfParceiro"",
	 							COALESCE(PT.""StreetB"", '')                       AS ""LogradouroParceiro"",
	 							COALESCE(PT.""BuildingB"", '')                     AS ""ComplementoParceiro"",
	 							COALESCE(PT.""BlockB"", '')                        AS ""BairroParceiro"",
	 							COALESCE(PT.""ZipCodeB"", '')                      AS ""CEPParceiro"",
	 							COALESCE(PT.""CountyB"", '')                       AS ""MunicipioParceiro"",
								COALESCE(PT.""StateB"", '')                        AS ""UFParceiro"",
	 							COALESCE(PT.""CityB"", '')                         AS ""CidadeParceiro"",
	 							COALESCE(PT.""AddrTypeB"", '')                     AS ""TipoLogradouroParceiro"",
	 							COALESCE(PT.""StreetNoB"", '')                     AS ""NumeroLogradouroParceiro"",
	 							COALESCE(OY.""CntCodNum"", '')                     AS ""CodigoPaisParceiro"",
		 						COALESCE(OT.""IbgeCode"", '')                      AS ""CodigoIBGEMunicipioParceiro"",
		 						COALESCE(CONCAT(OD.""Phone2"", OD.""Phone1""), '') AS ""FoneParceiro"",
		 						COALESCE(OD.""E_Mail"", '')                        AS ""EmailParceiro"",
		 						COALESCE(OY.""Name"", '')                          AS ""NomePaisParceiro"",
		 						COALESCE(UF.""U_TAX4_Cod"", '')                    AS ""CodigoUFParceiro"",
		 						COALESCE(D1.""U_TAX4_indIEDest"", '')              AS ""IndicadorIEParceiro"",
		 						COALESCE(PT.""Incoterms"", '')                     AS ""ModalidadeFrete"",
								COALESCE(PT.""TaxId5"",'')						   AS ""IdEstrangeiro""
								FROM {B1TableNameChild}12 PT
								LEFT JOIN OCRY OY ON PT.""CountryB"" = OY.""Code""
								LEFT JOIN OCNT OT ON PT.""CountyB"" = OT.""AbsId""
								LEFT JOIN OCRD OD ON T0.""CardCode"" = OD.""CardCode""
								LEFT JOIN ""@TAX4_UF"" UF ON PT.""StateB"" = UF.""U_TAX4_Uf""
								LEFT JOIN CRD1 D1 ON OD.""CardCode"" = D1.""CardCode"" and T0.""PayToCode"" = D1.""Address""
								WHERE PT.""DocEntry"" = T0.""DocEntry"" and D1.""AdresType"" = 'B'
								FOR JSON),'[',''),']','') 
								AS ""Parceiro""";
                return command;
            }
        }
        #endregion SETUP QUERY HANA 2.0

        #region SETUP QUERY HANA 1.0
        public string ReturnCommandB1CancelDocumentInOrbit()
        {
            sb = new StringBuilder();
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
							 COALESCE(C2.""U_TAX4_EstabID"",'')	     AS ""BranchId"",
							 COALESCE(T0.""DocDate"",'')			 AS ""DataDocumento"",
							 COALESCE(CO.""U_TAX4_CamPDF"",'')		 AS ""CaminhoPDF"",
							 COALESCE(CO.""U_TAX4_CamXML"",'')		 AS ""CaminhoXML""
							 FROM {B1TableName} T0
							 JOIN ONFM OM ON T0.""Model"" = OM.""AbsEntry""
							 JOIN {B1TableNameChild}1 T1 ON T0.""DocEntry"" = T1.""DocEntry""
							 JOIN ""@TAX4_CONFIG"" CO ON T0.""BPLId"" = CO.""U_TAX4_Filial""
							 JOIN ""@TAX4_LCONFIGADDON"" C2 ON T0.""BPLId"" = C2.""U_TAX4_Empresa""
							 LEFT JOIN NFN1 NF ON T0.""SeqCode"" = NF.""SeqCode""
							 WHERE
							 NF.""SeqCode"" <> 0
							 and T0.""U_TAX4_CARGAFISCAL"" = 'N'
							 and T0.""CANCELED"" = 'C'
							 and T0.""DocStatus"" = 'C'");
            sb.AppendLine(useCasesB1.GetCommandUseCase());
			sb.AppendLine(@"AND T0.""DocDate"" >= CO.""U_TAX4_DateInt""");
			//sb.AppendLine(@"AND ADD_DAYS (TO_DATE (CURRENT_DATE, 'YYYY-MM-DD'), -(CO.""U_TAX4_DtRetro"")) <= T0.""DocDate""");
			return Convert.ToString(sb);
        }
        public string ReturnCommandB1ConsultDocumentInOrbit()
        {
            sb = new StringBuilder();
            sb.AppendLine($@"SELECT
	                         COALESCE(T0.""DocEntry"",0)            AS ""DocEntry"",
							 COALESCE(T1.""BaseEntry"", 0)           AS ""BaseEntry"",
							 COALESCE(T0.""U_TAX4_CodInt"", '')      AS ""CodInt"",
							 COALESCE(T0.""U_TAX4_IdRet"", '')       AS ""IdRetornoOrbit"",
							 COALESCE(OM.""NfmCode"", '')            AS ""ModeloDocumento"",
							 COALESCE(T0.""ObjType"", 0)             AS ""ObjetoB1"",
							 COALESCE(CO.""U_TAX4_CamPDF"",'')		 AS ""CaminhoPDF"",
							 COALESCE(CO.""U_TAX4_CamXML"",'')		 AS ""CaminhoXML""
							 FROM {B1TableName} T0
							 JOIN ONFM OM ON T0.""Model"" = OM.""AbsEntry""
							 JOIN ""@TAX4_CONFIG"" CO ON T0.""BPLId"" = CO.""U_TAX4_Filial""
							 JOIN {B1TableNameChild}1 T1 ON T0.""DocEntry"" = T1.""DocEntry""
							 LEFT JOIN NFN1 NF ON T0.""SeqCode"" = NF.""SeqCode""");
            sb.AppendLine(useCasesB1.GetCommandUseCase());
            return Convert.ToString(sb);
        }
        public string ReturnCommandHeader()
        {
            sb.Append(@$"SELECT
	                         COALESCE(T0.""DocEntry"",0)            AS ""DocEntry"",
	                         COALESCE(T0.""U_TAX4_CodInt"",'')      AS ""CodInt"",
	                         COALESCE(OM.""NfmCode"",'')            AS ""ModeloDocumento"",
	                         COALESCE(NF.""SeqCode"",0)				AS ""CargaFiscal"",
	                         COALESCE(T0.""ObjType"",0)				AS ""ObjetoB1"",
	                         COALESCE(T0.""CANCELED"",'')           AS ""CANCELED"",
							 COALESCE(T0.""BPLId"",0)				AS ""BPLId"",
	                         CASE T0.""ObjType""
	 		                        WHEN '13' THEN ('output')
	 		                        WHEN '15' THEN ('output')
	 		                        WHEN '18' THEN ('input')
	 		                        WHEN '20' THEN ('input')
	 		                        END AS ""TipoDocumento"",
							 CASE T0.""ObjType""
	 						        WHEN '13' THEN ('1')
									WHEN '16' THEN ('0')
									WHEN '14' THEN ('0')
	 						        WHEN '15' THEN ('1')
	 						        WHEN '18' THEN ('0')
	 						        WHEN '20' THEN ('0')
	 						        END AS ""TipoNF""");
            sb.AppendLine($@"FROM {B1TableName} T0  
							JOIN ONFM OM ON T0.""Model"" = OM.""AbsEntry""
							JOIN ""@TAX4_CONFIG"" CO ON T0.""BPLId"" = CO.""U_TAX4_Filial""
							LEFT JOIN NFN1 NF ON T0.""SeqCode"" = NF.""SeqCode""");
            sb.AppendLine(useCasesB1.GetCommandUseCase());
            sb.AppendLine(@"AND T0.""DocDate"" >= CO.""U_TAX4_DateInt""");
            return Convert.ToString(sb);
        }
        public string ReturnCommandIdentificacao(Invoice invoice)
        {
            sb = new StringBuilder();
            sb.AppendLine($@"SELECT
	 	  							 COALESCE(OI.""DocEntry"",0) 	  			   AS ""DocEntry"",
									 COALESCE(OI.""DocNum"", 0)                    AS ""DocNum"",
									 COALESCE(OI.""DocTime"", 0)                   AS ""DocTime"",
									 COALESCE(OI.""TaxDate"", '')                  AS ""DataEmissao"",
									 COALESCE(OI.""DocDate"", '')                  AS ""DataLancamento"",
									 COALESCE(CAST(OI.""Serial"" AS VARCHAR), '')  AS ""NumeroDocumento"",
									 COALESCE(OI.""SeriesStr"", '')                AS ""SerieDocumento"",
									 COALESCE(OI.""U_TAX4_Justi"", '')             AS ""Justificativa"",
								     COALESCE(OI.""U_TAX4_Chave"", '')             AS ""ChaveDeAcessoNFe"",
									 COALESCE(OI.""U_TAX4_Prot"", '')              AS ""ProtocoloNFe"",
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
									 COALESCE(OP.""Descript"",'')				   AS ""DescricaoFormaDePagamento"",
									 COALESCE(CG.""U_TAX4_EstabID"", '')           AS ""BranchId"",
									 COALESCE(C2.""U_TAX4_versao"", '')            AS ""Versao"",
									 COALESCE(OI.""U_TAX4_Chave"", '')             AS ""Key"",
									 COALESCE(OI.""DocTotal"", 0)                  AS ""DadosCobranca"",
									 COALESCE(C2.""U_TAX4_Operacao"", '')          AS ""OperacaoNFe"",
									 COALESCE(OI.""U_TAX4_IndFinal"", '')          AS ""ConsumidorFinal"",
									 COALESCE(OI.""U_TAX4_IndPres"", '')           AS ""IndicadorPresenca"",
									 COALESCE(CAST(OI.""U_TAX4_indInter"" AS INT), 0) AS ""IndicadorIntermediario"",
									 COALESCE(OI.""U_TAX4_Prot"", '')              AS ""NumeroProtocolo"",
									 COALESCE(C2.""U_TAX4_itCultural"", '')        AS ""IncentivadorCultural"",
									 COALESCE(C2.""U_TAX4_tipoRPS"",'')			   AS ""TipoRps"",
									 COALESCE(C2.""U_TAX4_RegEspTrib"",'')		   AS ""RegEspTrib"",
									 COALESCE(OI.""U_TAX4_InfAd"",'')			   AS ""InfAdFisco"",
								     COALESCE(OI.""U_TAX4_DataHora"",'')		   AS ""EnviaDataHora"",
									 COALESCE(OI.""U_TAX4_DataEnt"",'0001-01-01T00:00:00')		   AS ""DataDeEnvio"",
									 COALESCE(OI.""U_TAX4_HoraEnt"",'')			   AS ""HoraDeEnvio"",
									 COALESCE(OI.""U_TAX4_Local_Exp"",'')		   AS ""LocalDeExportacao"",
									 COALESCE(OI.""U_TAX4_UF_Exp"",'')			   AS ""UFDeExportacao"",
									 COALESCE(OI.""Header"",'')					   AS ""ObsAbertura"",
									 COALESCE(C2.""U_TAX4_justf"",'')			   AS ""JustContigencia"",
								     COALESCE(OI.""U_TAX4_xCampo"",'')			   AS ""ObsFiscoCampo"",
									 COALESCE(OI.""U_TAX4_xTexto"",'')			   AS ""ObsFiscoTexto"",
									 COALESCE(PT.""NetWeight"",0)				   AS ""PesoLiquido"",
									 COALESCE(PT.""GrsWeight"",0)				   AS ""PesoBruto"",
									 COALESCE(PT.""QoP"",0)						   AS ""QuantidadeVolume"",
									 COALESCE(PT.""PackDesc"",'')				   AS ""DescricaoVolume"",
									 COALESCE(PT.""Brand"",'')					   AS ""MarcaVolume"",
									 COALESCE(PT.""NoSU"",0)					   AS ""NumeroVolume"",
									 COALESCE(C2.""U_TAX4_EnvEm"",'')			   AS ""EnviaEmail"",
									 COALESCE(PT.""Vehicle"",'')				   AS ""PlacaVeiculo"",
									 COALESCE(PT.""VidState"",'')				   AS ""EstadoVeiculo"",
									 COALESCE(PT.""Carrier"",'')				   AS ""Carrier"",
									 COALESCE(OI.""DiscSum"",0)					   AS ""DescontoTotal""

									 FROM {B1TableName} OI 
								     JOIN {B1TableNameChild}12 PT ON PT.""DocEntry"" = OI.""DocEntry""
									 LEFT JOIN OUSG OU ON PT.""MainUsage"" = OU.""ID""
									 LEFT JOIN OCTG OC ON OI.""GroupNum"" = OC.""GroupNum""
									 LEFT JOIN OPYM OP ON OI.""PeyMethod"" = OP.""PayMethCod""
									 JOIN ""@TAX4_LCONFIGADDON"" CG ON OI.""BPLId"" = CG.""U_TAX4_Empresa""
									 JOIN ""@TAX4_CONFIG"" C2 ON OI.""BPLId"" = C2.""U_TAX4_Filial""
									 WHERE OI.""DocEntry"" = {invoice.DocEntry}");
            return Convert.ToString(sb);
        }
        public string ReturnCommandParceiroHANA1(Invoice invoice)
        {
            sb = new StringBuilder();
            if (B1TableType == DBTableNameRepository.Type.Saida)
            {
                sb.AppendLine(@$"SELECT
								COALESCE(T0.""CardCode"",'') 					   AS ""CodigoParceiro"",
	 							COALESCE(T0.""CardName"", '')                      AS ""RazaoSocialParceiro"",
	 							COALESCE(T0.""Address"", '')                       AS ""EnderecoParceiro"",
	 							COALESCE(PT.""TaxId0"", '')                        AS ""CnpjParceiro"",
								COALESCE(PT.""TaxId8"",'')						   AS ""InscSuframa"",
	 						    CASE PT.""TaxId1""
	 							WHEN 'Isento' THEN('')
	 							ELSE COALESCE(PT.""TaxId1"", '')
	 							END AS ""InscIeParceiro"",
	 							COALESCE(PT.""TaxId3"", '')                        AS ""InscMunParceiro"",
	 							COALESCE(PT.""TaxId4"", '')                        AS ""CpfParceiro"",
	 							COALESCE(PT.""StreetS"", '')                       AS ""LogradouroParceiro"",
	 							COALESCE(PT.""BuildingS"", '')                     AS ""ComplementoParceiro"",
	 							COALESCE(PT.""BlockS"", '')                        AS ""BairroParceiro"",
	 							COALESCE(PT.""ZipCodeS"", '')                      AS ""CEPParceiro"",
	 							COALESCE(PT.""CountyS"", '')                       AS ""MunicipioParceiro"",
								COALESCE(PT.""StateS"", '')                        AS ""UFParceiro"",
	 							COALESCE(PT.""CityS"", '')                         AS ""CidadeParceiro"",
	 							COALESCE(PT.""AddrTypeS"", '')                     AS ""TipoLogradouroParceiro"",
	 							COALESCE(PT.""StreetNoS"", '')                     AS ""NumeroLogradouroParceiro"",
	 							COALESCE(OY.""CntCodNum"", '')                     AS ""CodigoPaisParceiro"",
		 						COALESCE(OT.""IbgeCode"", '')                      AS ""CodigoIBGEMunicipioParceiro"",
		 						COALESCE(CONCAT(OD.""Phone2"", OD.""Phone1""), '') AS ""FoneParceiro"",
		 						COALESCE(OD.""E_Mail"", '')                        AS ""EmailParceiro"",
		 						COALESCE(OY.""Name"", '')                          AS ""NomePaisParceiro"",
		 						COALESCE(UF.""U_TAX4_Cod"", '')                    AS ""CodigoUFParceiro"",
		 						COALESCE(D1.""U_TAX4_indIEDest"", '')              AS ""IndicadorIEParceiro"",
		 						COALESCE(PT.""Incoterms"", '')                     AS ""ModalidadeFrete"",
								COALESCE(PT.""TaxId5"",'')						   AS ""IdEstrangeiro""
								FROM {B1TableNameChild}12 PT
								JOIN {B1TableName} T0 ON PT.""DocEntry"" = T0.""DocEntry""
								LEFT JOIN OCRY OY ON PT.""CountryS"" = OY.""Code""
								LEFT JOIN OCNT OT ON PT.""CountyS"" = OT.""AbsId""
								LEFT JOIN OCRD OD ON T0.""CardCode"" = OD.""CardCode""
								LEFT JOIN ""@TAX4_UF"" UF ON PT.""StateS"" = UF.""U_TAX4_Uf""
								LEFT JOIN CRD1 D1 ON OD.""CardCode"" = D1.""CardCode"" and T0.""ShipToCode"" = D1.""Address""
								WHERE PT.""DocEntry"" = {invoice.DocEntry} and D1.""AdresType"" = 'S'");
            }
            else
            {
                sb.AppendLine(@$"SELECT
								COALESCE(T0.""CardCode"",'') 					   AS ""CodigoParceiro"",
	 							COALESCE(T0.""CardName"", '')                      AS ""RazaoSocialParceiro"",
	 							COALESCE(T0.""Address"", '')                       AS ""EnderecoParceiro"",
	 							COALESCE(PT.""TaxId0"", '')                        AS ""CnpjParceiro"",
	 							CASE PT.""TaxId1""
	 							WHEN 'Isento' THEN('')
	 							ELSE COALESCE(PT.""TaxId1"", '')
	 							END AS ""InscIeParceiro"",
	 							COALESCE(PT.""TaxId3"", '')                        AS ""InscMunParceiro"",
	 							COALESCE(PT.""TaxId4"", '')                        AS ""CpfParceiro"",
	 							COALESCE(PT.""StreetB"", '')                       AS ""LogradouroParceiro"",
	 							COALESCE(PT.""BuildingB"", '')                     AS ""ComplementoParceiro"",
	 							COALESCE(PT.""BlockB"", '')                        AS ""BairroParceiro"",
	 							COALESCE(PT.""ZipCodeB"", '')                      AS ""CEPParceiro"",
	 							COALESCE(PT.""CountyB"", '')                       AS ""MunicipioParceiro"",
								COALESCE(PT.""StateB"", '')                        AS ""UFParceiro"",
	 							COALESCE(PT.""CityB"", '')                         AS ""CidadeParceiro"",
	 							COALESCE(PT.""AddrTypeB"", '')                     AS ""TipoLogradouroParceiro"",
	 							COALESCE(PT.""StreetNoB"", '')                     AS ""NumeroLogradouroParceiro"",
	 							COALESCE(OY.""CntCodNum"", '')                     AS ""CodigoPaisParceiro"",
		 						COALESCE(OT.""IbgeCode"", '')                      AS ""CodigoIBGEMunicipioParceiro"",
		 						COALESCE(CONCAT(OD.""Phone2"", OD.""Phone1""), '') AS ""FoneParceiro"",
		 						COALESCE(OD.""E_Mail"", '')                        AS ""EmailParceiro"",
		 						COALESCE(OY.""Name"", '')                          AS ""NomePaisParceiro"",
		 						COALESCE(UF.""U_TAX4_Cod"", '')                    AS ""CodigoUFParceiro"",
		 						COALESCE(D1.""U_TAX4_indIEDest"", '')              AS ""IndicadorIEParceiro"",
		 						COALESCE(PT.""Incoterms"", '')                     AS ""ModalidadeFrete"",
								COALESCE(PT.""TaxId5"",'')						   AS ""IdEstrangeiro""
								FROM {B1TableNameChild}12 PT
								JOIN {B1TableName} T0 ON PT.""DocEntry"" = T0.""DocEntry""
								LEFT JOIN OCRY OY ON PT.""CountryB"" = OY.""Code""
								LEFT JOIN OCNT OT ON PT.""CountyB"" = OT.""AbsId""
								LEFT JOIN OCRD OD ON T0.""CardCode"" = OD.""CardCode""
								LEFT JOIN ""@TAX4_UF"" UF ON PT.""StateB"" = UF.""U_TAX4_Uf""
								LEFT JOIN CRD1 D1 ON OD.""CardCode"" = D1.""CardCode"" and T0.""PayToCode"" = D1.""Address""
								WHERE PT.""DocEntry"" = {invoice.DocEntry} and D1.""AdresType"" = 'B' OR PT.""DocEntry"" = {invoice.DocEntry} and D1.""AdresType"" is null ");


            }
            return Convert.ToString(sb);
        }
        public string ReturnCommandFilial(Invoice invoice)
        {
            sb = new StringBuilder();
            sb.AppendLine($@"SELECT
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
							 LEFT JOIN OCNT OT ON CAST(OB.""County"" AS VARCHAR) = CAST(OT.""AbsId"" AS VARCHAR)
							 JOIN ""@TAX4_UF"" UF ON OB.""State"" = UF.""U_TAX4_Uf""
							 WHERE OB.""BPLId"" = {invoice.BPLId}");



            return Convert.ToString(sb);
        }
        public string ReturnCommandHeaderLinha(Invoice invoice)
        {
            sb = new StringBuilder();
            sb.AppendLine($@"SELECT
								 COALESCE(TL.""DocEntry"",0)														AS ""DocEntry"",
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
								 COALESCE(TL.""TaxOnly"",'')                                                        AS ""SoImposto"",
								 COALESCE(OT.""U_TAX4_cest"",'')                                                    AS ""CodigoCEST"",
								 COALESCE(TL.""Dscription"", '')													AS ""DescricaoItemLinhaDocumento"",
								 COALESCE(OT.""CodeBars"", '')														AS ""CodigoDeBarras"",
								 COALESCE(OT.""SalUnitMsr"", '')													AS ""UnidadeComercial"",
								 COALESCE(OT.""U_TAX4_CodAti"", '')													AS ""CodigoAtividade"",
								 COALESCE(CM.""NcmCode"", '')														AS ""CodigoNCM"",
								 COALESCE(TL.""CFOPCode"", '')														AS ""CodigoCFOP"",
								 COALESCE(OT.""U_TAX4_LisSer"", '')													AS ""ItemListaServico"",
								 COALESCE(OD.""ServiceCD"", '')														AS ""CodigoTributacaoMuncipio"",
								 COALESCE(OS.""ServiceCD"",'')														AS ""CodigoServicoEntrada"",
								 COALESCE(TL.""U_TAX4_MotDes"", '')													AS ""MotivoDesoneracao"",
								 COALESCE(TL.""U_TAX4_nPedido"",'')													AS ""NumeroPedido"",
								 COALESCE(TL.""U_TAX4_nItemPedido"",'')											    AS ""NumeroItemPedido"",
								 CASE SUBSTRING(TL.""CFOPCode"",0,1) 
								 WHEN '1' THEN('1')
								 WHEN '5' THEN('1')
								 WHEN '2' THEN('2')
								 WHEN '6' THEN('2')
								 WHEN '3' THEN('3')
								 WHEN '7' THEN('3')
								 END AS ""IdLocalDestino"",
								 CASE(TL.""CSTfIPI"")
								 WHEN '02' THEN('302')
								 WHEN '52' THEN('302')
								 WHEN '04' THEN('002')
								 WHEN '54' THEN('002')
								 WHEN '05' THEN('102')
								 WHEN '55' THEN('102')
								 ELSE '999'
								 END AS ""cEnq"",
								 COALESCE(TL.""Text"",'')	as ""Text"" ");
			if (VerifyIfFieldExists("UFFiscBene"))
			{
				sb.AppendLine(@$"	 ,COALESCE(TX.""UFFiscBene"",0)		   AS ""CodigoBeneficioFiscal""");
			}
            sb.AppendLine($@"FROM {B1TableNameChild}1 TL 
							 JOIN OITM OT ON TL.""ItemCode"" = OT.""ItemCode""
							 LEFT JOIN ONCM CM ON OT.""NCMCode"" = CM.""AbsEntry""
							 LEFT JOIN ""OSCD"" OD ON OT.""OSvcCode"" = OD.""AbsEntry""
							 LEFT JOIN ""OSCD"" OS ON OT.""ISvcCode"" = OS.""AbsEntry""
							 WHERE TL.""DocEntry"" = {invoice.DocEntry}");
            return Convert.ToString(sb);
        }
        public string ReturnCommandImpostoLinha(CabecalhoLinha cabecalhoLinha)
        {
            sb = new StringBuilder();
            sb.AppendLine(@$"SELECT
								 COALESCE(TX.""TaxSum"",0) 			   AS ""ValorImposto"",
								 COALESCE(TX.""TaxRate"", 0)           AS ""PorcentagemImposto"",
								 COALESCE(TT.""U_TAX4_TpImp"", '')     AS ""TipoImpostoOrbit"",
								 COALESCE(TT.""Name"", '')             AS ""NomeImposto"",
								 COALESCE(TX.""BaseSum"", 0)           AS ""ValorBaseImposto"",
								 COALESCE(TX.""Unencumbrd"", '')       AS ""SimOuNaoDesoneracao"" ");
            if (VerifyIfFieldExists(VDifL))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_VDifL"",0)			   AS ""VDif""");
            }
            if (VerifyIfFieldExists(PDif))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_PDif"",0)			   AS ""PDif""");
            }
            if (VerifyIfFieldExists(ReduICMS))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_ReduICMS"",0)			   AS ""pRedBc""");
            }
            if (VerifyIfFieldExists(VICMSOpL))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_VICMSOpL"",0)			   AS ""VICMSOp""");
            }
            if (VerifyIfFieldExists(MVast))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_Lucro"",0)			   AS ""MVast""");
            }
            if (VerifyIfFieldExists(AliquotaIntDestino))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_AliqDest"",0)		   AS ""AliquotaIntDestino""");
            }
            if (VerifyIfFieldExists(PartilhaInterestadual))
            {
                sb.AppendLine(@$"	 ,COALESCE(TX.""U_IntPart"",0)		   AS ""PartilhaInterestadual""");
            }
			//ICMS_DE
			if (VerifyIfFieldExists("ICMS_DEL"))
			{
				sb.AppendLine(@$"	 ,COALESCE(TX.""U_ICMS_DEL"",0)		   AS ""ValorIcmsDesonerado""");
			}
			sb.AppendLine(@$"	FROM {B1TableNameChild}4 TX 
								JOIN OSTT TT ON TX.""staType"" = TT.""AbsId"" 
								WHERE TX.""DocEntry"" = {cabecalhoLinha.DocEntry}
								AND TX.""LineNum"" = {cabecalhoLinha.ItemLinhaDocumento}");
            return Convert.ToString(sb);
        }
        public string ReturnCommandRetencoesLinha(CabecalhoLinha cabecalhoLinha)
        {
            sb = new StringBuilder();
            sb.AppendLine(@$"SELECT
									 COALESCE(WT.""Rate"",0) 				AS ""PorcentagemImpostoRetido"",
									 COALESCE(WT.""WTAmnt"", 0)            AS ""ValorImpostoRetido"",
									 COALESCE(OW.""U_TAX4_TpImp"", '')     AS ""TipoImpostoOrbit"",
								     COALESCE(WT.""TaxbleAmnt"",0)		   AS ""TaxbleAmnt""
									 FROM {B1TableNameChild}5 WT
									 INNER JOIN OWHT OH on WT.""WTCode"" = OH.""WTCode""
									 INNER JOIN OWTT OW on OH.""WTTypeId"" = OW.""WTTypeId""
									 WHERE WT.""AbsEntry"" = {cabecalhoLinha.DocEntry}
									 and WT.""Doc1LineNo"" = {cabecalhoLinha.ItemLinhaDocumento}");
            return Convert.ToString(sb);
        }
        public string ReturnCommandDespesaAdicional(CabecalhoLinha cabecalhoLinha)
        {
            sb = new StringBuilder();
            sb.AppendLine(@$" SELECT
								COALESCE(OX.""ExpnsType"",'') 			AS ""TipoDespesa"",
								COALESCE(TD.""LineTotal"", 0)			AS ""ValorUnitarioDespesa""
								FROM {B1TableNameChild}13 TD
								JOIN OEXD OX ON TD.""ExpnsCode"" = OX.""ExpnsCode""
								WHERE TD.""DocEntry"" = {cabecalhoLinha.DocEntry}
								AND TD.""LineNum"" = {cabecalhoLinha.ItemLinhaDocumento}");
            return Convert.ToString(sb);
        }
        public string ReturnCommandDadosDI(CabecalhoLinha cabecalhoLinha)
        {
            sb = new StringBuilder();
            sb.AppendLine($@"SELECT 
							 COALESCE(""U_TAX4_CNPJAD"",'')				AS ""CNPJAdiquirente"",
							 COALESCE(""U_TAX4_DDA"",'')					AS ""DataDesembaracoAduaneiro"",
							 COALESCE(""U_TAX4_DDI"",'')					AS ""Ddi"",
							 COALESCE(""U_TAX4_FI"",'')					AS ""TpIntermedio"",
							 COALESCE(""U_TAX4_LDA"",'')					AS ""LocalDesembaracoAduaneiro"",
							 COALESCE(""U_TAX4_NAdicao"",'')				AS ""NumeroAdicao"",
							 COALESCE(""U_TAX4_NDI"",'')					AS ""NumeroDocImportacao"",
							 COALESCE(""U_TAX4_NRDRW"",'')					AS ""NumeroDrawBack"",
							 COALESCE(""U_TAX4_NSeqAdicao"",'')			AS ""NumeroSequenciaAdicao"",
							 COALESCE(""U_TAX4_UFAD"",'')					AS ""UFAdiquirente"",
							 COALESCE(""U_TAX4_UFD"",'')					AS ""UFDesembaracoAduaneiro"",
							 COALESCE(""U_TAX4_VAFRMM"",'')				AS ""ValorFrmm"",
							 COALESCE(""U_TAX4_VDesc"",0)					AS ""ValorDescontoDI"",
							 COALESCE(""U_TAX4_VT"",'')					AS ""ViaTransporteInternacional"" 
							 FROM ""@TAX4_DADOSDI"" DI
							 LEFT JOIN ""@TAX4_ADICOES"" AD ON DI.""U_TAX4_DocEntry"" = AD.""U_TAX4_DocEntry"" and DI.""U_TAX4_LineNum"" = AD.""U_TAX4_LineNum""
							 WHERE DI.""U_TAX4_DocEntry"" = {cabecalhoLinha.DocEntry}
							 AND DI.""U_TAX4_LineNum"" = {cabecalhoLinha.ItemLinhaDocumento}");
            return Convert.ToString(sb);
        }
        public string ReturnCommandDuplicata(Invoice invoice)
        {
            sb = new StringBuilder();
            sb.AppendLine($@"SELECT
								COALESCE(DP.""InstlmntID"",0) 		AS ""NumeroDuplicata"",
								COALESCE(DP.""DueDate"", '')           AS ""DataVencimento"",
								COALESCE(DP.""InsTotal"", 0)           AS ""ValorDuplicata""
								FROM {B1TableNameChild}6 DP
								WHERE DP.""DocEntry"" = {invoice.DocEntry}");
            return Convert.ToString(sb);
        }
        public string ReturnCommandDocRef(Invoice invoice)
        {
            sb = new StringBuilder();
            sb.AppendLine($@"SELECT
								COALESCE(DC.""U_TAX4_DtDoc"",'')				AS ""DataDocRef"",
								COALESCE(DC.""U_TAX4_Cnpj"",'')					AS ""CnpjDocRef"",
								COALESCE(DC.""U_TAX4_Chave"",'')				AS ""ChaveDocRef"",
								COALESCE(DC.""U_TAX4_Serie"",'')				AS ""SerieDocRef"",
								COALESCE(DC.""U_TAX4_NumNf"",'')				AS ""NumNfDocRef"",
								COALESCE(DC.""U_TAX4_CUf"",'')					AS ""CUfDocRef"",
								COALESCE(DC.""U_TAX4_Mod"",'')					AS ""ModDocRef""
								FROM ""@TAX4_DOCREF"" DC
								WHERE DC.""U_TAX4_DocEntry"" = {invoice.DocEntry}");
            return Convert.ToString(sb);
        }

        public string ReturnCommandListEmails(Invoice invoice)
        {
            sb = new StringBuilder();
            sb.AppendLine($@"SELECT
								COALESCE(T0.""E_MailL"",'') AS ""email""
								FROM ""OCPR"" T0
								WHERE T0.""CardCode"" = '{invoice.Parceiro.CodigoParceiro}' AND ""Active"" = 'Y' AND ""NFeRcpn"" = 'Y' ");
            return Convert.ToString(sb);

        }

        public string ReturnCommandTransportadora(Invoice invoice)
        {
            sb = new StringBuilder();
            sb.AppendLine($@"SELECT 
		COALESCE(T0.""CardName"",'')	AS ""CardName"",
		COALESCE(C7.""TaxId1"",'')    AS ""InscEstadual"",
		COALESCE(C7.""TaxId0"",'')    AS ""CNPJ"",
		COALESCE(C7.""TaxId4"",'')	AS ""CPF"",
		COALESCE(C1.""AddrType"",'')	AS ""TipoLogradouro"",
		COALESCE(C1.""Street"",'')	AS ""Logradouro"",
		COALESCE(C1.""StreetNo"",'')	AS ""Numero"",
		COALESCE(C1.""Building"",'')	AS ""Complemento"",
		COALESCE(C1.""Block"",'')		AS ""Bairro"",
		COALESCE(C1.""ZipCode"",'')	AS ""CEP"",
		COALESCE(OC.""Name"",'')		AS ""NomeMunicipio"",
		COALESCE(C1.""State"", '')     AS ""UF""
		FROM OCRD T0
		JOIN CRD1 C1 ON T0.""CardCode"" = C1.""CardCode""
		JOIN CRD7 C7 ON T0.""CardCode"" = C7.""CardCode"" AND C1.""Address"" = C7.""Address""
		LEFT JOIN OCNT OC ON T0.""County"" = OC.""AbsId""
		WHERE C1.""AdresType"" = 'S' AND T0.""CardCode"" = '{invoice.Identificacao.Carrier}'");
            return Convert.ToString(sb);
        }
        #endregion SETUP QUERY HANA 1.0

        public bool VerifyIfFieldExists(string ValidFieldExist)
        {
            DataSet queryResult = dbRepo.wrapper.ExecuteQuery(@$"SELECT ""AliasID"" FROM CUFD WHERE ""AliasID"" = '{ValidFieldExist}'");
            if (queryResult.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}

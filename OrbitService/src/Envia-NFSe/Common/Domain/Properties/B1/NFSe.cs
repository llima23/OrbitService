using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service.Common.Domain.Properties.B1
{
    public class NFSe : Orbit.Document_Service.NFSe
    {
        public class Header
        {
            [JsonProperty("DocEntry")]
            public static int DocEntry { get; set; }

            [JsonProperty("DocNum")]
            public static int DocNum { get; set; }

            [JsonProperty("CANCELED")]
            public static string CANCELED { get; set; }

            [JsonProperty("DocDate")]
            public static DateTime DocDate { get; set; }

            [JsonProperty("CardCode")]
            public static string CardCode { get; set; }

            [JsonProperty("ENDERECOT")]
            public static string ENDERECOT { get; set; }

            [JsonProperty("CardName")]
            public static string CardName { get; set; }

            [JsonProperty("DiscPrcnt")]
            public static double DiscPrcnt { get; set; }

            [JsonProperty("Comments")]
            public static string Comments { get; set; }

            [JsonProperty("GroupNum")]
            public static int GroupNum { get; set; }

            [JsonProperty("PeyMethod")]
            public static string PeyMethod { get; set; }

            [JsonProperty("BPChCode")]
            public static string BPChCode { get; set; }

            [JsonProperty("Serial")]
            public static int Serial { get; set; }

            [JsonProperty("SeriesStr")]
            public static string SeriesStr { get; set; }

            [JsonProperty("ObjectType")]
            public static string ObjectType { get; set; }

            [JsonProperty("TaxId0")]
            public static string TaxId0 { get; set; }

            [JsonProperty("TaxId1")]
            public static string TaxId1 { get; set; }

            [JsonProperty("TaxId3")]
            public static string TaxId3 { get; set; }

            [JsonProperty("TaxId4")]
            public static string TaxId4 { get; set; }

            [JsonProperty("TaxId5")]
            public static string TaxId5 { get; set; }

            [JsonProperty("RUAT")]
            public static string RUAT { get; set; }

            [JsonProperty("COMPLEMENTOT")]
            public static string COMPLEMENTOT { get; set; }

            [JsonProperty("BAIRROT")]
            public static string BAIRROT { get; set; }

            [JsonProperty("CEPT")]
            public static string CEPT { get; set; }

            [JsonProperty("CODMUNICIPIOT")]
            public static string CODMUNICIPIOT { get; set; }

            [JsonProperty("ESTADOT")]
            public static string ESTADOT { get; set; }

            [JsonProperty("CIDADET")]
            public static string CIDADET { get; set; }

            [JsonProperty("LOGRADOUROT")]
            public static string LOGRADOUROT { get; set; }

            [JsonProperty("NUMERORUAT")]
            public static string NUMERORUAT { get; set; }

            [JsonProperty("AddrType")]
            public static string AddrType { get; set; }

            [JsonProperty("E_Mail")]
            public static string E_Mail { get; set; }

            [JsonProperty("NTSWebSite")]
            public static string NTSWebSite { get; set; }

            [JsonProperty("Fax")]
            public static string Fax { get; set; }

            [JsonProperty("Phone1")]
            public static string Phone1 { get; set; }

            [JsonProperty("Phone2")]
            public static string Phone2 { get; set; }

            [JsonProperty("ShipToDef")]
            public static string ShipToDef { get; set; }

            [JsonProperty("CardFName")]
            public static string CardFName { get; set; }

            [JsonProperty("County")]
            public static string County { get; set; }

            [JsonProperty("TaxIdNum")]
            public static string TaxIdNum { get; set; }

            [JsonProperty("TaxIdNum3")]
            public static string TaxIdNum3 { get; set; }

            [JsonProperty("TaxIdNum2")]
            public static string TaxIdNum2 { get; set; }

            [JsonProperty("AddtnlId")]
            public static string AddtnlId { get; set; }

            [JsonProperty("CIDADEP")]
            public static string CIDADEP { get; set; }

            [JsonProperty("Block")]
            public static string Block { get; set; }

            [JsonProperty("CEPP")]
            public static string CEPP { get; set; }

            [JsonProperty("RUANUMEROP")]
            public static string RUANUMEROP { get; set; }

            [JsonProperty("RUAP")]
            public static string RUAP { get; set; }

            [JsonProperty("BPLName")]
            public static string BPLName { get; set; }

            [JsonProperty("BPLId")]
            public static int BPLId { get; set; }

            [JsonProperty("State")]
            public static string State { get; set; }

            [JsonProperty("U_TAX4_TmZn")]
            public static string U_TAX4_TmZn { get; set; }

            [JsonProperty("U_TAX4_RegEspTrib")]
            public static string U_TAX4_RegEspTrib { get; set; }

            [JsonProperty("U_TAX4_tipoRPS")]
            public static string U_TAX4_tipoRPS { get; set; }

            [JsonProperty("U_TAX4_ContAuth")]
            public static string U_TAX4_ContAuth { get; set; }

            [JsonProperty("U_TAX4_justf")]
            public static string U_TAX4_Justf { get; set; }

            [JsonProperty("U_TAX4_Operacao")]
            public static string U_TAX4_Operacao { get; set; }

            [JsonProperty("U_TAX4_regTrib")]
            public static string U_TAX4_RegTrib { get; set; }

            [JsonProperty("U_TAX4_Uf")]
            public static string U_TAX4_Uf { get; set; }

            [JsonProperty("U_TAX4_Cod")]
            public static string U_TAX4_Cod { get; set; }

            [JsonProperty("CntCodNum")]
            public static string CntCodNum { get; set; }

            [JsonProperty("IbgeCode")]
            public static string IbgeCode { get; set; }

            [JsonProperty("U_TAX4_NatOper")]
            public static string U_TAX4_NatOper { get; set; }

            [JsonProperty("U_TAX4_condPag")]
            public static string U_TAX4_CondPag { get; set; }

            [JsonProperty("U_TAX4_FormaPagto")]
            public static string U_TAX4_FormaPagto { get; set; }

            [JsonProperty("DocTotal")]
            public static double DocTotal { get; set; }

            [JsonProperty("VatSum")]
            public static double VatSum { get; set; }

            [JsonProperty("Model")]
            public static string Model { get; set; }

            [JsonProperty("U_TAX4_tpTribNfse")]
            public static string U_TAX4_tpTribNfse { get; set; }

            [JsonProperty("U_TAX4_itCultural")]
            public static string U_TAX4_itCultural { get; set; }

            [JsonProperty("ListaServico")]
            public static string ListaServico { get; set; }

            [JsonProperty("Linhas")]
            public List<Linhas> Linhas { get; set; }

        }

        public class Linhas
        {
            [JsonProperty("ItemCode")]
            public static string ItemCode { get; set; }

            [JsonProperty("LineNum")]
            public static int LineNum { get; set; }

            [JsonProperty("LineTotal")]
            public static double LineTotal { get; set; }

            [JsonProperty("U_TAX4_CodServ")]
            public static string U_TAX4_CodServ { get; set; }

            [JsonProperty("DocEntry")]
            public static int DocEntry { get; set; }

            [JsonProperty("BPLId")]
            public static int BPLId { get; set; }

            [JsonProperty("OSvcCode")]
            public static string OSvcCode { get; set; }

            [JsonProperty("U_TAX4_CodAti")]
            public static string U_TAX4_CodAti { get; set; }

            [JsonProperty("ServiceCD")]
            public static string ServiceCD { get; set; }

            [JsonProperty("U_TAX4_IBPT")]
            public static string UTAX4IBPT { get; set; }

            [JsonProperty("U_TAX4_LisSer")]
            public static string U_TAX4_LisSer { get; set; }

            [JsonProperty("Quantity")]
            public static double Quantity { get; set; }

            [JsonProperty("Price")]
            public static double Price { get; set; }

            [JsonProperty("ItemName")]
            public static string ItemName { get; set; }

            [JsonProperty("U_TAX4_CodCNAE")]
            public static string U_TAX4_CodCNAE { get; set; }

            [JsonProperty("U_TAX4_TrMun")]
            public static string U_TAX4_TrMun { get; set; }

            [JsonProperty("IbgeCode")]
            public static string IbgeCode { get; set; }

            [JsonProperty("DiscPrcnt")]
            public static double DiscPrcnt { get; set; }

            [JsonProperty("WTTypeId")]
            public static string WTTypeId { get; set; }

            [JsonProperty("LINETOTALDESPAD")]
            public static double LINETOTALDESPAD { get; set; }

            [JsonProperty("RATE")]
            public static double RATE { get; set; }

            [JsonProperty("WTAMNT")]
            public static double WTAMNT { get; set; }

            [JsonProperty("CSTFPIS")]
            public static int CSTFPIS { get; set; }

            [JsonProperty("CSTFCOFINS")]
            public static string CSTFCOFINS { get; set; }

            [JsonProperty("TAXSUM")]
            public static double TAXSUM { get; set; }

            [JsonProperty("TAXRATE")]
            public static double TAXRATE { get; set; }

            //[JsonProperty("U_TAX4_CodServ")]
            //public static string U_TAX4_CodServ { get; set; }

            //[JsonProperty("U_TAX4_CodAti")]
            //public static string U_TAX4_CodAti { get; set; }

            //[JsonProperty("U_TAX4_LisSer")]
            //public static string U_TAX4_LisSer { get; set; }

            //[JsonProperty("U_TAX4_CodCNAE")]
            //public static string U_TAX4_CodCNAE { get; set; }

            //[JsonProperty("U_TAX4_TrMun")]
            //public static string U_TAX4_TrMun { get; set; }

            [JsonProperty("staType")]
            public static string staType { get; set; }            
        }

    }
}

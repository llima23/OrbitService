using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service.Common.Domain
{
    public class NFSeB1Object
    {

        public NFSeB1Object(int DocEntry)
        {
            this.DocEntry = DocEntry;
            Linhas = new List<Linha>();
            LinesTaxWithholding = new List<LineTaxWithholding>();
            LinesTax = new List<LineTax>();
            Emails = new List<Emails>();
        }
        public string ItemClass { get; set; }
        public List<Emails> Emails { get; set; }

        [JsonProperty("BranchId")]
        public string BranchId { get; set; }
        
        [JsonProperty("DocEntry")]
        public Int32 DocEntry { get; }

        [JsonProperty("DocNum")]
        public Int32 DocNum { get; set; }

        [JsonProperty("CANCELED")]
        public string CANCELED { get; set; }

        [JsonProperty("DocDate")]
        public DateTime DocDate { get; set; }

        [JsonProperty("CardCode")]
        public string CardCode { get; set; }

        [JsonProperty("ENDERECOT")]
        public string ENDERECOT { get; set; }

        [JsonProperty("CardName")]
        public string CardName { get; set; }

        [JsonProperty("DiscPrcnt")]
        public double DiscPrcnt { get; set; }

        [JsonProperty("Comments")]
        public string Comments { get; set; }

        [JsonProperty("GroupNum")]
        public int GroupNum { get; set; }

        [JsonProperty("PeyMethod")]
        public string PeyMethod { get; set; }

        [JsonProperty("BPChCode")]
        public string BPChCode { get; set; }

        [JsonProperty("Serial")]
        public string Serial { get; set; }

        [JsonProperty("SeriesStr")]
        public string SeriesStr { get; set; }

        [JsonProperty("ObjectType")]
        public string ObjectType { get; set; }

        [JsonProperty("TaxId0")]
        public string TaxId0 { get; set; }

        [JsonProperty("TaxId1")]
        public string TaxId1 { get; set; }

        [JsonProperty("TaxId3")]
        public string TaxId3 { get; set; }

        [JsonProperty("TaxId4")]
        public string TaxId4 { get; set; }

        [JsonProperty("TaxId5")]
        public string TaxId5 { get; set; }

        [JsonProperty("RUAT")]
        public string RUAT { get; set; }

        [JsonProperty("COMPLEMENTOT")]
        public string COMPLEMENTOT { get; set; }

        [JsonProperty("BAIRROT")]
        public string BAIRROT { get; set; }

        [JsonProperty("CEPT")]
        public string CEPT { get; set; }

        [JsonProperty("CODMUNICIPIOT")]
        public string CODMUNICIPIOT { get; set; }

        [JsonProperty("ESTADOT")]
        public string ESTADOT { get; set; }

        [JsonProperty("CIDADET")]
        public string CIDADET { get; set; }

        [JsonProperty("LOGRADOUROT")]
        public string LOGRADOUROT { get; set; }

        [JsonProperty("NUMERORUAT")]
        public string NUMERORUAT { get; set; }

        [JsonProperty("AddrType")]
        public string AddrType { get; set; }

        [JsonProperty("E_Mail")]
        public string E_Mail { get; set; }

        [JsonProperty("NTSWebSite")]
        public string NTSWebSite { get; set; }

        [JsonProperty("Fax")]
        public string Fax { get; set; }

        [JsonProperty("Phone1")]
        public string Phone1 { get; set; }

        [JsonProperty("Phone2")]
        public string Phone2 { get; set; }

        [JsonProperty("ShipToDef")]
        public string ShipToDef { get; set; }

        [JsonProperty("CardFName")]
        public string CardFName { get; set; }

        [JsonProperty("County")]
        public string County { get; set; }

        [JsonProperty("TaxIdNum")]
        public string TaxIdNum { get; set; }

        [JsonProperty("TaxIdNum3")]
        public string TaxIdNum3 { get; set; }

        [JsonProperty("TaxIdNum2")]
        public string TaxIdNum2 { get; set; }

        [JsonProperty("AddtnlId")]
        public string AddtnlId { get; set; }

        [JsonProperty("CIDADEP")]
        public string CIDADEP { get; set; }

        [JsonProperty("Block")]
        public string Block { get; set; }

        [JsonProperty("CEPP")]
        public string CEPP { get; set; }

        [JsonProperty("RUANUMEROP")]
        public string RUANUMEROP { get; set; }

        [JsonProperty("RUAP")]
        public string RUAP { get; set; }

        [JsonProperty("BPLName")]
        public string BPLName { get; set; }

        [JsonProperty("BPLId")]
        public int BPLId { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("U_TAX4_TmZn")]
        public string U_TAX4_TmZn { get; set; }

        [JsonProperty("U_TAX4_RegEspTrib")]
        public string U_TAX4_RegEspTrib { get; set; }

        [JsonProperty("U_TAX4_tipoRPS")]
        public string U_TAX4_tipoRPS { get; set; }

        [JsonProperty("U_TAX4_ContAuth")]
        public string U_TAX4_ContAuth { get; set; }

        [JsonProperty("U_TAX4_justf")]
        public string U_TAX4_Justf { get; set; }

        [JsonProperty("U_TAX4_Operacao")]
        public string U_TAX4_Operacao { get; set; }

        [JsonProperty("U_TAX4_tpOperacao")]
        public string U_TAX4_tpOperacao { get; set; }

        [JsonProperty("U_TAX4_regTrib")]
        public string U_TAX4_RegTrib { get; set; }

        [JsonProperty("U_TAX4_Uf")]
        public string U_TAX4_Uf { get; set; }

        [JsonProperty("U_TAX4_Cod")]
        public string U_TAX4_Cod { get; set; }

        [JsonProperty("CntCodNum")]
        public string CntCodNum { get; set; }

        [JsonProperty("IbgeCode")]
        public string IbgeCode { get; set; }

        [JsonProperty("U_TAX4_NatOper")]
        public string U_TAX4_NatOper { get; set; }

        [JsonProperty("U_TAX4_NatOpNFSe")]
        public string U_TAX4_NatOpNFSe { get; set; }

        [JsonProperty("U_TAX4_TpOpNFSe")]
        public string U_TAX4_TpOpNFSe { get; set; }

        [JsonProperty("U_TAX4_condPag")]
        public string U_TAX4_CondPag { get; set; }

        [JsonProperty("U_TAX4_FormaPagto")]
        public string U_TAX4_FormaPagto { get; set; }

        [JsonProperty("DocTotal")]
        public double DocTotal { get; set; }

        [JsonProperty("NfeValue")]
        public double NfeValue { get; set; }


        [JsonProperty("VatSum")]
        public double VatSum { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("U_TAX4_tpTribNfse")]
        public string U_TAX4_tpTribNfse { get; set; }

        [JsonProperty("U_TAX4_itCultural")]
        public string U_TAX4_itCultural { get; set; }

        [JsonProperty("ListaServico")]
        public string ListaServico { get; set; }
        [JsonProperty("Name")]
        public string NomePais { get; set; }
        [JsonProperty("EnviaEmail")]
        public string EnviaEmail { get; set; }

        [JsonProperty("NumeroLote")]
        public int NumeroLote { get; set; }

        //TODO: Esconder e criar metodo para inserir e remover linhas
        [JsonProperty("Linhas")]
        public List<Linha> Linhas { get; set; }
        //TODO: Esconder e criar metodo para inserir e remover linhas
        [JsonProperty("LinesTaxWithholding")]
        public List<LineTaxWithholding> LinesTaxWithholding { get; set; }
        [JsonProperty("LinesTax")]
        public List<LineTax> LinesTax { get; set; }
    }

    public class Emails
    {
        public string email { get; set; }
    }
    public class Linha
    {
        [JsonProperty("PriceBefDi")]
        public double PriceBefDi { get; set; }
        [JsonProperty("ItemCode")]
        public string ItemCode { get; set; }

        [JsonProperty("LineNum")]
        public int LineNum { get; set; }

        [JsonProperty("LineTotal")]
        public double LineTotal { get; set; }

        [JsonProperty("U_TAX4_CodServ")]
        public string U_TAX4_CodServ { get; set; }

        [JsonProperty("DocEntry")]
        public int DocEntry { get; set; }

        [JsonProperty("BPLId")]
        public int BPLId { get; set; }

        [JsonProperty("OSVCCODE")]
        public string OSvcCode { get; set; }

        [JsonProperty("U_TAX4_CodAti")]
        public string U_TAX4_CodAti { get; set; }

        [JsonProperty("ServiceCD")]
        public string ServiceCD { get; set; }

        [JsonProperty("U_TAX4_IBPT")]
        public double U_TAX4_IBPT { get; set; }

        [JsonProperty("U_TAX4_LisSer")]
        public string U_TAX4_LisSer { get; set; }

        [JsonProperty("Quantity")]
        public double Quantity { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }

        [JsonProperty("ItemName")]
        public string ItemName { get; set; }

        [JsonProperty("U_TAX4_CodCNAE")]
        public string U_TAX4_CodCNAE { get; set; }

        [JsonProperty("U_TAX4_TrMun")]
        public string U_TAX4_TrMun { get; set; }

        [JsonProperty("IbgeCode")]
        public string IbgeCode { get; set; }

        [JsonProperty("DiscPrcnt")]
        public double DiscPrcnt { get; set; }

        [JsonProperty("WTTypeId")]
        public string WTTypeId { get; set; }

        [JsonProperty("LINETOTALDESPAD")]
        public double LINETOTALDESPAD { get; set; }

        [JsonProperty("RATE")]
        public double RATE { get; set; }

        [JsonProperty("WTAMNT")]
        public double WTAMNT { get; set; }

        [JsonProperty("CSTFPIS")]
        public int CSTFPIS { get; set; }

        [JsonProperty("CSTFCOFINS")]
        public string CSTFCOFINS { get; set; }

        [JsonProperty("TAXSUM")]
        public string TAXSUM { get; set; }

        [JsonProperty("TAXRATE")]
        public string TAXRATE { get; set; }

        [JsonProperty("RECEBEIBPT")]
        public string RECEBEIBPT { get; set; }

        //[JsonProperty("U_TAX4_CodServ")]
        //public  string U_TAX4_CodServ { get; set; }

        //[JsonProperty("U_TAX4_CodAti")]
        //public  string U_TAX4_CodAti { get; set; }

        //[JsonProperty("U_TAX4_LisSer")]
        //public  string U_TAX4_LisSer { get; set; }

        //[JsonProperty("U_TAX4_CodCNAE")]
        //public  string U_TAX4_CodCNAE { get; set; }

        //[JsonProperty("U_TAX4_TrMun")]
        //public  string U_TAX4_TrMun { get; set; }

        [JsonProperty("staType")]
        public string staType { get; set; }

        [JsonProperty("WtLiable")]
        public string WtLiable { get; set; }

    }

    public class LineTaxWithholding
    {
        public string Doc1LineNo { get; set; }
        [JsonProperty("RATE")]
        public double RATE { get; set; }

        [JsonProperty("WTAMNT")]
        public double WTAMNT { get; set; }

        [JsonProperty("U_TAX4_TpImp")]
        public string U_TAX4_TpImp { get; set; }

    }

    public class LineTax
    {
        [JsonProperty("TaxInPrice")]
        public string TaxInPrice { get; set; }
        [JsonProperty("TaxRate")]
        public double TaxRate { get; set; }

        [JsonProperty("TaxSum")]
        public double TaxSum { get; set; }

        [JsonProperty("U_TAX4_TpImp")]
        public string U_TAX4_TpImp { get; set; }
        [JsonProperty("CSTfPIS")]
        public string CSTfPIS { get; set; }
        [JsonProperty("CSTfCOFINS")]
        public string CSTfCOFINS { get; set; }

        [JsonProperty("LineNum")]
        public string LineNum { get; set; }
    }
}

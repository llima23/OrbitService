//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace B1Library.Data
//{
//    public class InvoiceB1
//    {
//        public InvoiceB1()
//        {
//            Branch  = new List<Branch>();
//            Header  = new List<Header>();
//            Lines   = new List<Line>();
//            Partner = new List<Partner>();
//        }

//        [JsonProperty("Branch")]
//        public List<Branch> Branch { get; set; }

//        [JsonProperty("DocEntry")]
//        public int DocEntry { get; set; }

//        [JsonProperty("Header")]
//        public List<Header> Header { get; set; }

//        //TODO: refactor to turn to private
//        [JsonProperty("Lines")]
//        public List<Line> Lines { get; set; }

//        [JsonProperty("Partner")]
//        public List<Partner> Partner { get; set; }

//        public void AddItemLine(Line itemLine)
//        {
//            if (itemLine == null)
//            {
//                throw new ArgumentException("itemLine argument should be a valid instance!");
//            }
//            if (itemLine.TaxLine == null)
//            {
//                throw new ArgumentException("TaxLine argument is obligatory!");
//            }
//            if (itemLine.WithholdingLine == null)
//            {
//                throw new ArgumentException("WithholdingLine argument is obligatory!");
//            }

//            this.Lines.Add(itemLine);
//        }

//        [JsonProperty("CodInt")]
//        public string CodInt { get; set; }

//        [JsonProperty("ModelInvoice")]
//        public string ModelInvoice { get; set; }

//        public double GetTaxNameSum(string taxName)
//        {
//            double sum = 0.0;
//            foreach (Line item in Lines)
//            {
//                var taxes = item.TaxLine.Where(tl => tl.TaxName == taxName);
//                sum += taxes.Sum(i => i.TaxValue);
//            }
//            return sum;
//        }
//    }

//    public class Branch
//    {
//        [JsonProperty("BranchAdressType")]
//        public string BranchAdressType { get; set; }

//        [JsonProperty("BranchCNPJ")]
//        public string BranchCNPJ { get; set; }

//        [JsonProperty("BranchCity")]
//        public string BranchCity { get; set; }

//        [JsonProperty("BranchCounty")]
//        public string BranchCounty { get; set; }

//        [JsonProperty("BranchDistrict")]
//        public string BranchDistrict { get; set; }

//        [JsonProperty("BranchInscIe")]
//        public string BranchInscIe { get; set; }

//        [JsonProperty("BranchName")]
//        public string BranchName { get; set; }

//        [JsonProperty("BranchState")]
//        public string BranchState { get; set; }

//        [JsonProperty("BranchStreet")]
//        public string BranchStreet { get; set; }

//        [JsonProperty("BranchStreetNo")]
//        public string BranchStreetNo { get; set; }

//        [JsonProperty("BranchZipCode")]
//        public string BranchZipCode { get; set; }
//    }

//    public class Header
//    {
//        [JsonProperty("BranchId")]
//        public string BranchId { get; set; }

//        [JsonProperty("CommentsDocument")]
//        public string CommentsDocument { get; set; }

//        [JsonProperty("DocDateDocument")]
//        public DateTime DocDateDocument { get; set; }

//        [JsonProperty("FinalideDocument")]
//        public string FinalideDocument { get; set; }

//        [JsonProperty("GrossAmountDocument")]
//        public double GrossAmountDocument { get; set; }

//        [JsonProperty("IdDocument")]
//        public int IdDocument { get; set; }

//        [JsonProperty("IdReturnOrbitDocument")]
//        public string IdReturnOrbitDocument { get; set; }

//        [JsonProperty("IsCanceled")]
//        public string IsCanceled { get; set; }

//        [JsonProperty("ModelDocument")]
//        public string ModelDocument { get; set; }

//        [JsonProperty("NumDocument")]
//        public int NumDocument { get; set; }

//        [JsonProperty("PaymentGroupDocument")]
//        public string PaymentGroupDocument { get; set; }

//        [JsonProperty("PeyMethodDocument")]
//        public string PeyMethodDocument { get; set; }

//        [JsonProperty("SerialDocument")]
//        public int SerialDocument { get; set; }

//        [JsonProperty("StatOrbitDocument")]
//        public string StatOrbitDocument { get; set; }

//        [JsonProperty("SubSerialDocument")]
//        public string SubSerialDocument { get; set; }

//        [JsonProperty("TaxationTypeDocument")]
//        public string TaxationTypeDocument { get; set; }

//        [JsonProperty("TotalDocument")]
//        public double TotalDocument { get; set; }

//        [JsonProperty("UsageDocument")]
//        public string UsageDocument { get; set; }
//    }

//    public class Line
//    {
//        public Line()
//        {
//            TaxLine = new List<TaxLine>();
//            WithholdingLine = new List<WithholdingLine>();
//        }

//        [JsonProperty("CalculationIBPT")]
//        public double CalculationIBPT { get; set; }

//        [JsonProperty("ItemCode")]
//        public string ItemCode { get; set; }

//        [JsonProperty("ItemCstCofins")]
//        public int ItemCstCofins { get; set; }

//        [JsonProperty("ItemCstPis")]
//        public int ItemCstPis { get; set; }

//        [JsonProperty("ItemDiscountPercent")]
//        public double ItemDiscountPercent { get; set; }

//        [JsonProperty("ItemIsWithholding")]
//        public string ItemIsWithholding { get; set; }

//        [JsonProperty("ItemPrice")]
//        public double ItemPrice { get; set; }

//        [JsonProperty("ItemQuantity")]
//        public double ItemQuantity { get; set; }

//        [JsonProperty("ItemRate")]
//        public double ItemRate { get; set; }

//        [JsonProperty("LineDocument")]
//        public int LineDocument { get; set; }

//        [JsonProperty("LineTotal")]
//        public double LineTotal { get; set; }

//        [JsonProperty("TaxLine")]
//        public List<TaxLine> TaxLine { get; set; }

//        [JsonProperty("WithholdingLine")]
//        public List<WithholdingLine> WithholdingLine { get; set; }
//    }

//    public class Partner
//    {
//        [JsonProperty("PartnerAddrType")]
//        public string PartnerAddrType { get; set; }

//        [JsonProperty("PartnerAddress")]
//        public string PartnerAddress { get; set; }

//        [JsonProperty("PartnerCity")]
//        public string PartnerCity { get; set; }

//        [JsonProperty("PartnerCnpj")]
//        public string PartnerCnpj { get; set; }

//        [JsonProperty("PartnerCode")]
//        public string PartnerCode { get; set; }

//        [JsonProperty("PartnerCounty")]
//        public string PartnerCounty { get; set; }

//        [JsonProperty("PartnerCpf")]
//        public string PartnerCpf { get; set; }

//        [JsonProperty("PartnerDistrict")]
//        public string PartnerDistrict { get; set; }

//        [JsonProperty("PartnerGroup")]
//        public int PartnerGroup { get; set; }

//        [JsonProperty("PartnerInscIe")]
//        public string PartnerInscIe { get; set; }

//        [JsonProperty("PartnerInscMun")]
//        public string PartnerInscMun { get; set; }

//        [JsonProperty("PartnerName")]
//        public string PartnerName { get; set; }

//        [JsonProperty("PartnerState")]
//        public string PartnerState { get; set; }

//        [JsonProperty("PartnerStreet")]
//        public string PartnerStreet { get; set; }

//        [JsonProperty("PartnerStreetNo")]
//        public string PartnerStreetNo { get; set; }
//    }

//    public class TaxLine
//    {
//        [JsonProperty("TaxName")]
//        public string TaxName { get; set; }

//        [JsonProperty("TaxRate")]
//        public double TaxRate { get; set; }

//        [JsonProperty("TaxTypeOrbit")]
//        public string TaxTypeOrbit { get; set; }

//        [JsonProperty("TaxValue")]
//        public double TaxValue { get; set; }
//    }

//    public class WithholdingLine
//    {
//        [JsonProperty("WithholdingRate")]
//        public double WithholdingRate { get; set; }

//        [JsonProperty("WithholdingTypeOrbit")]
//        public string WithholdingTypeOrbit { get; set; }

//        [JsonProperty("WithholdingValue")]
//        public double WithholdingValue { get; set; }
//    }

//}

using B1Library.Documents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace B1Library.Utilities
{
    public class Util
    {
        private static CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");

        public double GetTaxTypeB1Sum(string TipoImpostoOrbit, Invoice invoice)
        {
            double sum = 0.00;
            foreach (CabecalhoLinha item in invoice.CabecalhoLinha)
            {
                var taxes = item.ImpostoLinha.Where(tl => tl.TipoImpostoOrbit == TipoImpostoOrbit && tl.SimOuNaoDesoneracao == "N");
                sum += taxes.Sum(i => i.ValorImposto);
            }
            return sum;
        }
        public double GetTaxTypeB1SumDeson(string TipoImpostoOrbit, Invoice invoice)
        {
            double sum = 0.00;
            foreach (CabecalhoLinha item in invoice.CabecalhoLinha)
            {
                var taxesDeson = item.ImpostoLinha.Where(tl => tl.TipoImpostoOrbit == TipoImpostoOrbit && (tl.SimOuNaoDesoneracao == "Y" || tl.ValorIcmsDesonerado > 0));
                sum += taxesDeson.Sum(i => i.ValorImposto) + taxesDeson.Sum(i => i.ValorIcmsDesonerado);
            }
            return sum;
        }
        public double GetTaxTypeB1VBcSum(string TipoImpostoOrbit, Invoice invoice)
        {
            double sum = 0.00;
            foreach (CabecalhoLinha item in invoice.CabecalhoLinha)
            {
                var taxes = item.ImpostoLinha.Where(tl => tl.TipoImpostoOrbit == TipoImpostoOrbit & tl.SimOuNaoDesoneracao == "N");
                sum += taxes.Sum(i => i.ValorBaseImposto);
            }
            return sum;
        }
        public double GetVSumDespAdic(string TipoDespesa, Invoice invoice)
        {
            double sum = 0.00;
            foreach (CabecalhoLinha item in invoice.CabecalhoLinha)
            {
                var taxes = item.DespesaAdicional.Where(tl => tl.TipoDespesa == TipoDespesa);
                sum += taxes.Sum(i => i.ValorUnitarioDespesa);
            }
            return sum;
        }

        public double GetTaxTypeB1VBcSumForItem(List<ImpostoLinha> ListimpostoLinha, string TipoImposto)
        {
            double result = 0.00;
            foreach(ImpostoLinha impostoLinha in ListimpostoLinha.Where(i => i.TipoImpostoOrbit == TipoImposto))
            {
                result += impostoLinha.ValorBaseImposto;
            }
            return result;

        }
        public double GetTaxTypeB1VImpSumForItem(List<ImpostoLinha> ListimpostoLinha, string TipoImposto)
        {
            double result = 0.00;
            foreach (ImpostoLinha impostoLinha in ListimpostoLinha.Where(i => i.TipoImpostoOrbit == TipoImposto))
            {
                result += impostoLinha.ValorImposto + impostoLinha.ValorIcmsDesonerado;
            }
            return result;

        }
        public double GetVProdSum(List<CabecalhoLinha> cabecalhoLinha)
        {
            double sum = 0.00;
            foreach (var item in cabecalhoLinha)
            {
                sum += item.ValorTotalLinnha;
            }
            return sum;
        }
        public double GetValorSomaDescontoItens(List<CabecalhoLinha> cabecalhoLinha)
        {
            double sum = 0.00;
            foreach (var item in cabecalhoLinha)
            {
                sum += item.ValorTotalDescontoLinha;
            }
            return sum;
        }
        public string ConvertDateB1ToFormatOrbit(DateTime Data, string DocTime = null)
        {
            string DataOrbit;
            if (string.IsNullOrEmpty(DocTime))
            {
                DataOrbit = Data.ToString("yyyy-MM-dd");
            }
            else
            {
                DataOrbit = Data.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "-03:00";
            }
            return DataOrbit;
        }

        public string ToOrbitString(double valor)
        {
            if (valor > 0)
            {
                string ValueString = string.Format(cultureInfo, "{0:0.000000}", valor);
                ValueString = ValueString.Remove(ValueString.Length - 4);
                return ValueString;
            }
            else
            {
                return "0.00";
            }
        }

        public string ToOrbitString4CasaDecimais(double valor)
        {
            if (valor > 0)
            {
                string ValueString = string.Format(cultureInfo, "{0:0.000000}", valor);
                ValueString = ValueString.Remove(ValueString.Length - 2);
                return ValueString;
            }
            else
            {
                return "0.00";
            }
        }

        public string ToOrbitStringVolume(double valor)
        {
            if (valor > 0)
            {
                string ValueString = string.Format(cultureInfo, "{0:0.000000}", valor);
                ValueString = ValueString.Remove(ValueString.Length - 3);
                return ValueString;
            }
            else
            {
                return "0.00";
            }
        }
    }
}

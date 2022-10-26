using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service.Common.Domain.Properties
{
    public class DocumentServiceFunctions : B1.NFSe
    {
        public static string setTipoRps(string RPS, string MunFilial)
        {
            string result = String.Empty;

            if (!string.IsNullOrEmpty(RPS))
            {
                result = RPS;

                if ((MunFilial == "3283" || MunFilial == "2188") && RPS == "RPS")
                {
                    result = "1";
                }
                if ((MunFilial == "3283" || MunFilial == "2188") && RPS == "RPS-M")
                {
                    result = "2";
                }
                if ((MunFilial == "3283" || MunFilial == "2188") && RPS == "RPS-C")
                {
                    result = "3";
                }
            }
            else
            {
                result = "RPS";
            }

            return result;
        }

        public static void setInfoMunicipioServico(ref Servico servico, Identificacao identificacao)
        {
            Header.ListaServico = Linhas.U_TAX4_LisSer;
            if (String.IsNullOrEmpty(Linhas.U_TAX4_LisSer))
                servico.itemListaServico = "false";
            else
                servico.itemListaServico = Linhas.U_TAX4_LisSer;


            if (String.IsNullOrEmpty(Linhas.U_TAX4_CodCNAE))
                servico.cnae = "false";
            else
                servico.cnae = Functions.RemoveCaracteresEspeciais(Linhas.U_TAX4_CodCNAE);

            if (!String.IsNullOrEmpty(Linhas.U_TAX4_TrMun))
                servico.codigoTributacaoMunicipio = Linhas.U_TAX4_TrMun;
            else
                servico.codigoTributacaoMunicipio = Functions.RemoveCaracteresEspeciais(Linhas.ServiceCD);

            if (identificacao.regimeEspecialTributacao == "F" || identificacao.regimeEspecialTributacao == "B" ||
                    identificacao.regimeEspecialTributacao == "N" || identificacao.regimeEspecialTributacao == "V" ||
                    identificacao.regimeEspecialTributacao == "2")
            {
                servico.codigoMunicipioIncidencia = Header.IbgeCode;
            }

            else if (identificacao.regimeEspecialTributacao == "T" || identificacao.regimeEspecialTributacao == "X" ||
                   identificacao.regimeEspecialTributacao == "A" || identificacao.regimeEspecialTributacao == "M" ||
                   identificacao.regimeEspecialTributacao == "1")
            {
                Header.IbgeCode = Linhas.IbgeCode;
                servico.codigoMunicipioIncidencia = Header.IbgeCode;
            }
            else
            {
                servico.codigoMunicipioIncidencia = "";
            }
        }

        public static void setImpostosServico(ref Servico servico, ref Valores valores)
        {
            Pis oPis = new Pis();
            oPis.aliquota = 0.00;
            oPis.valor = 0.00;

            Cofins oCofins = new Cofins();
            oCofins.aliquota = 0.00;
            oCofins.valor = 0.00;

            Inss oInss = new Inss();
            Ir oIr = new Ir();
            oIr.aliquota = 0.00;
            oIr.valor = 0.00;
            oIr.baseCalculo = "0.00";
            oIr.percentual = "0.00";

            Csll oCsll = new Csll();
            oCsll.aliquota = 0.00;
            oCsll.valor = 0.00;
            oCsll.baseCalculo = "0.00";
            oCsll.percentual = "0.00";

            Iss oIss = new Iss();
            oInss.aliquota = 0.00;
            oInss.valor = 0.00;
            oInss.baseCalculo = "0.00";            

            valores.totalServicos = Header.DocTotal;
            valores.totalDeducoes = 0; //TODO
            valores.descontoCondicionado = 0;
            valores.descontoIncondicionado = 0;

            if (!String.IsNullOrEmpty(Linhas.WTTypeId))
            {
                switch (Linhas.WTTypeId)
                {
                    case "1":
                        oPis.aliquota = Linhas.RATE;
                        oPis.cst = Linhas.CSTFCOFINS;                        
                        oPis.baseCalculo = Linhas.LineTotal;
                        break;
                    case "2":
                        oCofins.aliquota = Linhas.RATE;
                        oCofins.cst = Linhas.CSTFCOFINS;
                        oCofins.baseCalculo = Linhas.LineTotal;
                        break;
                    case "3":
                        oIr.aliquota = Linhas.RATE;
                        break;
                    case "4":
                        oCsll.aliquota = Linhas.RATE;
                        break;
                    case "5":
                        oInss.aliquota = Linhas.RATE;
                        break;
                    case "6":
                        oIss.exigibilidadeIss = "2";
                        oIss.baseCalculo = Linhas.LineTotal;
                        oIss.retido = true;
                        oIss.aliquota = Linhas.RATE;
                        break;
                }

                valores.outrasRetencoes = 0.00;

            }

            if (Linhas.staType == "24" && Linhas.TAXSUM > 0 )
            {
                oIss.retido = false;

                oIss.exigibilidadeIss = "2";
                oIss.baseCalculo = Linhas.LineTotal;

                oIss.aliquota = Linhas.TAXRATE;
                oIss.valor = Linhas.TAXSUM;
                oIss.valorRetido = 0.00;
            }
            oIss.exigibilidadeIss = "2";

            oIss.valorRetido = Linhas.WTAMNT;
            oIss.valor = Linhas.TAXSUM;
            valores.iss = oIss;

            oCsll.valor = Linhas.WTAMNT;
            valores.csll = oCsll;

            oIr.valor = Linhas.WTAMNT;
            valores.ir = oIr;

            oInss.valor = Linhas.WTAMNT;
            valores.inss = oInss;

            if(Linhas.WTAMNT > 0)
            {
                oCofins.valor = Linhas.WTAMNT;
                valores.cofins = oCofins;
            }
            else
            {
                oCofins.valor = 0.00;
                valores.cofins = oCofins;
            }

            if(Linhas.WTAMNT > 0)
            {
                oPis.valor = Linhas.WTAMNT;
                valores.pis = oPis;
            }
            else
            {
                oPis.valor = 0.00;
                valores.pis = oPis;
            }
        }
    
    }
}

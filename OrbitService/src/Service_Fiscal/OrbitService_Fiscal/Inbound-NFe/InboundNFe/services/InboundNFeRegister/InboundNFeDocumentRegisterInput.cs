using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace OrbitService.InboundNFe.services.InboundNFeRegister
{
    public class Root
    {
        public string dfe { get; set; } = "nfe";

        [JsonProperty("data")]
        public InboundNFeDocumentRegisterInput inboundNFeDocumentRegisterInput { get; set; }
    }
    public class InboundNFeDocumentRegisterInput
    {

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("branchId")]
        public string BranchId { get; set; }

        [JsonProperty("versao")]
        public string Versao { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("identificacao")]
        public Identificacao identificacao { get; set; }

        [JsonProperty("avulsa")]
        public Avulsa avulsa { get; set; }

        [JsonProperty("destinatario")]
        public Destinatario Destinatario { get; set; }

        [JsonProperty("retirada")]
        public Retirada retirada { get; set; }

        [JsonProperty("entrega")]
        public Entrega entrega { get; set; }

        [JsonProperty("emitente")]
        public Emitente Emitente { get; set; }

        [JsonProperty("det")]
        public List<Det> det { get; set; }

        [JsonProperty("transp")]
        public Transp transp { get; set; }

        [JsonProperty("cobr")]
        public Cobr cobr { get; set; }

        [JsonProperty("pag")]
        public Pag pag { get; set; }

        [JsonProperty("infAdic")]
        public InfAdic infAdic { get; set; }

        [JsonProperty("exporta")]
        public Exporta exporta { get; set; }

        [JsonProperty("compra")]
        public Compra compra { get; set; }

        [JsonProperty("cana")]
        public Cana cana { get; set; }

        [JsonProperty("total")]
        public Total total { get; set; }

        [JsonProperty("autXML")]
        public List<AutXML> autXML { get; set; }

        [JsonProperty("infIntermed")]
        public InfIntermed infIntermed { get; set; }

        [JsonProperty("emails")]
        public List<string> Emails { get; set; }

        [JsonProperty("status")]
        public Status status { get; set; }

        [JsonProperty("eventos")]
        public List<Evento> eventos { get; set; }

        [JsonProperty("dadosCobranca")]
        public string dadosCobranca { get; set; }

        [JsonProperty("tipoOperacao")]
        public string tipoOperacao { get; set; }


    }

    public class Adi
    {
        [JsonProperty("nAdicao")]
        public string NAdicao { get; set; }

        [JsonProperty("nSeqAdic")]
        public string NSeqAdic { get; set; }

        [JsonProperty("cFabricante")]
        public string CFabricante { get; set; }

        [JsonProperty("vDescDI")]
        public string VDescDI { get; set; }

        [JsonProperty("nDraw")]
        public string NDraw { get; set; }
    }

    public class AutXML
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }
    }

    public class Avulsa
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("orgaoEmitente")]
        public string OrgaoEmitente { get; set; }

        [JsonProperty("matriculaAgente")]
        public string MatriculaAgente { get; set; }

        [JsonProperty("nomeAgente")]
        public string NomeAgente { get; set; }

        [JsonProperty("fone")]
        public string Fone { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("numeroDocumentoArrecadacao")]
        public string NumeroDocumentoArrecadacao { get; set; }

        [JsonProperty("dataEmissaoDocumentoArrecadacao")]
        public string DataEmissaoDocumentoArrecadacao { get; set; }

        [JsonProperty("valorTotalDocumentoArrecadacao")]
        public string ValorTotalDocumentoArrecadacao { get; set; }

        [JsonProperty("reparticaoFiscalEmitente")]
        public string ReparticaoFiscalEmitente { get; set; }

        [JsonProperty("dataPagamentoDocumentoArrecadacao")]
        public string DataPagamentoDocumentoArrecadacao { get; set; }
    }

    public class Cana
    {
        [JsonProperty("safra")]
        public string Safra { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("forDia")]
        public List<ForDium> ForDia { get; set; }

        [JsonProperty("qTotMes")]
        public string QTotMes { get; set; }

        [JsonProperty("qTotAnt")]
        public string QTotAnt { get; set; }

        [JsonProperty("qTotGer")]
        public string QTotGer { get; set; }

        [JsonProperty("deduc")]
        public List<Deduc> Deduc { get; set; }

        [JsonProperty("vFor")]
        public string VFor { get; set; }

        [JsonProperty("vTotDed")]
        public string VTotDed { get; set; }

        [JsonProperty("vLiqFor")]
        public string VLiqFor { get; set; }
    }

    public class Card
    {
        [JsonProperty("tpIntegra")]
        public string TpIntegra { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("tBand")]
        public string TBand { get; set; }

        [JsonProperty("cAut")]
        public string CAut { get; set; }
    }

    public class Cide
    {
        [JsonProperty("qBcProd")]
        public string QBcProd { get; set; }

        [JsonProperty("vAliqProd")]
        public string VAliqProd { get; set; }

        [JsonProperty("vCide")]
        public string VCide { get; set; }
    }

    public class Cobr
    {
        public Cobr()
        {
            Fatura = new Fatura();
            Duplicata = new List<Duplicatum>();
        }

        [JsonProperty("fatura")]
        public Fatura Fatura { get; set; }

        [JsonProperty("duplicata")]
        public List<Duplicatum> Duplicata { get; set; }
    }

    public class Cofins
    {
        [JsonProperty("cst")]
        public string Cst { get; set; }

        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("pImp")]
        public string PImp { get; set; }

        [JsonProperty("vImp")]
        public string VImp { get; set; }

        [JsonProperty("qBcProd")]
        public string QBcProd { get; set; }

        [JsonProperty("vAliqProd")]
        public string VAliqProd { get; set; }
    }

    public class CofinsSt
    {
        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("pCofins")]
        public string PCofins { get; set; }

        [JsonProperty("qBcProd")]
        public string QBcProd { get; set; }

        [JsonProperty("vAliqProd")]
        public string VAliqProd { get; set; }

        [JsonProperty("vCofins")]
        public string VCofins { get; set; }
    }

    public class Comb
    {
        [JsonProperty("cProdAnp")]
        public string CProdAnp { get; set; }

        [JsonProperty("descAnp")]
        public string DescAnp { get; set; }

        [JsonProperty("pglp")]
        public string Pglp { get; set; }

        [JsonProperty("pgNn")]
        public string PgNn { get; set; }

        [JsonProperty("pgNi")]
        public string PgNi { get; set; }

        [JsonProperty("vPart")]
        public string VPart { get; set; }

        [JsonProperty("codif")]
        public string Codif { get; set; }

        [JsonProperty("qTemp")]
        public string QTemp { get; set; }

        [JsonProperty("ufCons")]
        public string UfCons { get; set; }

        [JsonProperty("cide")]
        public Cide Cide { get; set; }

        [JsonProperty("encerrante")]
        public Encerrante Encerrante { get; set; }
    }

    public class Compra
    {
        [JsonProperty("xnEmp")]
        public string XnEmp { get; set; }

        [JsonProperty("xPed")]
        public string XPed { get; set; }

        [JsonProperty("xCont")]
        public string XCont { get; set; }
    }

    public class Deduc
    {
        [JsonProperty("xDed")]
        public string XDed { get; set; }

        [JsonProperty("vDed")]
        public string VDed { get; set; }
    }

    public class Destinatario
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }
        public bool ShouldSerializeCnpj()
        {
            return !string.IsNullOrEmpty(Cnpj);
        }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }
        public bool ShouldSerializeCpf()
        {
            return !string.IsNullOrEmpty(Cpf);
        }

        [JsonProperty("idEstrangeiro")]
        public string IdEstrangeiro { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }

        [JsonProperty("indIeDestinatario")]
        public string IndIeDestinatario { get; set; }


        [JsonProperty("ie")]
        public string Ie { get; set; }
        public bool ShouldSerializeIe()
        {
            return !string.IsNullOrEmpty(Ie);
        }

        [JsonProperty("isuf")]
        public string Isuf { get; set; }

        [JsonProperty("im")]
        public string Im { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class Det
    {
        public Det()
        {
            prod = new Prod();
            Imposto = new Imposto();
            ImpostoDevol = new ImpostoDevol();
        }

        [JsonProperty("nItem")]
        public string NItem { get; set; }

        [JsonProperty("prod")]
        public Prod prod { get; set; }

        [JsonProperty("imposto")]
        public Imposto Imposto { get; set; }

        [JsonProperty("impostoDevol")]
        public ImpostoDevol ImpostoDevol { get; set; }

        [JsonProperty("infAdProd")]
        public string InfAdProd { get; set; }
    }

    public class Prod
    {
        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("cean")]
        public string Cean { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("ncm")]
        public string Ncm { get; set; }

        [JsonProperty("cest")]
        public string Cest { get; set; }

        [JsonProperty("indEscala")]
        public string IndEscala { get; set; }

        [JsonProperty("cnpjFab")]
        public string CnpjFab { get; set; }

        [JsonProperty("cBenef")]
        public string CBenef { get; set; }

        [JsonProperty("codigoExtIpi")]
        public string CodigoExtIpi { get; set; }

        [JsonProperty("codigoFiscalOperacoes")]
        public string CodigoFiscalOperacoes { get; set; }

        [JsonProperty("unidadeComercial")]
        public string UnidadeComercial { get; set; }

        [JsonProperty("quantidadeComercial")]
        public string QuantidadeComercial { get; set; }

        [JsonProperty("valorUnitarioComercializacao")]
        public string ValorUnitarioComercializacao { get; set; }

        [JsonProperty("valorTotalBruto")]
        public string ValorTotalBruto { get; set; }

        [JsonProperty("ceanTrib")]
        public string CeanTrib { get; set; }

        [JsonProperty("unidadeTributavel")]
        public string UnidadeTributavel { get; set; }

        [JsonProperty("quantidadeTributavel")]
        public string QuantidadeTributavel { get; set; }

        [JsonProperty("valorUnitarioTributacao")]
        public string ValorUnitarioTributacao { get; set; }

        [JsonProperty("valorFrete")]
        public string ValorFrete { get; set; }

        [JsonProperty("valorSeguro")]
        public string ValorSeguro { get; set; }

        [JsonProperty("valorDesconto")]
        public string ValorDesconto { get; set; }

        [JsonProperty("vOutro")]
        public string VOutro { get; set; }

        [JsonProperty("indTot")]
        public string IndTot { get; set; }

        [JsonProperty("di")]
        public List<Di> Di { get; set; }

        [JsonProperty("detExport")]
        public List<DetExport> DetExport { get; set; }

        [JsonProperty("xPed")]
        public string XPed { get; set; }

        [JsonProperty("nItemPed")]
        public string NItemPed { get; set; }

        [JsonProperty("nfci")]
        public string Nfci { get; set; }

        [JsonProperty("rastro")]
        public List<Rastro> Rastro { get; set; }

        [JsonProperty("comb")]
        public Comb Comb { get; set; }

        [JsonProperty("nrecopi")]
        public string Nrecopi { get; set; }

        [JsonProperty("nve")]
        public List<string> Nve { get; set; }
    }

    public class DetExport
    {
        [JsonProperty("nDraw")]
        public string NDraw { get; set; }

        [JsonProperty("exportInd")]
        public ExportInd ExportInd { get; set; }
    }

    public class DetPag
    {
        [JsonProperty("indPag")]
        public string IndPag { get; set; }

        [JsonProperty("tPag")]
        public string TPag { get; set; }

        [JsonProperty("vPag")]
        public string VPag { get; set; }

        [JsonProperty("xPag")]
        public string XPag { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }
    }

    public class Di
    {
        [JsonProperty("numeroDocumentoImportacao")]
        public string NumeroDocumentoImportacao { get; set; }

        [JsonProperty("ddi")]
        public string Ddi { get; set; }

        [JsonProperty("localDesembaracoAduaneiro")]
        public string LocalDesembaracoAduaneiro { get; set; }

        [JsonProperty("ufDesembaracoAduaneiro")]
        public string UfDesembaracoAduaneiro { get; set; }

        [JsonProperty("dataDesembaracoAduaneiro")]
        public string DataDesembaracoAduaneiro { get; set; }

        [JsonProperty("viaTransporteInternacional")]
        public string ViaTransporteInternacional { get; set; }

        [JsonProperty("vafrmm")]
        public string Vafrmm { get; set; }

        [JsonProperty("tpIntermedio")]
        public string TpIntermedio { get; set; }

        [JsonProperty("cnpjAdiquirente")]
        public string CnpjAdiquirente { get; set; }

        [JsonProperty("ufAdiquirente")]
        public string UfAdiquirente { get; set; }

        [JsonProperty("codigoExportador")]
        public string CodigoExportador { get; set; }

        [JsonProperty("adi")]
        public List<Adi> Adi { get; set; }
    }

    public class Duplicatum
    {
        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("dataVencimento")]
        public string DataVencimento { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }
    }

    public class Emitente
    {
        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("inscricaoEstadual")]
        public string InscricaoEstadual { get; set; }
        public bool ShouldSerializeInscricaoEstadual()
        {
            return !string.IsNullOrEmpty(InscricaoEstadual);
        }


        [JsonProperty("codigoRegimeTributario")]
        public string CodigoRegimeTributario { get; set; }

        [JsonProperty("certificado_arquivo")]
        public string CertificadoArquivo { get; set; }

        [JsonProperty("certificado_senha")]
        public string CertificadoSenha { get; set; }

        [JsonProperty("inscricaoEstadualST")]
        public string InscricaoEstadualST { get; set; }

        [JsonProperty("inscricaoMunicipal")]
        public string InscricaoMunicipal { get; set; }

        [JsonProperty("cnae")]
        public string Cnae { get; set; }

        [JsonProperty("nomeFantasia")]
        public string NomeFantasia { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }
        public bool ShouldSerializeCnpj()
        {
            return !string.IsNullOrEmpty(Cnpj);
        }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }
        public bool ShouldSerializeCpf()
        {
            return !string.IsNullOrEmpty(Cpf);
        }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }
    }

    public class Encerrante
    {
        [JsonProperty("nBico")]
        public string NBico { get; set; }

        [JsonProperty("nBomba")]
        public string NBomba { get; set; }

        [JsonProperty("nTanque")]
        public string NTanque { get; set; }

        [JsonProperty("vEncIni")]
        public string VEncIni { get; set; }

        [JsonProperty("vEncFin")]
        public string VEncFin { get; set; }
    }

    public class Endereco
    {
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("codigoMunicipio")]
        public string CodigoMunicipio { get; set; }

        [JsonProperty("municipio")]
        public string Municipio { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("codigoPais")]
        public string CodigoPais { get; set; }

        [JsonProperty("pais")]
        public string Pais { get; set; }

        [JsonProperty("fone")]
        public string Fone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("ie")]
        public string Ie { get; set; }
    }

    public class Entrega
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("ie")]
        public string Ie { get; set; }
    }

    public class Evento
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("protocolo")]
        public string Protocolo { get; set; }

        [JsonProperty("dhEvento")]
        public string DhEvento { get; set; }
    }

    public class Exporta
    {
        [JsonProperty("ufSaidaPais")]
        public string UfSaidaPais { get; set; }

        [JsonProperty("xLocExporta")]
        public string XLocExporta { get; set; }

        [JsonProperty("xLocDespacho")]
        public string XLocDespacho { get; set; }
    }

    public class ExportInd
    {
        [JsonProperty("nre")]
        public string Nre { get; set; }

        [JsonProperty("chNFe")]
        public string ChNFe { get; set; }

        [JsonProperty("qExport")]
        public string QExport { get; set; }
    }

    public class Fatura
    {
        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("valorOriginal")]
        public string ValorOriginal { get; set; }

        [JsonProperty("valorDesconto")]
        public string ValorDesconto { get; set; }

        [JsonProperty("valorLiquido")]
        public string ValorLiquido { get; set; }
    }

    public class ForDium
    {
        [JsonProperty("qtde")]
        public string Qtde { get; set; }

        [JsonProperty("dia")]
        public string Dia { get; set; }
    }

    public class Icms
    {
        [JsonProperty("orig")]
        public string Orig { get; set; }

        [JsonProperty("cst")]
        public string Cst { get; set; }

        [JsonProperty("crt")]
        public string Crt { get; set; }

        [JsonProperty("csosn")]
        public string Csosn { get; set; }

        [JsonProperty("modBc")]
        public string ModBc { get; set; }

        [JsonProperty("modBcSt")]
        public string ModBcSt { get; set; }

        [JsonProperty("motDeson")]
        public string MotDeson { get; set; }

        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("vIcmsOp")]
        public string VIcmsOp { get; set; }

        [JsonProperty("pImp")]
        public string PImp { get; set; }

        [JsonProperty("pFcp")]
        public string PFcp { get; set; }

        [JsonProperty("pMvast")]
        public string PMvast { get; set; }

        [JsonProperty("pRedBcSt")]
        public string PRedBcSt { get; set; }

        [JsonProperty("pCredSn")]
        public string PCredSn { get; set; }

        [JsonProperty("pMva")]
        public string PMva { get; set; }

        [JsonProperty("pImpSt")]
        public string PImpSt { get; set; }

        [JsonProperty("pFcpSt")]
        public string PFcpSt { get; set; }

        [JsonProperty("pRedBc")]
        public string PRedBc { get; set; }

        [JsonProperty("pDif")]
        public string PDif { get; set; }

        [JsonProperty("pBcOp")]
        public string PBcOp { get; set; }

        [JsonProperty("vImp")]
        public string VImp { get; set; }

        [JsonProperty("vFcp")]
        public string VFcp { get; set; }

        [JsonProperty("vBcFcp")]
        public string VBcFcp { get; set; }

        [JsonProperty("vBcSt")]
        public string VBcSt { get; set; }

        [JsonProperty("vImpSt")]
        public string VImpSt { get; set; }

        [JsonProperty("vBcFcpSt")]
        public string VBcFcpSt { get; set; }

        [JsonProperty("vFcpSt")]
        public string VFcpSt { get; set; }

        [JsonProperty("vDeson")]
        public string VDeson { get; set; }

        [JsonProperty("vIcmsDif")]
        public string VIcmsDif { get; set; }

        [JsonProperty("vCredIcmsSn")]
        public string VCredIcmsSn { get; set; }

        [JsonProperty("vBcUfDest")]
        public string VBcUfDest { get; set; }

        [JsonProperty("vIcmsSubstituto")]
        public string VIcmsSubstituto { get; set; }

        [JsonProperty("vBcStDest")]
        public string VBcStDest { get; set; }

        [JsonProperty("vIcmsStDest")]
        public string VIcmsStDest { get; set; }

        [JsonProperty("ufSt")]
        public string UfSt { get; set; }
    }

    public class IcmsTot
    {
        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("vIcms")]
        public string VIcms { get; set; }

        [JsonProperty("vIcmsDeson")]
        public string VIcmsDeson { get; set; }

        [JsonProperty("vFcpUfDest")]
        public string VFcpUfDest { get; set; }

        [JsonProperty("vIcmsUfDest")]
        public string VIcmsUfDest { get; set; }

        [JsonProperty("vIcmsUfRemet")]
        public string VIcmsUfRemet { get; set; }

        [JsonProperty("vFcp")]
        public string VFcp { get; set; }

        [JsonProperty("vBcSt")]
        public string VBcSt { get; set; }

        [JsonProperty("vSt")]
        public string VSt { get; set; }

        [JsonProperty("vFcpSt")]
        public string VFcpSt { get; set; }

        [JsonProperty("vFcpStRet")]
        public string VFcpStRet { get; set; }

        [JsonProperty("vProd")]
        public string VProd { get; set; }

        [JsonProperty("vFrete")]
        public string VFrete { get; set; }

        [JsonProperty("vSeg")]
        public string VSeg { get; set; }

        [JsonProperty("vDesc")]
        public string VDesc { get; set; }

        [JsonProperty("vIi")]
        public string VIi { get; set; }

        [JsonProperty("vIpi")]
        public string VIpi { get; set; }

        [JsonProperty("vIpiDevol")]
        public string VIpiDevol { get; set; }

        [JsonProperty("vPis")]
        public string VPis { get; set; }

        [JsonProperty("vCofins")]
        public string VCofins { get; set; }

        [JsonProperty("vOutro")]
        public string VOutro { get; set; }

        [JsonProperty("vNf")]
        public string VNf { get; set; }

        [JsonProperty("vTotTrib")]
        public string VTotTrib { get; set; }
    }

    public class IcmsUfDest
    {
        [JsonProperty("vBcUfDest")]
        public string VBcUfDest { get; set; }

        [JsonProperty("vBcFcpUfDest")]
        public string VBcFcpUfDest { get; set; }

        [JsonProperty("pFcpUfDest")]
        public string PFcpUfDest { get; set; }

        [JsonProperty("pIcmsUfDest")]
        public string PIcmsUfDest { get; set; }

        [JsonProperty("pIcmsInter")]
        public string PIcmsInter { get; set; }

        [JsonProperty("pIcmsInterPart")]
        public string PIcmsInterPart { get; set; }

        [JsonProperty("vFcpUfDest")]
        public string VFcpUfDest { get; set; }

        [JsonProperty("vIcmsUfDest")]
        public string VIcmsUfDest { get; set; }

        [JsonProperty("vIcmsUfRemet")]
        public string VIcmsUfRemet { get; set; }
    }

    public class Identificacao
    {
        [JsonProperty("codigoUf")]
        public string CodigoUf { get; set; }

        [JsonProperty("codigoNf")]
        public string CodigoNf { get; set; }

        [JsonProperty("tpNf")]
        public string tpNf { get; set; }

        [JsonProperty("naturezaOperacao")]
        public string NaturezaOperacao { get; set; }

        [JsonProperty("serie")]
        public string Serie { get; set; }

        [JsonProperty("numeroDocFiscal")]
        public string NumeroDocFiscal { get; set; }

        [JsonProperty("dataHoraEmissao")]
        public string DataHoraEmissao { get; set; }

        [JsonProperty("dataHoraSaidaOuEntrada")]
        public string DataHoraSaidaOuEntrada { get; set; }

        [JsonProperty("idLocalDestino")]
        public string IdLocalDestino { get; set; }

        [JsonProperty("codigoMunicipioFg")]
        public string CodigoMunicipioFg { get; set; }

        [JsonProperty("formatoDanfe")]
        public string FormatoDanfe { get; set; }

        [JsonProperty("formatoNfe")]
        public string FormatoNfe { get; set; }

        [JsonProperty("finalidade")]
        public string Finalidade { get; set; }

        [JsonProperty("indFinal")]
        public string IndFinal { get; set; }

        [JsonProperty("indPres")]
        public string IndPres { get; set; }

        [JsonProperty("indIntermed")]
        public int IndIntermed { get; set; }

        [JsonProperty("xJust")]
        public string XJust { get; set; }

        [JsonProperty("dhCont")]
        public string DhCont { get; set; }

        [JsonProperty("nFref")]
        public List<NFref> NFref { get; set; }
    }

    public class Ii
    {
        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("vDespAdu")]
        public string VDespAdu { get; set; }

        [JsonProperty("vIi")]
        public string VIi { get; set; }

        [JsonProperty("vIof")]
        public string VIof { get; set; }
    }

    public class Imposto
    {
        public Imposto()
        {
            Icms = new Icms();
            Pis = new Pis();
            PisSt = new PisSt();
            Cofins = new Cofins();
            CofinsSt = new CofinsSt();
            IcmsUfDest = new IcmsUfDest();
            Ipi = new Ipi();
            Issqn = new Issqn();
            Ii = new Ii();            
        }
        [JsonProperty("vTotTrib")]
        public string VTotTrib { get; set; }

        [JsonProperty("icms")]
        public Icms Icms { get; set; }
        public bool ShouldSerializeIcms()
        {
            return !string.IsNullOrEmpty(Icms.VBc);
        }

        [JsonProperty("pis")]
        public Pis Pis { get; set; }
        public bool ShouldSerializePis()
        {
            return !string.IsNullOrEmpty(Pis.VBc);
        }

        [JsonProperty("pisSt")]
        public PisSt PisSt { get; set; }
        public bool ShouldSerializePisSt()
        {
            return !string.IsNullOrEmpty(PisSt.VBc);
        }

        [JsonProperty("cofins")]
        public Cofins Cofins { get; set; }
        public bool ShouldSerializeCofins()
        {
            return !string.IsNullOrEmpty(Cofins.VBc);
        }

        [JsonProperty("cofinsSt")]
        public CofinsSt CofinsSt { get; set; }
        public bool ShouldSerializeCofinsSt()
        {
            return !string.IsNullOrEmpty(CofinsSt.VBc);
        }

        [JsonProperty("icmsUfDest")]
        public IcmsUfDest IcmsUfDest { get; set; }
        public bool ShouldSerializeIcmsUfDest()
        {
            return !string.IsNullOrEmpty(IcmsUfDest.VBcUfDest);
        }

        [JsonProperty("ipi")]
        public Ipi Ipi { get; set; }
        public bool ShouldSerializeIpi()
        {
            return !string.IsNullOrEmpty(Ipi.VBc);
        }

        [JsonProperty("issqn")]
        public Issqn Issqn { get; set; }
        public bool ShouldSerializeIssqn()
        {
            return !string.IsNullOrEmpty(Issqn.VBc);
        }

        [JsonProperty("ii")]
        public Ii Ii { get; set; }
        public bool ShouldSerializeIi()
        {
            return !string.IsNullOrEmpty(Ii.VBc);
        }
    }

    public class ImpostoDevol
    {
        [JsonProperty("pDevol")]
        public string PDevol { get; set; }

        [JsonProperty("ipi")]
        public Ipi Ipi { get; set; }
    }

    public class InfAdic
    {
        [JsonProperty("infAdFisco")]
        public string InfAdFisco { get; set; }

        [JsonProperty("infCpl")]
        public string InfCpl { get; set; }

        [JsonProperty("obsCont")]
        public ObsCont ObsCont { get; set; }

        [JsonProperty("obsFisco")]
        public ObsFisco ObsFisco { get; set; }

        [JsonProperty("procRef")]
        public ProcRef ProcRef { get; set; }
    }

    public class InfIntermed
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("idCadIntTran")]
        public string IdCadIntTran { get; set; }
    }

    public class Ipi
    {
        [JsonProperty("cEnq")]
        public string CEnq { get; set; }

        [JsonProperty("cnpjProd")]
        public string CnpjProd { get; set; }

        [JsonProperty("cSelo")]
        public string CSelo { get; set; }

        [JsonProperty("qSelo")]
        public string QSelo { get; set; }

        [JsonProperty("cst")]
        public string Cst { get; set; }

        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("pImp")]
        public string PImp { get; set; }

        [JsonProperty("vImp")]
        public string VImp { get; set; }

        [JsonProperty("qUnid")]
        public string QUnid { get; set; }

        [JsonProperty("vUnid")]
        public string VUnid { get; set; }

        [JsonProperty("vIpiDevol")]
        public object VIpiDevol { get; set; }
    }

    public class Issqn
    {
        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("vAliq")]
        public string VAliq { get; set; }

        [JsonProperty("vIssqn")]
        public string VIssqn { get; set; }

        [JsonProperty("cMunFG")]
        public string CMunFG { get; set; }

        [JsonProperty("cListServ")]
        public string CListServ { get; set; }

        [JsonProperty("vDeducao")]
        public string VDeducao { get; set; }

        [JsonProperty("vOutro")]
        public string VOutro { get; set; }

        [JsonProperty("vDescIncond")]
        public string VDescIncond { get; set; }

        [JsonProperty("vDescCond")]
        public string VDescCond { get; set; }

        [JsonProperty("vIssRet")]
        public string VIssRet { get; set; }

        [JsonProperty("indIss")]
        public string IndIss { get; set; }

        [JsonProperty("cServico")]
        public string CServico { get; set; }

        [JsonProperty("cMun")]
        public string CMun { get; set; }

        [JsonProperty("cPais")]
        public string CPais { get; set; }

        [JsonProperty("nProcesso")]
        public string NProcesso { get; set; }

        [JsonProperty("indIncentivo")]
        public string IndIncentivo { get; set; }
    }

    public class IssqnTot
    {
        [JsonProperty("vServ")]
        public string VServ { get; set; }

        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("vIss")]
        public string VIss { get; set; }

        [JsonProperty("vPis")]
        public string VPis { get; set; }

        [JsonProperty("vCofins")]
        public string VCofins { get; set; }

        [JsonProperty("dCompet")]
        public string DCompet { get; set; }

        [JsonProperty("vDeducao")]
        public string VDeducao { get; set; }

        [JsonProperty("vOutro")]
        public string VOutro { get; set; }

        [JsonProperty("vDescIncond")]
        public string VDescIncond { get; set; }

        [JsonProperty("vDescCond")]
        public string VDescCond { get; set; }

        [JsonProperty("vIssRet")]
        public string VIssRet { get; set; }

        [JsonProperty("cRegTrib")]
        public string CRegTrib { get; set; }
    }

    public class Lacre
    {
        [JsonProperty("nLacre")]
        public string NLacre { get; set; }
    }

    public class NFref
    {
        [JsonProperty("refNFe")]
        public string RefNFe { get; set; }

        [JsonProperty("refNF")]
        public RefNF RefNF { get; set; }

        [JsonProperty("refNFP")]
        public RefNFP RefNFP { get; set; }

        [JsonProperty("refCTe")]
        public string RefCTe { get; set; }

        [JsonProperty("refECF")]
        public RefECF RefECF { get; set; }
    }

    public class ObsCont
    {
        [JsonProperty("xTexto")]
        public string XTexto { get; set; }

        [JsonProperty("xCampo")]
        public string XCampo { get; set; }
    }

    public class ObsFisco
    {
        [JsonProperty("xTexto")]
        public string XTexto { get; set; }

        [JsonProperty("xCampo")]
        public string XCampo { get; set; }
    }

    public class Pag
    {
        public Pag()
        {
            DetPag = new List<DetPag>();
        }

        [JsonProperty("valorTroco")]
        public string ValorTroco { get; set; }

        [JsonProperty("detPag")]
        public List<DetPag> DetPag { get; set; }
    }

    public class Pis
    {
        [JsonProperty("cst")]
        public string Cst { get; set; }

        [JsonProperty("qBcProd")]
        public string QBcProd { get; set; }

        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("pPis")]
        public string PPis { get; set; }

        [JsonProperty("vPis")]
        public string VPis { get; set; }

        [JsonProperty("vAliqProd")]
        public string VAliqProd { get; set; }
    }

    public class PisSt
    {
        [JsonProperty("vBc")]
        public string VBc { get; set; }

        [JsonProperty("pPis")]
        public string PPis { get; set; }

        [JsonProperty("qBcProd")]
        public string QBcProd { get; set; }

        [JsonProperty("vAliqProd")]
        public string VAliqProd { get; set; }

        [JsonProperty("vPis")]
        public string VPis { get; set; }
    }

    public class ProcRef
    {
        [JsonProperty("nProc")]
        public string NProc { get; set; }

        [JsonProperty("indProc")]
        public string IndProc { get; set; }
    }    

    public class Rastro
    {
        [JsonProperty("numeroLote")]
        public string NumeroLote { get; set; }

        [JsonProperty("quantidadeProdutoLote")]
        public string QuantidadeProdutoLote { get; set; }

        [JsonProperty("dataFabricacao")]
        public string DataFabricacao { get; set; }

        [JsonProperty("dataValidade")]
        public string DataValidade { get; set; }

        [JsonProperty("codigoAgregacao")]
        public string CodigoAgregacao { get; set; }
    }

    public class Reboque
    {
        [JsonProperty("placa")]
        public string Placa { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("rntc")]
        public string Rntc { get; set; }
    }

    public class RefECF
    {
        [JsonProperty("mod")]
        public string Mod { get; set; }

        [JsonProperty("necf")]
        public string Necf { get; set; }

        [JsonProperty("ncoo")]
        public string Ncoo { get; set; }
    }

    public class RefNF
    {
        [JsonProperty("cuf")]
        public string Cuf { get; set; }

        [JsonProperty("aamm")]
        public string Aamm { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("mod")]
        public string Mod { get; set; }

        [JsonProperty("serie")]
        public string Serie { get; set; }

        [JsonProperty("nnf")]
        public string Nnf { get; set; }
    }

    public class RefNFP
    {
        [JsonProperty("cuf")]
        public string Cuf { get; set; }

        [JsonProperty("aamm")]
        public string Aamm { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("ie")]
        public string Ie { get; set; }

        [JsonProperty("mod")]
        public string Mod { get; set; }

        [JsonProperty("serie")]
        public string Serie { get; set; }

        [JsonProperty("nnf")]
        public string Nnf { get; set; }
    }

    public class Retirada
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("ie")]
        public string Ie { get; set; }
    }

    public class RetTransp
    {
        [JsonProperty("valorServico")]
        public string ValorServico { get; set; }

        [JsonProperty("vBcRet")]
        public string VBcRet { get; set; }

        [JsonProperty("pIcmsRet")]
        public string PIcmsRet { get; set; }

        [JsonProperty("valorIcmsRetido")]
        public string ValorIcmsRetido { get; set; }

        [JsonProperty("cFop")]
        public string CFop { get; set; }

        [JsonProperty("codigoMunicipioFg")]
        public string CodigoMunicipioFg { get; set; }
    }

    public class RetTrib
    {
        [JsonProperty("vRetPis")]
        public string VRetPis { get; set; }

        [JsonProperty("vRetCofins")]
        public string VRetCofins { get; set; }

        [JsonProperty("vRetCsll")]
        public string VRetCsll { get; set; }

        [JsonProperty("vBcIrrf")]
        public string VBcIrrf { get; set; }

        [JsonProperty("vIrrf")]
        public string VIrrf { get; set; }

        [JsonProperty("vBcRetPrev")]
        public string VBcRetPrev { get; set; }

        [JsonProperty("vRetPrev")]
        public string VRetPrev { get; set; }
    }

    public class Status
    {
        [JsonProperty("mStat")]
        public string MStat { get; set; }

        [JsonProperty("cStat")]
        public string CStat { get; set; }
    }

    public class Total
    {
        [JsonProperty("icmsTot")]
        public IcmsTot IcmsTot { get; set; }

        [JsonProperty("issqnTot")]
        public IssqnTot IssqnTot { get; set; }

        [JsonProperty("retTrib")]
        public RetTrib RetTrib { get; set; }
    }

    public class Transp
    {
        [JsonProperty("modFrete")]
        public string ModFrete { get; set; }

        [JsonProperty("transporta")]
        public Transporta Transporta { get; set; }

        [JsonProperty("retTransp")]
        public RetTransp RetTransp { get; set; }

        [JsonProperty("veicTransp")]
        public VeicTransp VeicTransp { get; set; }

        [JsonProperty("reboque")]
        public List<Reboque> Reboque { get; set; }

        [JsonProperty("vagao")]
        public string Vagao { get; set; }

        [JsonProperty("balsa")]
        public string Balsa { get; set; }

        [JsonProperty("vol")]
        public List<Vol> Vol { get; set; }
    }

    public class Transporta
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("inscricaoEstadual")]
        public string InscricaoEstadual { get; set; }

        [JsonProperty("enderecoCompleto")]
        public string EnderecoCompleto { get; set; }

        [JsonProperty("nomeMunicipio")]
        public string NomeMunicipio { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }
    }

    public class VeicTransp
    {
        [JsonProperty("placa")]
        public string Placa { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("rntc")]
        public string Rntc { get; set; }
    }

    public class Vol
    {
        [JsonProperty("quantidade")]
        public string Quantidade { get; set; }

        [JsonProperty("especie")]
        public string Especie { get; set; }

        [JsonProperty("marca")]
        public string Marca { get; set; }

        [JsonProperty("numeracao")]
        public string Numeracao { get; set; }

        [JsonProperty("pesoLiquido")]
        public string PesoLiquido { get; set; }

        [JsonProperty("pesoBruto")]
        public string PesoBruto { get; set; }

        [JsonProperty("lacres")]
        public List<Lacre> Lacres { get; set; }
    }

}

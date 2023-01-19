using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.InboundOtherDocuments.services.Input
{

    public class Root
    {
        [JsonProperty("data")]
        public OtherDocumentRegisterInput Data { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; } = "emit";

        [JsonProperty("dfe")]
        public string Dfe { get; set; } = "outro";
    }

    public class OtherDocumentRegisterInput
    {
        [JsonProperty("branchId")]
        public string BranchId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("identificacao")]
        public Identificacao Identificacao { get; set; }

        [JsonProperty("emitente")]
        public Emitente Emitente { get; set; }

        [JsonProperty("destinatario")]
        public Destinatario Destinatario { get; set; }

        [JsonProperty("valores")]
        public Valores Valores { get; set; }

        [JsonProperty("itens")]
        public List<Iten> Itens { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }

    public class Iten
    {
        public Iten()
        {
            Impostos = new Impostos();
        }
        [JsonProperty("codigoItem")]
        public string CodigoItem { get; set; }

        [JsonProperty("descricaoItem")]
        public string DescricaoItem { get; set; }

        [JsonProperty("cfop")]
        public string Cfop { get; set; }

        [JsonProperty("unidadeMedida")]
        public string UnidadeMedida { get; set; }

        [JsonProperty("quantidade")]
        public string Quantidade { get; set; }

        [JsonProperty("valorItem")]
        public string ValorItem { get; set; }

        [JsonProperty("Impostos")]
        public Impostos Impostos { get; set; }
    }

    public class Impostos
    {
        [JsonProperty("cstPIS")]
        public string CstPIS { get; set; }

        [JsonProperty("vBcPIS")]
        public string VBcPIS { get; set; }

        [JsonProperty("aliqPIS")]
        public string AliqPIS { get; set; }

        [JsonProperty("valorPIS")]
        public string ValorPIS { get; set; }

        [JsonProperty("cstCOFINS")]
        public string CstCOFINS { get; set; }

        [JsonProperty("vBcCOFINS")]
        public string VBcCOFINS { get; set; }

        [JsonProperty("aliqCOFINS")]
        public string AliqCOFINS { get; set; }

        [JsonProperty("valorCOFINS")]
        public string ValorCOFINS { get; set; }
    }

    public class Cofins
    {
        [JsonProperty("cst")]
        public string Cst { get; set; }

        [JsonProperty("vBC")]
        public string VBC { get; set; }

        [JsonProperty("aliquotaCofins")]
        public string AliquotaCofins { get; set; }

        [JsonProperty("vCofins")]
        public string VCofins { get; set; }
    }

    public class Destinatario
    {
        [JsonProperty("razaoSocial")]
        public string RazaoSocial { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("inscricaoEstadual")]
        public string InscricaoEstadual { get; set; }

        [JsonProperty("inscricaoMunicipal")]
        public string InscricaoMunicipal { get; set; }

        [JsonProperty("regimeTributacao")]
        public string RegimeTributacao { get; set; }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }
    }

    public class Emitente
    {
        [JsonProperty("razaoSocial")]
        public string RazaoSocial { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("inscricaoEstadual")]
        public string InscricaoEstadual { get; set; }

        [JsonProperty("inscricaoMunicipal")]
        public string InscricaoMunicipal { get; set; }

        [JsonProperty("regimeTributacao")]
        public string RegimeTributacao { get; set; }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }
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
    }

    public class Identificacao
    {
        [JsonProperty("dataEmissao")]
        public string DataEmissao { get; set; }

        [JsonProperty("dataLancamento")]
        public string DataLancamento { get; set; }

        [JsonProperty("numeroDocFiscal")]
        public string NumeroDocFiscal { get; set; }

        [JsonProperty("modelo")]
        public string Modelo { get; set; }

        [JsonProperty("serie")]
        public string Serie { get; set; }

        [JsonProperty("finalidade")]
        public string Finalidade { get; set; }

        [JsonProperty("tipoOperacao")]
        public string TipoOperacao { get; set; }
    }

    public class Pis
    {
        [JsonProperty("cst")]
        public string Cst { get; set; }

        [JsonProperty("vBC")]
        public string VBC { get; set; }

        [JsonProperty("aliquotaPis")]
        public string AliquotaPis { get; set; }

        [JsonProperty("vPis")]
        public string VPis { get; set; }
    }


    public class Status
    {
        [JsonProperty("cStat")]
        public string CStat { get; set; }

        [JsonProperty("mStat")]
        public string MStat { get; set; }
    }

    public class Valores
    {
        [JsonProperty("valorBruto")]
        public double ValorBruto { get; set; }

        [JsonProperty("iss")]
        public double Iss { get; set; }

        [JsonProperty("pis")]
        public double Pis { get; set; }

        [JsonProperty("cofins")]
        public double Cofins { get; set; }

        [JsonProperty("csll")]
        public double Csll { get; set; }

        [JsonProperty("inss")]
        public double Inss { get; set; }

        [JsonProperty("ir")]
        public double Ir { get; set; }

        [JsonProperty("outrasRetencoes")]
        public double OutrasRetencoes { get; set; }

        [JsonProperty("valorLiquido")]
        public double ValorLiquido { get; set; }
    }
}

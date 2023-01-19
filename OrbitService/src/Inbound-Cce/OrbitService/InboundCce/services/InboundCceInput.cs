using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitService.InboundCce.services
{
    public class InboundCceInput
    {
        public InboundCceInput()
        {
            data = new Data();
        }
        public bool active { get; set; }
        public Data data { get; set; }
        [JsonProperty("event")]
        public string Event { get; } = "emit";
        public string dfe { get; } = "cce";
    }

    public class Data
    {
        public Data()
        {
            identificacao = new Identificacao();
            emitente = new Emitente();
            destinatario = new Destinatario();
            item = new List<Item>();
            valores = new Valores();
            StatusDoc = new StatusDoc();
        }
        public string branchId { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public Identificacao identificacao { get; set; }
        public Emitente emitente { get; set; }
        public Destinatario destinatario { get; set; }
        public List<Item> item { get; set; }
        public Valores valores { get; set; }
        public StatusDoc StatusDoc { get; set; }
    }

    public class Destinatario
    {
        public Destinatario()
        {
            endereco = new Endereco();
        }
        public string razaoSocial { get; set; }
        public string cnpj { get; set; }
        public string cpf { get; set; }
        public string inscricaoEstadual { get; set; }
        public string inscricaoMunicipal { get; set; }
        public string indIeDestinatario { get; set; }
        public Endereco endereco { get; set; }
    }

    public class Emitente
    {
        public Emitente()
        {
            endereco = new Endereco();
        }
        public string razaoSocial { get; set; }
        public string cnpj { get; set; }
        public string cpf { get; set; }
        public string inscricaoEstadual { get; set; }
        public string inscricaoMunicipal { get; set; }
        public string indIeEmitente { get; set; }
        public Endereco endereco { get; set; }
    }

    public class Endereco
    {
        public string logradouro { get; set; }
        public int numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public Int32 codigoMunicipio { get; set; }
        public string municipio { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public int codigoPais { get; set; }
        public string pais { get; set; }
        public Int64 fone { get; set; }
        public string email { get; set; }
    }

    public class Identificacao
    {
        public string modeloDoc { get; set; }
        public string serie { get; set; }
        public string subSerie { get; set; }
        public string numeroDoc { get; set; }
        public string dataEmissao { get; set; }
        public Int64 chvAcesso { get; set; }
        public string tipoOperacao { get; set; }
    }

    public class Impostos
    {
        public string cstICMS { get; set; }
        public double vbcICMS { get; set; }
        public double vRedBcICMS { get; set; }
        public double aliqICMS { get; set; }
        public double valorICMS { get; set; }
        public string natBCCreditoPIS { get; set; }
        public string cstPIS { get; set; }
        public double vbcPIS { get; set; }
        public double aliqPIS { get; set; }
        public double valorPIS { get; set; }
        public string contaContabilPIS { get; set; }
        public string natBCCreditoCOFINS { get; set; }
        public string cstCOFINS { get; set; }
        public double vbcCOFINS { get; set; }
        public double aliqCOFINS { get; set; }
        public double valorCOFINS { get; set; }
        public string contaContabilCOFINS { get; set; }
    }

    public class Item
    {
        public Item()
        {
            Impostos = new Impostos();
        }
        public string codigoItem { get; set; }
        public string descricaoItem { get; set; }
        public string CFOP { get; set; }
        public string unidadeMedida { get; set; }
        public double quantidade { get; set; }
        public double valorItem { get; set; }
        public Impostos Impostos { get; set; }
    }

    public class StatusDoc
    {
        public int cStat { get; set; }
        public string mStat { get; set; }
        public string dataLancamento { get; set; }
    }

    public class Valores
    {
        public double valorDocumento { get; set; }
        public double vbcICMS { get; set; }
        public double valorICMS { get; set; }
        public double vBcPIS { get; set; }
        public double valorPIS { get; set; }
        public double vbcCOFINS { get; set; }
        public double valorCOFINS { get; set; }
    }

}

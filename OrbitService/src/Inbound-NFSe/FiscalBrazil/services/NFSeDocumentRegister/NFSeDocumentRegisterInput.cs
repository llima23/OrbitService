using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OrbitService.FiscalBrazil.services.NFSeDocumentRegister
{
    public class NFSeDocumentRegisterInput
    {
        public string dfe { get; set; } = "nfse";
        [JsonProperty("event")]
        public string Event { get;set; } = "emit";

        [JsonProperty("data")]
        public NFServico NFServico { get; set; }
    }

    public partial class NFServico
    {
        public NFServico()
        {
            Nfse nfse = new Nfse();
        }
        public Nfse nfse { get; set; }
        [JsonProperty("branchId")]
        public string BranchId { get; set; }

        [JsonProperty("rps")]
        public Rps Rps { get; set; }

        [JsonProperty("numeroLote")]
        public string numeroLote { get; set; }
        public bool ShouldSerializenumeroLote()
        {
            return (!string.IsNullOrEmpty(numeroLote));
        }

        [JsonProperty("versao")]
        public string versao { get; set; }

        public bool ShouldSerializeversao()
        {
            return (!string.IsNullOrEmpty(versao));
        }

        public List<Eventos> eventos { get; set; }

        public Status status { get; set; }

     

        public string tipoOperacao { get; set; } = "input";

        public class Eventos
        {
            public string type { get; set; }
            //public string protocolo { get; set; }
            //public string dhEvento { get; set; }
            //public string communicationId { get; set; }
        }

        public class Status
        {
            public string cStat { get; set; }
            public string mStat { get; set; }
        }

        public class Nfse
        {
            public string numero { get; set; }

            public string dataEmissao { get; set; }
            public string codigoMunicipioGerador { get; set; }
        }

        public class Emails { }

        public string[] emails { get; set; }

        public string dataLancamento { get; set; }



    }

    public partial class Rps
    {
        [JsonProperty("identificacao")]
        public Identificacao Identificacao { get; set; }

        [JsonProperty("tomador")]
        public Tomador Tomador { get; set; }

        [JsonProperty("servico")]
        public Servico Servico { get; set; }

        //[JsonProperty("construcaoCivil")]
        //public construcaoCivil ConstrucaoCivil { get; set; }

        [JsonProperty("intermediario")]
        public Intermediario Intermediario { get; set; }

        public bool ShouldSerializeIntermediario()
        {
            return (Intermediario != null);
        }

        [JsonProperty("pag")]
        public Pag Pag { get; set; }
        [JsonProperty("prestador")]
        public Prestador Prestador { get; set; }

        public class Emails { }

    }

    public class Prestador
    {
        public string cnpj { get; set; }

        public string inscricaoMunicipal { get; set; }

        public string razaoSocial { get; set; }

        public string regimeDeTributacao { get; set; }

        [JsonProperty("endereco")]
        public enderecoPrestador Endereco { get; set; }

        public bool ShouldSerializeEndereco()
        {
            return (!string.IsNullOrEmpty(Endereco.logradouro));
        }


    }
    public class enderecoPrestador
    {
        public string tipoLogradouro { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string codigoMunicipio { get; set; }
        public string nomeMunicipio { get; set; }
        public string codigoPais { get; set; }
        public string nomePais { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public string tpBairro { get; set; }
        //public string enderecoExterior { get; set; }
        //public string siafiMunicipio { get; set; }
        public string cUf { get; set; }
        public string nomeUf { get; set; }
    }
    public partial class Pag
    {
        [JsonProperty("detPag")]
        public List<DetPag> DetPag { get; set; }
    }
    public partial class DetPag
    {
        public string tPag { get; set; }

        public string indPag { get; set; }

        public string vPag { get; set; }
    }
    public partial class Identificacao
    {
        [JsonProperty("cNf")]
        public string cNf { get; set; }


        [JsonProperty("indPres")]
        public int indPres { get; set; }


        [JsonProperty("serie")]
        public string Serie { get; set; }
        public bool ShouldSerializeSerie()
        {
            return (!string.IsNullOrEmpty(Serie));
        }

        [JsonProperty("numero")]

        public string Numero { get; set; }

        public bool ShouldSerializeNumero()
        {
            return (!string.IsNullOrEmpty(Numero));
        }

        [JsonProperty("tipoRps")]
        public string TipoRps { get; set; }

        [JsonProperty("dataEmissao")]
        public string DataEmissao { get; set; }

        [JsonProperty("competencia")]
        public string Competencia { get; set; }

        [JsonProperty("naturezaOperacao")]
        public string NaturezaOperacao { get; set; }

        [JsonProperty("regimeEspecialTributacao")]
        public string RegimeEspecialTributacao { get; set; }

        [JsonProperty("optanteSimplesNacional")]

        public string OptanteSimplesNacional { get; set; }

        [JsonProperty("incentivadorCultural")]

        public string IncentivadorCultural { get; set; }

        [JsonProperty("descricao")]

        public string descricao { get; set; }
        public bool ShouldSerializedescricao()
        {
            return (!string.IsNullOrEmpty(descricao));
        }

        [JsonProperty("tpOperacao")]

        public string tpOperacao { get; set; }

        public bool ShouldSerializetpOperacao()
        {
            return (!string.IsNullOrEmpty(tpOperacao));
        }

    }
    public partial class Servico
    {

        [JsonProperty("tpRecolhimento")]
        public string tpRecolhimento { get; set; }

        [JsonProperty("codigoAtividade")]
        public string CodigoAtividade { get; set; }

        [JsonProperty("aliquotaAtividade")]
        public int aliquotaAtividade { get; set; }

        [JsonProperty("codigoServico")]
        public string CodigoServico { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("valorUnitario")]
        public double ValorUnitario { get; set; }

        [JsonProperty("discriminacao")]
        public Array Discriminacao { get; set; }

        [JsonProperty("itemListaServico")]
        public string ItemListaServico { get; set; }

        [JsonProperty("cnae")]

        public string Cnae { get; set; }

        public bool ShouldSerializeCnae()
        {
            return (Cnae != "false");
        }

        [JsonProperty("codigoTributacaoMunicipio")]

        public string CodigoTributacaoMunicipio { get; set; }

        public bool ShouldSerializeCodigoTributacaoMunicipio()
        {
            return (CodigoTributacaoMunicipio != null);
        }

        [JsonProperty("codigoMunicipioIncidencia")]

        public string CodigoMunicipioIncidencia { get; set; }

        public bool ShouldSerializeCodigoMunicipioIncidencia()
        {
            return (!string.IsNullOrEmpty(CodigoMunicipioIncidencia));
        }

        [JsonProperty("valores")]
        public Valores Valores { get; set; }


    }
    public partial class Valores
    {



        //[JsonProperty("valorLiquido")]
        //public string ValorLiquido { get; set; }

        [JsonProperty("totalServicos")]
        public double TotalServicos { get; set; }

        [JsonProperty("totalDeducoes")]
        public double TotalDeducoes { get; set; }

        //[JsonProperty("enderecoUF")]
        //public string EnderecoUF { get; set; }
        //public bool ShouldSerializeEnderecoUF()
        //{
        //    return (EnderecoUF == "@@@");
        //}
        //Campos Removidos para processar NFS-e de São Bernardo do campo
        [JsonProperty("descontoCondicionado")]
        public double DescontoCondicionado { get; set; }

        //Campos Removidos para processar NFS-e de São Bernardo do campo
        [JsonProperty("descontoIncondicionado")]
        public double DescontoIncondicionado { get; set; }


        //Campos Removidos para processar NFS-e de São Bernardo do campo

        [JsonProperty("outrasRetencoes")]
        public double OutrasRetencoes { get; set; }

        [JsonProperty("iss")]
        public Iss Iss { get; set; }
        public bool ShouldSerializeIss()
        {
            return (Iss != null);
        }

        [JsonProperty("pis")]
        public Pis Pis { get; set; }
        public bool ShouldSerializePis()
        {
            return (Pis != null);
        }


        [JsonProperty("cofins")]
        public Cofins Cofins { get; set; }
        public bool ShouldSerializeCofins()
        {
            return (Cofins != null);
        }

        [JsonProperty("inss")]
        public Inss Inss { get; set; }
        public bool ShouldSerializeInss()
        {
            return (Inss != null);
        }

        [JsonProperty("ir")]
        public Ir Ir { get; set; }
        public bool ShouldSerializeIr()
        {
            return (Ir != null);
        }

        [JsonProperty("csll")]
        public Csll Csll { get; set; }

        public bool ShouldSerializeCsll()
        {
            return (Csll != null);
        }

    }
    public partial class Cofins
    {
        [JsonProperty("aliquota")]
        public double Aliquota { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }

        [JsonProperty("cst")]
        public string Cst { get; set; }

        [JsonProperty("baseCalculo")]
        public string baseCalculo { get; set; }

    }
    public partial class Pis
    {
        [JsonProperty("aliquota")]
        public double Aliquota { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }

        [JsonProperty("cst")]
        public string Cst { get; set; }

        [JsonProperty("baseCalculo")]
        public string baseCalculo { get; set; }

    }
    public partial class Inss
    {
        [JsonProperty("aliquota")]
        public double Aliquota { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }

        [JsonProperty("baseCalculo")]
        public string baseCalculo { get; set; }


    }
    public partial class Ir
    {
        [JsonProperty("aliquota")]
        public double Aliquota { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }

        [JsonProperty("baseCalculo")]
        public string baseCalculo { get; set; }
    }
    public partial class Csll
    {
        [JsonProperty("aliquota")]
        public double Aliquota { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }

        [JsonProperty("baseCalculo")]
        public string baseCalculo { get; set; }
    }
    public partial class Iss
    {
        [JsonProperty("baseCalculo")]
        public double BaseCalculo { get; set; }

        [JsonProperty("aliquota")]
        public double Aliquota { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }

        [JsonProperty("valorRetido")]
        public double ValorRetido { get; set; }

        public bool ShouldSerializeValorRetido()
        {
            return (ValorRetido > 0);
        }

        [JsonProperty("retido")]
        public bool Retido { get; set; }

        [JsonProperty("exigibilidadeIss")]
        public string ExigibilidadeIss { get; set; }
    }
    public partial class Tomador
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        public bool ShouldSerializeCnpj()
        {
            return (!string.IsNullOrEmpty(Cnpj));
        }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        public bool ShouldSerializeCpf()
        {
            return (!string.IsNullOrEmpty(Cpf));
        }

        [JsonProperty("razaoSocial")]
        public string RazaoSocial { get; set; }

        [JsonProperty("nomeFantasia")]
        public string NomeFantasia { get; set; }

        [JsonProperty("docEstrangeiro")]
        public string docEstrangeiro { get; set; }
        public bool ShouldSerializedocEstrangeiro()
        {
            return (!string.IsNullOrEmpty(docEstrangeiro));
        }

        [JsonProperty("inscricaoEstadual")]
        public string InscricaoEstadual { get; set; }

        public bool ShouldSerializeInscricaoEstadual()
        {
            return (!string.IsNullOrEmpty(InscricaoEstadual));
        }

        [JsonProperty("inscricaoMunicipal")]
        public string InscricaoMunicipal { get; set; }

        public bool ShouldSerializeInscricaoMunicipal()
        {
            return (Endereco.Uf != "AM" && !string.IsNullOrEmpty(InscricaoMunicipal));
        }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }

        [JsonProperty("contato")]
        public Contato Contato { get; set; }
    }
    public partial class Contato
    {

        [JsonProperty("ddd")]
        public string Ddd { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("site")]
        public string Site { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        public bool ShouldSerializeEmail()
        {
            return (!string.IsNullOrEmpty(Email));
        }
    }
    public partial class Endereco
    {
        [JsonProperty("tipoLogradouro")]
        public string TipoLogradouro { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("numero")]

        public string Numero { get; set; }

        [JsonProperty("complemento")]

        public string Complemento { get; set; }
        public bool ShouldSerializeComplemento()
        {
            return (Complemento != null);
        }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("codigoMunicipio")]

        public string CodigoMunicipio { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("cUf")]
        public string cUf { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("codigoPais")]

        public string CodigoPais { get; set; }

        [JsonProperty("nomePais")]

        public string nomePais { get; set; }

        [JsonProperty("nomeMunicipio")]

        public string nomeMunicipio { get; set; }

        [JsonProperty("tpBairro")]

        public string tpBairro { get; set; }

    }
    public partial class Intermediario
    {

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        public bool ShouldSerializeCpf()
        {
            return (Cpf != null);
        }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        public bool ShouldSerializeCnpj()
        {
            return (Cnpj != null);
        }

        [JsonProperty("razaoSocial")]
        public string RazaoSocial { get; set; }

        public bool ShouldSerializeRazaoSocial()
        {
            return (RazaoSocial != null);
        }

        [JsonProperty("inscricaoMunicipal")]
        public string InscricaoMunicipal { get; set; }
        public bool ShouldSerializeInscricaoMunicipal()
        {
            return (InscricaoMunicipal != null);
        }


        [JsonProperty("contato")]
        public Contato Contato { get; set; }

        public bool ShouldSerializeContato()
        {
            return (Contato != null);
        }
    }
    //public partial class construcaoCivil
    //{
    //    [JsonProperty("matriculaCei")]
    //    public string MatriculaCei { get; set; }

    //    public bool ShouldSerializeMatriculaCei()
    //    {
    //        return (MatriculaCei != null);
    //    }

    //    [JsonProperty("numeroArt")]
    //    public string NumeroArt { get; set; }

    //    public bool ShouldSerializeNumeroArt()
    //    {
    //        return (NumeroArt != null);
    //    }


    //    [JsonProperty("codigoSerieArt")]
    //    public string CodigoSerieArt { get; set; }
    //    public bool ShouldSerializeNumeroCodigoSerieArt()
    //    {
    //        return (CodigoSerieArt != null);
    //    }

    //    [JsonProperty("dataEmissaoArt")]
    //    public string DataEmissaoArt { get; set; }

    //    public bool ShouldSerializeNumeroDataEmissaoArt()
    //    {
    //        return (DataEmissaoArt != null);
    //    }
    //}
}

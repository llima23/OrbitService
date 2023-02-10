using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4TAX_Service.Services.Document.Properties
{
    public partial class Emit
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Alvara
        {
            public int numero { get; set; }
            public string ano { get; set; }
        }

        public class Card
        {
            public string tpIntegra { get; set; }
            public string cnpj { get; set; }
            public string tBand { get; set; }
            public string cAut { get; set; }
        }
        public class Cofins
        {
            public double aliquota { get; set; } = 0.00;
            public double valor { get; set; } = 0.00;
            public string baseCalculo { get; set; } = "0.00";
            public string cst { get; set; }
            public string percentual { get; set; }
        }

        public class ConstrucaoCivil
        {
            public int matriculaCei { get; set; }
            public int numeroArt { get; set; }
            public string codigoSerieArt { get; set; }
            public string dataEmissaoArt { get; set; }
            public Alvara alvara { get; set; }
            public string localLote { get; set; }
            public string localQuadra { get; set; }
            public string localBairro { get; set; }
            public string codObra { get; set; }
            public int numeroCdc { get; set; }
            public string numeroCei { get; set; }
            public string numeroProj { get; set; }
            public string numeroMatri { get; set; }
            public string endereco { get; set; }
        }

        public class Contato
        {
            public string ddd { get; set; }
            public string telefone { get; set; }
            public string fax { get; set; }
            public string email { get; set; }
            public string site { get; set; }
            public int ramal { get; set; }
        }

        public class CpfCnpj
        {
            public string cnpj { get; set; }
        }

        public class Csll
        {
            public double aliquota { get; set; } = 0.00;
            public double valor { get; set; } = 0.00;
            public string baseCalculo { get; set; } = "0.00";
            public string percentual { get; set; } = "0.00";
        }

        public class DadosFornecedor
        {
            public IdentificacaoFornecedor identificacaoFornecedor { get; set; }
        }

        public class DetDeducoes
        {
            public DetDeducoes()
            {
                lista = new List<Listum>();
            }
            public string tipo { get; set; }
            public int percentual { get; set; }
            public List<Listum> lista { get; set; }
        }

        public class DetPag
        {
            public string indPag { get; set; }
            public string tPag { get; set; }
            public bool contraApresentacao { get; set; }
            public string vPag { get; set; }
            public Card card { get; set; }
            public List<Parcela> parcelas { get; set; }
        }

        public class Endereco
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
            public string enderecoExterior { get; set; }
            public string siafiMunicipio { get; set; }
            public string cUf { get; set; }
            public string nomeUf { get; set; }
            public string pontoReferencia { get; set; }
        }

        public class Evento
        {
            public string identificacaoEvento { get; set; }
            public string descricaoEvento { get; set; }
        }

        public class Fatura
        {
            public string numfatura { get; set; }
            public string vencimentofatura { get; set; }
            public int valorfatura { get; set; }
            public string item { get; set; }
        }

        public class Identificacao
        {
            public Identificacao()
            {
                descricao = new List<string>();
                evento = new Evento();
            }
            public string serie { get; set; }
            public string numero { get; set; }
            public string tipoRps { get; set; }
            public DateTime dataEmissao { get; set; }
            public DateTime competencia { get; set; }
            public string sequencia { get; set; }
            public string naturezaOperacao { get; set; }
            public string regimeEspecialTributacao { get; set; }
            public string incentivadorCultural { get; set; }
            public string seriePrestacao { get; set; }
            public string tpOperacao { get; set; }
            public List<string> descricao { get; set; }
            public string situacao { get; set; }
            public Evento evento { get; set; }
            public string cNf { get; set; }
            public int indPres { get; set; }
            public string optanteSimplesNacional { get; set; }
        }

        public class IdentificacaoFornecedor
        {
            public CpfCnpj cpfCnpj { get; set; }
        }

        public class IdentificadorDocumento
        {
            public Nfse nfse { get; set; }
        }

        public class Inss
        {
            public double aliquota { get; set; } = 0.00;
            public double valor { get; set; } = 0.00;
            public string baseCalculo { get; set; } = "0.00";
        }

        public class Intermediario
        {
            public Intermediario()
            {
                endereco = new Endereco();
                contato = new Contato();
            }
            [JsonProperty("cnpj")]
            public string cnpj { get; set; }
            [JsonProperty("cpf")]
            public string cpf { get; set; }
            public string razaoSocial { get; set; }
            public string inscricaoMunicipal { get; set; }
            public Endereco endereco { get; set; }
            public Contato contato { get; set; }
        }

        public class Ir
        {
            public double aliquota { get; set; } = 0.00;
            public double valor { get; set; } = 0.00;
            public string baseCalculo { get; set; } = "0.00";
            public string percentual { get; set; } = "0.00";
        }

        public class Iss
        {
            public double baseCalculo { get; set; } = 0.00;
            public double aliquota { get; set; } = 0.00;
            public double valor { get; set; } = 0.00;
            public double valorRetido { get; set; } = 0.00;
            public bool retido { get; set; }
            public string exigibilidadeIss { get; set; }
            public int valorReducaoBC { get; set; }
            public int reducao { get; set; }
        }

        public class Listum
        {
            public string por { get; set; }
            public string tipo { get; set; }
            public int percentualDeduzir { get; set; }
            public int valorDeduzir { get; set; }
            public string valorTotRef { get; set; }
            public string descricao { get; set; }
            public IdentificadorDocumento identificadorDocumento { get; set; }
            public DadosFornecedor dadosFornecedor { get; set; }
            public string dataEmissao { get; set; }
        }

        public class Nfse
        {
            public string codigoMunicipioGerador { get; set; }
            public int numeroNfse { get; set; }
            public string codigoVerificacao { get; set; }
            public string serie { get; set; }
        }

        public class NfseSubstituida
        {
            public int numero { get; set; }
            public DateTime dataEmissao { get; set; }
        }

        public class Pag
        {
            public string valorTroco { get; set; }
            public List<DetPag> detPag { get; set; }
        }


        public class Parcela
        {
            public string numero { get; set; }
            public string valor { get; set; }
            public string dataVencimento { get; set; }
        }

        public class Pedagio
        {
            public string codigoEquipamentoAutomatico { get; set; }
        }

        public class Pis
        {
            public double aliquota { get; set; } = 0.00;
            public double valor { get; set; } = 0.00;
            public string baseCalculo { get; set; } = "0.00";
            public string cst { get; set; }
            public double percentual { get; set; } = 0.00;
        }

       

        public class Rps
        {
            public Rps()
            {
                identificacao = new Identificacao();
                tomador = new Tomador();
                servico = new Servico();
                pedagio = new Pedagio();
                pag = new Pag();
                intermediario = new Intermediario();
                transporte = new Transporte();
            }

            public Identificacao identificacao { get; set; }
            public Tomador tomador { get; set; }                      
            public Servico servico { get; set; }
            public Pedagio pedagio { get; set; }
            public Pag pag { get; set; }
            public Intermediario intermediario { get; set; }
            public bool ShouldSerializeintermediario()
            {
                return (intermediario.cnpj != null && intermediario.cpf != null);
            }
            public Transporte transporte { get; set; }
        }

        public class RpsSubstituido
        {
            public string serie { get; set; }
            public int numero { get; set; }
            public string tipoRps { get; set; }
            public DateTime dataEmissao { get; set; }
        }

        public class Servico
        {
            public Servico()
            {
                valores = new Valores();
                detDeducoes = new DetDeducoes();
                endereco = new Endereco();
                fatura = new Fatura();
            }
            public string codigoNbs { get; set; }
            public string identifNaoExigibilidade { get; set; }
            public string itemListaServico { get; set; }
            public string codigoAtividade { get; set; }
            public string cnae { get; set; }
            public string codigoTributacaoMunicipio { get; set; }
            public Array discriminacao { get; set; }
            public string codigoMunicipioIncidencia { get; set; }
            public double quantidade { get; set; }
            public double valorUnitario { get; set; }
            public Valores valores { get; set; }
            public string tributavel { get; set; }
            public string codigoServico { get; set; }
            public DetDeducoes detDeducoes { get; set; } = null;// Verificar Funcionalidade do Campo - ToDo:
            public string codigoPais { get; set; }
            public string numeroProcesso { get; set; }
            public string dataVencimento { get; set; }
            public string incentivoFiscal { get; set; }
            public bool contraApresentacao { get; set; }
            public string unidade { get; set; }
            public string localServico { get; set; }
            public Endereco endereco { get; set; }
            public bool ShouldSerializeendereco()
            {
                return (endereco.codigoMunicipio != "9999999");
            }
            public Fatura fatura { get; set; }
        }

        public class Tomador
        {
            public Tomador()
            {
                endereco = new Endereco();
                contato = new Contato();
            }

            [JsonProperty("cnpj")]
            public string cnpj { get; set; }
            [JsonProperty("cpf")]
            public string cpf { get; set; }
            public string nif { get; set; }
            public string tipo { get; set; }
            public string docEstrangeiro { get; set; }
            public string razaoSocial { get; set; }
            public bool extrangeiro { get; set; }
            public string nomeFantasia { get; set; }
            public string inscricaoEstadual { get; set; }
            public string inscricaoMunicipal { get; set; }
            public Endereco endereco { get; set; }
            public Contato contato { get; set; }
        }

        public class Transporte
        {
            public string razaotransportadora { get; set; }
            public string cpfcnpjtransportadora { get; set; }
            public string enderecotransportadora { get; set; }
            public string tipofrete { get; set; }
            public string quantidade { get; set; }
            public string especie { get; set; }
            public string pesoliquido { get; set; }
            public string pesobruto { get; set; }
        }

        public class Valores
        {
            public Valores()
            {
                iss = new Iss();
                pis = new Pis();
                cofins = new Cofins();
                inss = new Inss();
                ir = new Ir();
                csll = new Csll();
            }
            public double totalServicos { get; set; }
            public double totalDeducoes { get; set; }
            public double descontoCondicionado { get; set; }
            public double descontoIncondicionado { get; set; }
            public double outrasRetencoes { get; set; }
            public double desconto { get; set; }
            public double valorRepasse { get; set; }
            public Iss iss { get; set; }
            public Pis pis { get; set; }
            public Cofins cofins { get; set; }
            public Inss inss { get; set; }
            public Ir ir { get; set; }
            public Csll csll { get; set; }
        }

        public class EmitRequestInput
        {
            public EmitRequestInput()
            {
                rps = new Rps();
            }
            public Rps rps { get; set; }

            [JsonProperty("emails")]
            public List<string> emails { get; set; }

            public string branchId { get; set; }
            public string erpId { get; set; }
            public bool isForceProductionEnv { get; set; }
            public string numeroLote { get; set; }
            public string _id { get; set; }
        }      

    }
}

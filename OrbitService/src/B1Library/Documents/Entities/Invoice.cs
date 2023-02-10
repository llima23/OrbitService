using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace B1Library.Documents
{
    public class Invoice
    {

        public Invoice()
        {
            CabecalhoLinha = new List<CabecalhoLinha>();
            Parceiro = new Parceiro();
            Filial = new Filial();
            Identificacao = new Identificacao();
            Duplicata = new List<Duplicata>();
            DocRef = new List<DocRef>();
            Emails = new List<Emails>();
            Transportadora = new Transportadora();
        }
        public string DownloadAutomatico { get; set; }
        public string CaminhoPDF { get; set; }
        public string CaminhoXML { get; set; }
        public int DocEntry { get; set; }
        public string CodInt { get; set; }
        public string BranchId { get; set; }
        public string ModeloDocumento { get; set; }
        public int CargaFiscal { get; set; }
        public int ObjetoB1 { get; set; }
        public string CANCELED { get; set; }
        public int BPLId { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoNF { get; set; }
        public string ChaveDeAcessoNFe { get; set; }
        public string ProtocoloNFe { get; set; }
        public int BaseEntry { get; set; }
        public string Justificativa { get; set; }
        public string IdRetornoOrbit { get; set; }

        public DateTime DataDocumento { get; set; }
        public List<Emails> Emails { get; set; }
        public List<Duplicata> Duplicata { get; set; }
        public List<DocRef> DocRef { get; set; }
        public List<CabecalhoLinha> CabecalhoLinha { get; set; }
        public Parceiro Parceiro { get; set; }
        public Filial Filial { get; set; }
        public Identificacao Identificacao { get; set; }
        public Transportadora Transportadora { get; set; }

        public void AddItemLine(CabecalhoLinha itemLine)
        {
            if (itemLine == null)
            {
                throw new ArgumentException("itemLine argument should be a valid instance!");
            }
            if (itemLine.ImpostoLinha == null)
            {
                throw new ArgumentException("TaxLine argument is obligatory!");
            }
            if (itemLine.ImpostoRetidoLinha == null)
            {
                throw new ArgumentException("WithholdingLine argument is obligatory!");
            }
            this.CabecalhoLinha.Add(itemLine);
        }


    }

    public class Transportadora
    {
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string CardName { get; set; }
        public string Complemento { get; set; }
        public string InscEstadual { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string TipoLogradouro { get; set; }
        public string NomeMunicipio { get; set; }
        public string UF { get; set; }
    }
    public class Emails
    {
        public string email { get; set; }
    }
    public class DocRef
    {
        public DateTime DataDocRef { get; set; }
        public string CnpjDocRef { get; set; }
        public string ChaveDocRef { get; set; }
        public string SerieDocRef { get; set; }
        public string NumNfDocRef { get; set; }
        public string CUfDocRef { get; set; }
        public string ModDocRef { get; set; }
    }

    public class CabecalhoLinha
    {
        public CabecalhoLinha()
        {
            ImpostoLinha = new List<ImpostoLinha>();
            ImpostoRetidoLinha = new List<ImpostoRetidoLinha>();
            DespesaAdicional = new List<DespesaAdicional>();
            DadosDI = new List<DadosDI>();
        }
        public string Text { get; set; }
        public string CodigoBeneficioFiscal { get; set; }
        public string CodigoAtividade { get; set; }
        public string CodigoServicoEntrada { get; set; }
        public int DocEntry { get; set; }
        public string NumeroItemPedido { get; set; }
        public string NumeroPedido { get; set; }
        public string SoImposto { get; set; }
        public string NItem { get; set; }
        public string CSTCofinsLinha { get; set; }
        public string CSTICMSLinha { get; set; }
        public string CSTIPILinha { get; set; }
        public string CSTPisLinha { get; set; }
        public string CodigoDeBarras { get; set; }
        public string CodigoItem { get; set; }
        public string CodigoNCM { get; set; }
        public string CodigoServicoLinha { get; set; }
        public string DescricaoItemLinhaDocumento { get; set; }
        public string IdLocalDestino { get; set; }
        public string CodigoCEST { get; set; }
        public List<ImpostoLinha> ImpostoLinha { get; set; }
        public List<ImpostoRetidoLinha> ImpostoRetidoLinha { get; set; }
        public List<DespesaAdicional> DespesaAdicional { get; set; }
        public List<DadosDI> DadosDI { get; set; }
        public int ItemLinhaDocumento { get; set; }
        public string OrigICMS { get; set; }
        public double ValorTotalDescontoLinha { get; set; }
        public double PorcentagemIBPTLinha { get; set; }
        public double QuantidadeLinha { get; set; }
        public string SimOuNaoRetidoLinha { get; set; }
        public string UnidadeComercial { get; set; }
        public double ValorTotalLinnha { get; set; }
        public double ValorUnitarioLinha { get; set; }
        public string CodigoCFOP { get; set; }
        public string cEnq { get; set; }
        public string CodigoTributacaoMuncipio { get; set; }
        public string ItemListaServico { get; set; }
        public string MotivoDesoneracao { get; set; }

        public int BaseEntry { get; set; }//Criar campo na View
    }

    public class DadosDI
    {
        public string CNPJAdiquirente { get; set; }
        public DateTime DataDesembaracoAduaneiro { get; set; }
        public DateTime Ddi { get; set; }
        public string LocalDesembaracoAduaneiro { get; set; }
        public string NumeroAdicao { get; set; }
        public string NumeroDocImportacao { get; set; }
        public string NumeroDrawBack { get; set; }
        public string NumeroSequenciaAdicao { get; set; }
        public string TpIntermedio { get; set; }
        public string UFAdiquirente { get; set; }
        public string UFDesembaracoAduaneiro { get; set; }
        public double ValorDescontoDI { get; set; }
        public string ValorFrmm { get; set; }
        public string ViaTransporteInternacional { get; set; }
    }

    public class DespesaAdicional
    {
        public string TipoDespesa { get; set; }
        public double ValorUnitarioDespesa { get; set; }
    }

    public class Duplicata
    {
        public DateTime DataVencimento { get; set; }
        public int NumeroDuplicata { get; set; }
        public double ValorDuplicata { get; set; }
    }


    public class Parceiro
    {
        public string IdEstrangeiro { get; set; }
        public string InscSuframa { get; set; }
        public string BairroParceiro { get; set; }
        public string CEPParceiro { get; set; }
        public string CidadeParceiro { get; set; }
        public string CnpjParceiro { get; set; }
        public string CodigoParceiro { get; set; }
        public string CodigoIBGEMunicipioParceiro { get; set; }
        public string CodigoPaisParceiro { get; set; }
        public string CodigoUFParceiro { get; set; }
        public string ComplementoParceiro { get; set; }
        public string CpfParceiro { get; set; }
        public string EmailParceiro { get; set; }
        public string EnderecoParceiro { get; set; }
        public string FoneParceiro { get; set; }
        public string IndicadorIEParceiro { get; set; }
        public string InscIeParceiro { get; set; }
        public string InscMunParceiro { get; set; }
        public string LogradouroParceiro { get; set; }
        public string MunicipioParceiro { get; set; }
        public string NomePaisParceiro { get; set; }
        public string NumeroLogradouroParceiro { get; set; }
        public string RazaoSocialParceiro { get; set; }
        public string TipoLogradouroParceiro { get; set; }
        public string UFParceiro { get; set; }
        public string ModalidadeFrete { get; set; }


    }

    public class Filial
    {
        public string AdressTypeFilial { get; set; }
        public string BairroFilial { get; set; }
        public string CEPFilial { get; set; }
        public string CNPJFilial { get; set; }
        public string CPFFilial { get; set; }
        public string CidadeFilial { get; set; }
        public string CodigoIBGEMunicipioFilial { get; set; }
        public string CodigoPaisFilial { get; set; }
        public string CodigoUFFilial { get; set; }
        public string ComplementoFilial { get; set; }
        public string InscIeFilial { get; set; }
        public string LogradouroFilial { get; set; }
        public string MunicipioFilial { get; set; }
        public string NomePaisFilial { get; set; }
        public string NumeroLogradouroFilial { get; set; }
        public string RazaoSocialFilial { get; set; }
        public int RegimeTributacaoFilial { get; set; }
        public string UFFilial { get; set; }
        public string IndicadorIEFilial { get; set; }
    }

    public class Identificacao
    {
        public double DescontoTotal { get; set; }
        public string Carrier { get; set; }
        public string PlacaVeiculo { get; set; }
        public string EstadoVeiculo { get; set; }
        public string EnviaEmail { get; set; }
        public string MarcaVolume { get; set; }
        public string DescricaoVolume { get; set; }
        public double QuantidadeVolume { get; set; }
        public double NumeroVolume { get; set; }
        public double PesoBruto { get; set; }
        public double PesoLiquido { get; set; }
        public string DescricaoFormaDePagamento { get; set; }
        public string ObsFiscoCampo { get; set; }
        public string ObsFiscoTexto { get; set; }
        public string ObsAbertura { get; set; }
        public string JustContigencia { get; set; }
        public string LocalDeExportacao { get; set; }
        public string UFDeExportacao { get; set; }
        public string HoraDeEnvio { get; set; }
        public string EnviaDataHora { get; set; }
        public DateTime DataDeEnvio { get; set; }
        public string InfAdFisco { get; set; }
        public string RegEspTrib { get; set; }
        public string TipoRps { get; set; }
        public string BranchId { get; set; }
        public string CondicaoDePagamentoDocumento { get; set; }
        public string ConsumidorFinal { get; set; }
        public double DadosCobranca { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataLancamento { get; set; }
        public string DocTime { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string DocumentoCancelado { get; set; }
        public string FinalideDocumento { get; set; }
        public string FormaDePagamentoDocumento { get; set; }
        public string IdRetornoOrbit { get; set; }
        public int IndicadorIntermediario { get; set; }
        public string IndicadorPresenca { get; set; }
        public string Key { get; set; }
        public string NaturezaOperacaoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string OperacaoNFe { get; set; }
        public string SerieDocumento { get; set; }
        public string StatusOrbit { get; set; }
        public string TipoOperacaoDocumento { get; set; }
        public string TipoTributacaoNFSe { get; set; }
        public double ValorTotalNF { get; set; }
        public string Versao { get; set; }
        public string NumeroProtocolo { get; set; }
        public string IncentivadorCultural { get; set; }
        public string Justificativa { get; set; }
        public double ValorDesconto { get; set; }
        public double ValorLiquido { get; set; } // CRIAR NA VIEW
    }

    public class ImpostoLinha
    {
        public double BaseIsenta { get; set; }
        public double BaseDeCalculo { get; set; }
        public double BaseOutras { get; set; }
        public double ValorIcmsDesonerado { get; set; } = 0.00;
        public double pRedBc { get; set; }
        public double PDif { get; set; } 
        public double VDif { get; set; }
        public double VICMSOp { get; set; }
        public string NomeImposto { get; set; }
        public double PorcentagemImposto { get; set; }
        public string TipoImpostoOrbit { get; set; }
        public double ValorBaseImposto { get; set; }
        public double ValorImposto { get; set; }
        public string SimOuNaoDesoneracao { get; set; } = "N";
        public double MVast { get; set; } //TODO : Colocar na View U_lucro
        public double AliquotaIntDestino { get; set; }
        public double PartilhaInterestadual { get; set; }    
    }

    public class ImpostoRetidoLinha
    {
        public double PorcentagemImpostoRetido { get; set; }
        public string TipoImpostoOrbit { get; set; }
        public double ValorImpostoRetido { get; set; }
        public double TaxbleAmnt { get; set;}
    }
}
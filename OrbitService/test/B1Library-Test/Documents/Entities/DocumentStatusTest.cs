using B1Library.Documents;
using Xunit;

namespace B1Library_Tests.Documents
{
    public class DocumentStatusTest
    {
        private DocumentStatus cut;
        [Fact]
        public void ShouldCreateInstanceWithParameters()
        {
            string IdOrbit = "";
            string DescricaoErro = "";
            string StatusOrbit = "";
            int ObjetoB1 = 0;
            int docEntry = 12345678;
            cut = new DocumentStatus(IdOrbit,StatusOrbit,DescricaoErro,ObjetoB1,docEntry, StatusCode.Erro);
            Assert.Equal(docEntry, cut.DocEntry);
            Assert.Equal((int)StatusCode.Erro, (int)cut.Status);
        }

        [Fact]
        public void ShouldReturnCorrectStatusMessageSucess()
        {
            string IdOrbit = "";
            string DescricaoErro = "";
            string StatusOrbit = "";
            int ObjetoB1 = 0;
            int docEntry = 12345678;

            cut = new DocumentStatus(IdOrbit, StatusOrbit, DescricaoErro, ObjetoB1, docEntry, StatusCode.CargaFiscal);
            string teste = cut.GetStatusMessage();
            Assert.Equal("Carga fiscal efetuada", cut.GetStatusMessage());
        }

        [Fact]
        public void ShouldReturnCorrectStatusMessageErro()
        {
            string IdOrbit = "";
            string DescricaoErro = "descricaoErro";
            string StatusOrbit = "";
            int ObjetoB1 = 0;
            int docEntry = 12345678;

            cut = new DocumentStatus(IdOrbit, StatusOrbit, DescricaoErro, ObjetoB1, docEntry, StatusCode.Erro);
            string teste = cut.GetStatusMessage();
            Assert.Equal("descricaoErro", cut.GetStatusMessage());
        }
    }
}

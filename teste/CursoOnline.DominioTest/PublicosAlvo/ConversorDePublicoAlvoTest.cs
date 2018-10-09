using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest.Util;
using Xunit;

namespace CursoOnline.DominioTest.PublicosAlvo
{
    public class ConversorDePublicoAlvoTest
    {
        private readonly ConversorDePublicoAlvo _conversor = new ConversorDePublicoAlvo();
       
        [Theory]
        [InlineData(PublicoAlvo.Empreendedor, "Empreendedor")]
        [InlineData(PublicoAlvo.Empregado, "Empregado")]
        [InlineData(PublicoAlvo.Estudante, "Estudante")]
        [InlineData(PublicoAlvo.Universitario, "Universitario")]
        public void DeveConverterPublicoAlvo(PublicoAlvo publicoAlvoEsperado, string publicoAlvoStr)
        {            
            var publicoAlvoConvertido = _conversor.Converter(publicoAlvoStr);

            Assert.Equal(publicoAlvoEsperado, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterPublicoAlvoInvalido()
        {
            const string publicoAlvoInvalido = "Medico";

            Assert.Throws<ExcecaoDeDominio>(() =>
                _conversor.Converter(publicoAlvoInvalido))
                .ComMensagem(Resource.PublicoAlvoInvalido);
        }
    }
}

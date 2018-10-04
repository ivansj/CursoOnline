using Bogus;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    /// <summary>
    /// Teste de Curso
    /// </summary>
    public class CursoTeste 
    {        
        private readonly Faker _faker;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTeste()
        {
            _faker = new Faker();

            _nome = _faker.Random.Word();
            _cargaHoraria = _faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = _faker.Random.Double(100, 1000);
            _descricao = _faker.Lorem.Paragraph();
        }


        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, 
                cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
        {           
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(Resource.NomeInvalido);            
        }

        [Theory]
        [InlineData(0.9)]
        [InlineData(0)]
        [InlineData(-100)]
        public void NaoDeveCursoTerCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {            
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComCargaHorario(cargaHorariaInvalida).Build())
                .ComMensagem(Resource.CargaHorariaInvalida);            
        }

        [Theory]
        [InlineData(0)]        
        [InlineData(-10.2)]
        [InlineData(-66)]
        public void NaoDeveCursoTerValorMenorOuIgualAZero(double valrInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComValor(valrInvalido).Build())
                .ComMensagem(Resource.ValorInvalido);            
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _faker.Commerce.Department();
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, curso.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
                curso.AlterarNome(nomeInvalido))
                .ComMensagem(Resource.NomeInvalido);
        }

        [Fact]
        public void DeveAlterarCargaHorario()
        {
            var cargaHorariaEsperada = _faker.Random.Double(10, 1200);
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarCargaHoraria(cargaHorariaEsperada);

            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }

        [Theory]
        [InlineData(0.9)]
        [InlineData(0)]
        [InlineData(-100)]
        public void NaoDeveAlterarComCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
                curso.AlterarCargaHoraria(cargaHorariaInvalida))
                .ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Fact]
        public void DeveAlterarValor()
        {
            var valorEsperado = _faker.Random.Double(50, 10000);
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarValor(valorEsperado);

            Assert.Equal(valorEsperado, curso.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10.2)]
        [InlineData(-66)]
        public void NaoAlteraValorInvalido(double valorInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
                curso.AlterarValor(valorInvalido))
                .ComMensagem(Resource.ValorInvalido);
        }
    }    
}

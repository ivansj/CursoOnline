using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
    /// <summary>
    /// Teste de Curso
    /// </summary>
    public class CursoTeste : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTeste(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Contrutor sendo executado.");

            var faker = new Faker();

            _nome = faker.Random.Word();
            _cargaHoraria = faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(100, 1000);
            _descricao = faker.Lorem.Paragraph();
                        
            _output.WriteLine($"Decimal {faker.Random.Decimal()}");
            _output.WriteLine($"Company {faker.Company.CompanyName()}");
            _output.WriteLine($"Email {faker.Person.Email}");
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
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem("Nome inválido");            
        }

        [Theory]
        [InlineData(0.9)]
        [InlineData(0)]
        [InlineData(-100)]
        public void NaoDeveCursoTerCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {            
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComCargaHorario(cargaHorariaInvalida).Build())
                .ComMensagem("Carga Horaria inválida");            
        }

        [Theory]
        [InlineData(0)]        
        [InlineData(-10.2)]
        [InlineData(-66)]
        public void NaoDeveCursoTerValorMenorOuIgualAZero(double valrInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComValor(valrInvalido).Build())
                .ComMensagem("Valor inválido");            
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado.");
        }
    }    
}

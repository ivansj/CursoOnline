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
        private readonly decimal _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly decimal _valor;

        public CursoTeste(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Contrutor sendo executado.");

            _nome = "Informática básica";
            _cargaHoraria = 80m;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = 950m;
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, 
                cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
        {           
            Assert.Throws<ArgumentException>(() =>
                new Curso(nomeInvalido, _cargaHoraria, _publicoAlvo,_valor))
                .ComMensagem("Nome inválido");            
        }

        [Theory]
        [InlineData(0.9)]
        [InlineData(0)]
        [InlineData(-100)]
        public void NaoDeveCursoTerCargaHorariaMenorQue1(decimal cargaHorariaInvalida)
        {            
            Assert.Throws<ArgumentException>(() =>
                new Curso(_nome, cargaHorariaInvalida, _publicoAlvo, _valor))
                .ComMensagem("Carga Horaria inválida");            
        }

        [Theory]
        [InlineData(0)]        
        [InlineData(-10.2)]
        [InlineData(-66)]
        public void NaoDeveCursoTerValorMenorOuIgualAZero(decimal valrInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                new Curso(_nome, _cargaHoraria, _publicoAlvo,
               valrInvalido)).ComMensagem("Valor inválido");            
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado.");
        }
    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }

    public class Curso
    {
        public string Nome { get; }
        public decimal CargaHoraria { get; }
        public PublicoAlvo PublicoAlvo { get; }
        public decimal Valor { get;  }

        public Curso(string nome, decimal cargaHoraria, PublicoAlvo publicoAlvo, decimal valor)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome inválido");

            if(cargaHoraria <= 1)
                throw new ArgumentException("Carga Horaria inválida");
            
            if (valor <= 0)
                throw new ArgumentException("Valor inválido");

            this.Nome = nome;
            this.CargaHoraria = cargaHoraria;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;                        
        }                
    }
}

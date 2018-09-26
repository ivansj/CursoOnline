using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTeste
    {
        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = 80m,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = 950m
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
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = 80m,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = 950m
            };

            var message = Assert.Throws<ArgumentException>(() =>
                new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, 
                cursoEsperado.Valor)).Message;

            Assert.Equal("Nome inválido", message);
        }

        [Theory]
        [InlineData(0.9)]
        [InlineData(0)]
        [InlineData(-100)]
        public void NaoDeveCursoTerCargaHorariaMenorQue1(decimal cargaHorariaInvalida)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = 80m,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = 950m
            };

            var message = Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo,
                cursoEsperado.Valor)).Message;
            Assert.Equal("Carga Horaria inválida", message);
        }

        [Theory]
        [InlineData(0)]        
        [InlineData(-10.2)]
        [InlineData(-66)]
        public void NaoDeveCursoTerValorMenorOuIgualAZero(decimal valrInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = 80m,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = 950m
            };

            var message = Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo,
               valrInvalido)).Message;
            Assert.Equal("Valor inválido", message);
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

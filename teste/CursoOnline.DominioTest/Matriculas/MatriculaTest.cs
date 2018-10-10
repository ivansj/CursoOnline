using Bogus;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Util;
using ExpectedObjects;
using MatriculaOnline.DominioTest.Builders;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class MatriculaTest
    {
        Faker _faker = new Faker();

        [Fact]
        public void DeveCriarMatricula()
        {
            var curso = CursoBuilder.Novo().Build(); 
            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };

            var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void NaoCriarDeveMatriculaSemAluno()
        {
            Aluno alunoInvalido = null;
            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build())
                .ComMensagem(Resource.AlunoInvalido);
        }

        [Fact]
        public void NaoCriarDeveMatriculaSemCurso()
        {
            Curso cursoInvalido = null;
            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build())
                .ComMensagem(Resource.CursoInvalido);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        [InlineData(0.99)]
        public void NaoCriarDeveMatriculaValorPagoInvalido(double valorPagoInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo().ComValorPago(valorPagoInvalido).Build())
                .ComMensagem(Resource.ValorPagoInvalido);
        }

        [Fact]
        public void NaoCriarDeveMatriculaValorPagoMaiorQueValorDoCurso()
        {
            var curso = CursoBuilder.Novo().Build();
            var valorPagoMaiorQueCurso = curso.Valor + 1;

            Assert.Throws<ExcecaoDeDominio>(() =>
                MatriculaBuilder.Novo()
                .ComCurso(curso)
                .ComValorPago(valorPagoMaiorQueCurso)
                .Build())
                .ComMensagem(Resource.ValorPagoMaiorQueCurso);
        }


        [Fact]
        public void DeveIndicarQueHouveDescontoNaMatricula()
        {
            var curso = CursoBuilder.Novo().ComValor(_faker.Random.Double(10, 99999)).Build();
            var vaalorPagoComDesconto = curso.Valor - 1.0;

            var matricula = MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(vaalorPagoComDesconto).Build();

            Assert.True(matricula.TemDesconto);
        }
    }

    
}

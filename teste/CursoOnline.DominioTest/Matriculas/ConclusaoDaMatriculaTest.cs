using Bogus;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest.Util;
using MatriculaOnline.DominioTest.Builders;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class ConclusaoDaMatriculaTest
    {
        Mock<IMatriculaRepositorio> _matriculaRepositorioMock;
        ConclusaoDaMatricula _conclusaoDaMatricula;
        Faker _faker;

        public ConclusaoDaMatriculaTest()
        {
            _faker = new Faker();
            _matriculaRepositorioMock = new Mock<IMatriculaRepositorio>();
            _conclusaoDaMatricula = new ConclusaoDaMatricula(_matriculaRepositorioMock.Object);
        }

        [Fact]
        public void DeveInformarNotaDoAluno()
        {

            var matricula = MatriculaBuilder.Novo().Build();
            var notaEsperada = _faker.Random.Double(0, 10);

            _matriculaRepositorioMock.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);

            _conclusaoDaMatricula.Concluir(matricula.Id, notaEsperada);

            Assert.Equal(notaEsperada, matricula.NotaDoAluno);
        }

        [Fact]
        public void DevoNotificarQuandoAlunoNaoForEncontrado()
        {
            Matricula matriculaInvalida = null;
            var matriculaIdInvalida = _faker.Random.Int(1, 9999999);
            var notaAluno = _faker.Random.Double(0, 10);
            _matriculaRepositorioMock.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);


            Assert.Throws<ExcecaoDeDominio>(() =>
                _conclusaoDaMatricula.Concluir(matriculaIdInvalida, notaAluno))
                .ComMensagem(Resource.MatriculaNaoEncontrada);
        }
    }
}

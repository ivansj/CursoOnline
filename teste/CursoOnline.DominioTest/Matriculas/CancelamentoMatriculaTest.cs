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
    public class CancelamentoMatriculaTest
    {
        Mock<IMatriculaRepositorio> _matriculaRepositorioMock;
        CancelamentoDaMatricula _cancelamentoDaMatricula;

        public CancelamentoMatriculaTest()
        {
            _matriculaRepositorioMock = new Mock<IMatriculaRepositorio>();
            _cancelamentoDaMatricula = new CancelamentoDaMatricula(_matriculaRepositorioMock.Object);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();

            _matriculaRepositorioMock.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);

            _cancelamentoDaMatricula.Cancelar(matricula.Id);

            Assert.True(matricula.Cancelada);
        }

        [Fact]
        public void DevoNotificarQuandoAlunoNaoForEncontrado()
        {
            Faker faker = new Faker();
            Matricula matriculaInvalida = null;
            var matriculaIdInvalida = faker.Random.Int(1, 9999999);

            _matriculaRepositorioMock.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            Assert.Throws<ExcecaoDeDominio>(() =>
                _cancelamentoDaMatricula.Cancelar(matriculaIdInvalida))
                .ComMensagem(Resource.MatriculaNaoEncontrada);
        }
    }


}

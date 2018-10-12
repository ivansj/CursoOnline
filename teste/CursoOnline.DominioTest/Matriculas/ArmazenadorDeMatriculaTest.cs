using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest.Builders;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class ArmazenadorDeMatriculaTest
    {
        private MatriculaDto _matriculaDto;
        private Mock<IMatriculaRepositorio> _matriculaRepositorioMock;
        private ArmazenadorDeMatricula _armazenadorDeMatricula;

        public ArmazenadorDeMatriculaTest()
        {
            var curso = CursoBuilder.Novo().Build();
            _matriculaDto = new MatriculaDto
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };

            _matriculaRepositorioMock = new Mock<IMatriculaRepositorio>();
            _armazenadorDeMatricula = new ArmazenadorDeMatricula(_matriculaRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            _armazenadorDeMatricula.Armazenar(_matriculaDto);

            _matriculaRepositorioMock.Verify(r =>
                r.Adicionar(It.Is<Matricula>(
                    a => string.Equals(a.Aluno, _matriculaDto.Aluno) &&
                         string.Equals(a.Curso, _matriculaDto.Curso)))
                );
        }
    }
}

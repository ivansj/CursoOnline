using Bogus;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class ArmazenadorDeMatriculaTest
    {
        private MatriculaDto _matriculaDto;
        private Mock<ICursoRepositorio> _cursoRepositorioMock;
        private Mock<IAlunoRepositorio> _alunoRepositorioMock;
        private Mock<IMatriculaRepositorio> _matriculaRepositorioMock;
        private ArmazenadorDeMatricula _armazenadorDeMatricula;
        private Curso _curso;
        private Aluno _aluno;

        public ArmazenadorDeMatriculaTest()
        {
            var faker = new Faker();

            _curso = CursoBuilder.Novo().ComId(faker.Random.Int(1)).Build();
            _aluno = AlunoBuilder.Novo().ComId(faker.Random.Int(1)).Build();

            _matriculaDto = new MatriculaDto
            {
                AlunoId = _aluno.Id,
                CursoId = _curso.Id,
                ValorPago = _curso.Valor
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
            _matriculaRepositorioMock = new Mock<IMatriculaRepositorio>();
            _armazenadorDeMatricula = new ArmazenadorDeMatricula(_alunoRepositorioMock.Object, _cursoRepositorioMock.Object, _matriculaRepositorioMock.Object);

            _cursoRepositorioMock.Setup(r => r.ObterPorId(_curso.Id)).Returns(_curso);
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);
        }


        //[Fact]
        //public void NaoDeveTerCursoComPublicoAlvoDiferenteDoAluno()
        //{
        //    var curso = CursoBuilder.Novo().ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Estudante).Build();
        //    _cursoRepositorioMock.Setup(r => r.ObterPorId(curso.Id)).Returns(curso);
        //    var aluno = AlunoBuilder.Novo().ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Empregado).Build();
        //    _alunoRepositorioMock.Setup(r => r.ObterPorId(aluno.Id)).Returns(aluno);

        //    var matriculaDto = new MatriculaDto
        //    {
        //        AlunoId = aluno.Id,
        //        CursoId = curso.Id, 
        //        ValorPago = curso.Valor
        //    };


        //    Assert.Throws<ExcecaoDeDominio>(() =>
        //      _armazenadorDeMatricula.Armazenar(matriculaDto))
        //       .ComMensagem(Resource.PublicoAlvoCursoDiferenteDoAluno);
        //}

        [Fact]
        public void DeveAdicionarMatricula()
        {
            _armazenadorDeMatricula.Armazenar(_matriculaDto);

            _matriculaRepositorioMock.Verify(r =>
                r.Adicionar(It.Is<Matricula>(
                    a => a.Aluno == _aluno &&
                         a.Curso == _curso))
                );
        }

        [Fact]
        public void DevoNotificarQuandoCursoNaoForEncontrado()
        {
            Curso cursoInvalido = null;
            _cursoRepositorioMock.Setup(r => r.ObterPorId(_matriculaDto.CursoId)).Returns(cursoInvalido);
                      

            Assert.Throws<ExcecaoDeDominio>(() =>
                _armazenadorDeMatricula.Armazenar(_matriculaDto))
                .ComMensagem(Resource.CursoNaoEncontrado);
        }

        [Fact]
        public void DevoNotificarQuandoAlunoNaoForEncontrado()
        {
            Aluno alunoInvalido = null;
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_matriculaDto.AlunoId)).Returns(alunoInvalido);


            Assert.Throws<ExcecaoDeDominio>(() =>
                _armazenadorDeMatricula.Armazenar(_matriculaDto))
                .ComMensagem(Resource.AlunoNaoEncontrado);
        }
    }
}

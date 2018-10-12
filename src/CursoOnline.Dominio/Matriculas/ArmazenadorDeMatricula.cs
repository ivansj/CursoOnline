using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class ArmazenadorDeMatricula
    {
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IMatriculaRepositorio _matriculaRepositorio;

        public ArmazenadorDeMatricula(IAlunoRepositorio alunoRepositorio, ICursoRepositorio cursoRepositorio, IMatriculaRepositorio matriculaRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
            _cursoRepositorio = cursoRepositorio;
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Armazenar(MatriculaDto matriculaDto)
        {
            var aluno = _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);
            var curso = _cursoRepositorio.ObterPorId(matriculaDto.CursoId);


            ValidadorDeRegra.Novo()
             //.Quando(curso != null && aluno != null && curso.PublicoAlvo != aluno.PublicoAlvo, Resource.PublicoAlvoCursoDiferenteDoAluno)
             .Quando(curso == null, Resource.CursoNaoEncontrado)
             .Quando(aluno == null, Resource.AlunoNaoEncontrado)
             .DisperarExcecaoseExistir();


            var matricula = new Matricula(aluno, curso, matriculaDto.ValorPago);

            _matriculaRepositorio.Adicionar(matricula);
        }

    }
}

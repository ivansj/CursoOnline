using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class ConclusaoDaMatricula
    {
        private IMatriculaRepositorio _matriculaRepositorio;

        public ConclusaoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            this._matriculaRepositorio = matriculaRepositorio;
        }

        public void Concluir(int matriculaId, double notaEsperada)
        {
            var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

            ValidadorDeRegra.Novo()
             .Quando(matricula == null, Resource.MatriculaNaoEncontrada)
             .DisperarExcecaoseExistir();

            matricula.InformarNota(notaEsperada);
        }
    }
}

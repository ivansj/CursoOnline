using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class CancelamentoDaMatricula
    {
        private IMatriculaRepositorio _matriculaRepositorio;

        public CancelamentoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Cancelar(int matriculaId)
        {
            var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

            ValidadorDeRegra.Novo()
             .Quando(matricula == null, Resource.MatriculaNaoEncontrada)
             .DisperarExcecaoseExistir();

            matricula.Cancelar();
        }
    }
}

using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class ArmazenadorDeMatricula
    {
        private readonly IMatriculaRepositorio _matriculaRepositorio;

        public ArmazenadorDeMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Armazenar(MatriculaDto matriculaDto)
        {
            var matricula = new Matricula(matriculaDto.Aluno, matriculaDto.Curso, matriculaDto.ValorPago);

            _matriculaRepositorio.Adicionar(matricula);
        }
    }
}

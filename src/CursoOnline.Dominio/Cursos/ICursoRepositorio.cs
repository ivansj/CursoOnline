namespace CursoOnline.Dominio.Cursos
{
    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
        Curso OBterPeloNome(string nome);
    }
}

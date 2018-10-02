using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Cursos;
using System.Linq;

namespace CursoOnline.Dados.Repositorios
{
    public class CursoRepositorio : RepositorioBase<Curso>, ICursoRepositorio
    {
        public CursoRepositorio(ApplicationDbContext context) : base(context)
        {
        }

        public void Atualizar(Curso curso)
        {
            //throw new System.NotImplementedException();
        }

        public Curso ObterPeloNome(string nome)
        {
            //var entidade = Context.Set<Curso>().Where(c => c.Nome.Contains(nome));
            //if (entidade.Any())
            //    return entidade.First();
            //return null;
            return Context.Set<Curso>().FirstOrDefault(c => c.Nome.Equals(nome));
        }      
    }
}

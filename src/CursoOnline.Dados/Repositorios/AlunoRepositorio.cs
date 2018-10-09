using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Alunos;
using System.Linq;

namespace CursoOnline.Dados.Repositorios
{
    public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
    {
        public AlunoRepositorio(ApplicationDbContext context) : base(context)
        {
        }

        public Aluno ObterPeloCpf(string cpf)
        {
            return Context.Set<Aluno>().FirstOrDefault(c => c.Cpf.Equals(cpf));
        }
    
    }
}

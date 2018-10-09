using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.DominioTest.Builders
{
    public class AlunoBuilder
    {
        private string _nome = "Joao das Neves";
        private  string _cpf = "62722131072";
        private  string _email = "joaodasneves@gmail.com.br";
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;

        public static AlunoBuilder Novo()
        {
            return new AlunoBuilder();
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AlunoBuilder ComCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public Aluno Build()
        {
            var aluno = new Aluno(_nome, _cpf, _email, _publicoAlvo);
            return aluno;
        }
    }
}

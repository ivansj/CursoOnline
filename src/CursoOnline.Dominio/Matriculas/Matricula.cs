using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class Matricula : Entidade
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public double ValorPago { get; private set; }
        public bool TemDesconto { get; private set; }

        public Matricula(Aluno aluno, Curso curso, double valorPago)
        {
            ValidadorDeRegra.Novo()
               .Quando(aluno == null, Resource.AlunoInvalido)
               .Quando(curso == null, Resource.CursoInvalido)
               .Quando(valorPago < 1, Resource.ValorPagoInvalido)
               .Quando(curso != null && valorPago > curso.Valor, Resource.ValorPagoMaiorQueCurso)
               .Quando(curso != null && aluno != null && curso.PublicoAlvo != aluno.PublicoAlvo, Resource.PublicoAlvoCursoDiferenteDoAluno)
               .DisperarExcecaoseExistir();

            this.Aluno = aluno;
            this.Curso = curso;
            this.ValorPago = valorPago;
            this.TemDesconto = valorPago < curso.Valor;
        }
    }
}

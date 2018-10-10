using Bogus;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest.Builders;

namespace MatriculaOnline.DominioTest.Builders
{
    public class MatriculaBuilder
    {
        protected Aluno _aluno;
        protected Curso _curso;
        protected double _valorPago;
        

        public static MatriculaBuilder Novo()
        {
            var faker = new Faker();
            var valor = faker.Random.Double(1, 99999);
            return new MatriculaBuilder
            {
                _aluno = AlunoBuilder.Novo().Build(),
                _curso = CursoBuilder.Novo().ComValor(valor).Build(),
                _valorPago = valor
            };
        }

       
        public MatriculaBuilder ComAluno(Aluno aluno)
        {
            _aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComCurso(Curso curso)
        {
            _curso = curso;
            return this;
        }

        public MatriculaBuilder ComValorPago(double valorPago)
        {
            _valorPago = valorPago;
            return this;
        }

        //public MatriculaBuilder ComId(int id)
        //{
        //    _id = id;
        //    return this;
        //}

        public Matricula Build()
        {
            var Matricula = new Matricula(_aluno, _curso, _valorPago);

            //if (_id > 0)
            //{
            //    var propertyInfo = Matricula.GetType().GetProperty("Id");
            //    propertyInfo.SetValue(Matricula, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            //}
            return Matricula;
        }
        
    }
}

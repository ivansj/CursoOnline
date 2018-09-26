using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest.Cursos;

namespace CursoOnline.DominioTest.Builders
{
    public class CursoBuilder
    {
        private string _nome = "Informática básica";
        private decimal _cargaHoraria = 80m;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private decimal _valor = 950m;
        private string _descricao = "Uma descrição";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHorario(decimal cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public Curso Build()
        {
            return new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
        }
    }
}

using CursoOnline.Dominio.Cursos;
using System;

namespace CursoOnline.DominioTest.Builders
{
    public class CursoBuilder
    {
        private string _nome = "Informática básica";
        private double _cargaHoraria = 80.00;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private double _valor = 950.00;
        private string _descricao = "Uma descrição";
        private int _id;

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

        public CursoBuilder ComCargaHorario(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public Curso Build()
        {
            var curso = new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);

            if (_id > 0)
            {
                var propertyInfo = curso.GetType().GetProperty("Id");
                propertyInfo.SetValue(curso, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }
            return curso;
        }
        
    }
}

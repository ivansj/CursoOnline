using CursoOnline.Dominio.Base;
using System;

namespace CursoOnline.Dominio.Cursos
{

    public class Curso : Entidade
    {
        public string Nome { get; }
        public string Descricao { get; }
        public decimal CargaHoraria { get; }
        public PublicoAlvo PublicoAlvo { get; }
        public decimal Valor { get;  }

        public Curso(string nome, string descricao, decimal cargaHoraria, PublicoAlvo publicoAlvo, decimal valor)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome inválido");

            if(cargaHoraria <= 1)
                throw new ArgumentException("Carga Horaria inválida");
            
            if (valor <= 0)
                throw new ArgumentException("Valor inválido");

            this.Nome = nome;
            this.Descricao = descricao;
            this.CargaHoraria = cargaHoraria;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;                        
        }                
    }
}

using CursoOnline.Dominio.Base;
using System;

namespace CursoOnline.Dominio.Cursos
{

    public class Curso : Entidade
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        private Curso()
        {

        }

        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrWhiteSpace(nome), "Nome inválido")
                .Quando(cargaHoraria <= 1, "Carga Horaria inválida")
                .Quando(valor <= 0, "Valor inválido")
                .DisperarExcecaoseExistir();           

            this.Nome = nome;
            this.Descricao = descricao;
            this.CargaHoraria = cargaHoraria;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;                        
        }                
    }
}

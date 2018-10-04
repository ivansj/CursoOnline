using CursoOnline.Dominio.Base;

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
                .Quando(string.IsNullOrWhiteSpace(nome), Resource.NomeInvalido)
                .Quando(cargaHoraria <= 1, Resource.CargaHorariaInvalida)
                .Quando(valor <= 0, Resource.ValorInvalido)
                .DisperarExcecaoseExistir();           

            this.Nome = nome;
            this.Descricao = descricao;
            this.CargaHoraria = cargaHoraria;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;                        
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
              .Quando(string.IsNullOrWhiteSpace(nome), Resource.NomeInvalido)              
              .DisperarExcecaoseExistir();

            this.Nome = nome;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            ValidadorDeRegra.Novo()              
              .Quando(cargaHoraria <= 1, Resource.CargaHorariaInvalida)              
              .DisperarExcecaoseExistir();

            this.CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(double valor)
        {
            ValidadorDeRegra.Novo()             
             .Quando(valor <= 0, Resource.ValorInvalido)
             .DisperarExcecaoseExistir();

            this.Valor = valor;
        }
    }
}

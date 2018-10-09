using System;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Alunos
{
    public class Aluno
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }

        public Aluno(string nome, string cpf, string email, PublicoAlvo publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrWhiteSpace(nome), Resource.NomeInvalido)
                .Quando(!ValidadorCpf.Validar(cpf), Resource.CPFInvalido)
                .Quando(!ValidadorEmail.Validar(email), Resource.EmailInvalido)
                .DisperarExcecaoseExistir();


            Nome = nome;
            Cpf = cpf;
            Email = email;
            PublicoAlvo = publicoAlvo;
        }

        public void AlterarNome(string nome)
        {

            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrWhiteSpace(nome), Resource.NomeInvalido)
                .DisperarExcecaoseExistir();

            Nome = nome;
        }
    }
}

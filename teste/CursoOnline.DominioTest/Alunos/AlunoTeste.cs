using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class AlunoTeste
    {
        private readonly Faker _faker;
        private readonly string _nome;
        private readonly string _cpf;
        private readonly string _email;
        private readonly PublicoAlvo _publicoAlvo;

        public AlunoTeste()
        {
            _faker = new Faker();

            _nome = _faker.Person.FullName;
            _cpf = _faker.Person.Cpf();
            _publicoAlvo = PublicoAlvo.Estudante;
            _email = _faker.Person.Email;
        }

        [Fact]
        public void DeveCriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = _nome,
                Cpf = _cpf,
                Email = _email,
                PublicoAlvo = _publicoAlvo
            };

            var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.Cpf, alunoEsperado.Email, alunoEsperado.PublicoAlvo);

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlunoTerNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData("0000000000")]
        [InlineData("999999999")]
        [InlineData("Invalido")]
        [InlineData(null)]
        public void NaoDeveAlunoTerCpfInvalido(string cpfInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComCpf(cpfInvalido).Build())
                .ComMensagem(Resource.CPFInvalido);
        }

        [Theory]
        [InlineData("email@.")]
        [InlineData("@com.br")]
        [InlineData(null)]
        [InlineData("")]
        public void NaoDeveAlunoTerEmailInvalido(string emailInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
                .ComMensagem(Resource.EmailInvalido);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _nome;
            var aluno = AlunoBuilder.Novo().Build();

            aluno.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, aluno.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var aluno = AlunoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
                aluno.AlterarNome(nomeInvalido))
                .ComMensagem(Resource.NomeInvalido);
        }
    }
}

using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class ArmazanadorDeAlunoTeste
    {
        Faker _faker;
        private AlunoDto _alunoDto;
        private Mock<IAlunoRepositorio> _alunoRepositorioMock;
        private ArmazenadorDeAluno _armazenadorDeAluno;

        public ArmazanadorDeAlunoTeste()
        {
            _faker = new Faker();
            _alunoDto = new AlunoDto
            {
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                Email = _faker.Person.Email,
                PublicoAlvo = "Estudante"
            };

            _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
            var conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorioMock.Object, conversorDePublicoAlvo.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorioMock.Verify(r =>
                r.Adicionar(It.Is<Aluno>(
                    a => string.Equals(a.Nome, _alunoDto.Nome) &&
                         string.Equals(a.Cpf, _alunoDto.Cpf)))
                );
        }

        //[Fact]
        //public void NaoDeveAdicionarPublicoAlvoInvalido()
        //{
        //    const string publicoAlvoInvalido = "Operario";
        //    _alunoDto.PublicoAlvo = publicoAlvoInvalido;

        //    Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDto))
        //        .ComMensagem(Resource.PublicoAlvoInvalido);
        //}

        [Fact]
        public void NaoDeveAdicionarAlunoComMesmoCpf()
        {
            var alunoJaSalvo = AlunoBuilder.Novo()
                .ComCpf(_alunoDto.Cpf)
                .ComId(_faker.Random.Int(0, 99999))
                .Build();

            _alunoRepositorioMock.Setup(r => r.ObterPeloCpf(_alunoDto.Cpf)).Returns(alunoJaSalvo);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDto))
                .ComMensagem(Resource.CPFExistente);
        }

        [Fact]
        public void DeveAlterarNomeAluno()
        {
            _alunoDto.Id = _faker.Random.Int(0, 99999999);

            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);


            Assert.Equal(_alunoDto.Nome, alunoJaSalvo.Nome);
        }

        [Fact]
        public void NaoDeveAlterarDemaisInformacoesAluno()
        {
            _alunoDto.Id = _faker.Random.Int(0, 99999999);

            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            var cpfEsperado = alunoJaSalvo.Cpf;

            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);


            Assert.Equal(cpfEsperado, alunoJaSalvo.Cpf);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoAlunoJaExiste()
        {
            _alunoDto.Id = _faker.Random.Int(0, 99999999);

            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }

    }
}

using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private CursoDto _cursoDto;
        private Mock<ICursoRepositorio> _cursoRepositorioMock;
        private ArmazenadorDeCurso _armazenadorDeCurso;                

        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Decimal(50, 100),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Decimal(100, 3000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {                
            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r => r
                .Adicionar(It
                .Is<Curso>(
                    c => string.Equals(c.Nome, _cursoDto.Nome) && 
                    string.Equals(c.Descricao, _cursoDto.Descricao))
            ));            
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            const string publicoAlvoInvalido = "Medico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Publico Alvo inválido");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNome()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.OBterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Nome do curso já conta no banco de dados.");
        }
    }
}

using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.Dominio.Alunos
{
    public class ArmazenadorDeAluno
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;

        public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
        {
            _alunoRepositorio = alunoRepositorio;
            _conversorDePublicoAlvo = conversorDePublicoAlvo;
        }

        public void Armazenar(AlunoDto alunoDto)
        {
            //PublicoAlvo publicoAlvo;

            var alunoJaSalvo = _alunoRepositorio.ObterPeloCpf(alunoDto.Cpf);

            ValidadorDeRegra.Novo()
                //.Quando(!Enum.TryParse(alunoDto.PublicoAlvo, out publicoAlvo), Resource.PublicoAlvoInvalido)
                .Quando(alunoJaSalvo != null && alunoJaSalvo.Id != alunoDto.Id, Resource.CPFExistente)
                .DisperarExcecaoseExistir();

            var publicoAlvo = _conversorDePublicoAlvo.Converter(alunoDto.PublicoAlvo);


            if (alunoDto.Id > 0)
            {
                var aluno = _alunoRepositorio.ObterPorId(alunoDto.Id);
                aluno.AlterarNome(alunoDto.Nome);
            }
            else
            {
                var aluno = new Aluno(alunoDto.Nome, alunoDto.Cpf, alunoDto.Email, publicoAlvo);
                _alunoRepositorio.Adicionar(aluno);
            }
        }
    }
}

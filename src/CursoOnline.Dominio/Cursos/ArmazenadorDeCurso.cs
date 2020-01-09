using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.Dominio.Cursos
{

    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
        {
            _cursoRepositorio = cursoRepositorio;
            _conversorDePublicoAlvo = conversorDePublicoAlvo;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

            ValidadorDeRegra.Novo()
                //.Quando(cursoJaSalvo != null, Resource.NomeCursoExistente)
                .Quando(cursoJaSalvo != null && cursoJaSalvo.Id != cursoDto.Id,
                    Resource.NomeCursoExistente)
                //.Quando(!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo), Resource.PublicoAlvoInvalido)                
                .DisperarExcecaoseExistir();

            var publicoAlvo = _conversorDePublicoAlvo.Converter(cursoDto.PublicoAlvo);

            if (cursoDto.Id > 0)
            {
                var curso = _cursoRepositorio.ObterPorId(cursoDto.Id);
                curso.AlterarNome(cursoDto.Nome);
                curso.AlterarValor(cursoDto.Valor);
                curso.AlterarCargaHoraria(cursoDto.CargaHoraria);
            }
            else
            {
                var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria,
                    publicoAlvo, cursoDto.Valor);

                _cursoRepositorio.Adicionar(curso);
            }
        }
    }
}

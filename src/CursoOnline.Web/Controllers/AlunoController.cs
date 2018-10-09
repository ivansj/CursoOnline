using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AlunoOnline.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly IRepositorio<Aluno> _AlunoRepositorio;

        public AlunoController(ArmazenadorDeAluno armazenadorDeAluno, IRepositorio<Aluno> AlunoRepositorio)
        {
            _armazenadorDeAluno = armazenadorDeAluno;
            _AlunoRepositorio = AlunoRepositorio;
        }

        public IActionResult Index()
        {
            var Alunos = _AlunoRepositorio.Consultar();


            if (Alunos.Any())
            {
                var dtos = Alunos.Select(a => new AlunoDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Cpf = a.Cpf,
                    Email = a.Email,
                    PublicoAlvo = a.PublicoAlvo.ToString()
                });
                return View("Index", PaginatedList<AlunoDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<AlunoDto>.Create(null, Request));
        }

        public IActionResult Editar(int id)
        {
            var Aluno = _AlunoRepositorio.ObterPorId(id);
            var dto = new AlunoDto
            {
                Id = Aluno.Id,
                Nome = Aluno.Nome,
                Cpf = Aluno.Cpf,
                Email = Aluno.Email,
                PublicoAlvo = Aluno.PublicoAlvo.ToString()
            };

            return View("NovoOuEditar", dto);
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new AlunoDto());
        }

        [HttpPost]
        public IActionResult Salvar(AlunoDto model)
        {
            _armazenadorDeAluno.Armazenar(model);
            return Ok();
        }
    }
}

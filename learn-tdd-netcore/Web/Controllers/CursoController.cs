using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Dominio.Cursos;
using Web.Util;
using Dominio._Base;
using System.Linq;

namespace Web.Controllers
{
    public class CursoController : Controller
    {
        private ArmazenadorDeCurso _armazenadorDeCurso;
        private IRepositorio<Curso> _cursoRepositorio;

        public CursoController(ArmazenadorDeCurso armazenadorDeCurso, IRepositorio<Curso> cursoRepositorio)
        {
			_armazenadorDeCurso = armazenadorDeCurso;
			_cursoRepositorio = cursoRepositorio;

		}
        public IActionResult Index()
        {
			IEnumerable<CursoParaListagemDto> dtos = null;
			var cursos = _cursoRepositorio.Consultar();

            if (cursos.Any())
            {
				dtos = cursos.Select(c => new CursoParaListagemDto
				{
					Id = c.Id,
					Nome = c.Nome,
					CargaHoraria = c.CargaHoraria,
					PublicoAlvo = c.PublicoAlvo.ToString(),
					Valor = c.Valor
				});
            }
            return View("index", PaginatedList<CursoParaListagemDto>.Create(dtos, Request));
        }

		public IActionResult Novo()
		{
			return View("NovoOuEditar", new CursoDto());
		}

		[HttpPost]
		public IActionResult Salvar(CursoDto model)
		{
			_armazenadorDeCurso.Armazenar(model);
			return Ok();
		}
	}
}

using Dominio._Base;
using Dominio.PublicoAlvo;

namespace Dominio.Cursos
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
				.Quando(cursoJaSalvo != null && cursoJaSalvo.Id != cursoDto.Id, Resource.NomeCursoJaExiste)
				.DispararExcecaoSeExistir();

			var publicoAlvo = _conversorDePublicoAlvo.Converter(cursoDto.PublicoAlvo);

			var curso = new Curso(cursoDto.Nome,
				cursoDto.CargaHoraria,
				publicoAlvo,
				cursoDto.Valor,
				cursoDto.Descricao);

			if (cursoDto.Id > 0)
			{
				curso = _cursoRepositorio.ObterPorId(cursoDto.Id);
				curso.AlterarNome(cursoDto.Nome);
				curso.AlterarCargaHoraria(cursoDto.CargaHoraria);
				curso.AlterarValor(cursoDto.Valor);
			}
			else
			{
				_cursoRepositorio.Adicionar(curso);
			}
		}
    }
}
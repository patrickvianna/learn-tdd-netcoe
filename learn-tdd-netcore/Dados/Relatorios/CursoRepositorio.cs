using System.Linq;
using Dados.Contextos;
using Dominio.Cursos;

namespace Dados.Relatorios
{
    public class CursoRepositorio : RepositorioBase<Curso>, ICursoRepositorio
	{
		public CursoRepositorio(ApplicationDbContext context) : base(context)
		{
		}

        Curso ICursoRepositorio.ObterPeloNome(string nome)
        {
			var entidade = Context.Set<Curso>().Where(c => c.Nome.Contains(nome));
			if (entidade.Any())
				return entidade.First();
			return null;
		}
    }
}

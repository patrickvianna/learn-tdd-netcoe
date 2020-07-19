using Dominio._Base;

namespace Dominio.Cursos
{
    public interface ICursoRepositorio : IRepositorio<Curso>
    {
        //void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}
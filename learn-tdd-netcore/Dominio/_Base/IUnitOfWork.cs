using System.Threading.Tasks;

namespace Dominio._Base
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}

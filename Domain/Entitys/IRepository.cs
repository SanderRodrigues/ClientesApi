using System.Threading;
using System.Threading.Tasks;

namespace Domain.Entitys
{
    public interface IRepository<T>
    {
        Task<T> InsertAsync(T entidade, CancellationToken cancellationToken);
        Task UpdateAsync(T entidade);
        Task<T> ObterPorIdAsync(string id);
        Task ExcluirPorId(string id);
    }
}

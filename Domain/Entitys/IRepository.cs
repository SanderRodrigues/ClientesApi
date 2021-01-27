using System.Threading;
using System.Threading.Tasks;
using CrossCutting.Domain;

namespace Domain.Entitys
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T> InsertAsync(T entidade, CancellationToken cancellationToken);
        Task UpdateAsync(T entidade);
        Task<T> ObterPorIdAsync(string id);
        Task ExcluirPorId(string id);
    }
}

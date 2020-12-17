using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Entitys.Cliente
{
    public interface IClienteRepository: IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> ListarClientesAsync();
    }
}

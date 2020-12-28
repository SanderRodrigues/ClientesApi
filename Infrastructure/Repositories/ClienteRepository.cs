using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entitys.Cliente;
using Infrastructure.CoreServices.DataAccess;
using Infrastructure.DataModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class ClienteRepository : IClienteRepository
    {
        public IMapper _mapper { get; set; }
        public SqlDbContext _dbContext { get; set; }

        public ClienteRepository(SqlDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public Task ExcluirPorId(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> InsertAsync(Cliente entidade, CancellationToken cancellationToken)
        {
            ClienteDbModel cliente = _mapper.Map<ClienteDbModel>(entidade);

            _dbContext.Clientes.Add(cliente);
            _ = await _dbContext.SaveChangesAsync();
            return _mapper.Map<Cliente>(cliente);
        }

        public async Task<Cliente> ObterPorIdAsync(string id)
        {
            Guid ClienteId = Guid.Parse(id);
            var cliente = await _dbContext.Clientes.FirstOrDefaultAsync(a => a.Id == ClienteId);

            return _mapper.Map<Cliente>(cliente);
        }

        public async Task UpdateAsync(Cliente entidade)
        {
            var clientedb = _dbContext.Clientes.FirstOrDefault(a => a.Id == entidade.Id);
            _dbContext.Clientes.Update(_mapper.Map(entidade, clientedb));
            _ = await _dbContext.SaveChangesAsync();
        }
    }
}

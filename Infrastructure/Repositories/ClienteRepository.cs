using System;
using System.Collections.Generic;
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

            cliente.Id = Guid.NewGuid();

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

        public async Task<IEnumerable<Cliente>> ListarClientesAsync() 
        {
            return await _dbContext.Clientes.Select(a => _mapper.Map<Cliente>(a)).ToListAsync();
        }

        public Task UpdateAsync(Cliente entidade)
        {
            throw new NotImplementedException();
        }
    }
}

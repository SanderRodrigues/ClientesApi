using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Infrastructure.CoreServices.DataAccess;
using MediatR;
using Queries.Dto;
using Queries.Requests;

namespace Queries.QueryHandlers
{
    public class ListarClientesTodosRequestHandler : BaseQueryHandler,  IRequestHandler<ListarClientesTodosRequest, IEnumerable<ClienteDto>>
    {
        const string sql = "SELECT Id, Nome + ' ' + SobreNome as NomeCompleto, Email FROM Cliente";

        public ListarClientesTodosRequestHandler(IDbConnectionFactory dbConnectionFactory) :base(dbConnectionFactory)
        {
        }

        public async Task<IEnumerable<ClienteDto>> Handle(ListarClientesTodosRequest request, CancellationToken cancellationToken)
        {
            using (var conn = GetDbConnection())
            {
                var clientes = await conn.QueryAsync<ClienteDto>(sql, cancellationToken);
                return clientes;
            }
        }
    }
}

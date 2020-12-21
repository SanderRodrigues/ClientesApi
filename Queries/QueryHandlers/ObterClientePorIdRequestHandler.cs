using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Infrastructure.CoreServices.DataAccess;
using MediatR;
using Queries.Dto;
using Queries.Requests;

namespace Queries.QueryHandlers
{
    public class ObterClientePorIdRequestHandler : BaseQueryHandler, IRequestHandler<ObterClientePorIdRequest, ClienteDto>
    {
        const string sql = "SELECT Id, Nome + ' ' + SobreNome as NomeCompleto, Email FROM Cliente where Id = @Id";

        public ObterClientePorIdRequestHandler(IDbConnectionFactory dbConnectionFactory): base(dbConnectionFactory)
        {
            
        }

        public async Task<ClienteDto> Handle(ObterClientePorIdRequest request, CancellationToken cancellationToken)
        {
            using (var conn = GetDbConnection())
            {
                var cliente = await conn.QueryFirstAsync<ClienteDto>(sql,new {Id = request.Id});
                return cliente;
            }
        }
    }
}

using System.Data;
using Infrastructure.CoreServices.DataAccess;

namespace Queries.QueryHandlers
{
    public class BaseQueryHandler
    {
        protected readonly IDbConnectionFactory _dbConnectionFactory;

        protected BaseQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        protected IDbConnection GetDbConnection()
        {
            return _dbConnectionFactory.GetDbConnection();
        }
    }
}

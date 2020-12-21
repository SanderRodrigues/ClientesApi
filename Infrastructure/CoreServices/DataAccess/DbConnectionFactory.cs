using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CoreServices.DataAccess
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetDbConnection();
    }

    internal class DbConnectionFactory: IDbConnectionFactory
    {
        public SqlDbContext _dbContext { get; set; }

        public DbConnectionFactory(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDbConnection GetDbConnection()
        {
            return _dbContext.Database.GetDbConnection();
        }
    }
}

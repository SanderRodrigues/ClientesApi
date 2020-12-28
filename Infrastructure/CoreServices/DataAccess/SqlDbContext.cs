using Infrastructure.DataModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CoreServices.DataAccess
{
    internal class SqlDbContext: DbContext
    {
        public DbSet<ClienteDbModel> Clientes { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClienteDbModel>().ToTable("Cliente").HasKey(a => a.Id);
            modelBuilder.Entity<ClienteDbModel>().Property(a => a.Nome).HasMaxLength(10);
        }
    }
}

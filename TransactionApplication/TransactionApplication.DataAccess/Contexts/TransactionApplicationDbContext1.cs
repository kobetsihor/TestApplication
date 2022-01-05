using Microsoft.EntityFrameworkCore;
using TransactionApplication.DataAccess.Entitties;

namespace TransactionApplication.DataAccess.Contexts
{
    /// <summary>
    /// in memory db was used for more simple testing
    /// </summary>
    public class TransactionApplicationDbContext : DbContext
    {
        public TransactionApplicationDbContext(DbContextOptions<TransactionApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Player> Players { get; set; }
    }
}
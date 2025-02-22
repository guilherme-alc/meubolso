using MeuBolso.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuBolso.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        protected DbSet<Transaction> Transactions { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

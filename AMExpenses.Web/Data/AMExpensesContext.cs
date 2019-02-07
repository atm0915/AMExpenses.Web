using AMExpenses.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMExpenses.Web.Data
{
    public class AMExpensesContext : IdentityDbContext<StoreUser>
    {
        public AMExpensesContext(DbContextOptions<AMExpensesContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");
            builder.Entity<StoreUser>()
                .Property(u => u.Credit)
                .HasColumnType("decimal(18,2)");
            builder.Entity<StoreUser>()
                .Property(u => u.CreditOnAccount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
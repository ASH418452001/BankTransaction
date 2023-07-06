using BankTransaction.Model.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BankTransaction.Model
{
    public class DataContext : DbContext
    {
        public DbSet<bankTransaction> bankTransaction { get; set; }
        public DbSet<User> User { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
    
using Microsoft.EntityFrameworkCore;

namespace CosmeticAppLr4.Models
{
    public class CreamContext : DbContext
    {
        public CreamContext(DbContextOptions<CreamContext> options) : base(options) { }
        public DbSet<Cream> Creams { get; set; }
    }
}
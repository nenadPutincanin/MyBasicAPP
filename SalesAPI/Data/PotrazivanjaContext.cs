using Microsoft.EntityFrameworkCore;
using SalesAPI.Data.Entities;

namespace SalesAPI.Data
{
    public class PotrazivanjaContext : DbContext
    {
        public PotrazivanjaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Potrazivanja> Potrazivanja { get; set; }
    }
}

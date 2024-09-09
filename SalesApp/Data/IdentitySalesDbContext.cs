using Microsoft.EntityFrameworkCore;
using SalesApp.Models;

namespace SalesApp.Data
{
    public class IdentitySalesDbContext : DbContext
    {
        public IdentitySalesDbContext(DbContextOptions options) : base(options)
        {
        }

      public  DbSet<SalesUser> Users { get; set; }    
    }
}

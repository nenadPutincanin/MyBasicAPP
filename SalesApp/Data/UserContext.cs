using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesApp.Models;
using SalesApp.Models.ViewModel;

namespace SalesApp.Data
{
    public class UserContext : IdentityDbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

       
        public DbSet<LoginUser> LoginUsers { get; set; }

       
        public DbSet<SalesApp.Models.ViewModel.Potrazivanja>? Potrazivanja { get; set; }

    }
}

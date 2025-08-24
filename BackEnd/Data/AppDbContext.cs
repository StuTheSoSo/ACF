using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Officer> Officers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Case> Cases { get; set; }

        public DbSet<Client> Clients { get; set; }
    }
}

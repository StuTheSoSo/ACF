using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class AppDbContext : DbContext
    {
        /// <summary> Initializes a new instance of the <see cref="AppDbContext"/> class. </summary>
        /// <param name="options"> The options. </param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary> Gets or sets the audit logs. </summary>
        /// <value> The audit logs. </value>
        public DbSet<AuditLog> AuditLogs { get; set; }

        /// <summary> Gets or sets the cases. </summary>
        /// <value> The cases. </value>
        public DbSet<Case> Cases { get; set; }

        /// <summary> Gets or sets the clients. </summary>
        /// <value> The clients. </value>
        public DbSet<Client> Clients { get; set; }

        /// <summary> Gets or sets the officers. </summary>
        /// <value> The officers. </value>
        public DbSet<Officer> Officers { get; set; }

        /// <summary> Gets or sets the roles. </summary>
        /// <value> The roles. </value>
        public DbSet<Role> Roles { get; set; }
    }
}
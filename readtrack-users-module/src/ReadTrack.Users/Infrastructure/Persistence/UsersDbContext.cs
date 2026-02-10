using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReadTrack.Users.Domain.Entities;

namespace ReadTrack.Users.Infrastructure.Persistence
{
    /// <summary>
    /// Database context for the Users module, handling User and DataExportJob persistence.
    /// </summary>
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Users db set.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the DataExportJobs db set.
        /// </summary>
        public DbSet<DataExportJob> DataExportJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Isolate User module tables in a specific schema
            modelBuilder.HasDefaultSchema("users");

            // Apply entity configurations from the current assembly (Level 3 configurations)
            // This picks up UserConfiguration and DataExportJobConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Implementation of Domain Events dispatching would typically go here
            // invoking the Mediator to publish events stored in the entities.
            // For this implementation, we focus on persistence.
            
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReadTrack.Reading.Application.Interfaces;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Infrastructure.Persistence
{
    /// <summary>
    /// The Entity Framework Core DbContext for the Reading module.
    /// Manages the persistence of LibraryItems and ReadingSessions.
    /// </summary>
    public class ReadingDbContext : DbContext
    {
        public ReadingDbContext(DbContextOptions<ReadingDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the LibraryItems set.
        /// </summary>
        public DbSet<LibraryItem> LibraryItems { get; set; }

        /// <summary>
        /// Gets or sets the ReadingSessions set.
        /// </summary>
        public DbSet<ReadingSession> ReadingSessions { get; set; }

        /// <summary>
        /// Configures the model building process.
        /// Applies configurations from the current assembly.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all IEntityTypeConfiguration implementations in the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Ensure the schema is defined if using a shared database approach, 
            // though typical modular monoliths might use a specific schema per module.
            modelBuilder.HasDefaultSchema("reading");
        }
    }
}
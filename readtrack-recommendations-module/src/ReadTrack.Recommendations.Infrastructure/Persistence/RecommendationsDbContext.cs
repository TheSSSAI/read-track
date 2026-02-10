using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReadTrack.Recommendations.Domain.Entities;

namespace ReadTrack.Recommendations.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework Core database context for the Recommendations module.
    /// Manages persistence for Recommendations, Jobs, Feedback, and Context Embeddings.
    /// </summary>
    public class RecommendationsDbContext : DbContext
    {
        public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Recommendations data set.
        /// </summary>
        public DbSet<Recommendation> Recommendations { get; set; }

        /// <summary>
        /// Gets or sets the Recommendation Jobs data set.
        /// </summary>
        public DbSet<RecommendationJob> RecommendationJobs { get; set; }

        /// <summary>
        /// Gets or sets the Recommendation Feedback data set.
        /// </summary>
        public DbSet<RecommendationFeedback> RecommendationFeedbacks { get; set; }

        /// <summary>
        /// Gets or sets the User Context Embeddings data set.
        /// </summary>
        public DbSet<UserContextEmbedding> UserContextEmbeddings { get; set; }

        /// <summary>
        /// Configures the model building process.
        /// Applies all entity configurations defined in the Infrastructure assembly (Level 2).
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations from the current assembly (Infrastructure)
            // This picks up RecommendationConfiguration, RecommendationJobConfiguration, etc. from Level 2
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Default schema for the module to avoid collision with other modules
            modelBuilder.HasDefaultSchema("recommendations");
        }
    }
}
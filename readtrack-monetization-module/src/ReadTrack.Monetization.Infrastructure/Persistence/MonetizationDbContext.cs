using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReadTrack.Monetization.Domain.Entities;

namespace ReadTrack.Monetization.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework Core DbContext for the Monetization module.
    /// This context manages the persistence of Subscriptions and PaymentTransactions
    /// within the isolated 'monetization' schema.
    /// </summary>
    public class MonetizationDbContext : DbContext
    {
        public MonetizationDbContext(DbContextOptions<MonetizationDbContext> options)
            : base(options)
        {
        }

        // Domain Entities
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set the default schema for this module to ensure database isolation
            // in a modular monolith architecture.
            modelBuilder.HasDefaultSchema("monetization");

            // Automatically apply all IEntityTypeConfiguration implementations 
            // defined in the current assembly (e.g., SubscriptionConfiguration).
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            // Additional configuration for PaymentTransaction if a separate configuration file
            // is not detected or to enforce specific defaults not covered by configurations.
            modelBuilder.Entity<PaymentTransaction>(entity =>
            {
                entity.ToTable("PaymentTransactions", "monetization");
                
                entity.HasKey(pt => pt.TransactionId);
                
                entity.Property(pt => pt.ExternalTransactionId)
                    .HasMaxLength(255)
                    .IsRequired();
                    
                entity.Property(pt => pt.Amount)
                    .HasColumnType("decimal(18,2)") // Standard currency precision
                    .IsRequired();
                    
                entity.Property(pt => pt.Currency)
                    .HasMaxLength(3) // ISO Currency Code
                    .IsRequired();
                    
                entity.HasIndex(pt => pt.ExternalTransactionId)
                    .HasDatabaseName("IX_PaymentTransactions_ExternalTransactionId");
            });
        }
    }
}
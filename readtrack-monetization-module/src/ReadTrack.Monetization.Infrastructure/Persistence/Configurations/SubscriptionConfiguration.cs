using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadTrack.Monetization.Domain.Entities;
using ReadTrack.Monetization.Domain.ValueObjects;

namespace ReadTrack.Monetization.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the Subscription entity to map to the database using Entity Framework Core.
    /// Follows Domain-Driven Design principles for aggregate root persistence.
    /// </summary>
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            // Table and Schema Configuration
            // Isolating monetization data into its own schema for modularity
            builder.ToTable("Subscriptions", "monetization");

            // Primary Key
            builder.HasKey(s => s.SubscriptionId);

            // Property Configurations
            builder.Property(s => s.SubscriptionId)
                .IsRequired()
                .ValueGeneratedNever(); // GUIDs are typically generated in the application layer or constructor

            builder.Property(s => s.UserId)
                .IsRequired();

            // ExternalSubscriptionId is the link to Apple/Google
            // It is critical for webhook correlation
            builder.Property(s => s.ExternalSubscriptionId)
                .HasMaxLength(255)
                .IsRequired();

            // Enums are mapped to integers by default, which aligns with the SDS specifications
            // Explicitly configuring them ensures schema clarity
            builder.Property(s => s.Provider)
                .IsRequired()
                .HasConversion<string>(); // Storing as string for readability in DB, or int for performance. Using string for Provider (Apple/Google) is safer for debugging.

            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion<int>(); // Mapping status to int for performance/indexing

            builder.Property(s => s.Tier)
                .IsRequired()
                .HasConversion<int>(); // Mapping tier to int as per SDS

            builder.Property(s => s.CurrentPeriodEnd)
                .IsRequired();

            // Indexes for Performance Optimization
            // Critical for looking up subscriptions during login/auth checks
            builder.HasIndex(s => s.UserId)
                .HasDatabaseName("IX_Subscriptions_UserId");

            // Critical for webhook processing (O(1) lookup to find subscription by provider ID)
            builder.HasIndex(s => s.ExternalSubscriptionId)
                .HasDatabaseName("IX_Subscriptions_ExternalSubscriptionId");

            // Critical for the background job that finds expired subscriptions
            builder.HasIndex(s => s.CurrentPeriodEnd)
                .HasDatabaseName("IX_Subscriptions_CurrentPeriodEnd");

            // Relationships
            // Configures the One-to-Many relationship with PaymentTransactions
            builder.HasMany(s => s.PaymentTransactions)
                .WithOne() // Assuming PaymentTransaction has a navigation property back to Subscription, likely Shadow FK if not explicitly defined in Entity
                .HasForeignKey("SubscriptionId") // Explicitly defining the FK column name
                .OnDelete(DeleteBehavior.Cascade); // If a subscription is hard deleted (rare), transactions go with it. Usually soft delete is preferred.

            // Concurrency Control
            // Essential for high-integrity financial records to prevent race conditions during webhook processing
            builder.Property(s => s.RowVersion)
                .IsRowVersion();
                
            // Ignore Domain Events as they are not persisted to the DB directly
            // They are dispatched via the Context/Repository infrastructure
            builder.Ignore(s => s.DomainEvents);
        }
    }
}
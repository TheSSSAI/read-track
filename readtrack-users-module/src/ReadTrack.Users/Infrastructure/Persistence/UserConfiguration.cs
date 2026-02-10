using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadTrack.Users.Domain.Entities;
using ReadTrack.Users.Domain.Enums;

namespace ReadTrack.Users.Infrastructure.Persistence
{
    /// <summary>
    /// EF Core configuration for the User entity.
    /// Enforces schema constraints, indexing strategy, and PII isolation rules.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table Mapping - Vertical Slice Isolation
            builder.ToTable("Users", schema: "users");

            // Primary Key
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedNever(); // GUIDs are generated in app/domain layer

            // Auth0 Identity Mapping (Critical for Performance)
            builder.Property(u => u.Auth0Id)
                .IsRequired()
                .HasMaxLength(128);
            
            builder.HasIndex(u => u.Auth0Id)
                .IsUnique()
                .HasDatabaseName("IX_Users_Auth0Id");

            // Profile Data
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);
            
            // Email should be unique per system rules
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");

            builder.Property(u => u.DisplayName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.ProfilePictureUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            // Role Enum Mapping
            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (UserRole)System.Enum.Parse(typeof(UserRole), v));

            // Audit Properties
            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.LastLoginAt)
                .IsRequired(false);

            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Relationships
            builder.HasMany(u => u.DataExportJobs)
                .WithOne()
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Concurrency
            builder.Property(u => u.RowVersion)
                .IsRowVersion();
        }
    }
}
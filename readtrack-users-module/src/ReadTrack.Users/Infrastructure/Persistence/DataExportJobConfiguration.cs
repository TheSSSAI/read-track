using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadTrack.Users.Domain.Entities;
using ReadTrack.Users.Domain.Enums;

namespace ReadTrack.Users.Infrastructure.Persistence
{
    /// <summary>
    /// EF Core configuration for the DataExportJob entity.
    /// Manages the persistence of asynchronous export requests compliant with GDPR.
    /// </summary>
    public class DataExportJobConfiguration : IEntityTypeConfiguration<DataExportJob>
    {
        public void Configure(EntityTypeBuilder<DataExportJob> builder)
        {
            // Table Mapping
            builder.ToTable("DataExportJobs", schema: "users");

            // Primary Key
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Id)
                .ValueGeneratedNever();

            // Status Enum Mapping
            builder.Property(j => j.Status)
                .IsRequired()
                .HasConversion<int>(); // Store as int for efficiency

            // File Metadata
            builder.Property(j => j.S3Key)
                .HasMaxLength(1024)
                .IsRequired(false);

            builder.Property(j => j.FileUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            // Timestamps
            builder.Property(j => j.CreatedAt)
                .IsRequired();

            builder.Property(j => j.CompletedAt)
                .IsRequired(false);

            builder.Property(j => j.ExpirationDate)
                .IsRequired(false);

            // Relationships
            // Explicitly configured in UserConfiguration, but validated here
            builder.HasOne<User>()
                .WithMany(u => u.DataExportJobs)
                .HasForeignKey(j => j.UserId)
                .IsRequired();

            // Indexes for Performance (Job Polling)
            builder.HasIndex(j => j.UserId)
                .HasDatabaseName("IX_DataExportJobs_UserId");
            
            // Index for background cleanup jobs finding expired exports
            builder.HasIndex(j => j.ExpirationDate)
                .HasDatabaseName("IX_DataExportJobs_ExpirationDate");
        }
    }
}
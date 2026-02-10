using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Infrastructure.Persistence
{
    /// <summary>
    /// Configures the database mapping for the ReadingSession entity.
    /// </summary>
    public class ReadingSessionConfiguration : IEntityTypeConfiguration<ReadingSession>
    {
        public void Configure(EntityTypeBuilder<ReadingSession> builder)
        {
            builder.ToTable("ReadingSessions");

            builder.HasKey(rs => rs.Id);

            builder.Property(rs => rs.Id)
                .ValueGeneratedNever();

            builder.Property(rs => rs.StartTime)
                .IsRequired();

            builder.Property(rs => rs.EndTime)
                .IsRequired();

            // Store TimeSpan as ticks (long) or standard time type depending on DB provider.
            // EF Core typically handles TimeSpan mapping to appropriate column types (e.g., time, interval, or bigint ticks).
            // Here we rely on default mapping which is usually sufficient for Postgres (interval) or SQL Server (time).
            builder.Property(rs => rs.Duration)
                .IsRequired();

            builder.Property(rs => rs.PagesRead)
                .IsRequired();

            builder.Property(rs => rs.IsManualEntry)
                .IsRequired();

            // Configuration for shadow FK if it's not exposed in the Domain Entity
            // Assuming ReadingSession might not have a public LibraryItemId property 
            // to enforce aggregate root access, we explicitly map the shadow property.
            // If the domain model has LibraryItemId, this maps it. If not, it creates a shadow property.
            builder.Property<Guid>("LibraryItemId")
                .IsRequired();
            
            // Index on LibraryItemId for faster retrieval of sessions for a book
            builder.HasIndex("LibraryItemId");
        }
    }
}
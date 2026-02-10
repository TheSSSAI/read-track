using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Infrastructure.Persistence
{
    /// <summary>
    /// Configures the database mapping for the LibraryItem aggregate root.
    /// </summary>
    public class LibraryItemConfiguration : IEntityTypeConfiguration<LibraryItem>
    {
        public void Configure(EntityTypeBuilder<LibraryItem> builder)
        {
            builder.ToTable("LibraryItems");

            builder.HasKey(li => li.Id);

            builder.Property(li => li.Id)
                .ValueGeneratedNever();

            builder.Property(li => li.UserId)
                .IsRequired();

            // Indexing UserId for performance as most queries filter by user
            builder.HasIndex(li => li.UserId);

            builder.Property(li => li.Shelf)
                .IsRequired()
                .HasConversion<int>(); // Storing enum as int for efficiency

            builder.Property(li => li.AddedAt)
                .IsRequired();

            builder.Property(li => li.LastActivityAt)
                .IsRequired();

            // Configure the Owned Type (Value Object) BookMetadata
            builder.OwnsOne(li => li.Metadata, metadata =>
            {
                metadata.Property(m => m.GoogleBookId)
                    .HasColumnName("GoogleBookId")
                    .HasMaxLength(50)
                    .IsRequired();

                metadata.Property(m => m.Title)
                    .HasColumnName("Title")
                    .HasMaxLength(500)
                    .IsRequired();

                // Serialize List<string> Authors to JSON for storage
                var authorsConverter = new ValueConverter<List<string>, string>(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());

                var authorsComparer = new ValueComparer<List<string>>(
                    (c1, c2) => JsonSerializer.Serialize(c1, (JsonSerializerOptions?)null) == JsonSerializer.Serialize(c2, (JsonSerializerOptions?)null),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => JsonSerializer.Deserialize<List<string>>(JsonSerializer.Serialize(c, (JsonSerializerOptions?)null), (JsonSerializerOptions?)null) ?? new List<string>());

                metadata.Property(m => m.Authors)
                    .HasColumnName("Authors")
                    .HasConversion(authorsConverter)
                    .Metadata.SetValueComparer(authorsComparer);

                metadata.Property(m => m.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(4000); // Standard max length for descriptions

                metadata.Property(m => m.CoverUrl)
                    .HasColumnName("CoverUrl")
                    .HasMaxLength(2048);

                metadata.Property(m => m.TotalPages)
                    .HasColumnName("TotalPages");

                metadata.Property(m => m.Isbn)
                    .HasColumnName("Isbn")
                    .HasMaxLength(20);
                
                // Composite index to prevent a user from adding the same book twice
                metadata.HasIndex("GoogleBookId"); 
            });

            // Configure the relationship with ReadingSessions
            // A LibraryItem has many ReadingSessions
            builder.HasMany(li => li.ReadingSessions)
                .WithOne()
                .HasForeignKey("LibraryItemId")
                .OnDelete(DeleteBehavior.Cascade);

            // Configure navigation access to private field if necessary, 
            // though HasMany usually handles the collection access via the property.
            builder.Metadata.FindNavigation(nameof(LibraryItem.ReadingSessions))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
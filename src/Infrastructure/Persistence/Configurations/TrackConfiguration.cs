using ChinookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChinookStore.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.Property(t => t.Name);
        builder.Property(t => t.Composer);
        builder.Property(t => t.Milliseconds);
        builder.Property(t => t.Bytes);
        builder.Property(t => t.UnitPrice);
    }
}
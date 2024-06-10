using ChinookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChinookStore.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.Property(t => t.Number).IsRequired();
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
        builder.Property(t => t.Composer).HasMaxLength(200).IsRequired();
        builder.Property(t => t.Milliseconds).IsRequired();
        builder.Property(t => t.UnitPrice).IsRequired();
    }
}

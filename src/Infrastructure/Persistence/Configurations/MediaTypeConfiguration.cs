using ChinookStore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChinookStore.Infrastructure.Persistence.Configurations;

public class MediaTypeConfiguration : IEntityTypeConfiguration<MediaType>
{
    public void Configure(EntityTypeBuilder<MediaType> builder)
    {
        builder.Property(m => m.Name);

        builder.HasData(
            MediaType.Aac,
            MediaType.Mpeg,
            MediaType.Mpeg4,
            MediaType.ProtectedAac,
            MediaType.PurchasedAac);
    }
}
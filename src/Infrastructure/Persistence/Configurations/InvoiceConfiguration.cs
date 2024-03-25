using ChinookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChinookStore.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(i => i.Date);
        builder.Property(i => i.Total);

        builder.OwnsOne(i => i.Address, b =>
        {
            b.Property(a => a.Line);
            b.Property(a => a.City);
            b.Property(a => a.State);
            b.Property(a => a.Country);
            b.Property(a => a.PostalCode);
        });
    }
}

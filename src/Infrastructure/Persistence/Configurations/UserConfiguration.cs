using ChinookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChinookStore.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName);
        builder.Property(u => u.LastName);

        builder.OwnsOne(u => u.Address, b =>
        {
            b.Property(a => a.Line);
            b.Property(a => a.City);
            b.Property(a => a.State);
            b.Property(a => a.Country);
            b.Property(a => a.PostalCode);
        });
        builder.OwnsOne(u => u.Email, b =>
        {
            b.Property(e => e.Address);
        });
        builder.OwnsOne(u => u.Phone, b =>
        {
            b.Property(p => p.Number);
        });
    }
}

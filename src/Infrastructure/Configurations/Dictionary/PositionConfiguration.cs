using Domain.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Dictionary;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.Property(p => p.NameRu).IsRequired().HasMaxLength(256);
        builder.Property(p => p.NameKg).IsRequired().HasMaxLength(256);
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.ToTable("candidates");
        builder.Property(e => e.NameRu).IsRequired().HasMaxLength(256);
        builder.Property(e => e.NameKg).IsRequired().HasMaxLength(256);
    }
}
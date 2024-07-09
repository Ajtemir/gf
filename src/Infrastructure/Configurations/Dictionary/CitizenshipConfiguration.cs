using Domain.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Dictionary;

public class CitizenshipConfiguration : IEntityTypeConfiguration<Citizenship>
{
    public void Configure(EntityTypeBuilder<Citizenship> builder)
    {
        builder.Property(c => c.NameRu).IsRequired().HasMaxLength(256);
        builder.Property(c => c.NameKg).IsRequired().HasMaxLength(256);

        
    }
}
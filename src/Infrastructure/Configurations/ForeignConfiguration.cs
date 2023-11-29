using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ForeignConfiguration : IEntityTypeConfiguration<Foreigner>
{
    public void Configure(EntityTypeBuilder<Foreigner> builder)
    {
        builder.ToTable("candidates");

        builder.HasOne(f => f.Citizenship)
            .WithMany(c => c.Foreigners)
            .HasForeignKey(f => f.CitizenshipId)
            .IsRequired();
    }
}
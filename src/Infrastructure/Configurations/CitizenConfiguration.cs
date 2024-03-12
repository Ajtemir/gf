using Domain.Entities;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CitizenConfiguration : IEntityTypeConfiguration<Citizen>
{
    public void Configure(EntityTypeBuilder<Citizen> builder)
    {
        builder.Property(x => x.ScienceDegree).HasMaxLength(256);

        builder.HasOne(c => c.Education)
            .WithMany(e => e.Citizens)
            .HasForeignKey(c => c.EducationId)
            .IsRequired();
    }
}
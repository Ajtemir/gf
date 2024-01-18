using Domain.Entities;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CitizenConfiguration : IEntityTypeConfiguration<Citizen>
{
    public void Configure(EntityTypeBuilder<Citizen> builder)
    {
        builder.ToTable("candidates");

        builder.Property(x => x.PassportNumber).HasMaxLength(10).HasColumnName(SnakeNamingConvention.GetSnakeName(nameof(Citizen.PassportNumber)));
        
        builder.Property(c => c.RegisteredAddress)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName(SnakeNamingConvention.GetSnakeName(nameof(Citizen.RegisteredAddress)));
        
        builder.Property(c => c.ActualAddress)
            .HasMaxLength(256)
            .HasColumnName(SnakeNamingConvention.GetSnakeName(nameof(Citizen.ActualAddress)));

        builder.Property(x => x.ScienceDegree).HasMaxLength(256);

        builder.HasOne(c => c.Education)
            .WithMany(e => e.Citizens)
            .HasForeignKey(c => c.EducationId)
            .IsRequired();
    }
}
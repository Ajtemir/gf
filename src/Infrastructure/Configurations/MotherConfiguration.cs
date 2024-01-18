using Domain.Entities;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MotherConfiguration : IEntityTypeConfiguration<Mother>
{
    public void Configure(EntityTypeBuilder<Mother> builder)
    {
        builder.ToTable("candidates");

        builder.Property(x => x.PassportNumber).HasMaxLength(10).HasColumnName(SnakeNamingConvention.GetSnakeName(nameof(Mother.PassportNumber)));
        
        builder.Property(m => m.RegisteredAddress)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName(SnakeNamingConvention.GetSnakeName(nameof(Mother.RegisteredAddress)));
        
        builder.Property(m => m.ActualAddress)
            .HasMaxLength(256)
            .HasColumnName(SnakeNamingConvention.GetSnakeName(nameof(Mother.ActualAddress)));
    }
}
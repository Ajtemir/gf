using Application.Common.Helpers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Dictionary;

public class RewardConfiguration : IEntityTypeConfiguration<Reward>
{
    public void Configure(EntityTypeBuilder<Reward> builder)
    {
        builder.HasQueryFilter(x => x.CreatedByUser!.IsDeleted == false && x.ModifiedByUser!.IsDeleted == false);
        
        builder.Property(r => r.NameRu).IsRequired().HasMaxLength(256);
        builder.Property(r => r.NameKg).IsRequired().HasMaxLength(256);
        builder.Property(r => r.ImageName).HasMaxLength(256);
        builder.Property(r => r.Image).HasMaxLength(FileSizeHelper.ToMegabytes(4));

        builder.HasOne(r => r.CreatedByUser)
            .WithMany()
            .HasForeignKey(r => r.CreatedBy)
            .IsRequired();
        
        builder.HasOne(r => r.ModifiedByUser)
            .WithMany()
            .HasForeignKey(r => r.ModifiedBy)
            .IsRequired();
    }
}
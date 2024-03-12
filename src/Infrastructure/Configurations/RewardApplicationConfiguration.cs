using Domain.Entities;
using Domain.Enums;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RewardApplicationConfiguration : IEntityTypeConfiguration<Domain.Entities.Application>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Application> builder)
    {
        builder.HasQueryFilter(x => x.CreatedByUser!.IsDeleted == false && x.ModifiedByUser!.IsDeleted == false);
        
        // builder.Property(ra => ra.Region)
        //     .HasConversion(EnumerationValueConverter<Region>.Create());

        builder.HasOne(ra => ra.CreatedByUser)
            .WithMany()
            .HasForeignKey(ra => ra.CreatedBy)
            .IsRequired();
        
        builder.HasOne(ra => ra.ModifiedByUser)
            .WithMany()
            .HasForeignKey(ra => ra.ModifiedBy)
            .IsRequired();
    }
}
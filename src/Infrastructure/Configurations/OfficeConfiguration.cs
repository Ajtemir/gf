using Domain.Entities;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder.HasQueryFilter(x => !x.CreatedByUser!.IsDeleted && !x.ModifiedByUser!.IsDeleted);

        builder.HasMany(x => x.ChildOffices)
            .WithOne(x => x.ParentOffice)
            .HasForeignKey(x => x.ParentOfficeId)
            .IsRequired();
        
        builder.HasMany(x => x.ParentOffices)
            .WithOne(x => x.ChildOffice)
            .HasForeignKey(x => x.ChildOfficeId)
            .IsRequired();

        builder.HasOne(rc => rc.CreatedByUser)
            .WithMany()
            .HasForeignKey(rc => rc.CreatedBy)
            .IsRequired();

        builder.HasOne(rc => rc.ModifiedByUser)
            .WithMany()
            .HasForeignKey(rc => rc.ModifiedBy)
            .IsRequired();
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OfficeRelationshipConfiguration : IEntityTypeConfiguration<OfficeRelationship>
{
    public void Configure(EntityTypeBuilder<OfficeRelationship> builder)
    {
        builder.ToTable("office_relationships");
        builder.HasKey(x => new { x.ChildOfficeId, x.ParentOfficeId });

        builder.HasQueryFilter(x => 
            !x.ChildOffice!.CreatedByUser!.IsDeleted &&
            !x.ChildOffice!.ModifiedByUser!.IsDeleted &&
            !x.ParentOffice!.CreatedByUser!.IsDeleted &&
            !x.ParentOffice!.ModifiedByUser!.IsDeleted);
    }
}
using Application.Common.Helpers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        // builder.UseTphMappingStrategy();
        
        builder.ToTable("candidates");
        builder.HasDiscriminator(x => x.CandidateTypeId);

        builder.HasQueryFilter(x => x.CreatedByUser!.IsDeleted == false && x.ModifiedByUser!.IsDeleted == false);

        builder.Property(x => x.Image).HasMaxLength(FileSizeHelper.ToMegabytes(4));
        
        builder.HasMany(rc => rc.Applications)
            .WithOne(ra => ra.Candidate)
            .HasForeignKey(ra => ra.CandidateId)
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
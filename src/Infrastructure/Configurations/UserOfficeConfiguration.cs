using Domain.Entities;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserOfficeConfiguration : IEntityTypeConfiguration<UserOffice>
{
    public void Configure(EntityTypeBuilder<UserOffice> builder)
    {
        builder.ToTable("user_offices");
        builder.HasKey(e => new { e.UserId, e.OfficeId });
        builder.HasQueryFilter(userOffice => !userOffice.User!.IsDeleted);

        builder.Property(x => x.UserId).HasColumnName(SnakeNamingConvention.GetSnakeName(nameof(UserOffice.UserId)));
        builder.Property(x => x.OfficeId).HasColumnName(SnakeNamingConvention.GetSnakeName(nameof(UserOffice.OfficeId)));
    }
}
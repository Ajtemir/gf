using Application.Common.Helpers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.UserName).IsRequired();
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(64);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(64);
        builder.Property(u => u.PatronymicName).HasMaxLength(64);
        builder.Property(u => u.Email).HasMaxLength(64);
        builder.Property(u => u.Pin).HasMaxLength(14).IsFixedLength();
        builder.Property(u => u.Image).HasMaxLength(FileSizeHelper.ToMegabytes(4));

        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        builder.HasMany(user => user.Roles)
            .WithOne(userRole => userRole.User)
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired();

        builder.HasMany(user => user.Offices)
            .WithMany(office => office.Users)
            .UsingEntity<UserOffice>();

        builder.HasMany(x => x.CreatedUsers)
            .WithOne(x => x.CreatedByUser)
            .HasForeignKey(x => x.CreatedBy);
        
        builder.HasMany(x => x.ModifiedUsers)
            .WithOne(x => x.ModifiedByUser)
            .HasForeignKey(x => x.ModifiedBy);
    }
}
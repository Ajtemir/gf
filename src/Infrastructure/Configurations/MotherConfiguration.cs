using Domain.Entities;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MotherConfiguration : IEntityTypeConfiguration<Mother>
{
    public void Configure(EntityTypeBuilder<Mother> builder)
    {

    }
}
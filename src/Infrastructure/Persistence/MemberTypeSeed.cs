using Application.Common.Extensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void MemberTypeSeed(this ModelBuilder builder)
    {
        builder.Entity<MemberType>().HasData(
            new MemberType
            {
                Id = MemberType.Individual,
            },
            new MemberType
            {
                Id = MemberType.Legal,
            }
        );
    }
}
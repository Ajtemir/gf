using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void MemberSeed(this ModelBuilder builder)
    {
        builder.Entity<Member>().HasData(
            FirstMember
        );
    }

    private static Member FirstMember => new Member
    {
        Id = 1,
        Pin = "21808200150020",
    };
}
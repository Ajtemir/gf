using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void MemberSeed(this ModelBuilder builder)
    {
        builder.Entity<Member>().HasData(
            KaldarbekovaBermetMember,
            AytemirUlanbekUuluMember
        );
    }

    private static Member KaldarbekovaBermetMember => new()
    {
        Id = 1,
        Pin = "12812197500736",
    };
    
    private static Member AytemirUlanbekUuluMember => new ()
    {
        Id = 2,
        Pin = "21808200150020",
    };
}
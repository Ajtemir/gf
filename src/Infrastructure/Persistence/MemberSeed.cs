using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void PersonSeed(this ModelBuilder builder)
    {
        builder.Entity<Person>().HasData(
            KaldarbekovaBermetMember,
            AytemirUlanbekUuluMember
        );
    }

    private static Person KaldarbekovaBermetMember => new()
    {
        Id = 1,
        Pin = "12812197500736",
        Avatar = null,
        Gender = Gender.Female,
        AvatarId = null,
        LastName = "Kaldarbekova",
        FirstName = "Bermet",
        PassportNumber = "id3232",
        RegisteredAddress = "Archa Beshik",
    };
    
    private static Person AytemirUlanbekUuluMember => new ()
    {
        Id = 2,
        Pin = "21808200150020",
        LastName = "Ulanbek uulu",
        FirstName = "Aytemir",
        Gender = Gender.Male,
        PassportNumber = "A23423",
        RegisteredAddress = "dsgdsfg",
    };
}
using Application.Common.Extensions;
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
            AytemirUlanbekUuluMember,
            MehmetKenanDonmezMember
        );

        builder.Entity<PinEntity>().HasData(
            InfocomEntityMember
        );
    }

    private static Person KaldarbekovaBermetMember => new()
    {
        Id = 1,
        Pin = "12812197500736",
        Avatar = null,
        Gender = Gender.Female,
        AvatarId = FirstAvatar.Id,
        LastName = "Kaldarbekova",
        FirstName = "Bermet",
        PassportSeriesNumber = "id3232",
        RegisteredAddress = "Archa Beshik",
        BirthDate = DateOnly.Parse("18.08.1975"),
    };
    
    private static Person AytemirUlanbekUuluMember => new ()
    {
        Id = 2,
        Pin = "21808200150020",
        LastName = "Ulanbek uulu",
        FirstName = "Aytemir",
        Gender = Gender.Male,
        PassportSeriesNumber = "A23423",
        RegisteredAddress = "dsgdsfg",
        Avatar = null,
        BirthDate = DateOnly.Parse("18.08.2001"),
    };
    
    private static Person MehmetKenanDonmezMember => new ()
    {
        Id = 3,
        Pin = "22604197550012",
        LastName = "Donmez",
        FirstName = "Mehmet Kenan",
        Gender = Gender.Male,
        PassportSeriesNumber = "A23423",
        RegisteredAddress = "dsgdsfg",
        Avatar = null,
        BirthDate = DateOnly.Parse("26.04.1875"),
    };
    
    private static PinEntity InfocomEntityMember => new ()
    {
        Id = 4,
        Pin = "01211200710029",
        Avatar = null,
        
    };
}
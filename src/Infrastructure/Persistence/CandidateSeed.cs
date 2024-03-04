using Application.Common.Extensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class Seed
{
    private static void CandidateSeed(this ModelBuilder builder)
    {
        builder.Entity<Mother>().HasData(
            KaldarbekovaBermet
        );
    }

    private static Mother KaldarbekovaBermet => new()
    {
        Id = 1,
        Gender = Gender.Female,
        Image = new byte[]{0},
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = Admin.Id,
        MemberId = KaldarbekovaBermetMember.Id,
        PassportNumber = "ID0241126",
        FirstName = "Kalicha",
        LastName = "Askerova",
        RegisteredAddress = "Street 23",
        ModifiedBy = Admin.Id,
        BirthDate = DateOnly.Parse("12.12.2012"),
    };
    
    private static Child AytemirUlanbekUulu => new()
    {
        Id = 2,
        Gender = Gender.Male,
        Image = new byte[]{0},
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = Admin.Id,
        MemberId = AytemirUlanbekUuluMember.Id,
        PassportNumber = "ID0241126",
        FirstName = "Aytemir",
        LastName = "Ulanbek uulu",
        RegisteredAddress = "Street 23",
        ModifiedBy = Admin.Id,
        BirthDate = DateOnly.Parse("18.08.2001"),
    };
}
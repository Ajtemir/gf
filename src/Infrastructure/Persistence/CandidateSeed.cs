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
            FirstMother
        );
    }

    private static Mother FirstMother => new Mother
    {
        Id = 1,
        Gender = Gender.Female,
        Image = new byte[]{0},
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = Admin.Id,
        MemberId = FirstMember.Id,
        PassportNumber = null,
        FirstName = "Kalicha",
        LastName = "Askerova",
        RegisteredAddress = "Street 23",
        ModifiedBy = Admin.Id,
    };
}
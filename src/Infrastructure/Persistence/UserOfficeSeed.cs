using Domain.Dictionary;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void UserOfficeSeed(this ModelBuilder builder)
    {
        builder.Entity<UserOffice>().HasData(
            new UserOffice
            {
                OfficeId = Secretariat.Id,
                UserId = Admin.Id,
            }
        );
    }
}
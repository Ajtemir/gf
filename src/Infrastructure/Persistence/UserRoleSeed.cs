using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void UserRoleSeed(this ModelBuilder builder)
    {
        builder.Entity<ApplicationUserRole>().HasData(
            new ApplicationUserRole
            {
                RoleId = 1,
                UserId = 1,
            }
        );
    }
}
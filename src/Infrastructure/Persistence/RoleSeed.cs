using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void RoleSeed(this ModelBuilder builder)
    {
        builder.Entity<ApplicationRole>().HasData(
            UserRole,
            AdministratorRole,
            Manager,
            Specialist,
            Expert
        );
    }

    public static ApplicationRole UserRole = new ApplicationRole
    {
        Id = 1,
        Name = "User",
        NormalizedName = "USER",
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        Note = "Пользователь",
    };

    public static ApplicationRole AdministratorRole = new ApplicationRole
    {
        Id = 2,
        Name = "Administrator",
        NormalizedName = "ADMINISTRATOR",
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        Note = "Администратор",
    };

    public static ApplicationRole Manager = new ApplicationRole
    {
        Id = 3,
        Name = "Manager",
        NormalizedName = "MANAGER",
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        Note = "Заведующий",
    };

    public static ApplicationRole Specialist = new ApplicationRole
    {
        Id = 4,
        Name = "Specialist",
        NormalizedName = "SPECIALIST",
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        Note = "Специалист",
    };

    public static ApplicationRole Expert = new ApplicationRole
    {
        Id = 5,
        Name = "EXpert",
        NormalizedName = "EXPERT",
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        Note = "Эксперт",
    };
}
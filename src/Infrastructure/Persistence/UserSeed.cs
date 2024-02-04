using Application.Common.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void UserSeed(this ModelBuilder builder)
    {
        var admin = new ApplicationUser
        {
            Id = 1,
            UserName = "Admin",
            Email = "Admin@test.ru",
            LockoutEnabled = false,
            PhoneNumber = "996111222333",
            CreatedAt = DateTime.Now.SetKindUtc(),
            ModifiedAt = DateTime.Now.SetKindUtc(),
            FirstName = "Админ",
            LastName = "Админов",
            NormalizedUserName = "ADMIN",
            NormalizedEmail = "ADMIN@TEST.RU",
            SecurityStamp = Guid.NewGuid().ToString(),
            ModifiedBy = 1,
            CreatedBy = 1,
        };
        var password = new PasswordHasher<ApplicationUser>();
        var hashed = password.HashPassword(admin,"password");
        admin.PasswordHash = hashed;
        builder.Entity<ApplicationUser>().HasData(
            admin
        );
    }
    
    private static ApplicationUser Admin => new()
    {
        Id = 1,
        UserName = "Admin",
        Email = "Admin@test.ru",
        LockoutEnabled = false,
        PhoneNumber = "996111222333",
        CreatedAt = DateTime.Now.SetKindUtc(),
        ModifiedAt = DateTime.Now.SetKindUtc(),
        FirstName = "Админ",
        LastName = "Админов",
        NormalizedUserName = "ADMIN",
        NormalizedEmail = "ADMIN@TEST.RU",
        SecurityStamp = Guid.NewGuid().ToString(),
        ModifiedBy = 1,
        CreatedBy = 1,
    };
}
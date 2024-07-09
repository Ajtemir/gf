using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void AvatarSeed(this ModelBuilder builder)
    {
        builder.Entity<Avatar>().HasData(
            FirstAvatar
        );
    }

    private static Avatar FirstAvatar => new()
    {
        Id = 1,
        Image = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Avatars", "1.jpg")),
        ImageName = "1.jpg",
    };
}
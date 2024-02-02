using Application.Common.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void RewardSeed(this ModelBuilder builder)
    {
        builder.Entity<Reward>().HasData(
            EneDanky
        );
    }

    private static Reward EneDanky => new Reward
    {
        Id = 1,
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = 1,
        NameKg = "Эне данкы кг",
        NameRu = "Эне данкы ру",
        ModifiedBy = 1,
        ModifiedAt = DateTime.Now.SetKindUtc(),
        ImageName = "ene.jpeg",
        Image = new byte[] { },
    };
}
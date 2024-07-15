using Application.Common.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void RewardSeed(this ModelBuilder builder)
    {
        builder.Entity<Reward>().HasData(
            EneDanky,
            GreatBuilder,
            DiplomaGramota,
            OrdenDank
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
        Image = new byte[] { 1 },
    };

    private static Reward GreatBuilder => new Reward
    {
        Id = 2,
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = 1,
        NameKg = "Заслуженный строитель Кыргызской Республики",
        NameRu = "Заслуженный строитель Кыргызской Республики",
        ModifiedBy = 1,
        ModifiedAt = DateTime.Now.SetKindUtc(),
        ImageName = "ene.jpeg",
        Image = new byte[] { 1 },
    };

    private static Reward DiplomaGramota => new Reward
    {
        Id = 3,
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = 1,
        NameKg = "Почетная грамота Кыргызской Республики",
        NameRu = "Почетная грамота Кыргызской Республики",
        ModifiedBy = 1,
        ModifiedAt = DateTime.Now.SetKindUtc(),
        ImageName = "ene.jpeg",
        Image = new byte[] { 1 },
    };

    private static Reward OrdenDank => new Reward
    {
        Id = 4,
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = 1,
        NameKg = "орден «Данк»",
        NameRu = "орден «Данк»",
        ModifiedBy = 1,
        ModifiedAt = DateTime.Now.SetKindUtc(),
        ImageName = "ene.jpeg",
        Image = new byte[] { 1 },
    };
}
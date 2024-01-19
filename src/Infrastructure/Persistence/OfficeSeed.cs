using Application.Common.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void OfficeSeed(this ModelBuilder builder)
    {
        builder.Entity<Office>().HasData(
            new Office
            {
                Id = 51617,
                NameRu = "Секретариат по государственным наградам",
                NameKg = "Мамлекеттик сыйлыктардын секретариаты",
                CreatedAt = DateTime.Now.SetKindUtc(),
                CreatedBy = 1,
                ModifiedAt = DateTime.Now.SetKindUtc(),
                ModifiedBy = 1,
            }
        );
    }
}
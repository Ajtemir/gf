using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void RewardDocumentTypesSeed(this ModelBuilder builder)
    {
        builder.Entity<RewardDocumentType>().HasData(
            new RewardDocumentType
            {
                RewardId = EneDanky.Id,
                DocumentTypeId = PassportDocumentType.Id,
            },
            new RewardDocumentType
            {
                RewardId = EneDanky.Id,
                DocumentTypeId = Nesudimost.Id,
            }
        );
    }
}
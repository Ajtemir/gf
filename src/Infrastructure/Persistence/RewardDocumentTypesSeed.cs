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
                RewardId = 1,
                DocumentTypeId = PassportDocumentType.Id,
            }
        );
    }
}
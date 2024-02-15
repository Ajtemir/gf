using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void DocumentSeed(this ModelBuilder builder)
    {
        builder.Entity<Document>().HasData(
            FirstRequiredDocument,
            FirstOptionalDocument
        );
    }

    private static Document FirstRequiredDocument => new Document
    {
        Id = 1,
        DocumentTypeId = PassportDocumentType.Id,
        RewardApplicationId = FirstRewardApplication.Id,
    };
    
    private static Document FirstOptionalDocument => new Document
    {
        Id = 2,
        DocumentTypeId = Nesudimost.Id,
        RewardApplicationId = FirstRewardApplication.Id,
    };
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void ChildDocumentTypeSeed(this ModelBuilder builder)
    {
        builder.Entity<ChildDocumentType>().HasData(
            new ChildDocumentType
            {
                DocumentTypeId = 3,
            },
            new ChildDocumentType
            {
                DocumentTypeId = 4,
            }
        );
    }
}
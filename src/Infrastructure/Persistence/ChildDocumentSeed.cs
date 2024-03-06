using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void ChildDocumentSeed(this ModelBuilder builder)
    {
        builder.Entity<ChildDocument>().HasData(
            new ChildDocument
            {
                ChildId = AytemirUlanbekUulu.Id,
                DocumentId = FirstOptionalChildDocument.Id,
            },
            new ChildDocument
            {
                ChildId = AytemirUlanbekUulu.Id,
                DocumentId = FirstRequiredChildDocument.Id,
            }
        );
    }
}
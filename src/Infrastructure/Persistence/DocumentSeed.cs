using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void DocumentSeed(this ModelBuilder builder)
    {
        builder.Entity<Document>().HasData(
            FirstRequiredDocument,
            FirstOptionalDocument,
            FirstOptionalChildDocument,
            FirstRequiredChildDocument
        );
    }

    private static Document FirstRequiredDocument => new Document
    {
        Id = 1,
        DocumentTypeId = PassportDocumentType.Id,
    };
    
    private static Document FirstOptionalDocument => new Document
    {
        Id = 2,
        DocumentTypeId = Nesudimost.Id,
    }; 
    private static Document FirstOptionalChildDocument => new()
    {
        Id = 3,
        DocumentTypeId = Attestat.Id,
    };
    private static Document FirstRequiredChildDocument => new()
    {
        Id = 4,
        DocumentTypeId = Svidetelstvo.Id,
    };
}
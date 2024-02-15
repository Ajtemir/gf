using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void DocumentTypeSeed(this ModelBuilder builder)
    {
        builder.Entity<DocumentType>().HasData(
            PassportDocumentType,
            Nesudimost
        );
    }

    private static DocumentType PassportDocumentType => new DocumentType
    {
        Id = 1,
        Required = true,
        NameKg = "Паспорт KG",
        NameRu = "Паспорт RU",
    };

    private static DocumentType Nesudimost => new DocumentType
    {
        Id = 2, Required = false, NameKg = "Справка о несудимости", NameRu = "Справка о несудимости билдирме",
    };
}
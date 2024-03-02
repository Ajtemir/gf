using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void DocumentTypeSeed(this ModelBuilder builder)
    {
        builder.Entity<DocumentType>().HasData(
            PassportDocumentType,
            Nesudimost,
            Svidetelstvo,
            Attestat
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
    private static DocumentType Svidetelstvo => new DocumentType
    {
        Id = 3, Required = true, NameKg = "Cвиделтельство о рождении", NameRu = "Куболук туулгандыгы тууралуу",
    };
    private static DocumentType Attestat => new DocumentType
    {
        Id = 4, Required = false, NameKg = "Школьный аттестат", NameRu = "Мектеп аттестаты",
    };
}
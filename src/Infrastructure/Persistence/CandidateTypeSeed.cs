using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void CandidateTypeSeed(this ModelBuilder builder)
    {
        builder.Entity<CandidateType>().HasData(
            new CandidateType
            {
                NameKg = "Эне",
                NameEn = "Mother",
                NameRu = "Мать",
            },
            new CandidateType
            {
                NameKg = "Жараан",
                NameEn = "Citizen",
                NameRu = "гражданин",
            },
            new CandidateType
            {
                NameKg = "Чет өлкөлүк жараан",
                NameEn = "Foreigner",
                NameRu = "Иностранец",
            },
            new CandidateType
            {
                NameKg = "Коом",
                NameEn = "Entity",
                NameRu = "Организация",
            }
        );
    }
}
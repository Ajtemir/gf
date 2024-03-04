using Domain.Entities;
using Domain.Enums;
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
                NameEn = CandidateTypes.Mother,
                NameRu = "Мать",
            },
            new CandidateType
            {
                NameKg = "Жараан",
                NameEn = CandidateTypes.Citizen,
                NameRu = "гражданин",
            },
            new CandidateType
            {
                NameKg = "Чет өлкөлүк жараан",
                NameEn = CandidateTypes.Foreigner,
                NameRu = "Иностранец",
            },
            new CandidateType
            {
                NameKg = "Коом",
                NameEn = CandidateTypes.Entity,
                NameRu = "Организация",
            },
            new CandidateType
            {
                NameKg = "Бала",
                NameEn = CandidateTypes.Child,
                NameRu = "Ребенок",
            }
        );
    }
}
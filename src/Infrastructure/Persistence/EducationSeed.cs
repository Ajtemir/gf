using Domain.Dictionary;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void EducationSeed(this ModelBuilder builder)
    {
        builder.Entity<Education>().HasData(
            Beginner,
            Middle,
            Main,
            Specific,
            NotFinishedHigh,
            High
        );
    }
    public static Education Beginner => new Education { Id = 1, NameRu = "Начальное и ниже", NameKg = "Начальное и ниже" };
    public static Education Middle => new Education { Id = 2, NameRu = "Неполное среднее", NameKg = "Неполное среднее" };
    public static Education Main => new Education { Id = 3, NameRu = "Среднее общее", NameKg = "Среднее общее" };
    public static Education Specific => new Education { Id = 4, NameRu = "Среднее специальное", NameKg = "Среднее специальное" };
    public static Education NotFinishedHigh => new Education { Id = 5, NameRu = "Незаконченное высшее", NameKg = "Незаконченное высшее" };
    public static Education High => new Education { Id = 6, NameRu = "Высшее", NameKg = "Высшее" };
}
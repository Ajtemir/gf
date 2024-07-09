using Domain.Dictionary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Dictionary;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.Property(e => e.NameRu).IsRequired().HasMaxLength(256);
        builder.Property(e => e.NameKg).IsRequired().HasMaxLength(256);

        // builder.HasData(
        //     new Education { Id = 1, NameRu = "Начальное и ниже", NameKg = "Начальное и ниже" },
        //     new Education { Id = 2, NameRu = "Неполное среднее", NameKg = "Неполное среднее" },
        //     new Education { Id = 3, NameRu = "Среднее общее", NameKg = "Среднее общее" },
        //     new Education { Id = 4, NameRu = "Среднее специальное", NameKg = "Среднее специальное" },
        //     new Education { Id = 5, NameRu = "Незаконченное высшее", NameKg = "Незаконченное высшее" },
        //     new Education { Id = 6, NameRu = "Высшее", NameKg = "Высшее" }
        //     );
    }
}
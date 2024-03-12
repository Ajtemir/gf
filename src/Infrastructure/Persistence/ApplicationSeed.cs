using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void ApplicationSeed(this ModelBuilder builder)
    {
        builder.Entity<Domain.Entities.Application>().HasData(
            KaldarbekovaBermetApplication
        );
    }

    private static Domain.Entities.Application KaldarbekovaBermetApplication => new()
    {
        SpecialAchievements = "SpecialAchievements",
        Id = 1,
        CreatedBy = Admin.Id,
        CandidateId = KaldarbekovaBermet.Id,
        RewardId = EneDanky.Id,
        ModifiedBy = Admin.Id,
    };
}
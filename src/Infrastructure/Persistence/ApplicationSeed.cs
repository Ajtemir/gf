using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void ApplicationSeed(this ModelBuilder builder)
    {
        builder.Entity<RewardApplication>().HasData(
            FirstRewardApplication
        );
    }

    private static RewardApplication FirstRewardApplication => new RewardApplication
    {
        SpecialAchievements = "SpecialAchievements",
        Id = 1,
        CreatedBy = Admin.Id,
        CandidateId = FirstMother.Id,
        RewardId = EneDanky.Id,
        ModifiedBy = Admin.Id,
    };
}
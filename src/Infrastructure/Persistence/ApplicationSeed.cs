using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void ApplicationSeed(this ModelBuilder builder)
    {
        builder.Entity<RewardApplication>().HasData(
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 1,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            },
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 2,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            },
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 3,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            },
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 4,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            },
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 5,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            },
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 6,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            },
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 7,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            },
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 8,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            },
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 9,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            },
            new RewardApplication
            {
                SpecialAchievements = "SpecialAchievements",
                Id = 10,
                CreatedBy = Admin.Id,
                CandidateId = KaldarbekovaBermet.Id,
                RewardId = EneDanky.Id,
                ModifiedBy = Admin.Id,
            }
        );
    }

    private static RewardApplication FirstRewardApplication => new RewardApplication
    {
        SpecialAchievements = "SpecialAchievements",
        Id = 1,
        CreatedBy = Admin.Id,
        CandidateId = KaldarbekovaBermet.Id,
        RewardId = EneDanky.Id,
        ModifiedBy = Admin.Id,
    };
}
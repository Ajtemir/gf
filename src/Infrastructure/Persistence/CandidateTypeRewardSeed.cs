using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class Seed
{
    private static void CandidateTypeRewardSeed(this ModelBuilder builder)
    {
        builder.Entity<CandidateTypeReward>().HasData(
            new CandidateTypeReward
            {
                RewardId = 1,
                CandidateTypeId = CandidateTypes.Citizen,
            }
        );
    }
}
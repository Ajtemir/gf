using Application.Common.Extensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void StatusSeed(this ModelBuilder builder)
    {
        builder.Entity<RewardApplicationStatus>().HasData(
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 1,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 1,
                UserId = 1,
            },
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 2,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 2,
                UserId = 1,
            },
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 3,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 3,
                UserId = 1,
            },
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 4,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 4,
                UserId = 1,
            },
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 5,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 5,
                UserId = 1,
            },
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 6,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 6,
                UserId = 1,
            },
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 7,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 7,
                UserId = 1,
            },
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 8,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 8,
                UserId = 1,
            },
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 9,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 9,
                UserId = 1,
            },
            new RewardApplicationStatus
            {   
                Status = RewardApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 10,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                RewardApplicationId = 10,
                UserId = 1,
            }
        );
    }
}
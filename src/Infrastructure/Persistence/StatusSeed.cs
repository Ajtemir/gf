using Application.Common.Extensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void StatusSeed(this ModelBuilder builder)
    {
        builder.Entity<ApplicationStatus>().HasData(
            new ApplicationStatus
            {   
                Status = ApplicationStatusType.Saved,
                OfficeId = Secretariat.Id,
                Id = 1,
                ChangeDate = DateTime.Today.SetKindUtc(),
                PreviousStatusId = null,
                ApplicationId = 1,
                UserId = 1,
            }
        );
    }
}
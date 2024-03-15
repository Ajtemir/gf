using Application.Common.Extensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class Seed
{
    private static void CandidateSeed(this ModelBuilder builder)
    {
        builder.Entity<Mother>().HasData(
            KaldarbekovaBermet
        );
    }

    private static Mother KaldarbekovaBermet => new()
    {
        Id = 1,
        Image = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Avatars", "1.jpg")),
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = Admin.Id,
        PersonId = KaldarbekovaBermetMember.Id,
        ModifiedBy = Admin.Id,
        ApplicationId = KaldarbekovaBermetApplication.Id,
    };
    
    
}
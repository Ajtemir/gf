using Application.Common.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class Seed
{
    private static void CandidateSeed(this ModelBuilder builder)
    {
        builder.Entity<Mother>().HasData(
            KaldarbekovaBermetCandidate
        );

        builder.Entity<Citizen>().HasData(
            AytemirUlanbekUuluCandidate
        );

        builder.Entity<Foreigner>().HasData(
            MemhetKenanCandidate
        );

        builder.Entity<Entity>().HasData(
            InfocomEntityCandidate
        );
    }

    private static Mother KaldarbekovaBermetCandidate => new()
    {
        Id = 1,
        Image = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Avatars", "1.jpg")),
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = Admin.Id,
        PersonId = KaldarbekovaBermetMember.Id,
        ModifiedBy = Admin.Id,
    };

    private static Citizen AytemirUlanbekUuluCandidate => new()
    {
        Id = 2,
        Image = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Avatars", "1.jpg")),
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = Admin.Id,
        PersonId = AytemirUlanbekUuluMember.Id,
        ModifiedBy = Admin.Id,
        EducationId = High.Id, // высшее
    };
    
    private static Foreigner MemhetKenanCandidate => new()
    {
        Id = 3,
        Image = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot",
            "Avatars",
            "1.jpg")),
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = Admin.Id,
        PersonId = MehmetKenanDonmezMember.Id,
        ModifiedBy = Admin.Id,
        CitizenshipId = 166, // Турция
    };

    private static Entity InfocomEntityCandidate => new()
    {
        Id = 4,
        Image = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Avatars", "1.jpg")),
        CreatedAt = DateTime.Now.SetKindUtc(),
        CreatedBy = Admin.Id,
        EntityId = InfocomEntityMember.Id,
        ModifiedBy = Admin.Id,
        NameRu = "Infocom",
        NameKg = "Infocom",
    };

}
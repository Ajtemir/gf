using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void ApplicationDocumentSeed(this ModelBuilder builder)
    {
        builder.Entity<ApplicationDocument>().HasData(
            new ApplicationDocument
            {
                ApplicationId = KaldarbekovaBermetApplication.Id,
                DocumentId = FirstOptionalChildDocument.Id,
            },
            new ApplicationDocument
            {
                ApplicationId = KaldarbekovaBermetApplication.Id,
                DocumentId = FirstRequiredChildDocument.Id,
            }
        );
    }
}
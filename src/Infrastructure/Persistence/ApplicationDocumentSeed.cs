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
                ApplicationId = 1,
                DocumentId = 1,
            },
            new ApplicationDocument
            {
                ApplicationId = 1,
                DocumentId = 2,
            }
        );
    }
}
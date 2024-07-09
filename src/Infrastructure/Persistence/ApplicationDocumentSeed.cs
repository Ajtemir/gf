using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void CandidateDocumentSeed(this ModelBuilder builder)
    {
        builder.Entity<CandidateDocument>().HasData(
            new CandidateDocument
            {
                CandidateId = KaldarbekovaBermetCandidate.Id,
                DocumentId = FirstOptionalChildDocument.Id,
            },
            new CandidateDocument
            {
                CandidateId = KaldarbekovaBermetCandidate.Id,
                DocumentId = FirstRequiredChildDocument.Id,
            }
        );
    }
}
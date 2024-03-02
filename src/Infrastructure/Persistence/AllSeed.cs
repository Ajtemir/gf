using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    public static void AllSeed(this ModelBuilder builder)
    {
        builder.RoleSeed();
        builder.UserSeed();
        builder.OfficeSeed();
        builder.UserRoleSeed();
        builder.UserOfficeSeed();
        builder.CandidateTypeSeed();
        builder.DocumentTypeSeed();
        builder.ChildDocumentTypeSeed();
        builder.RewardSeed();
        builder.CandidateTypeRewardSeed();
        builder.RewardDocumentTypesSeed();
        builder.MemberSeed();
        builder.CandidateSeed();
        builder.ApplicationSeed();
        builder.DocumentSeed();
        builder.ChildDocumentSeed();
        builder.ApplicationDocumentSeed();
        builder.StatusSeed();
    }
}
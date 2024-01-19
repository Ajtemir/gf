using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    public static void AllSeed(this ModelBuilder builder)
    {
        builder.RoleSeed();
        builder.UserSeed();
        builder.OfficeSeed();
        // builder.UserRoleSeed();
        builder.CandidateTypeSeed();
    }
}
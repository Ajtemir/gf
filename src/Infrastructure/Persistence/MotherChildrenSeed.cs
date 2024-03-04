using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void MotherChildrenSeed(this ModelBuilder builder)
    {
        builder.Entity<MotherChild>().HasData(
            new MotherChild
            {
                MotherId = KaldarbekovaBermet.Id,
                ChildId = AytemirUlanbekUulu.Id
            }
        );
    }
}
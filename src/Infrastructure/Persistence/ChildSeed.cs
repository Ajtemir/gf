using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class Seed
{
    private static void ChildSeed(this ModelBuilder builder)
    {
        builder.Entity<Child>().HasData(
            AytemirUlanbekUulu
        );
    }
}
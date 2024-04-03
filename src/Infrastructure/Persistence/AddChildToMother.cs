using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class DbContextExtensions
{
    public static async Task AddChildToMotherIfNotExistsAsync(this ApplicationDbContext context, Child child, int motherId)
    {
        var motherChild =
            await context.MotherChildren.FirstOrDefaultAsync(x =>
                x.MotherId == motherId && x.ChildId == child.Id);
        if (motherChild == null)
        {
            await context.MotherChildren.AddAsync(new MotherChild
            {
                MotherId = motherId, ChildId = child.Id
            });
            await context.SaveChangesAsync();
        }
    }
}
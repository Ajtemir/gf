using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static partial class DbContextExtensions
{
    public static async Task SaveChildWithDocumentsAsync(this ApplicationDbContext context, Child child)
    {
        await context.Children.AddAsync(child);
        await context.SaveChangesAsync();
        var documentTypes = await context.ChildDocumentTypes.ToListAsync();
        var addedDocuments = documentTypes.Select(x => new Document { DocumentTypeId = x.DocumentTypeId }).ToList();
        await context.Documents.AddRangeAsync(addedDocuments);
        await context.SaveChangesAsync();
        await context.ChildDocuments.AddRangeAsync(addedDocuments.Select(x => new ChildDocument
        {
            DocumentId = x.Id, ChildId = child.Id
        }));
        await context.SaveChangesAsync();
    }
}
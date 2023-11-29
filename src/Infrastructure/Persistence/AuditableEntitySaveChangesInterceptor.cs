using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeUtcService _dateTimeUtcService;

    public AuditableEntitySaveChangesInterceptor(
        ICurrentUserService currentUserService,
        IDateTimeUtcService dateTimeUtcService)
    {
        _currentUserService = currentUserService;
        _dateTimeUtcService = dateTimeUtcService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (_currentUserService.UserId == 0)
            {
                throw new ForbiddenAccessException();
            }

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUserService.UserId;
                entry.Entity.CreatedAt = _dateTimeUtcService.Now;
            }

            if (entry.State is EntityState.Added or EntityState.Modified ||
                entry.HasChangedOwnedEntities())
            {
                entry.Entity.ModifiedBy = _currentUserService.UserId;
                entry.Entity.ModifiedAt = _dateTimeUtcService.Now;
            }
        }

        var softDeletedEntities = context.ChangeTracker
            .Entries()
            .Where(x => x.Entity is ISoftDeleteEntity && x.State == EntityState.Deleted);
        foreach (var entry in softDeletedEntities)
        {
            var entity = (ISoftDeleteEntity)entry.Entity;
            entity.IsDeleted = true;
            entry.State = EntityState.Modified;
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}
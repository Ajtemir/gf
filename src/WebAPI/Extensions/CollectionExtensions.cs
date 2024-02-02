using System.Linq.Expressions;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions;

public static class CollectionExtensions
{
    public static T FirstOrError<T>(this IQueryable<T> source, Expression<Func<T, bool>>? predicate = null, string? errorMessage = null) where T: class
    {
        var elem = predicate == null ? source.FirstOrDefault() : source.FirstOrDefault(predicate);
        if(elem == null) throw new NotFoundException(errorMessage ?? $"Not found entity {nameof(T)}");
        return elem;
    }
    
    public static async Task<T> FirstOrErrorAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>>? predicate = null, string? errorMessage = null) where T: class
    {
        var elem = predicate == null ? await source.FirstOrDefaultAsync() : await source.FirstOrDefaultAsync(predicate);
        if(elem == null) throw new NotFoundException(errorMessage ?? $"Not found entity {nameof(T)}");
        return elem;
    }

    public static bool NotContains<T>(this ICollection<T> source, T searched) => !source.Contains(searched);
    public static bool IsEmpty<T>(this ICollection<T> source) => !source.Any();
    public static bool IsNullOrEmpty<T>(this ICollection<T>? source) => source is null || !source.Any();

}
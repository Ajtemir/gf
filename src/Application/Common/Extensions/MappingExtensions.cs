using Application.Common.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions;

public static class MappingExtensions
{
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
        this IQueryable queryable,
        IConfigurationProvider configuration,
        CancellationToken cancellationToken = default)
        where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync(cancellationToken);

    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize, int? count = null,
        CancellationToken cancellationToken = default)
        where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize, count,
            cancellationToken);
}
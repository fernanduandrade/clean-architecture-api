using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bot.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Bot.Application.Common.Mapping;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        int pageNumber,
        int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        IConfigurationProvider configuraiton) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuraiton).AsNoTracking().ToListAsync();
}

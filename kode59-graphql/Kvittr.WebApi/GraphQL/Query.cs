using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Data.Projections.Expressions;
using HotChocolate.Resolvers;
using HotChocolate.Types.Pagination.Extensions;
using Kvittr.Model.Models;
using Kvittr.WebApi.ViewModels;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Kvittr.Model.GraphQL;

public class Query
{
    [GraphQLDescription("Get all Kvitts, use minWorms to filter for likes")]
    [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 5), UseProjection, UseFiltering, UseSorting]
    public async Task<List<KvittDto>> GetKvitts(
        int? minWorms,
        IResolverContext resolverContext,
        KvittrDbContext kvittrDbContext,
        CancellationToken ct)
    {
        var query = kvittrDbContext.Kvitts.AsQueryable();

        if (minWorms is not null)
        {
            query = query.Where(x => x.Worms >= minWorms);
        }

        var data = await query
            .ProjectToType<KvittDto>()
            .Filter(resolverContext)
            .Project(resolverContext)
            .ApplyOffsetPaginationAsync(resolverContext, cancellationToken: ct)
            ;

        foreach (var item in data.Items)
        {
            item.IsTrending = item.Worms > 10000;
        }

        return new List<KvittDto>(data.Items);
    }
    
    [UseProjection, UseFiltering, UseSorting]
    public IQueryable<AuthorDto> GetAuthors(KvittrDbContext kvittrDbContext,
        CancellationToken ct)
    {
        return kvittrDbContext.Authors
            .ProjectToType<AuthorDto>();
    }
}
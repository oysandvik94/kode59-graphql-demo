using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Data.Sorting.Expressions;
using HotChocolate.Resolvers;
using HotChocolate.Types.Pagination.Extensions;
using Kvittr.Model;
using Kvittr.Model.Models;
using Kvittr.WebApi.ViewModels;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Kvittr.WebApi;

public class Query
{
    [GraphQLDescription("Return all kvitts")]
    [UseOffsetPaging(DefaultPageSize = 5, MaxPageSize = 10, IncludeTotalCount = true), UseProjection, UseFiltering(), UseSorting()]
    public IQueryable<KvittDto> GetKvitts(int? minWorms,  KvittrDbContext kvittrDbContext)
    {
        var query = kvittrDbContext.Kvitts.AsQueryable();

        if (minWorms is not null)
        {
            query = query.Where(x => x.Worms > minWorms);
        }

        return query.ProjectToType<KvittDto>();
    }
    
    
    [GraphQLDescription("Return all authors")]
    public IQueryable<AuthorDto> GetAuthors( KvittrDbContext kvittrDbContext)
    {
        return kvittrDbContext.Authors.ProjectToType<AuthorDto>();
    }
}
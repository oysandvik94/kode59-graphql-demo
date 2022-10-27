using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Kvittr.Model;
using Microsoft.EntityFrameworkCore;

namespace Kvittr.WebApi;

public class RequestInterceptor : DefaultHttpRequestInterceptor
{
    private readonly KvittrDbContext _dbContext;

    public RequestInterceptor(IDbContextFactory<KvittrDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public override ValueTask OnCreateAsync(HttpContext context,
        IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        var username = context.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
        
        var author = _dbContext.Authors
            .FirstOrDefault(x => x.UserName == username);

        requestBuilder.TryAddProperty(AuthorAttribute.AuthorAttributeKey, author);
        
        return base.OnCreateAsync(context, requestExecutor, requestBuilder,
            cancellationToken);
    }
}
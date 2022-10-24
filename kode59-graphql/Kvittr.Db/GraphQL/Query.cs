using Kvittr.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kvittr.Model.GraphQL;

public class Query
{
    public async Task<List<Kvitt>> GetKvitts(KvittrDbContext kvittrDbContext, CancellationToken ct)
    {
        return await kvittrDbContext.Kvitts.ToListAsync(ct);
    }
}
using System.Security.Claims;
using Kvittr.Model;
using Kvittr.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kvittr.WebApi.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class KvittMutation
{
    public async Task<int> PostKvitt(string body, KvittrDbContext kvittrDbContext, [Author] Author author, CancellationToken ct)
    {
      var kvitt = new Kvitt(body, author.Id);

      throw new NotFoundException("Whoops");
      await kvittrDbContext.AddAsync(kvitt, ct);
      await kvittrDbContext.SaveChangesAsync(ct);

      return kvitt.Id;
    }
}
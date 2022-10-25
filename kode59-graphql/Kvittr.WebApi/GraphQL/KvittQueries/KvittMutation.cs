using System.Security.Claims;
using HotChocolate.AspNetCore.Authorization;
using Kvittr.Model.Models;
using Kvittr.WebApi;
using Kvittr.WebApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Kvittr.Model.GraphQL.KvittQueries;

[ExtendObjectType(typeof(Mutation))]
public class KvittMutation
{
   [Authorize]
   [Error(typeof(NotFoundException))]
   public async Task<int> PostKvitt(string body, KvittrDbContext kvittrDbContext, [Author] Author author, CancellationToken ct)
   {
      var kvitt = new Kvitt(body, author.Id);

      await kvittrDbContext.AddAsync(kvitt, ct);
      await kvittrDbContext.SaveChangesAsync(ct);

      return kvitt.Id;
   }
}
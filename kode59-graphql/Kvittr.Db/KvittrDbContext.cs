using System.Data.Common;
using Kvittr.Model.Configurations;
using Kvittr.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kvittr.Model;

public class KvittrDbContext : DbContext
{
    public KvittrDbContext(DbContextOptions<KvittrDbContext> options)
    : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<Kvitt> Kvitts { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new AuthorConfiguration().Configure(modelBuilder.Entity<Author>());
        new KvittConfiguration().Configure(modelBuilder.Entity<Kvitt>());
    }
}
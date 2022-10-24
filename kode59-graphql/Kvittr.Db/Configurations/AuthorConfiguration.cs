using Kvittr.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kvittr.Model.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(new Author("musketeer", "Elon", "Musk", new DateTime(1981, 06, 28))  {Id = 1});
        builder.HasData(new Author("trumpinator", "Donald", "Trump", new DateTime(1946, 06, 14)) { Id = 2});
        builder.HasData(new Author("bezoswisser", "Jeff", "Bezos", new DateTime(1964, 12, 12)){ Id = 3});
    }
}
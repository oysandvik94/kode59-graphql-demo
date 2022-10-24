using Kvittr.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kvittr.Model.Configurations;

public class KvittConfiguration : IEntityTypeConfiguration<Kvitt>
{
    public void Configure(EntityTypeBuilder<Kvitt> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Kvitts);

        builder.HasData(new Kvitt("Nuke Mars!", 1)
        {
            Id = 1,
            Worms = 12323,
        });
        
        builder.HasData(new Kvitt("Also, I'm buying Manchested United ur welcome", 1)
        {
            Id = 2,
            Worms = 6436456,
        });
        
        builder.HasData(new Kvitt("the color organge is named after the fruit", 1)
        {
            Id = 3,
            Worms = 23942934,
        });
        
        builder.HasData(new Kvitt("Sorry losers and haters, but my I.Q. is one of the highest -and you all know it! Please don’t feel so stupid or insecure,it’s not your fault", 2)
        {
            Id = 4,
            Worms = 4575675,
        });
        
        builder.HasData(new Kvitt("Windmills are the greatest threat in the US to both bald and golden eagles. Media claims fictional ‘global warming’ is worse.", 2)
        {
            Id = 5,
            Worms = 95769579,
        });
        
        builder.HasData(new Kvitt("AWS > Azure", 3)
        {
            Id = 6,
            Worms = 846846,
        });
        
        builder.HasData(new Kvitt("My dream is to be shot up in a rocket", 3)
        {
            Id = 7,
            Worms = 1,
        });
        
        builder.HasData(new Kvitt("I got shot up in a rocket", 3)
        {
            Id = 8,
            Worms = 89769769,
        });
    }
}
namespace Kvittr.WebApi.ViewModels;

[GraphQLDescription("Representation of a kvitt")]
public record KvittDto
{
    public DateTime Created { get; set; } = default!;
    public string Body { get; set; } = default!;
    public int Worms { get; set; }
    [GraphQLDescription("Kvitt is trending if it has over 10 000 worms")]
    public bool IsTrending { get; set; }
    public int AuthorId { get; set; }

    public AuthorDto Author { get; set; }
}
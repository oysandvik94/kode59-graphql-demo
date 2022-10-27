using Kvittr.WebApi.ViewModels;

namespace Kvittr.WebApi;

[GraphQLDescription("The query root of this schema")]
public class KvittDto
{
    public DateTime Created { get; set; } = default!;
    public string Body { get; set; } = default!;
    [GraphQLDescription("Amount of worms")]
    public int Worms { get; set; }
    public int AuthorId { get; set; }
    
    public AuthorDto Author { get; set; }
}
using HotChocolate.AspNetCore.Authorization;

namespace Kvittr.WebApi.ViewModels;


public record AuthorDto
{
    public string UserName { get; set; } = default!;
    [Authorize]
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public List<KvittDto> Kvitts { get; set; }
}
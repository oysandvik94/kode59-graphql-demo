namespace Kvittr.WebApi;

public class AuthorAttribute : GlobalStateAttribute
{
    public const string AuthorAttributeKey = "Author";
    
    public AuthorAttribute() : base(AuthorAttributeKey)
    {
    }
}
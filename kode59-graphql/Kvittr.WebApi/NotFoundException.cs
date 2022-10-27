namespace Kvittr.WebApi;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }
}
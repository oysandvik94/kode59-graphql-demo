namespace Kvittr.Model.Models;

public class Author
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; } = default!;

    private readonly List<Kvitt> _kvitts = new();
    public IReadOnlyCollection<Kvitt> Kvitts => _kvitts;

    public Author(string userName, string firstName, string lastName, DateTime birthDate)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }
    
    protected Author() {}
}
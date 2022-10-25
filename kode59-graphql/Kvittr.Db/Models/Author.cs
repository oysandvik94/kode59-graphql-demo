namespace Kvittr.Model.Models;

public class Author
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; } = default!;

    public virtual ICollection<Kvitt> Kvitts  { get; set; }

    public Author(string userName, string firstName, string lastName, DateTime birthDate)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }
    
    protected Author() {}
}
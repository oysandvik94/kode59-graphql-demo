namespace Kvittr.Model.Models;

public class Kvitt
{
    public int Id { get; set; }
    public DateTime Created { get; set; } = default!;
    public string Body { get; set; } = default!;
    public int Worms { get; set; }
    public int AuthorId { get; set; }
    public virtual Author Author { get; set; } = default!; 

    public Kvitt(string body, int authorId)
    {
        Created = DateTime.Now;
        Body = body;
        AuthorId = authorId;
    }

    protected Kvitt() { }

    public int GiveWork()
    {
        return ++Worms;
    }
}
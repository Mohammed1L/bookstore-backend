namespace BookStoreBackend.Models;

public class Category : BaseClass
{
    public long Id { get; set; }    
    public string Name { get; set; }

    // Navigation Property
    public ICollection<Book> Books { get; set; }
}
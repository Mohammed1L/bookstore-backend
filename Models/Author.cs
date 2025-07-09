
using System.ComponentModel.DataAnnotations;

namespace BookStoreBackend.Models;
public class Author : BaseClass
{
    
    [Key]
    public long Id
    {
        get; set;
    }
    public string Name { get; set; }
    public string Bio { get; set; }

    // Navigation Property
    public ICollection<Book> Books { get; set; }
}
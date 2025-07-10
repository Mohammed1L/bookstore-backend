
using System.ComponentModel.DataAnnotations;

namespace BookStoreBackend.Models;

public class Book : BaseClass
{
    [Key]
    public long Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    

    // Foreign Keys
    public long AuthorId { get; set; }
    public long CategoryId { get; set; }

    // Navigation Properties
    public Author Author { get; set; }
    public Category Category { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;
    

    // Foreign Keys
    public long AuthorId { get; set; }
    public long CategoryId { get; set; }

    // Navigation Properties
    public Author Author { get; set; }
    public Category Category { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
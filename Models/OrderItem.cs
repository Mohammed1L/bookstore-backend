using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace BookStoreBackend.Models;

public class OrderItem : BaseClass
{
    public long Id { get; set; }  
    public long BookId { get; set; }
    public long OrderId { get; set; }
    [Required]
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Navigation Properties
    [JsonIgnore]
    [ValidateNever]    
    public Book Book { get; set; }
    [ValidateNever]    
    [JsonIgnore]
    public Order Order { get; set; }
}
using System.Text.Json.Serialization;

namespace BookStoreBackend.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
public class Order : BaseClass
{
    public long Id { get; set; }  
    [Required]
    public decimal PriceAdded { get; set; }
    public DateTime Date { get; set; }
    [Required]
    public string OrderStatus { get; set; }

    // Foreign Key
    
    public long UserId { get; set; }

    // Navigation Properties
    [JsonIgnore] // ignore it for now 
    public User User { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
}
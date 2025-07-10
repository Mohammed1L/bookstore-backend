
using System.ComponentModel.DataAnnotations;

namespace BookStoreBackend.Models;

public class User : BaseClass
{
    public long Id { get; set; }  
    [Required]
    public string Email { get; set; }
    [Required]
    public string HashedPassword { get; set; }
    public string Role { get; set; }

    // Navigation Properties
    public ICollection<Role> Roles { get; set; }
    public ICollection<Order> Orders { get; set; }
}

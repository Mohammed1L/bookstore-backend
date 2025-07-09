
namespace BookStoreBackend.Models;

public class User : BaseClass
{
    public long Id { get; set; }  
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string Role { get; set; }

    // Navigation Properties
    public ICollection<Role> Roles { get; set; }
    public ICollection<Order> Orders { get; set; }
}

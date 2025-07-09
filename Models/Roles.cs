
namespace BookStoreBackend.Models;

public class Role : BaseClass
{
    public long Id { get; set; }  
    public string RoleName { get; set; }

    // Navigation Property
    public long UserId { get; set; }
    public User User { get; set; }
}
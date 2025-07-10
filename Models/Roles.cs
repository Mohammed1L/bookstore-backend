
using System.ComponentModel.DataAnnotations;

namespace BookStoreBackend.Models;

public class Role : BaseClass
{
    public long Id { get; set; }  
    [Required]
    public string RoleName { get; set; }

    // Navigation Property
    public long UserId { get; set; }
    public User User { get; set; }
}
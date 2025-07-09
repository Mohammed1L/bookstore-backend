
namespace BookStoreBackend.Models;

public abstract class BaseClass
{
   
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
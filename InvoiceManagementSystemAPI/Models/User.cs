using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models;

public class User
{
    public int UserId { get; set; }
    [Required]
    [StringLength(50)]
    public string UserName { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    [StringLength(20)]
    public string Password { get; set; }
    [Required]
    [StringLength(20)]
    public string Role { get; set; }
    
}
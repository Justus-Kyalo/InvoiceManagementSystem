using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models;

public class Customer
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }
    [Required]
    public  int AccountNumber { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public  bool Taxable { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set; }
    [Required]
    public ICollection<CustomerItemPrice> CustomerItemPrices { get; set; }
}
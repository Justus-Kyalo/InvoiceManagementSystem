using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models;

public class Customer
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }
    [Required]
    public  string AccountNumber { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public  bool Taxable { get; set; }
    [Required]
    public  decimal PricePerItem { get; set; }
}
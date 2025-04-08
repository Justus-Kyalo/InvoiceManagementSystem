using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerUpdateDto
{
    [Required]
    public  string AccountNumber { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public  bool Taxable { get; set; }
    [Required]
    public  decimal PricePerItem { get; set; }
}